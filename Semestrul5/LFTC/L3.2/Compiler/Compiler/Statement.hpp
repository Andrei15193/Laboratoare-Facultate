#ifndef STATEMENT_HPP
#define STATEMENT_HPP
namespace Compiler
{
	class Statement
	{
	public:
		enum Type
		{
			VariableAssign,
			OutputValue
		};
		virtual Statement::Type GetStatementType() const = 0;
	};
}
#endif