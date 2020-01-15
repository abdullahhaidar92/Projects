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
%}
%option noyywrap

IntegerValue [0-9]+
CharacterValue \'[a-zA-Z0-9]\'
SingleValue [0-9]+(\.[0-9]{1,8})?
DoubleValue [0-9]+(\.[0-9]{1,16})?
Id [a-zA-Z][a-zA-Z0-9]*
Text \"[a-zA-Z0-9%\\n+-=}{)( ]*\"
Delim [ \t\n]+
%%

int{Delim} {yylval.strVal="Integer";return(Integer);}
char{Delim} {yylval.strVal="Char";return (Char);}
float{Delim} {yylval.strVal="Single";return(Single);}
double{Delim} {yylval.strVal="Double";return(Double);}
if {return(If);} 
else[ \t\n]+ {return(Else);} 
while {return(While);}
for {return(For);}
{Text} {yylval.strVal = strdup(yytext);return(Text);}
printf {return(Print); }
scanf {return(Scan);}
{IntegerValue} {yylval.strVal=strdup(yytext);return(IntegerValue);}
{CharacterValue} {yylval.strVal=fixCharacterString(yytext);return(CharacterValue);}
{SingleValue} {yylval.strVal=strdup(yytext);return(SingleValue);}
{DoubleValue} {yylval.strVal=strdup(yytext);return(DoubleValue);}
{Id} {yylval.strVal = strdup(yytext);return(Id);}
[=][=] {return(EQ);}
[<][=] {return(LE);}
[>][=] {return(GE);}
[&][&] {return(AND);}
[|][|] {return(OR);}
[+][=] {return(ADD);}
[-][=] {return(SUB);}
[+][+] {return(INC);}
[-][-] {return(DEC);}
[+\-*/=,;&()%><|!}{] return(yytext[0]);
[ \t]+ ;
[\n] {lnb++;}
. {fprintf(stderr,"line %d :unrecognized token \n",lnb);return yytext[0];}

%%