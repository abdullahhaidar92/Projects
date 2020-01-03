%{
#include <stdio.h>
#include "linkedlist.h"
#include <string.h>
int lineNb=1,i,counter;
int yyerror();
int yylex();
node_t * variablesStack = NULL;      
node_t * variablesWithValuesStack = NULL;  
node_t * variablesValuesStack = NULL;  
char* variable; 
char* value;
int flag; 
char *text;
char *newText,*buff;
%}
%name parse
%union
{
    int intValue;
    float floatValue;
   char* stringValue;
}
%token Integer Char Single Double Id NewLine 
%token IntegerValue CharacterValue SingleValue DoubleValue
%token Print Scan Text Address
%token If Else
%token While For
%%
start: Code  {printf("valid\n");return(0);}
;

Code: Lines  {printf("%s",$<stringValue>1);}
|'{'  Lines '}'  {printf("%s",$<stringValue>2);}
;

Lines : Blank Statement  Lines  {$<stringValue>$=strdup($<stringValue>2);
                                  strcat($<stringValue>$,strdup("\n"));
                                 strcat($<stringValue>$,strdup($<stringValue>3))};
|WhileLoop  {$<stringValue>$=strdup($<stringValue>1); }
|ForLoop  {$<stringValue>$=strdup($<stringValue>1);  }
|Blank  {$<stringValue>$=strdup(" ");  }
;
Statement:Statement1
;
Statement1 : MatchedStatement 
|UnMatchedStatement
;

MatchedStatement : 	If '(' LogicExpression ')' Blank MatchedStatement Blank Else Blank MatchedStatement {
                                $<stringValue>$=strdup("If ");
                                strcat($<stringValue>$,strdup($<stringValue>3));
                                strcat($<stringValue>$,strdup(" Then\n    "));
                                strcat($<stringValue>$,strdup($<stringValue>6));
                                strcat($<stringValue>$,strdup(" \nElse\n    "));
                                strcat($<stringValue>$,strdup($<stringValue>10));
                                strcat($<stringValue>$,strdup("\nEnd If"));
                                }
|NonAlternativeStatement
;

UnMatchedStatement 	: If '(' LogicExpression ')' Blank Statement1 { 
                               $<stringValue>$=strdup("If ");
                                strcat($<stringValue>$,strdup($<stringValue>3));
                                strcat($<stringValue>$,strdup(" Then\n    "));
                                strcat($<stringValue>$,strdup($<stringValue>6));
                                strcat($<stringValue>$,strdup("\nEnd If"));
                                }
|If '(' LogicExpression ')' Blank MatchedStatement Blank Else Blank UnMatchedStatement   {
                                $<stringValue>$=strdup("If ");
                                strcat($<stringValue>$,strdup($<stringValue>3));
                                strcat($<stringValue>$,strdup(" Then\n    "));
                                strcat($<stringValue>$,strdup($<stringValue>6));
                                strcat($<stringValue>$,strdup("\nElse    "));
                                strcat($<stringValue>$,strdup($<stringValue>10));
                                strcat($<stringValue>$,strdup("\nEnd If"));
                                }
;

NonAlternativeStatement :DataType Variables ';' { 
                                              $<stringValue>$=strdup("Dim "); 
                                              flag=0;
                                              variable=pop(&variablesStack);
                                              if(variable){
                                                flag=1;
                                                strcat($<stringValue>$,strdup(variable));
                                                variable=pop(&variablesStack);
                                                while(variable){ 
                                                sprintf($<stringValue>$,"%s , %s",strdup($<stringValue>$),strdup(variable));
                                                variable=pop(&variablesStack);  
                                                }
                                               strcat($<stringValue>$," As ");
                                               strcat($<stringValue>$,strdup($<stringValue>1));
                                             }
                                             variable=pop(&variablesWithValuesStack);
                                             value=pop(&variablesValuesStack);
                                             while(variable){
                                                if(flag)
                                                     strcat($<stringValue>$,", ");
                                                sprintf($<stringValue>$,"%s%s As %s = %s ",strdup($<stringValue>$),
                                                strdup(variable),strdup($<stringValue>1),strdup(value));
                                                variable=pop(&variablesWithValuesStack);
                                                value=pop(&variablesValuesStack);
                                                flag=1;
                                             }
                                        }
