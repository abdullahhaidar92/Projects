%{
#include<string.h>
#include "y.tab.h"
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

IntegerValue -?[0-9]+
CharacterValue \'[a-zA-Z0-9]\'
SingleValue -?[0-9]+(\.[0-9]{1,8})?
DoubleValue -?[0-9]+(\.?[0-9]{1,16})?
Id [a-zA-Z][a-zA-Z0-9]*
Text \"[a-zA-Z0-9%\\n ]*\"
%%

int[ \t]+ {yylval.stringValue="Integer";return(Integer);}
char[ \t]+ {yylval.stringValue="Char";return (Char);}
float[ \t]+ {yylval.stringValue="Single";return(Single);}
double[ \t]+ {yylval.stringValue="Double";return(Double);}
if {return(If);} 
else[ \t]+ {return(Else);} 
while {return(While);}
for {return(For);}
{Text} {yylval.stringValue = strdup(yytext);return(Text);}
printf {return(Print); }
scanf {return(Scan);}
{IntegerValue} {yylval.stringValue=strdup(yytext);return(IntegerValue);}
{CharacterValue} {yylval.stringValue=fixCharacterString(yytext);return(CharacterValue);}
{SingleValue} {yylval.stringValue=strdup(yytext);return(SingleValue);}
{DoubleValue} {yylval.stringValue=strdup(yytext);return(DoubleValue);}
{Id} {yylval.stringValue = strdup(yytext);return(Id);}
[+\-*/=,;&()%><|!}{] return(yytext[0]);
[ \t]+ ;
[\n] return(NewLine);
. {fprintf(stderr,"unrecognized token \n");return yytext[0];}

%%