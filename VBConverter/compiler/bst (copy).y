%{
#include <stdio.h>
#include <stdlib.h> 
int yyerror();
int yylex();
int flag=1;
%}
%name parse
%union{
	struct{
		int val;
		int min;
		int max;
		int isEmpty;
	} node;
	int intVal;
}
%token <intVal> INT 
%type <node> tree
%%
start : tree  { if(flag==1) printf("Binary Search Tree\n"); else printf("Not Binary Search Tree\n");}
tree : INT tree tree {	
						if($2.isEmpty!=1)
							if($2.max >= $1)
								flag=0;
							else
								$$.min=$2.min;
						else
							$$.min=$1;

	
						if($3.isEmpty!=1)
							if($3.min <= $1)
								flag=0;
							else
								$$.max=$3.max;
						else
							$$.max=$1;
		
	
						$$.val=$1;
	
						$$.isEmpty=-1;	
						
					 }
								
| '(' ')' {$$.isEmpty=1;}
;
%%
int yyerror(char* s){fprintf(stderr,"%s\n",s);exit(0);}
int main(void){ yyparse();}