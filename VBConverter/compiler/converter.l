%{
#include<string.h>
#include "y.tab.h"
int lnb=1;
int datatype; 
int yylex();
char* fixCharacterString(char* str){
	char* s=strdup(str);
	s[0]='"';
	s[2]='"';
	return s;
}
extern int yylex();
%}
%option noyywrap

IntegerValue [0-9]+
CharacterValue \'[a-zA-Z0-9]\'
SingleValue [0-9]+(\.[0-9]{1,8})?
DoubleValue [0-9]+(\.[0-9]{1,16})?
Id [a-zA-Z][a-zA-Z0-9]*
OpenString \"([^%"\\]|\\.)*%
InnerString ([dcf]|lf)([^%("\\]|\\.)*%
CloseString ([dcf]|lf)([^"%(\\]|\\.)*\"
PureString \"([^%"\\]|\\.)*\"
Delim [ \t\n]+
BlockComment  "/*"([^*]|\*+[^*/])*\*+"/"  
LineComment "//".*\n
%%

{BlockComment}  ;
{LineComment}  ;
int{Delim} {yylval.strVal="Integer";return(INTEGER);}
char{Delim} {yylval.strVal="Char";return (CHAR);}
float{Delim} {yylval.strVal="Single";return(SINGLE);}
double{Delim} {yylval.strVal="Double";return(DOUBLE);}
void{Delim} {return(VOID);}
main {return(MAIN);}
if {return(IF);} 
else {return(ELSE);} 
while {return(WHILE);}
for {return(FOR);}
{OpenString} {yylval.strVal=strdup(yytext);yylval.strVal[strlen(yylval.strVal)-1]='\0';return(OPENSTRING); }
{InnerString} {yylval.strVal=strdup(yytext+1);
			   yylval.strVal[strlen(yylval.strVal)-1]='\0';
			   if(yytext[0]=='l') yylval.strVal=yylval.strVal+1;
			   return(INNERSTRING);}
{CloseString} {yylval.strVal=strdup(yytext+1);
			   if(yytext[0]=='l') yylval.strVal=yylval.strVal+1;
			   return(CLOSESTRING);}
{PureString} {yylval.strVal=strdup(yytext);return(PURESTRING); }
printf {return(PRINT); }
scanf {return(SCAN);}
{IntegerValue} {yylval.strVal=strdup(yytext);return(INTEGERVALUE);}
{CharacterValue} {yylval.strVal=fixCharacterString(yytext);return(CHARVALUE);}
{SingleValue} {yylval.strVal=strdup(yytext);return(SINGLEVALUE);}
{DoubleValue} {yylval.strVal=strdup(yytext);return(DOUBLEVALUE);}
{Id} {yylval.strVal = strdup(yytext);return(ID);}
"==" {return(EQ);}
"<=" {return(LE);}
">=" {return(GE);}
"&&" {return(AND);}
"||" {return(OR);}
"+=" {return(ADD);}
"-=" {return(SUB);}
"*=" {return(MUL);}
"/=" {return(DIV);}
"++" {return(INC);}
"--" {return(DEC);}
[+\-*/=,;&()><|!}{] return(yytext[0]);
[ \t]+ ;
[\n] {lnb++;}
				 
. {fprintf(stderr,"line %d :unrecognized token \n",lnb);return yytext[0];}

%%