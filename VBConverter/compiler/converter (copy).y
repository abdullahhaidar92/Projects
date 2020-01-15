%{
#include <stdio.h>
#include "linkedlist.h"
#include <string.h>
int lineNb=1,i,counter;
int yyerror();
int yylex();
int indent=0;
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
%token Integer Char Single Double Id  
%token IntegerValue CharacterValue SingleValue DoubleValue
%token Print Scan Text Address
%token If Else EQ LE GE AND OR ADD
%token While For
%%
start: Code  {printf("valid\n");
              printf("%s\n",$<stringValue>1);return(0);}
;

Code : Line  Code  {$<stringValue>$=strdup($<stringValue>1);
                                  strcat($<stringValue>$,strdup("\n"));
                                 strcat($<stringValue>$,strdup($<stringValue>2))};
|  {$<stringValue>$=strdup(" ");  }
;

Line:Statement 
|OpenIfStatement
;

Statement : SimpleStatement {$<stringValue>$=strdup($<stringValue>1);}
| WhileLoop  {$<stringValue>$=strdup($<stringValue>1); }
| ForLoop  {$<stringValue>$=strdup($<stringValue>1);  }
| ClosedIfStatement  {$<stringValue>$=strdup($<stringValue>1); }
| '{' Code '}'   {$<stringValue>$=strdup($<stringValue>2); }
;

SimpleStatement: DataType Variables  ';' { 
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
| Print LP Text Arguments RP ';'  { 
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
                                    // printf(".");
                                  }
| Scan  LP Text Addresses RP  ';'   {  $<stringValue>$=strdup("");
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
                                             strcat($<stringValue>$,strdup(" = Console.ReadLine()\t"));
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
| ArithmaticExpression  ';'  {$<stringValue>$=strdup($<stringValue>1); }  
| Assignment  ';'  {$<stringValue>$=strdup($<stringValue>1); }
;

ClosedIfStatement :If LP LogicExpression RP Statement Else Statement {
                                $<stringValue>$=strdup("If ");
                                strcat($<stringValue>$,strdup($<stringValue>3));
                                strcat($<stringValue>$,strdup(" Then\n    "));
                                    strcat($<stringValue>$,strdup("\t"));
                               buff=strdup($<stringValue>5);
                                for(i=0;i<strlen(buff);i++)
                                    if(buff[i]=='\n')
                                        strcat($<stringValue>$,strdup("\n\t"));
                                    else
                                         strncat($<stringValue>$,buff+i,1);
                                strcat($<stringValue>$,strdup("\n"));
                                strcat($<stringValue>$,strdup("Else\n    "));
                                  strcat($<stringValue>$,strdup("\t"));
                                buff=strdup($<stringValue>7);
                                for(i=0;i<strlen(buff);i++)
                                     if(buff[i]=='\n')
                                         strcat($<stringValue>$,strdup("\n\t"));
                                    else
                                         strncat($<stringValue>$,buff+i,1);
                                strcat($<stringValue>$,strdup("\n"));
                                strcat($<stringValue>$,strdup("\nEnd If"));
                                 printf(".");
                                }
;

OpenIfStatement 	:If LP LogicExpression RP Statement Else OpenIfStatement   {
                                $<stringValue>$=strdup("If ");
                                strcat($<stringValue>$,strdup($<stringValue>3));
                                strcat($<stringValue>$,strdup(" Then\n    "));
                                    strcat($<stringValue>$,strdup("\t"));
                               buff=strdup($<stringValue>6);
                                for(i=0;i<strlen(buff);i++)
                                    if(buff[i]=='\n')
                                        strcat($<stringValue>$,strdup("\n\t"));
                                    else
                                         strncat($<stringValue>$,buff+i,1);
                                strcat($<stringValue>$,strdup("\n"));
                                strcat($<stringValue>$,strdup("Else\n    "));
                                  strcat($<stringValue>$,strdup("\t"));
                                buff=strdup($<stringValue>10);
                                for(i=0;i<strlen(buff);i++)
                                     if(buff[i]=='\n')
                                         strcat($<stringValue>$,strdup("\n\t"));
                                    else
                                         strncat($<stringValue>$,buff+i,1);
                                strcat($<stringValue>$,strdup("\n"));
                                strcat($<stringValue>$,strdup("\nEnd If"));
                                 printf(".");
                                }
| If LP LogicExpression RP  Line { 
                               $<stringValue>$=strdup("If ");
                                strcat($<stringValue>$,strdup($<stringValue>3));
                                strcat($<stringValue>$,strdup(" Then\n    "));
                                strcat($<stringValue>$,strdup("\t"));
                                buff=strdup($<stringValue>5);
                                for(i=0;i<strlen(buff);i++)
                                    if(buff[i]=='\n')
                                        strcat($<stringValue>$,strdup("\n\t"));
                                    else
                                         strncat($<stringValue>$,buff+i,1);
                                strcat($<stringValue>$,strdup("\n"));
                                strcat($<stringValue>$,strdup("\nEnd If"));
                                printf(".");
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
|Id {$<stringValue>$=strdup($<stringValue>1); }
|'-' Id   { $<stringValue>$=strdup("-");
            strcat($<stringValue>$,$<stringValue>2);
           }
|Value {$<stringValue>$=strdup($<stringValue>1); }
;

LogicExpression: LogicExpression OR LogicTerm  { $<stringValue>$=$<stringValue>1;
                                                   strcat($<stringValue>$," Or ");
                                                   strcat($<stringValue>$,$<stringValue>3);
                                                  }
|LogicTerm 
;

LogicTerm: LogicTerm AND LogicFactor   { $<stringValue>$=$<stringValue>1;
                                          strcat($<stringValue>$," And ");
                                          strcat($<stringValue>$,$<stringValue>3);
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

Operation: EQ {$<stringValue>$= " = " ;}
| '>'  {$<stringValue>$= " > " ;}
| '<'  {$<stringValue>$= " < " ;}
| LE {$<stringValue>$= " <= " ;}
| GE {$<stringValue>$= " >= " ;} 
; 

Assignment: Id '=' ArithmaticExpression  { $<stringValue>$=strdup($<stringValue>1);
                                              strcat($<stringValue>$,strdup(" = "));
                                              strcat($<stringValue>$,strdup($<stringValue>3));
                                  }
;

WhileLoop: While '(' LogicExpression ')' Statement {  $<stringValue>$=strdup("While ");
                                                           strcat($<stringValue>$,strdup($<stringValue>3));
                                                           strcat($<stringValue>$,strdup("\n\t"));
                                                           strcat($<stringValue>$,strdup($<stringValue>5));
                                                           strcat($<stringValue>$,strdup("\nEnd While"));}
;

ForLoop: For LP Id '=' Value ';' Id LE Value ';' Id ADD Value RP Statement 
                                                        {  if(strcmp($<stringValue>3,$<stringValue>7)!=0
                                                            || strcmp($<stringValue>7,$<stringValue>11)!=0 )
                                                            yyerror(" Index of for loop must be the same ");
                                                           $<stringValue>$=strdup("For  ");
                                                           strcat($<stringValue>$,strdup($<stringValue>3));
                                                           strcat($<stringValue>$,strdup(" = "));
                                                           strcat($<stringValue>$,strdup($<stringValue>5));
                                                           strcat($<stringValue>$,strdup(" To "));
                                                           strcat($<stringValue>$,strdup($<stringValue>9));
                                                           strcat($<stringValue>$,strdup(" step "));
                                                           strcat($<stringValue>$,strdup($<stringValue>13));
                                                           strcat($<stringValue>$,strdup("\n\t"));
                                                           buff=strdup($<stringValue>15);
                                                           for(i=0;i<strlen(buff);i++)
                                                           if(buff[i]=='\n')
                                                           strcat($<stringValue>$,strdup("\n\t"));
                                                           else
                                                           strncat($<stringValue>$,buff+i,1);
                                                           strcat($<stringValue>$,strdup("\nNext "));
                                                           strcat($<stringValue>$,strdup($<stringValue>3));}                                                           
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
Value: CharacterValue {$<stringValue>$=$<stringValue>1;}
| NumberValue {$<stringValue>$=$<stringValue>1;}
| '-' NumberValue { $<stringValue>$=strdup("-");
                    strcat($<stringValue>$,$<stringValue>2);}
;

NumberValue: IntegerValue 
| SingleValue 
| DoubleValue 
;

Arguments : ',' Id Arguments  { push(&variablesStack,$<stringValue>2); }
|                     
;

Addresses : ',' '&' Id Addresses  { push(&variablesStack,$<stringValue>3); }
|                     
;

RP: ')'
;
LP: '('
;

%%
int yyerror(char* s){fprintf(stderr,"line %d :%s\n",lineNb,s);}
int main(void){ yyparse();}
