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
%token Integer Char Single Double Id  Space
%token IntegerValue CharacterValue SingleValue DoubleValue
%token Print Scan Text Address
%token If Else
%%
start: line   {printf("valid\n");return(0);}
;

line : Blank  Statement  line  {printf("%s\n",$<stringValue>2);};
|Blank  
;

Statement : MatchedStatement 
|UnMatchedStatement
;

NonAlternativeStatement :DataType Delim Variables ';' { 
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
                                              strcat($<stringValue>$,"\n");
                                        }
|Print '(' Text Arguments ')' ';' { 
                                    $<stringValue>$=strdup("Console.Write(");
                                    text=$<stringValue>3;
                                    counter=0;
                                    for(i=0;i<strlen(text);i++)
                                        if(text[i]=='\\' && text[i+1]=='n'){
                                            strcat($<stringValue>$,"\"& vbcrlf &\"");
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
                                            strncat($<stringValue>$, text+i, 1);
                                   
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
                                   
                                     strcat($<stringValue>$,") ");
                                  }
|Scan '(' Text Addresses ')' ';' {  $<stringValue>$=strdup("");
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
|LogicExpression ';'   
;


MatchedStatement : 	If '(' LogicExpression ')'  MatchedStatement Else MatchedStatement {
                                $<stringValue>$=strdup("If ");
                                strcat($<stringValue>$,strdup($<stringValue>3));
                                strcat($<stringValue>$,strdup(" Then\n"));
                                strcat($<stringValue>$,strdup($<stringValue>5));
                                strcat($<stringValue>$,strdup(" \nElse\n"));
                                strcat($<stringValue>$,strdup($<stringValue>7));
                                strcat($<stringValue>$,strdup("\nEnd If"));
                                }
|NonAlternativeStatement
;

UnMatchedStatement 	: If '(' LogicExpression ')' Statement { 
                               $<stringValue>$=strdup("If ");
                                strcat($<stringValue>$,strdup($<stringValue>3));
                                strcat($<stringValue>$,strdup(" Then\n"));
                                strcat($<stringValue>$,strdup($<stringValue>5));
                                strcat($<stringValue>$,strdup("\nEnd If"));
                                }
|If '(' LogicExpression ')' MatchedStatement Else UnMatchedStatement   {
                                $<stringValue>$=strdup("If ");
                                strcat($<stringValue>$,strdup($<stringValue>3));
                                strcat($<stringValue>$,strdup(" Then\n"));
                                strcat($<stringValue>$,strdup($<stringValue>5));
                                strcat($<stringValue>$,strdup("\nElse\n"));
                                strcat($<stringValue>$,strdup($<stringValue>7));
                                strcat($<stringValue>$,strdup("\nEnd If"));
                                }
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

Delim: Space Delim 
|'\n' Delim  {lineNb++;}
|Space
|'\n' {lineNb++;}
;

Blank: Delim
|
;
%%
int yyerror(char* s){fprintf(stderr,"line %d :%s\n",lineNb,s);}
int main(void){ yyparse();}
