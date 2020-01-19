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
%token Print Scan Text '(' ')'
%token If Else EQ LE GE AND OR ADD SUB MUL DIV INC DEC
%token While For
%%
start: Code  {printf("valid\n");
              printf("%s",$<strVal>1);return(0);}
;

Code : Line  Code  { $<strVal>$=concat(concat($<strVal>1," \n"),$<strVal>2)};
|  {$<strVal>$="";  }
;

Line:Statement {$<strVal>$=strdup($<strVal>1);}
|OpenStatement {$<strVal>$=strdup($<strVal>1);}
;

Statement : SimpleStatement {$<strVal>$=strdup($<strVal>1);}
| WhileLoop  {$<strVal>$=strdup($<strVal>1); }
| ForLoop  {$<strVal>$=strdup($<strVal>1); }
| IfStatement  {$<strVal>$=strdup($<strVal>1); }
| '{' Code '}'   {$<strVal>$=strdup($<strVal>2); }
;

OpenStatement:OpenIfStatement {$<strVal>$=strdup($<strVal>1);}
| OpenWhileLoop {$<strVal>$=strdup($<strVal>1);}
| OpenForLoop {$<strVal>$=strdup($<strVal>1);}
;

SimpleStatement: DataType Variables  ';' {    str="Dim "; 
                                              flag=0;
                                              variable=pop(&variablesStack);
                                              if(variable){
                                                flag=1;
                                                str=concat(str,variable);
                                                variable=pop(&variablesStack);
                                                while(variable){ 
                                                     str=concat(str," , ");
                                                    str=concat(str,variable);
                                                variable=pop(&variablesStack);  
                                                }
                                                str=concat(str," As ");
                                                str=concat(str,$<strVal>1);
                                             }
                                             variable=pop(&variablesWithValuesStack);
                                             value=pop(&variablesValuesStack);
                                             while(variable){
                                                if(flag)
                                                  str=concat(str,", ");
                                                  str=concat(str,variable);
                                                  str=concat(str," As ");
                                                  str=concat(str,$<strVal>1);
                                                  str=concat(str," = ");
                                                  str=concat(str,value);
                                                variable=pop(&variablesWithValuesStack);
                                                value=pop(&variablesValuesStack);
                                                flag=1;
                                             }
                                          $<strVal>$=str;
                                        }
| Print '(' Text Arguments ')' ';'  { 
                                    str="Console.Write(";
                                    text=$<strVal>3;
                                    counter=0;
                                    N=strlen(text);
                                    for(i=0;i<N-1;i++)
                                        if(text[i]=='\\' && text[i+1]=='n'){
                                           str=concat(str,"\"& vbcrlf &\"");
                                            i++;
                                        }
                                            
                                        else if(text[i]=='%' && (text[i+1]=='d' || text[i+1]=='c' || text[i+1]=='f' 
                                                                 || (text[i+1]=='l' && text[i+2]=='f' && (i=i+1) )))
                                        {  
                                            str=concat(str,"{");
                                            str=concat(str,stringValue(strdup(" "),3,counter));
                                            str=concat(str,"}");
                                           counter++;
                                           i+=1;
                                        }
                                        else
                                            str=append(str,text[i]);
                                        
                                     str=append(str,'"');
                                    while(counter > 0){
                                       variable=pop(&variablesStack);
                                       if(variable)
                                       {
                                           str=concat(str," , ");
                                           str=concat(str,variable);
                                       }
                                       else
                                           yyerror("Not enough arguments");
                                       counter--;
                                   }
                                   if(pop(&variablesStack))
                                       yyerror("Too many arguments");
                                   
                                    $<strVal>$=concat(str,") ");
                                    free(str);
                                  }
