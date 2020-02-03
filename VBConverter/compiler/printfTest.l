%{
#include <stdio.h>
%}
%option noyywrap
OpenString \"([^%"\\]|\\.)*%
InnerString [dcf]([^%"\\]|\\.)*%
CloseString [dcf]([^"%\\]|\\.)*\"
PureString \"([^%"\\]|\\.)*\"
%%
{OpenString} {printf("open "); }
{InnerString} {printf("inner "); }
{CloseString} {printf("close"); }
{PureString} {printf("pure"); }
. ;
%%

void main(){
yylex();
printf("\n");
}

// Desktop-U5A6FCP\Farah(Farah)