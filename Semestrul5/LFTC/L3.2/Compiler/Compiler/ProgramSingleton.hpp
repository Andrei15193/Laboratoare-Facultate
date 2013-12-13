#ifndef PROGRAMSINGLETON_HPP
#define PROGRAMSINGLETON_HPP
#include <vector>
#include "Statement.hpp"
namespace Compiler
{
	class ProgramSingleton
	{
	public:
		void AddVariable(std::string variableName)
		{
			_variables.push_back(variableName);
		}
		const std::vector<std::string>& GetVariableNames() const
		{
			return _variables;
		}
		void Clear()
		{
			_variables.clear();
			for (auto statement = _statements.begin(); statement != _statements.end(); statement++)
				delete *statement;
			_statements.clear();
		}
		void AddStatement(Statement* statement)
		{
			_statements.push_back(statement);
		}
		const std::vector<Statement*>& GetStatements() const
		{
			return _statements;
		}
		static ProgramSingleton& GetInstance()
		{
			return _instance;
		}
	private:
		ProgramSingleton()
		{
			_variables = std::vector<std::string>();
			_statements = std::vector<Statement*>();
		}
		~ProgramSingleton()
		{
			_variables.clear();
			for (auto statement = _statements.begin(); statement != _statements.end(); statement++)
				delete *statement;
			_statements.clear();
		}
		std::vector<std::string> _variables;
		std::vector<Statement*> _statements;
		static ProgramSingleton _instance;
	};
}
#endif