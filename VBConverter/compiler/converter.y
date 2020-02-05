%{
#include <stdio.h>
#include <string.h>
#include <stdlib.h> 
#include "linkedlist.h"
#include "strings.h"
extern int yylex();
int yyerror(const char *);
int counter=0;
char *temp;
int lineNb=1,i; 
char* inhType; 
char* value;
int flag,N=0,n; 
char* str;
char *text;
char *newText,*buff;
node_t * symbolTable = NULL;
void verifyDoesntExist(char* id){
    if(contains(symbolTable,id) > 0 )
        yyerror(concat(id," already defined\n"));
    else
        push(&symbolTable,id);       
}
void verifyExists(char* id){
    if(contains(symbolTable,id) <=0 )
        yyerror(concat(id," is not defined\n"));      
}
char* indent(char* buff){
    char* res="";
    for(i=0;i<strlen(buff);i++)
        if(buff[i]=='\n')
            res=concat(res,"\n\t");
            else
            res=append(res,buff[i]);
    return res;
}
%}
%name parse
%union {
  char *strVal;
  struct {
  	char *varWithValues;
	char *varWithoutValues;
  } variables;
  struct {
  	char *expr;
	char *index;
  } forStruct;
}
%token <strVal> OPENSTRING INNERSTRING CLOSESTRING PURESTRING ID 
%token PRINT SCAN 
%token <strVal> MAIN VOID INTEGER CHAR SINGLE DOUBLE 
%token <strVal> INTEGERVALUE CHARVALUE SINGLEVALUE DOUBLEVALUE
%token IF ELSE EQ LE GE AND OR ADD SUB MUL DIV INC DEC
%token WHILE FOR
%type <strVal> line code output input declaration type
%type <strVal> stmt simpleStmt ifStmt
%type <strVal> openStmt openIfStmt whileLoop openWhileLoop
%type <strVal> forLoop openForLoop 
%type <strVal> printfExpression stringAndArguments innerString stringAndAddresses
%type <strVal> arithmaticExpression factor term value numberValue
%type <strVal> assignment assignementOperation 
%type <strVal> logicExpression logicTerm logicFactor notFactor comparator comparisonOperator 
%type <variables> varList 
%type <forStruct> forExpr initExpr stepExpr boundExpr 
%%
start: VOID MAIN '(' ')' '{' code '}' {printf("Module VBModule\n\tSub Main()\n");
              printf("\t\t%s\n\tEnd Sub\nEnd Module\n",indent(indent($6)));return(0);}
;
code : line code  { $$=concat(concat($1,"\n"),$2)};
|  {$$="";  }
;

line : stmt {$$=strdup($1);}
| openStmt {$$=strdup($1);}
;

stmt : simpleStmt
| whileLoop
| forLoop
| ifStmt 
| '{' code '}'  { $$ = $2 }
;

openStmt:openIfStmt
| openWhileLoop 
| openForLoop 
;

simpleStmt : declaration ';'
| output ';'
| input ';'
| arithmaticExpression  ';' 
| assignment  ';' 
;

declaration : type varList  { if(strcmp($2.varWithoutValues,"")==0)
									str=strdup($2.varWithValues);
								else if(strcmp($2.varWithValues,"")==0)
										str=strdup($2.varWithoutValues);
									else str=concat(concat($2.varWithoutValues,concat(" As ",$1)),$2.varWithValues);
								$$=concat("Dim ",str);
									
                            }
;

varList : varList ',' ID { verifyDoesntExist($3);
						  $$.varWithoutValues = concat($1.varWithoutValues,concat(" , ",$3));
						  $$.varWithValues=$1.varWithValues;
						 }
| varList ',' ID '=' arithmaticExpression  { verifyDoesntExist($3);
							    temp = concat(concat($3," As "),concat(inhType," = " ));
							    $$.varWithValues = concat(concat($1.varWithValues," , "),concat(temp,$5));          
							  $$.varWithoutValues=$1.varWithoutValues;
										   }
