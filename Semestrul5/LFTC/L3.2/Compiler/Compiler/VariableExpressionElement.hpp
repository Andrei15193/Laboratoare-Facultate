#ifndef VARIABLEEXPRESSIONELEMENT_HPP
#define VARIABLEEXPRESSIONELEMENT_HPP
#include <string>
#include "ExpressionElement.hpp"
namespace Compiler
{
	class VariableExpressionElement
		: public ExpressionElement
	{
	public:
		VariableExpressionElement(const std::string& variableName)
		{
			_variableName = variableName;
		}
		const std::string& GetVariableName() const
		{
			return _variableName;
		}
		virtual ExpressionElement::Type GetExpressionType() const
		{
			return ExpressionElement::Type::Variable;
		}
	private:
		std::string _variableName;
	};
}
#endif