|Print '(' Text Arguments ')' ';' { 
                                    $<stringValue>$=strdup("Console.Write(");
                                    text=$<stringValue>3;
                                    counter=0;
                                    for(i=0;i<strlen(text);i++)
                                        if(text[i]=='\\' && text[i+1]=='n'){
                                            strcat($<stringValue>$,strdup("\"& vbcrlf &\""));
                                            i++;
                                        }
                                            
                                        else if(text[i]=='%' && (text[i+1]=='d' || text[i+1]=='c' || text[i+1]=='f' 
                                                                 || (text[i+1]=='l' && text[i+2]=='f' && (i=i+1) )))
                                        {  
                                           sprintf($<stringValue>$, "%s{%d}",strdup($<stringValue>$),counter);
                                           counter++;
                                           i+=1;
                                        }
                                        else
                                            strncat($<stringValue>$,strdup(text+i), 1);
                                   
                                    while(counter > 0){
                                       variable=pop(&variablesStack);
                                       if(variable)
                                           sprintf($<stringValue>$, "%s,%s",strdup($<stringValue>$),strdup(variable));
                                       else
                                           yyerror("Not enough arguments");
                                       counter--;
                                   }
                                   if(pop(&variablesStack))
                                       yyerror("Too many arguments");
                                   
                                     strcat($<stringValue>$,strdup(") "));
                                     strcat($<stringValue>$,strdup(" "));
                                     
                                  }
|Scan  '(' Text Addresses ')' ';' {  $<stringValue>$=strdup("");
                                    text=$<stringValue>3;
                                    for(i=1;i<strlen(text)-1;i++)
                                        if(text[i]==' ')
                                            continue;
                                        else if(text[i]=='%' && (text[i+1]=='d' || text[i+1]=='c' || text[i+1]=='f' 
                                                                 || (text[i+1]=='l' && text[i+2]=='f' && (i=i+1) )))
                                        { 
                                          variable=pop(&variablesStack);
                                          if(variable){
                                          strcat($<stringValue>$,strdup(variable));
                                             strcat($<stringValue>$,strdup(" = Console.ReadLine()\n"));
                                          }
                                          else
                                           yyerror("Not enough arguments");
                                           i+=1;
                                        }
                                       else{
                                          yyerror("Invalid string in scanf");
                                           break;
                                       }
                                   if(pop(&variablesStack))
                                       yyerror("Too many arguments");
                                  }
|ArithmaticExpression ';'     
|Assignment ';'
;

ArithmaticExpression: ArithmaticExpression '+' Term  { $<stringValue>$=$<stringValue>1;
                                   strcat($<stringValue>$," + ");
                                   strcat($<stringValue>$,$<stringValue>3);
                                  }
|ArithmaticExpression '-' Term             { $<stringValue>$=$<stringValue>1;
                                   strcat($<stringValue>$," - ");
                                   strcat($<stringValue>$,$<stringValue>3);
                                  }
|Term 
;

Term: Term '*' Factor      { $<stringValue>$=$<stringValue>1;
                             strcat($<stringValue>$," * ");
                             strcat($<stringValue>$,$<stringValue>3);
                            }
|Term '/' Factor           { $<stringValue>$=$<stringValue>1;
                             strcat($<stringValue>$," / ");
                             strcat($<stringValue>$,$<stringValue>3);
                            }
|Term '%' Factor           { $<stringValue>$=$<stringValue>1;
                             strcat($<stringValue>$," Mod ");
                             strcat($<stringValue>$,$<stringValue>3);
                            }
|Factor 
;

Factor: '(' ArithmaticExpression ')'  { $<stringValue>$=strdup("( ");
                              strcat($<stringValue>$,$<stringValue>2);
                              strcat($<stringValue>$," )");
                             }
|Id
|'-' Id   { $<stringValue>$=strdup("-");
            strcat($<stringValue>$,$<stringValue>2);
           }
|Value
;

LogicExpression: LogicExpression '|' '|' LogicTerm  { $<stringValue>$=$<stringValue>1;
                                                   strcat($<stringValue>$," Or ");
                                                   strcat($<stringValue>$,$<stringValue>4);
                                                  }
|LogicTerm 
;

