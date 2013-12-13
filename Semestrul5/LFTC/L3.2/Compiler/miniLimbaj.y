%{
	#include <stdio.h>
	#include <stdlib.h>
	#include "Bridge.h"
	extern FILE* yyin;
	extern char* yytext;
	extern int yylineno;
%}
%union {
	int number;
	char* text;
}
%token Var
%token ReadInt
%token WriteInt
%token OpenningParenthesis
%token ClosingParenthesis
%token Dot
%token Comma
%token Equal
%token <text> Plus
%token <text> Minus
%token <text> Star
%token <text> Percentage
%token <text> Slash
%token <text> Identifier
%token <number> IntegerNumericConstant

%type <text> operator
%%
program : Var identifiers Dot statements
		| statements
		;
identifiers : Identifier														{ AddVariable($1); }
			| Identifier Comma identifiers										{ AddVariable($1); }
			;
statements : statement Dot
		   | statement Dot statements
		   ;
statement : Identifier Equal expression											{ VariableSet($1); }
		  | WriteInt OpenningParenthesis expression ClosingParenthesis			{ OutputValue(); }
		  ;
expression : Identifier															{ ExpressionIdentifier($1); }
		   | IntegerNumericConstant												{ ExpressionConstant($1); }
		   | ReadInt OpenningParenthesis ClosingParenthesis						{ ExpressionReadInt(); }
		   | expression operator Identifier										{ ExpressionOperator($2); ExpressionIdentifier($3); }
		   | expression operator IntegerNumericConstant							{ ExpressionOperator($2); ExpressionConstant($3); }
		   | expression operator ReadInt OpenningParenthesis ClosingParenthesis { ExpressionOperator($2); ExpressionReadInt(); }
		   ;
operator : Plus 																{ $$ = "+"; }
		 | Minus																{ $$ = "-"; }
		 | Star																	{ $$ = "*"; }
		 | Slash																{ $$ = "/"; }
		 | Percentage															{ $$ = "%"; }
		 ;
%%
int yyerror()
{
	printf("Syntax error: %s, line: %i\r\n", yytext, yylineno);
	exit(3);
}
void Parse(char* fileName)
{
	fopen_s(&yyin, fileName, "r");
	yyparse();
	fclose(yyin);
}