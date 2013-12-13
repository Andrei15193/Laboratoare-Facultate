#ifndef VARIABLEASSIGNMENTSTATEMENT_HPP
#define VARIABLEASSIGNMENTSTATEMENT_HPP
#include <vector>
#include "ExpressionElement.hpp"
#include "Statement.hpp"
namespace Compiler
{
	class VariableAssigmentStatement
		: public Statement
	{
	public:
		VariableAssigmentStatement(const std::string& variableName, std::vector<ExpressionElement*> expression)
		{
			_variableName = variableName;
			_expression = expression;
		}
		~VariableAssigmentStatement()
		{
			for (auto expressionElement = _expression.begin(); expressionElement != _expression.end(); expressionElement++)
				delete *expressionElement;
		}
		const std::string& GetVariableName() const
		{
			return _variableName;
		}
		const std::vector<ExpressionElement*>& GetExpression() const
		{
			return _expression;
		}
		virtual Statement::Type GetStatementType() const
		{
			return Statement::VariableAssign;
		}
	private:
		std::vector<ExpressionElement*> _expression;
		std::string _variableName;
	};
}
#endif