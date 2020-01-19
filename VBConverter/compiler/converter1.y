%{
#include <stdio.h>
#include <string.h>
#include <stdlib.h> 
#include "linkedlist.h"
#include "strings.h"
int lineNb=1,i,counter;
int yyerror();
int yylex();
int indent=0;
node_t * variablesStack = NULL;      
node_t * variablesWithValuesStack = NULL;  
node_t * variablesValuesStack = NULL;  
char* variable; 
char* value;
int flag,N=0; 
char* str;
char *text;
char *newText,*buff;
%}
%name parse
%union
{
   char* strVal;
}
%token Integer Char Single Double Id  
%token IntegerValue CharacterValue SingleValue DoubleValue
%token Print Scan Text Address
%token If Else EQ LE GE AND OR ADD SUB INC DEC
%token While For
%%
start: Line '\n' {printf("Valid\n");}
;

Line:IfStatement 
|OpenIfStatement 
|WhileLoop2
;

Statement : Id
|WhileLoop1
;

IfStatement :If Integer  IfStatement Else IfStatement
|  Statement
;

OpenIfStatement :If Integer IfStatement Else OpenIfStatement  
| If Integer  Line 
;

WhileLoop1: While Integer IfStatement
WhileLoop2: While Integer OpenIfStatement
%%
int yyerror(char* s){fprintf(stderr,"line %d :%s\n",lineNb,s);}
int main(void){ yyparse();}
