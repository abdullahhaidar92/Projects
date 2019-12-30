#!/bin/bash

flex $1
bison -dy $2
gcc y.tab.c lex.yy.c -o target.exe
./target.exe 