#ifndef READEXPRESSIONELEMENT_HPP
#define READEXPRESSIONELEMENT_HPP
#include "ExpressionElement.hpp"
namespace Compiler
{
	class ReadExpressionElement
		: public ExpressionElement
	{
	public:
		virtual Type GetExpressionType() const
		{
			return ExpressionElement::Read;
		}
	};
}
#endif