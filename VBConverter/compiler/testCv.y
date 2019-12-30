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
char *newText;
%}
%union
{
    int intValue;
    float floatValue;
    char *stringValue;
}
%token Integer Char Single Double Id  Space
%token IntegerValue CharacterValue SingleValue DoubleValue
%token Print Scan Text Address
%%
start: line   {printf("valid\n");return(0);}
;

line : Blank  Statement  line
|Blank  
;

Statement : DataType Delim Variables ';' {    printf("Dim ");
                                                flag=0;
                                             variable=pop(&variablesStack);
                                             if(variable){
                                                flag=1;
                                                printf("%s ",variable);
                                                variable=pop(&variablesStack);
                                                while(variable){ 
                                                printf(", %s ",variable);
                                                variable=pop(&variablesStack);  
                                                }
                                                printf("As %s ",$<stringValue>1);
                                             }
                                             variable=pop(&variablesWithValuesStack);
                                             value=pop(&variablesValuesStack);
                                             if(variable!=NULL){
                                                if(flag) 
                                                    printf(",");
                                                printf(" %s As %s = %s ",variable,$<stringValue>1,value);
                                                variable=pop(&variablesWithValuesStack);
                                                value=pop(&variablesValuesStack);
                                                while(variable)
                                                { printf(", %s As %s = %s ",variable,$<stringValue>1,value);
                                                  variable=pop(&variablesWithValuesStack);
                                                  value=pop(&variablesValuesStack); 
                                                }
                                             }
                                             printf("\n");
                                        }
                                        
|Print '(' Text Arguments ')' ';' { printf("Console.Write(");
                                    text=$<stringValue>3;
                                    counter=0;
                                    for(i=0;i<strlen(text);i++)
                                        if(text[i]=='\\' && text[i+1]=='n'){
                                            printf("\"& vbcrlf &\"");
                                            i++;
                                        }
                                            
                                        else if(text[i]=='%' && (text[i+1]=='d' || text[i+1]=='c' || text[i+1]=='f' 
                                                                 || (text[i+1]=='l' && text[i+2]=='f' && (i=i+1) )))
                                        {  printf("{%d}",counter);
                                           counter++;
                                           i+=1;
                                        }
                                        else
                                            printf("%c",text[i]);
                                   
                                    while(counter > 0){
                                       variable=pop(&variablesStack);
                                       if(variable)
                                           printf(",%s",variable);
                                       else
                                           yyerror("Not enough arguments");
                                       counter--;
                                   }
                                   if(pop(&variablesStack))
                                       yyerror("Too many arguments");
                                   
                                    printf(") \n");
                                  }

|Scan '(' Text Addresses ')' ';' {  text=$<stringValue>3;
                                    for(i=1;i<strlen(text)-1;i++)
                                        if(text[i]==' ')
                                            continue;
                                        else if(text[i]=='%' && (text[i+1]=='d' || text[i+1]=='c' || text[i+1]=='f' 
                                                                 || (text[i+1]=='l' && text[i+2]=='f' && (i=i+1) )))
                                        { 
                                          variable=pop(&variablesStack);
                                          if(variable)
                                             printf("%s = Console.ReadLine()\n",variable);
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
|Expression ';'   { printf("Here"); }
;

Expression: Expression '+' Term  { printf(" + %s",$<stringValue>3); }
|Term { printf("%s",$<stringValue>1); }
;

Term: '(' Expression ')'
|Id     { $<stringValue>$=$<stringValue>1 ;}
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
