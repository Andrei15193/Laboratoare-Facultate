#ifndef OUTPUTVALUESTATEMENT_HPP
#define OUTPUTVALUESTATEMENT_HPP
#include <vector>
#include "ExpressionElement.hpp"
#include "Statement.hpp"
namespace Compiler
{
	class OutputValueStatement
		: public Statement
	{
	public:
		OutputValueStatement(std::vector<ExpressionElement*> expression)
		{
			_expression = expression;
		}
		~OutputValueStatement()
		{
			for (auto expressionElement = _expression.begin(); expressionElement != _expression.end(); expressionElement++)
				delete *expressionElement;
		}
		const std::vector<ExpressionElement*>& GetOutputExpression() const
		{
			return _expression;
		}
		virtual Statement::Type GetStatementType() const
		{
			return Statement::OutputValue;
		}
	private:
		std::vector<ExpressionElement*> _expression;
	};
}
#endif