| ID {verifyDoesntExist($1);
	  $$.varWithoutValues = strdup($1);
	  $$.varWithValues="";
	 }
| ID '=' arithmaticExpression { verifyDoesntExist($1);
							    temp = concat(concat($1," As "),concat(inhType," = " ));
							    $$.varWithValues = concat(temp,$3); 
							   $$.varWithoutValues="";
							   
							  }
;

type : INTEGER {inhType=strdup($1);}
| CHAR  {inhType=strdup($1);}
| SINGLE  {inhType=strdup($1);}
| DOUBLE  {inhType=strdup($1);}
;

output: PRINT '(' printfExpression  ')'   {$$=concat(concat("Console.Write(",$3),")");}
;

printfExpression : PURESTRING 
| OPENSTRING stringAndArguments  {$$=concat($1,$2);}
;

stringAndArguments : innerString stringAndArguments ',' arithmaticExpression {
	$$=concat(concat(concat(concat("{",stringValue(--counter)),"}"),$1),concat($2,concat(",",$4)));
	
}
|CLOSESTRING ',' arithmaticExpression {
	$$=concat(concat(concat(concat("{",stringValue(counter)),"}"),$1),concat(",",$3));}
;

innerString: INNERSTRING {counter++;}
;

input: SCAN '(' OPENSTRING stringAndAddresses   ')'  {$$=$4;}
;

stringAndAddresses : INNERSTRING stringAndAddresses ',' '&' ID 
{ $$ = concat($2,concat(concat("\n",$5)," = Console.ReadLine()")); }
|CLOSESTRING ',' '&' ID { $$=concat($4," = Console.ReadLine()");}
;


ifStmt :IF '(' logicExpression ')' stmt ELSE stmt {
                                 str = concat(concat("If ",$3)," Then\n\t");
                                 str = concat(str,concat(indent($5),"\nElse\n\t"));
                                 $$ = concat(str,concat(indent($7),"\nEnd If"));
                                }
;

openIfStmt :IF '(' logicExpression ')' stmt ELSE openIfStmt   {                 
                                str = concat(concat("If ",$3)," Then\n\t");
                                str = concat(str,concat(indent($5),"\nElse\n\t"));
                                 $$ = concat(str,concat(indent($7),"\nEnd If"));
                                }
| IF '(' logicExpression ')'  line { 
                               str = concat(concat("If ",$3)," Then\n\t");
                               $$ = concat(str,concat(indent($5),"\nEnd If"));
                                }
;

arithmaticExpression: arithmaticExpression '+' term  { $$=concat($1,concat(strdup(" + "),$3)); }
|arithmaticExpression '-' term   { $$=concat(concat($1," - "),$3); }
|term {$$=strdup($1);}
;

term: term '*' factor      { $$=concat(concat($1," * "),$3);}
|term '/' factor           { $$=concat(concat($1," / "),$3);}
|factor {$$=strdup($1);}
;

factor: '(' arithmaticExpression ')'  { $$=concat(concat("( ",$2)," )");}
| '-' factor {$$=concat("-",$2);}
|ID {verifyExists($1); $$=strdup($1); }
|value {$$=strdup($1); }
;

logicExpression: logicExpression OR logicTerm  {$$=concat(concat($1," Or "),$3); }
|logicTerm {$$=strdup($1);}
;

logicTerm: logicTerm AND logicFactor   { $$=concat(concat($1," And "),$3);}
|logicFactor {$$=strdup($1);}
;

logicFactor :'!' logicFactor   { $$=concat("Not ",$2); }
|notFactor  {$$=strdup($1);}
;

notFactor: '(' logicExpression ')'  {  $$=concat(concat("( ",$2)," )");}
| comparator  {$$=strdup($1);}
;

comparator: arithmaticExpression comparisonOperator arithmaticExpression { $$=concat(concat($1,$2),$3);}
;

comparisonOperator: EQ {$$= " = " ;}
| '>'  {$$= " > " ;}
| '<'  {$$= " < " ;}
| LE   {$$= " <= " ;}
| GE   {$$= " >= " ;} 
; 

