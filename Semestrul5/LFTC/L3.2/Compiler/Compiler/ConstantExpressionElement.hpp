#ifndef CONSTANTEXPRESSIONELEMENT_HPP
#define CONSTANTEXPRESSIONELEMENT_HPP
#include "ExpressionElement.hpp"
namespace Compiler
{
	class ConstantExpressionElement
		: public ExpressionElement
	{
	public:
		ConstantExpressionElement(int value)
		{
			_value = value;
		}
		int GetValue()
		{
			return _value;
		}
		virtual ExpressionElement::Type GetExpressionType() const
		{
			return ExpressionElement::Type::Constant;
		}
	private:
		int _value;
	};
}
#endif