LogicTerm: LogicTerm '&' '&' LogicFactor   { $<stringValue>$=$<stringValue>1;
                                          strcat($<stringValue>$," And ");
                                          strcat($<stringValue>$,$<stringValue>4);
                                         }
|LogicFactor 
;

LogicFactor :'!' LogicFactor   { $<stringValue>$=strdup("Not ");
                               strcat($<stringValue>$,$<stringValue>2);
                             }
|NotFactor
;

NotFactor: '(' LogicExpression ')'  { $<stringValue>$=strdup("( ");
                                        strcat($<stringValue>$,$<stringValue>2);
                                        strcat($<stringValue>$," )");
                                       }
|Comparator
;

Comparator: ArithmaticExpression Operation ArithmaticExpression { $<stringValue>$=$<stringValue>1;
                                                                    strcat($<stringValue>$,$<stringValue>2);
                                                                    strcat($<stringValue>$,$<stringValue>3);
                                                                   }
;

Operation: '=' '=' {$<stringValue>$= " = " ;}
| '>'  {$<stringValue>$= " > " ;}
| '<'  {$<stringValue>$= " < " ;}
| '<' '=' {$<stringValue>$= " <= " ;}
| '>' '=' {$<stringValue>$= " >= " ;} 
; 

Assignment: Id '=' ArithmaticExpression  { $<stringValue>$=strdup($<stringValue>1);
                                              strcat($<stringValue>$,strdup(" = "));
                                              strcat($<stringValue>$,strdup($<stringValue>3));
                                  }
;

WhileLoop: While '(' LogicExpression ')' WhileStatement {  $<stringValue>$=strdup("while ");
                                                           strcat($<stringValue>$,strdup($<stringValue>3));
                                                           strcat($<stringValue>$,strdup("\n\t"));
                                                           strcat($<stringValue>$,strdup($<stringValue>5));
                                                           strcat($<stringValue>$,strdup("\nEnd While"));}
;
WhileStatement: WhileLoop
|NonAlternativeStatement
|'{'  Lines '}'  {$<stringValue>$=strdup($<stringValue>2);}
;

ForLoop: For '(' Id '=' Value ';' Id '<' '=' Value ';' Id '+' '=' Value ')' ForStatement 
                                                        {  if(strcmp($<stringValue>3,$<stringValue>7)!=0
                                                            || strcmp($<stringValue>7,$<stringValue>12)!=0 )
                                                            yyerror(" Index of for loop must be the same ");
                                                           $<stringValue>$=strdup("For  ");
                                                           strcat($<stringValue>$,strdup($<stringValue>3));
                                                           strcat($<stringValue>$,strdup(" = "));
                                                           strcat($<stringValue>$,strdup($<stringValue>5));
                                                           strcat($<stringValue>$,strdup(" To "));
                                                           strcat($<stringValue>$,strdup($<stringValue>10));
                                                           strcat($<stringValue>$,strdup(" step "));
                                                           strcat($<stringValue>$,strdup($<stringValue>15));
                                                           strcat($<stringValue>$,strdup("\n\t"));
                                                           strcat($<stringValue>$,strdup($<stringValue>17));
                                                           strcat($<stringValue>$,strdup("\nNext "));
                                                           strcat($<stringValue>$,strdup($<stringValue>3));}                                                           
;
ForStatement: ForLoop
|NonAlternativeStatement
|'{'  Lines '}'  {$<stringValue>$=strdup($<stringValue>2);}
;



Variables : Id List { push(&variablesStack,$<stringValue>1); }
| Id '=' Value List { push(&variablesWithValuesStack,$<stringValue>1);
                      push(&variablesValuesStack,$<stringValue>3); }

List: ',' Variables
|
;

DataType:Integer
|Char
|Single
|Double
;

Value: IntegerValue 
| CharacterValue 
| SingleValue 
| DoubleValue ;

Arguments : ',' Id Arguments  { push(&variablesStack,$<stringValue>2); }
|                     
;

Addresses : ',' '&' Id Addresses  { push(&variablesStack,$<stringValue>3); }
|                     
;
Blank: NewLine Blank {lineNb++;}
|
;
%%
int yyerror(char* s){fprintf(stderr,"line %d :%s\n",lineNb,s);}
int main(void){ yyparse();}
