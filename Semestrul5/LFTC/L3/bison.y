%{
    #include <stdio.h>
    #include <stdlib.h>

    extern char* yytext;
%}

%token Assert
%token Begin
%token Do
%token Else
%token End
%token Fact
%token False
%token New
%token Predicate
%token Skip
%token Stop
%token Then
%token True
%token When
%token While
%token Throw
%token Try
%token Catch
%token Finally
%token Scope
%token Star
%token Percentage
%token Slash
%token Backslash
%token Plus
%token Minus
%token LessThan
%token LessThanOrEqualTo
%token Equal
%token GreaterThanOrEqualTo
%token GreaterThan
%token Negation
%token And
%token Or
%token OpeningRoundParenthesis
%token ClosingRoundParenthesis
%token OpeningSquareParenthesis
%token ClosingSquareParenthesis
%token Dot
%token Colon
%token Comma
%token Identifier
%token IntegerNumericConstant
%token FloatNumericConstant
%token StringConstant
%token CharConstant

%%

program : predicateDeclaration
        | program predicateDeclaration
        ;

qualifiedIdentifier : Identifier
                    | qualifiedIdentifier Scope Identifier
                    ;

constant : IntegerNumericConstant
         | FloatNumericConstant
         | StringConstant
         | CharConstant
         | booleanConstant
         ;

booleanConstant : True
                | False
                ;

unaryPrefixedOperator : Plus
                      | Minus
                      | Negation
                      ;

binaryOperator : Plus
               | Minus
               | Star
               | Percentage
               | Slash
               | Backslash
               | And
               | Or
               | LessThan
               | LessThanOrEqualTo
               | Equal
               | GreaterThanOrEqualTo
               | GreaterThan
               ;

predicateDeclaration : Predicate Identifier body
                     | Predicate Identifier OpeningRoundParenthesis formalParameters ClosingRoundParenthesis body
                     | Fact Identifier Dot
                     | Fact Identifier OpeningRoundParenthesis factParameters ClosingRoundParenthesis Dot
                     ;

formalParameters : variableDeclaration
                 | formalParameters Comma variableDeclaration
                 ;

factParameters : variableDeclaration binaryOperator constant
               | factParameters variableDeclaration binaryOperator constant
               ;

body : statement
     | Begin End
     | Begin bodyStatements End
     ;

bodyStatements : statement
               | bodyStatements statement
               ;

type : qualifiedIdentifier
     ;

genericParameters : LessThan genericArguments GreaterThan
                  ;

genericArguments : type
                 | genericArguments Comma type
                 ;

typeInstance : qualifiedIdentifier
             | qualifiedIdentifier genericParameters
             | qualifiedIdentifier boundedArray
             | qualifiedIdentifier genericParameters boundedArray
             ;

boundedArray : OpeningSquareParenthesis boundedArrayDimensions ClosingSquareParenthesis
             | boundedArray OpeningSquareParenthesis boundedArrayDimensions ClosingSquareParenthesis
             ;

boundedArrayDimensions : IntegerNumericConstant
                       | boundedArrayDimensions Comma IntegerNumericConstant
                       ;

variableDeclaration : Identifier Colon type
                    ;

statement : whenStatement
          | whileStatement
          | tryCatchFinallyStatement
          | inlineStatement Dot
          ;

inlineStatement : throwStatement
                | variableDeclarationStatement
                | variableAssignmentStatement
                | functionCall
                | exitStatement
                ;

whenStatement : When expression Then body End
              | When expression Then body Else body End
              ;

whileStatement : While expression Do body
               ;

tryCatchFinallyStatement : Try body finallyStatement
                         | Try body catchStatement finallyStatement
                         ;

catchStatement : Catch body
               | Begin catchExceptionStatement End
               | catchExceptionStatement Catch body
               ;

catchExceptionStatement : Catch variableDeclaration body
                        | catchExceptionStatement Catch variableDeclaration body
                        ;

finallyStatement : Finally body
                 ;

throwStatement : Throw
               | Throw qualifiedIdentifier
               | Throw OpeningRoundParenthesis functionCall ClosingRoundParenthesis
               ;

variableDeclarationStatement : variableDeclaration Equal New typeInstance
                             | variableDeclaration Equal expression
                             ;

variableAssignmentStatement : qualifiedIdentifier Equal New typeInstance
                            | qualifiedIdentifier Equal expression
                            ;

functionCall : qualifiedIdentifier OpeningRoundParenthesis ClosingRoundParenthesis
             | New qualifiedIdentifier OpeningRoundParenthesis ClosingRoundParenthesis
             | qualifiedIdentifier OpeningRoundParenthesis actualParameters ClosingRoundParenthesis
             | New qualifiedIdentifier OpeningRoundParenthesis actualParameters ClosingRoundParenthesis
             | qualifiedIdentifier genericParameters OpeningRoundParenthesis actualParameters ClosingRoundParenthesis
             | New qualifiedIdentifier genericParameters OpeningRoundParenthesis actualParameters ClosingRoundParenthesis
             ;

actualParameters : expression
                 | actualParameters Comma expression
                 ;

exitStatement : Stop
              | Skip
              | Assert expression
              ;

expression : operand
           | unaryPrefixedOperator operand
           | operand binaryOperator expression
           | unaryPrefixedOperator operand binaryOperator expression
           ;

operand : qualifiedIdentifier
        | OpeningRoundParenthesis functionCall ClosingRoundParenthesis
        | IntegerNumericConstant
        | FloatNumericConstant
        | StringConstant
        | CharConstant
        ;

%%

int yyerror()
{
    printf("Syntax error: %s\r\n", yytext);
    exit(3);
}

int main()
{
    return yyparse();
}