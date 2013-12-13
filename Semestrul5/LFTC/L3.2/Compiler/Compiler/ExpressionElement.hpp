#ifndef EXPRESSIONELEMENT_HPP
#define EXPRESSIONELEMENT_HPP
namespace Compiler
{
	class ExpressionElement
	{
	public:
		enum Type
		{
			Variable,
			Constant,
			Read,
			Operator
		};
		virtual Type GetExpressionType() const = 0;
	};
}
#endif