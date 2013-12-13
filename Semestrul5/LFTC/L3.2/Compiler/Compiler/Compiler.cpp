// Compiler.cpp : main project file.
#include "stdafx.h"
#include <iostream>
#include <stack>
#include <queue>
#include "ProgramSingleton.hpp"
#include "ConstantExpressionElement.hpp"
#include "OperatorExpressionElement.hpp"
#include "ReadExpressionElement.hpp"
#include "VariableExpressionElement.hpp"
#include "OutputValueStatement.hpp"
#include "VariableAssignmentStatement.hpp"
#include "Bridge.h"
#include "Compiler.hpp"
using namespace System;
using namespace System::Collections::Generic;
using namespace System::Linq::Expressions;
using namespace System::Text;
using namespace System::Reflection;
using namespace System::Reflection::Emit;
using namespace std;
using namespace Compiler;
void Compile(String^ assemblyFileName);
void GenerateMethod(MethodBuilder^ methodBuilder, Dictionary<String^, ParameterExpression^>^ variables);
int GetPriority(OperatorExpressionElement::OperationName operationName)
{
	switch (operationName)
	{
	case OperatorExpressionElement::Addition:
	case OperatorExpressionElement::Subtraction:
		return 1;
	case OperatorExpressionElement::Multiplication:
	case OperatorExpressionElement::Division:
	case OperatorExpressionElement::Modulo:
		return 2;
	default:
		return 0;
	}
}
int main(array<System::String ^> ^args)
{
	if (args->Length > 0)
	{
		for (int sourceFileIndex = 0; sourceFileIndex < args->Length; sourceFileIndex++)
		{
			ProgramSingleton::GetInstance().Clear();
			char* fileName = new char[args[sourceFileIndex]->Length + 1];
			for (int fileNameCharIndex = 0; fileNameCharIndex <args[ sourceFileIndex]->Length; fileNameCharIndex++)
				fileName[fileNameCharIndex] = char(args[sourceFileIndex][fileNameCharIndex]);
			fileName[args[sourceFileIndex]->Length] = '\0';
			Parse(fileName);
			delete fileName;
			Compile(args[sourceFileIndex]);
		}
		Console::WriteLine(L"Compilation finished!");
	}
	else
		printf("No source file(s) supplied!");
}
void Compile(String^ assemblyFileName)
{
	Dictionary<String^, ParameterExpression^>^ variables = gcnew Dictionary<String^, ParameterExpression^>;
	for(auto variableName = ProgramSingleton::GetInstance().GetVariableNames().begin(); variableName != ProgramSingleton::GetInstance().GetVariableNames().end(); variableName++)
		variables->Add(gcnew String(variableName->c_str()), Expression::Variable(Type::GetType(L"System.Int32", true), gcnew String(variableName->c_str())));
	AssemblyBuilder^ assemblyBuilder = AppDomain::CurrentDomain->DefineDynamicAssembly(gcnew AssemblyName(assemblyFileName), AssemblyBuilderAccess::Save);
	TypeBuilder^ typeBuilder = assemblyBuilder->DefineDynamicModule(assemblyFileName, assemblyFileName + ".exe")->DefineType(L"Main", TypeAttributes::Class);
	MethodBuilder^ methodBuilder = typeBuilder->DefineMethod(L"Main", MethodAttributes::Static);
	GenerateMethod(methodBuilder, variables);
	typeBuilder->CreateType();
	assemblyBuilder->SetEntryPoint(methodBuilder);
	assemblyBuilder->Save(assemblyFileName + ".exe");
}
vector<ExpressionElement*> ToPostFixedForm(const vector<ExpressionElement*>& expression)
{
	vector<ExpressionElement*> postfixedExpressionElements = vector<ExpressionElement*>();
	stack<OperatorExpressionElement*> operators = stack<OperatorExpressionElement*>();
	queue<ExpressionElement*> operands = queue<ExpressionElement*>();

	for (auto expressionElement = expression.begin(); expressionElement != expression.end(); expressionElement++)
		if ((*expressionElement)->GetExpressionType() != ExpressionElement::Operator)
			operands.push(*expressionElement);
		else
		{
			OperatorExpressionElement* operatorElement = dynamic_cast<OperatorExpressionElement*>(*expressionElement);
			if (operators.size() > 0 && GetPriority(operatorElement->GetOperationName()) < GetPriority(operators.top()->GetOperationName()))
			{
				while (operands.size() > 0)
				{
					postfixedExpressionElements.push_back(operands.front());
					operands.pop();
				}
				while (operators.size() > 0)
				{
					postfixedExpressionElements.push_back(operators.top());
					operators.pop();
				}
			}
			operators.push(operatorElement);
		}
		while (operands.size() > 0)
		{
			postfixedExpressionElements.push_back(operands.front());
			operands.pop();
		}
		while (operators.size() > 0)
		{
			postfixedExpressionElements.push_back(operators.top());
			operators.pop();
		}
		return postfixedExpressionElements;
}
Expression^ GetReadLine()
{
	return Expression::Call(Type::GetType(L"System.Int32", true)->GetMethod(L"Parse", gcnew array<Type^>{ Type::GetType(L"System.String", true) }),
		Expression::Call(Type::GetType(L"System.Console", true)->GetMethod(L"ReadLine", Type::EmptyTypes)));
}
Expression^ MakeExpression(const vector<ExpressionElement*>& expression, Dictionary<String^, ParameterExpression^>^ variables)
{
	vector<ExpressionElement*> postfixedForm = ToPostFixedForm(expression);
	Stack<Expression^>^ operands = gcnew Stack<Expression^>;

	for(auto expressionElement = postfixedForm.begin(); expressionElement != postfixedForm.end(); expressionElement++)
		switch ((*expressionElement)->GetExpressionType())
	{
		case ExpressionElement::Constant:
			operands->Push(Expression::Constant(gcnew Int32(dynamic_cast<ConstantExpressionElement*>(*expressionElement)->GetValue())));
			break;
		case ExpressionElement::Read:
			operands->Push(GetReadLine());
			break;
		case ExpressionElement::Variable:
			operands->Push(variables[gcnew String(dynamic_cast<VariableExpressionElement*>(*expressionElement)->GetVariableName().c_str())]);
			break;
		case ExpressionElement::Operator:
			Expression^ operand2 = operands->Pop();
			Expression^ operand1 = operands->Pop();
			switch (dynamic_cast<OperatorExpressionElement*>(*expressionElement)->GetOperationName())
			{
			case OperatorExpressionElement::Addition:
				operands->Push(Expression::Add(operand1, operand2));
				break;
			case OperatorExpressionElement::Subtraction:
				operands->Push(Expression::Subtract(operand1, operand2));
				break;
			case OperatorExpressionElement::Multiplication:
				operands->Push(Expression::Multiply(operand1, operand2));
				break;
			case OperatorExpressionElement::Division:
				operands->Push(Expression::Divide(operand1, operand2));
				break;
			case OperatorExpressionElement::Modulo:
				operands->Push(Expression::Modulo(operand1, operand2));
				break;
			}
			break;
	}
	return operands->Pop();
}
Expression^ GetOutputStatement(OutputValueStatement* outputValueStatement, Dictionary<String^, ParameterExpression^>^ variables)
{
	return Expression::Call(Type::GetType("System.Console", true)->GetMethod("WriteLine", gcnew array<Type^>{ Type::GetType(L"System.Int32", true) }), MakeExpression(outputValueStatement->GetOutputExpression(), variables));
}
Expression^ GetVariableAssignment(VariableAssigmentStatement* variableAssignmentStatement, Dictionary<String^, ParameterExpression^>^ variables)
{
	return Expression::Assign(variables[gcnew String(variableAssignmentStatement->GetVariableName().c_str())],
		MakeExpression(variableAssignmentStatement->GetExpression(), variables));
}
void GenerateMethod(MethodBuilder^ methodBuilder, Dictionary<String^, ParameterExpression^>^ variables)
{
	List<Expression^>^ expressions = gcnew List<Expression^>;
	if (ProgramSingleton::GetInstance().GetStatements().size() == 0)
		expressions->Add(Expression::Empty());
	else
		for (auto statement = ProgramSingleton::GetInstance().GetStatements().begin(); statement !=  ProgramSingleton::GetInstance().GetStatements().end(); statement++)
			switch ((*statement)->GetStatementType())
		{
			case Statement::OutputValue:
				expressions->Add(GetOutputStatement(dynamic_cast<OutputValueStatement*>(*statement), variables));
				break;
			case Statement::VariableAssign:
				expressions->Add(GetVariableAssignment(dynamic_cast<VariableAssigmentStatement*>(*statement), variables));
				break;
		}
		Expression::Lambda(Expression::Block(Type::GetType("System.Void"), variables->Values, expressions))->CompileToMethod(methodBuilder);
}