assignment: ID assignementOperation arithmaticExpression  { verifyExists($1);$$=concat(concat($1,$2),$3);}  
| ID INC  { verifyExists($1);$$=concat($1," += 1 ");} 
| ID DEC  { verifyExists($1);$$=concat($1," -= 1 ");} 
;

assignementOperation: '=' {$$= " = " ;}
| ADD  {$$= " += " ;}
| SUB  {$$= " -= " ;}
| MUL  {$$= " *= " ;}
| DIV  {$$= " /= " ;} 
;


whileLoop: WHILE '(' logicExpression ')' stmt {   str=concat("While ",$3);
                                                  str=concat(str,"\n\t");
                                                  str=concat(str,indent($5));
                                                  $$=concat(str,"\nEnd While");}
;

forLoop: forExpr stmt  { str=concat($1.expr,indent($2));
                         $$=concat(concat(str,"\nNext "),$1.index);}                                                           
;

openForLoop: forExpr openStmt  { str=concat($1.expr,indent($2));
                                 $$=concat(concat(str,"\nNext "),$1.index);} 
;

forExpr : FOR initExpr boundExpr stepExpr {if(strcmp($2.index,$3.index)!=0
                                       || strcmp($3.index,$4.index)!=0 )
                                       yyerror(" Index of for loop must be the same ");
                                       str=concat("For ",$2.index);
                                       str=concat(str," = ");
                                       str=concat(str,$2.expr);
                                       str=concat(str," To ");
                                       str=concat(str,$3.expr);
                                       str=concat(str," Step ");
                                       str=concat(str,$4.expr);
                                       $$.expr=concat(str,"\n\t");
                                       $$.index=$2.index;
                                             }
;

initExpr : '(' ID '=' arithmaticExpression ';' { verifyExists($2);
                                                $$.expr=$4;
                                                $$.index=$2;}

boundExpr : ID LE arithmaticExpression ';' {$$.expr=$3;$$.index=$1;}
| ID GE arithmaticExpression ';'  {$$.expr=$3;$$.index=$1;}
| ID '<' arithmaticExpression ';' {if(isNumeric($3,&n)!=-1)
                                            $$.expr=stringValue(n-1);
                                        else
                                            $$.expr=concat($3," - 1");
                                  $$.index=$1;}
| ID '>' arithmaticExpression ';' {if(isNumeric($3,&n)!=-1)
                                            $$.expr=stringValue(n-1);
                                        else
                                            $$.expr=concat($3," - 1");
                                  $$.index=$1;}

;
stepExpr : ID ADD arithmaticExpression ')' {$$.expr=strdup($3);$$.index=$1;}
|ID SUB  arithmaticExpression ')' {if(isNumeric($3,&n))
                                            $$.expr=stringValue(-n);
                                        else
                                            $$.expr=concat(concat("- ( ",$3)," )");
                                  $$.index=strdup($1);}
|ID INC  ')' {  $$.expr=strdup("1");$$.index=strdup($1);} 
|ID DEC  ')' {  $$.expr=strdup("-1");$$.index=strdup($1);}
;


value: CHARVALUE {$$=strdup($1);}
| numberValue {$$=strdup($1);}
;

numberValue: INTEGERVALUE {$$=strdup($1);}
| SINGLEVALUE {$$=strdup($1);}
| DOUBLEVALUE {$$=strdup($1);}
;

openWhileLoop: WHILE '(' logicExpression ')' openStmt {   str=concat("While ",$3);
                                                       str=concat(str,"\n\t");
                                                       buff=$5;
                                                       for(i=0;i<strlen(buff);i++)
                                                          if(buff[i]=='\n')
                                                              str=concat(str,"\n\t");
                                                           else
                                                               str=append(str,buff[i]);
                                                        $$=concat(str,"\nEnd While");}
;



%%

int main(){yyparse();return -1;}
int yyerror(const char*s){fprintf(stderr,"%s\n",s);return -1;}






