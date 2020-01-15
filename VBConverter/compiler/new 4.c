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
DoubleValue [0-9]+(\.?[0-9]{1,16})?
Id [a-zA-Z][a-zA-Z0-9]*
Text \"[a-zA-Z0-9%\\n+-=}{)( ]*\"
%%

int[ \t]+ {yylval.stringValue="Integer";return(Integer);}
char[ \t]+ {yylval.stringValue="Char";return (Char);}
float[ \t]+ {yylval.stringValue="Single";return(Single);}
double[ \t]+ {yylval.stringValue="Double";return(Double);}
if {return(If);} 
else[ \t\n] {if(yytext[0]=='\n')yylval.intValue=1;else yylval.intValue=0;return(Else);} 
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
[=][=] {return(EQ);}
[<][=] {return(LE);}
[>][=] {return(GE);}
[&][&] {return(AND);}
[|][|] {return(OR);}
[+][=] {return(ADD);}
[+\-*/=,;&()%><|!}{] return(yytext[0]);
[ \t]+ ;
[\n] {lnb++;}
. {fprintf(stderr,"line %d :unrecognized token \n",lnb);return yytext[0];}

%%