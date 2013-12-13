#include "stdafx.h"
#include <iostream>
#include <vector>
#include "ProgramSingleton.hpp"
#include "ConstantExpressionElement.hpp"
#include "VariableExpressionElement.hpp"
#include "ReadExpressionElement.hpp"
#include "OperatorExpressionElement.hpp"
#include "OutputValueStatement.hpp"
#include "VariableAssignmentStatement.hpp"

#include "Bridge.h"
using namespace std;
using namespace Compiler;
vector<ExpressionElement*> expressionElements = vector<ExpressionElement*>();
void AddVariable(char* variableName)
{
	ProgramSingleton::GetInstance().AddVariable(string(variableName));
}
void OutputValue()
{
	ProgramSingleton::GetInstance().AddStatement(new OutputValueStatement(expressionElements));
	expressionElements.clear();
}
void VariableSet(char* name)
{
	ProgramSingleton::GetInstance().AddStatement(new VariableAssigmentStatement(string(name), expressionElements));
	expressionElements.clear();
}
void ExpressionOperator(char* symbol)
{
	switch (symbol[0])
	{
	case '+':
		expressionElements.push_back(new OperatorExpressionElement(OperatorExpressionElement::Addition));
		break;
	case '-':
		expressionElements.push_back(new OperatorExpressionElement(OperatorExpressionElement::Subtraction));
		break;
	case '*':
		expressionElements.push_back(new OperatorExpressionElement(OperatorExpressionElement::Multiplication));
		break;
	case '/':
		expressionElements.push_back(new OperatorExpressionElement(OperatorExpressionElement::Division));
		break;
	case '%':
		expressionElements.push_back(new OperatorExpressionElement(OperatorExpressionElement::Modulo));
		break;
	}
}
void ExpressionConstant(int value)
{
	expressionElements.push_back(new ConstantExpressionElement(value));
}
void ExpressionIdentifier(char* name)
{
	expressionElements.push_back(new VariableExpressionElement(string(name)));
}
void ExpressionReadInt()
{
	expressionElements.push_back(new ReadExpressionElement());
}