| Scan  '(' Text Addresses ')'  ';'   { str="";
                                    text=$<strVal>3;
                                    for(i=1;i<strlen(text)-1;i++)
                                        if(text[i]==' ')
                                            continue;
                                        else if(text[i]=='%' && (text[i+1]=='d' || text[i+1]=='c' || text[i+1]=='f' 
                                                                 || (text[i+1]=='l' && text[i+2]=='f' && (i=i+1) )))
                                        { 
                                          variable=pop(&variablesStack);
                                          if(variable)
                                          str=concat(str,concat(variable," = Console.ReadLine()\n"));
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
                                    str[strlen(str)-1]='\0';
                                    $<strVal>$=str;
                                  }
| ArithmaticExpression  ';'  {$<strVal>$=strdup($<strVal>1); }  
| Assignment  ';'  {$<strVal>$=strdup($<strVal>1); }
;

IfStatement :If '(' LogicExpression ')' Statement Else Statement {
                                str=concat("If ",$<strVal>3);
                                str=concat(str,strdup(" Then\n\t"));
                                buff=strdup($<strVal>5);
                                for(i=0;i<strlen(buff);i++)
                                    if(buff[i]=='\n')
                                        str=concat(str,"\n\t");
                                    else
                                         str=append(str,buff[i]);
                                str=concat(str,strdup("\nElse\n\t"));
                                buff=strdup($<strVal>7);
                                for(i=0;i<strlen(buff);i++)
                                     if(buff[i]=='\n')
                                      str=concat(str,"\n\t");
                                     else
                                          str=append(str,buff[i]);
                                $<strVal>$=concat(str,strdup("\nEnd If"));
                                }
;

OpenIfStatement :If '(' LogicExpression ')' Statement Else OpenIfStatement   {
                               str=concat("If ",$<strVal>3);
                               str=concat(str," Then\n\t");
                               buff=$<strVal>5;
                                for(i=0;i<strlen(buff);i++)
                                    if(buff[i]=='\n')
                                        str=concat(str,"\n\t");
                                    else
                                         str=append(str,buff[i]);
                                str=concat(str,"\nElse\n\t");
                                buff=$<strVal>7;
                                for(i=0;i<strlen(buff);i++)
                                    if(buff[i]=='\n')
                                        str=concat(str,"\n\t");
                                    else
                                         str=append(str,buff[i]);
                                $<strVal>$=concat(str,"\nEnd If");
                                }
| If '(' LogicExpression ')'  Line { 
                                str=concat("If ",$<strVal>3);
                                str=concat(str," Then\n\t");
                                buff=$<strVal>5;
                                for(i=0;i<strlen(buff);i++)
                                     if(buff[i]=='\n')
                                        str=concat(str,"\n\t");
                                    else
                                         str=append(str,buff[i]);
                               $<strVal>$=concat(str,"\nEnd If");
                                }
;

ArithmaticExpression: ArithmaticExpression '+' Term  {
                                   str=concat($<strVal>1,strdup(" + "));
                                   $<strVal>$=concat(str,$<strVal>3);
                                  }
|ArithmaticExpression '-' Term   { str=concat($<strVal>1," - ");
                                   $<strVal>$=concat(str,$<strVal>3);
                                  }
|Term {$<strVal>$=strdup($<strVal>1);}
;

Term: Term '*' Factor      {  str=concat($<strVal>1," * ");
                              $<strVal>$=concat(str,$<strVal>3);
                            }
|Term '/' Factor           {  str=concat($<strVal>1," / ");
                              $<strVal>$=concat(str,$<strVal>3);
                            }
|Term '%' Factor           { str=concat($<strVal>1," Mod ");
                             $<strVal>$=concat(str,$<strVal>3);
                            }
|Factor {$<strVal>$=strdup($<strVal>1);}
;

Factor: '(' ArithmaticExpression ')'  { str=concat("( ",$<strVal>2);
                                        $<strVal>$=concat(str," )");
                                      }
|Id {$<strVal>$=strdup($<strVal>1); }
|'-' Id   { $<strVal>$=concat("-",$<strVal>2);}
|Value {$<strVal>$=strdup($<strVal>1); }
;

LogicExpression: LogicExpression OR LogicTerm  { str=concat($<strVal>1," Or ");
                                                 $<strVal>$=concat(str,$<strVal>3);        
                                                  }
|LogicTerm {$<strVal>$=strdup($<strVal>1);}
;

LogicTerm: LogicTerm AND LogicFactor   { str=concat($<strVal>1," And ");
                                         $<strVal>$=concat(str,$<strVal>3);
                                         }
|LogicFactor {$<strVal>$=strdup($<strVal>1);}
;

LogicFactor :'!' LogicFactor   { $<strVal>$=concat("Not ",$<strVal>2); }
|NotFactor  {$<strVal>$=strdup($<strVal>1);}
;

NotFactor: '(' LogicExpression ')'  { str=concat("( ",$<strVal>2);
                                         $<strVal>$=concat(str," )");
                                    }
| Comparator  {$<strVal>$=strdup($<strVal>1);}
;

Comparator: ArithmaticExpression comparisonOperator ArithmaticExpression { str=concat($<strVal>1,$<strVal>2);
                                                                  $<strVal>$=concat(str,$<strVal>3);
                                                                 }
;

comparisonOperator: EQ {$<strVal>$= " = " ;}
| '>'  {$<strVal>$= " > " ;}
| '<'  {$<strVal>$= " < " ;}
| LE   {$<strVal>$= " <= " ;}
| GE   {$<strVal>$= " >= " ;} 
; 

Assignment: Id assignementOperation ArithmaticExpression  {  $<strVal>$=concat(concat($<strVal>1,$<strVal>2),$<strVal>3);
                                         }
;

assignementOperation: '=' {$<strVal>$= " = " ;}
| ADD  {$<strVal>$= " += " ;}
| SUB  {$<strVal>$= " -= " ;}
| MUL  {$<strVal>$= " *= " ;}
| DIV  {$<strVal>$= " /= " ;} 
;



WhileLoop: While '(' LogicExpression ')' Statement {   str=concat("While ",$<strVal>3);
                                                       str=concat(str,"\n\t");
                                                       buff=$<strVal>5;
                                                       for(i=0;i<strlen(buff);i++)
                                                          if(buff[i]=='\n')
                                                              str=concat(str,"\n\t");
                                                           else
                                                               str=append(str,buff[i]);
                                                        $<strVal>$=concat(str,"\nEnd While");}
;

ForLoop: For '(' Id '=' Value ';' Id LE Value ';' Id ADD Value ')' Statement 
                                                        {  if(strcmp($<strVal>3,$<strVal>7)!=0
                                                            || strcmp($<strVal>7,$<strVal>11)!=0 )
                                                            yyerror(" Index of for loop must be the same ");
                                                            str=concat("For ",$<strVal>3);
                                                            str=concat(str," = ");
                                                            str=concat(str,$<strVal>5);
                                                            str=concat(str," To ");
                                                            str=concat(str,$<strVal>9);
                                                            str=concat(str," Step ");
                                                            str=concat(str,$<strVal>13);
                                                            str=concat(str,"\n\t");
                                                            buff=$<strVal>15;
                                                           for(i=0;i<strlen(buff);i++)
                                                            if(buff[i]=='\n')
                                                                str=concat(str,"\n\t");
                                                           else
                                                               str=append(str,buff[i]);
                                                           str=concat(str,"\nNext ");
                                                           $<strVal>$=concat(str,$<strVal>3);
                                                           }                                                           
;

OpenWhileLoop: While '(' LogicExpression ')' OpenStatement {   str=concat("While ",$<strVal>3);
                                                       str=concat(str,"\n\t");
                                                       buff=$<strVal>5;
                                                       for(i=0;i<strlen(buff);i++)
                                                          if(buff[i]=='\n')
                                                              str=concat(str,"\n\t");
                                                           else
                                                               str=append(str,buff[i]);
                                                        $<strVal>$=concat(str,"\nEnd While");}
;

OpenForLoop: For '(' Id '=' Value ';' Id LE Value ';' Id ADD Value ')' OpenStatement 
                                                        {  if(strcmp($<strVal>3,$<strVal>7)!=0
                                                            || strcmp($<strVal>7,$<strVal>11)!=0 )
                                                            yyerror(" Index of for loop must be the same ");
                                                            str=concat("For ",$<strVal>3);
                                                            str=concat(str," = ");
                                                            str=concat(str,$<strVal>5);
                                                            str=concat(str," To ");
                                                            str=concat(str,$<strVal>9);
                                                            str=concat(str," Step ");
                                                            str=concat(str,$<strVal>13);
                                                            str=concat(str,"\n\t");
                                                            buff=$<strVal>15;
                                                           for(i=0;i<strlen(buff);i++)
                                                            if(buff[i]=='\n')
                                                                str=concat(str,"\n\t");
                                                           else
                                                               str=append(str,buff[i]);
                                                           str=concat(str,"\nNext ");
                                                           $<strVal>$=concat(str,$<strVal>3);
                                                           }                                                           
;

Variables : Id List { push(&variablesStack,$<strVal>1); }
| Id '=' Value List { push(&variablesWithValuesStack,$<strVal>1);
                      push(&variablesValuesStack,$<strVal>3); }

List: ',' Variables
|
;

DataType:Integer {$<strVal>$=strdup($<strVal>1);}
|Char  {$<strVal>$=strdup($<strVal>1);}
|Single  {$<strVal>$=strdup($<strVal>1);}
|Double  {$<strVal>$=strdup($<strVal>1);}
;
Value: CharacterValue {$<strVal>$=strdup($<strVal>1);}
| NumberValue {$<strVal>$=strdup($<strVal>1);}
| '-' NumberValue { $<strVal>$=strdup(" -");
                    $<strVal>$=concat($<strVal>$,strdup($<strVal>2));}
;

NumberValue: IntegerValue {$<strVal>$=strdup($<strVal>1);}
| SingleValue {$<strVal>$=strdup($<strVal>1);}
| DoubleValue {$<strVal>$=strdup($<strVal>1);}
;

Arguments : ',' Id Arguments  { push(&variablesStack,$<strVal>2); }
|                     
;

Addresses : ',' '&' Id Addresses  { push(&variablesStack,$<strVal>3); }
|                     
;
%%
int yyerror(char* s){fprintf(stderr,"line %d :%s\n",lineNb,s);}
int main(void){ yyparse();}
