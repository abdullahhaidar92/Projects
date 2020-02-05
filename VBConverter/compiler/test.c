#include <stdio.h>
#include <string.h>
#define N 10
void main(){
	int i;
	char *buff=strdup("test");
	for(i=0;i<N;i++)
		strcat(buff,"test");
	printf("%s\n",buff);
}

