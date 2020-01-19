%{
#include <stdio.h>
#include <string.h>
#include <stdlib.h> 
#include "linkedlist.h"
#include "strings.h"
int lineNb=1,i,counter;
int yyerror();
int yylex();
node_t * variablesStack = NULL;      
node_t * variablesWithValuesStack = NULL;  
node_t * variablesValuesStack = NULL;  
char* variable; 
char* value;
int flag,N=0,n; 
char* str;
char *text;
char *newText,*buff;
node_t * vlist = NULL;
void verifyDoesntExist(char* id){
    if(contains(vlist,id) > 0 )
        yyerror(concat(id," already defined\n"));
    else
        push(&vlist,id);       
}
void verifyExists(char* id){
    if(contains(vlist,id) <=0 )
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
%union
{
   char* strVal;
   char* strVals[2];
}
%token INTEGER CHAR SINGLE DOUBLE ID  
%token INTEGERVALUE CHARVALUE SINGLEVALUE DOUBLEVALUE
%token PRINT SCAN TEXT '(' ')'
%token IF ELSE EQ LE GE AND OR ADD SUB MUL DIV INC DEC
%token WHILE FOR
%%
start: code  {printf("valid\n");
              printf("%s",$<strVal>1);return(0);}
;

code : line code  { $<strVal>$=concat(concat($<strVal>1," \n"),$<strVal>2)};
|  {$<strVal>$="";  }
;

line : stmt {$<strVal>$=strdup($<strVal>1);}
| openStmt {$<strVal>$=strdup($<strVal>1);}
;

stmt : simpleStmt {$<strVal>$=strdup($<strVal>1);}
| whileLoop  {$<strVal>$=strdup($<strVal>1); }
| forLoop  {$<strVal>$=strdup($<strVal>1); }
| ifStmt  {$<strVal>$=strdup($<strVal>1); }
| '{' code '}'   {$<strVal>$=strdup($<strVal>2); }
;

openStmt:openIfStmt {$<strVal>$=strdup($<strVal>1);}
| openWhileLoop {$<strVal>$=strdup($<strVal>1);}
| openForLoop {$<strVal>$=strdup($<strVal>1);}
;

simpleStmt: type variables  ';' {    str="Dim "; 
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
| PRINT '(' TEXT args ')' ';'  { 
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
| SCAN  '(' TEXT addresses ')'  ';'   { str="";
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
| arithmaticExpression  ';'  {$<strVal>$=strdup($<strVal>1); }  
| assignment  ';'  {$<strVal>$=strdup($<strVal>1); }
;

ifStmt :IF '(' logicExpression ')' stmt ELSE stmt {
                                str=concat("If ",$<strVal>3);
                                str=concat(str,strdup(" Then\n\t"));
                                str=concat(str,indent($<strVal>5));
                                str=concat(str,strdup("\nElse\n\t"));
                                str=concat(str,indent($<strVal>7));
                                $<strVal>$=concat(str,strdup("\nEnd If"));
                                }
;

openIfStmt :IF '(' logicExpression ')' stmt ELSE openIfStmt   {
                               str=concat("If ",$<strVal>3);
                               str=concat(str," Then\n\t");
                                str=concat(str,indent($<strVal>5));
                                str=concat(str,"\nElse\n\t");
                               str=concat(str,indent($<strVal>7));
                                $<strVal>$=concat(str,"\nEnd If");
                                }
| IF '(' logicExpression ')'  line { 
                                str=concat("If ",$<strVal>3);
                                str=concat(str," Then\n\t");
                               str=concat(str,indent($<strVal>5));
                               $<strVal>$=concat(str,"\nEnd If");
                                }
;

arithmaticExpression: arithmaticExpression '+' term  {
                                   str=concat($<strVal>1,strdup(" + "));
                                   $<strVal>$=concat(str,$<strVal>3);
                                  }
|arithmaticExpression '-' term   { str=concat($<strVal>1," - ");
                                   $<strVal>$=concat(str,$<strVal>3);
                                  }
|term {$<strVal>$=strdup($<strVal>1);}
;

term: term '*' factor      {  str=concat($<strVal>1," * ");
                              $<strVal>$=concat(str,$<strVal>3);
                            }
|term '/' factor           {  str=concat($<strVal>1," / ");
                              $<strVal>$=concat(str,$<strVal>3);
                            }
|term '%' factor           { str=concat($<strVal>1," Mod ");
                             $<strVal>$=concat(str,$<strVal>3);
                            }
|factor {$<strVal>$=strdup($<strVal>1);}
;

factor: '(' arithmaticExpression ')'  { str=concat("( ",$<strVal>2);
                                        $<strVal>$=concat(str," )");
                                      }
|ID {verifyExists($<strVal>1); $<strVal>$=strdup($<strVal>1); }
|'-' ID   { verifyExists($<strVal>1); $<strVal>$=concat("-",$<strVal>2);}
|value {$<strVal>$=strdup($<strVal>1); }
;

logicExpression: logicExpression OR logicTerm  { str=concat($<strVal>1," Or ");
                                                 $<strVal>$=concat(str,$<strVal>3);        
                                                  }
|logicTerm {$<strVal>$=strdup($<strVal>1);}
;

logicTerm: logicTerm AND logicFactor   { str=concat($<strVal>1," And ");
                                         $<strVal>$=concat(str,$<strVal>3);
                                         }
|logicFactor {$<strVal>$=strdup($<strVal>1);}
;

logicFactor :'!' logicFactor   { $<strVal>$=concat("Not ",$<strVal>2); }
|notFactor  {$<strVal>$=strdup($<strVal>1);}
;

notFactor: '(' logicExpression ')'  { str=concat("( ",$<strVal>2);
                                         $<strVal>$=concat(str," )");
                                    }
| comparator  {$<strVal>$=strdup($<strVal>1);}
;

comparator: arithmaticExpression comparisonOperator arithmaticExpression { str=concat($<strVal>1,$<strVal>2);
                                                                  $<strVal>$=concat(str,$<strVal>3);
                                                                 }
;

comparisonOperator: EQ {$<strVal>$= " = " ;}
| '>'  {$<strVal>$= " > " ;}
| '<'  {$<strVal>$= " < " ;}
| LE   {$<strVal>$= " <= " ;}
| GE   {$<strVal>$= " >= " ;} 
; 

assignment: ID assignementOperation arithmaticExpression  { verifyExists($<strVal>1);$<strVal>$=concat(concat($<strVal>1,$<strVal>2),$<strVal>3);}  
| ID INC  { verifyExists($<strVal>1);$<strVal>$=concat($<strVal>1," += 1 ");} 
| ID DEC  { verifyExists($<strVal>1);$<strVal>$=concat($<strVal>1," -= 1 ");} 
;

assignementOperation: '=' {$<strVal>$= " = " ;}
| ADD  {$<strVal>$= " += " ;}
| SUB  {$<strVal>$= " -= " ;}
| MUL  {$<strVal>$= " *= " ;}
| DIV  {$<strVal>$= " /= " ;} 
;



whileLoop: WHILE '(' logicExpression ')' stmt {   str=concat("While ",$<strVal>3);
                                                  str=concat(str,"\n\t");
                                                  str=concat(str,indent($<strVal>5));
                                                  $<strVal>$=concat(str,"\nEnd While");}
;

forLoop: forExpr stmt  { str=concat($<strVals>1[0],indent($<strVal>2));
                         $<strVal>$=concat(concat(str,"\nNext "),$<strVals>1[1]);}                                                           
;

openForLoop: forExpr openStmt  { str=concat($<strVals>1[0],indent($<strVal>2));
                                 $<strVal>$=concat(concat(str,"\nNext "),$<strVals>1[1]);} 
;

forExpr : FOR initExpr boundExpr stepExpr {if(strcmp($<strVals>2[1],$<strVals>3[1])!=0
                                       || strcmp($<strVals>3[1],$<strVals>4[1])!=0 )
                                       yyerror(" Index of for loop must be the same ");
                                       str=concat("For ",$<strVals>2[1]);
                                       str=concat(str," = ");
                                       str=concat(str,$<strVals>2[0]);
                                       str=concat(str," To ");
                                       str=concat(str,$<strVals>3[0]);
                                       str=concat(str," Step ");
                                       str=concat(str,$<strVals>4[0]);
                                       $<strVals>$[0]=concat(str,"\n\t");
                                       $<strVals>$[1]=strdup($<strVals>2[1]);
                                             }
;

initExpr : '(' ID '=' arithmaticExpression ';' { verifyExists($<strVal>2);
                                                $<strVals>$[0]=strdup($<strVal>4);
                                                $<strVals>$[1]=strdup($<strVal>2);}

boundExpr : ID LE arithmaticExpression ';' {$<strVals>$[0]=strdup($<strVal>3);$<strVals>$[1]=strdup($<strVal>2);}
| ID GE arithmaticExpression ';'  {$<strVals>$[0]=strdup($<strVal>3);$<strVals>$[1]=strdup($<strVal>2);}
| ID '<' arithmaticExpression ';' {if(isNumeric($<strVal>3,&n))
                                            $<strVals>$[0]=stringValue(strdup(" "),16,n-1);
                                        else
                                            $<strVals>$[0]=concat($<strVal>3," - 1");
                                  $<strVals>$[1]=strdup($<strVal>2);}
| ID '>' arithmaticExpression ';' {if(isNumeric($<strVal>3,&n))
                                            $<strVals>$[0]=stringValue(strdup(" "),16,n-1);
                                        else
                                            $<strVals>$[0]=concat($<strVal>3," - 1");
                                  $<strVals>$[1]=strdup($<strVal>2);}

;
stepExpr : ID ADD arithmaticExpression ')' {$<strVals>$[0]=strdup($<strVal>3);$<strVals>$[1]=strdup($<strVal>2);}
|ID SUB  arithmaticExpression ')' {if(isNumeric($<strVal>3,&n))
                                            $<strVals>$[0]=stringValue(strdup(" "),16,-n);
                                        else
                                            $<strVals>$[0]=concat(concat("- ( ",$<strVal>3)," )");
                                  $<strVals>$[1]=strdup($<strVal>2);}
|ID INC  ')' {  $<strVals>$[0]=strdup("1");$<strVals>$[1]=strdup($<strVal>2);} 
|ID DEC  ')' {  $<strVals>$[0]=strdup("-1");$<strVals>$[1]=strdup($<strVal>2);}
;

openWhileLoop: WHILE '(' logicExpression ')' openStmt {   str=concat("While ",$<strVal>3);
                                                       str=concat(str,"\n\t");
                                                       buff=$<strVal>5;
                                                       for(i=0;i<strlen(buff);i++)
                                                          if(buff[i]=='\n')
                                                              str=concat(str,"\n\t");
                                                           else
                                                               str=append(str,buff[i]);
                                                        $<strVal>$=concat(str,"\nEnd While");}
;

variables : ID list { verifyDoesntExist($<strVal>1);
                      push(&variablesStack,$<strVal>1); }
| ID '=' value list { verifyDoesntExist($<strVal>1);
                      push(&variablesWithValuesStack,$<strVal>1);
                      push(&variablesValuesStack,$<strVal>3); }
;
list: ',' variables
|
;

type:INTEGER {$<strVal>$=strdup($<strVal>1);}
|CHAR  {$<strVal>$=strdup($<strVal>1);}
|SINGLE  {$<strVal>$=strdup($<strVal>1);}
|DOUBLE  {$<strVal>$=strdup($<strVal>1);}
;
value: CHARVALUE {$<strVal>$=strdup($<strVal>1);}
| numberValue {$<strVal>$=strdup($<strVal>1);}
| '-' numberValue { $<strVal>$=strdup(" -");
                    $<strVal>$=concat($<strVal>$,strdup($<strVal>2));}
;

numberValue: INTEGERVALUE {$<strVal>$=strdup($<strVal>1);}
| SINGLEVALUE {$<strVal>$=strdup($<strVal>1);}
| DOUBLEVALUE {$<strVal>$=strdup($<strVal>1);}
;

args : ',' ID args  { verifyExists($<strVal>2);
                     push(&variablesStack,$<strVal>2); }
|                     
;

addresses : ',' '&' ID addresses  { verifyExists($<strVal>3);
                                   push(&variablesStack,$<strVal>3); }
|                     
;
%%
int yyerror(char* s){fprintf(stderr,"%s\n",s);exit(0);}
int main(void){ yyparse();}
