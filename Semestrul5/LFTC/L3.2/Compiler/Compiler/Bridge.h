#ifndef BRIDGE_H
#define BRIDGE_H
void Parse(char* fileName);
void AddVariable(char* variableName);
void ExpressionOperator(char* symbol);
void ExpressionConstant(int value);
void ExpressionIdentifier(char* name);
void ExpressionReadInt();
void VariableSet(char* name);
void OutputValue();
#endif