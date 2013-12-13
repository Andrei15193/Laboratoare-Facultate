#ifndef OperatorExpressionElement_HPP
#define OperatorExpressionElement_HPP
#include "ExpressionElement.hpp"
namespace Compiler
{
	class OperatorExpressionElement
		: public ExpressionElement
	{
	public:
		enum OperationName
		{
			Addition,
			Subtraction,
			Multiplication,
			Division,
			Modulo
		};
		OperatorExpressionElement(OperationName operationName)
		{
			_operationName = operationName;
		}
		OperationName GetOperationName()
		{
			return _operationName;
		}
		virtual ExpressionElement::Type GetExpressionType() const
		{
			return ExpressionElement::Operator;
		}
	private:
		OperationName _operationName;
	};
}
#endif