%{
#include<stdio.h>
#include <stdlib.h>
#include "y.tab.h"
%}
%option noyywrap
%%
[0-9]+ {yylval.intVal=atoi(yytext);return(INT);}
[)(] return yytext[0];
[ \t\n]+ ;
. printf("Unrecognized token\n");
%%