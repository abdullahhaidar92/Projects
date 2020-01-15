#include<stdio.h>
#include<string.h>
#include <stdlib.h>
char* concat(char* buff1,char* buff2)
{
	int i=0;
	int N1=strlen(buff1);
	int N2=strlen(buff2);
	char* buff=(char*)malloc(N1+N2+1);
	for(i=0;i<N1;i++)
	{
		buff[i]=buff1[i];
	}
	for(i=0;i<N2;i++)
	{
		buff[N1+i]=buff2[i];
	}
	buff[N1+N2]='\0';
	free(buff1);
	free(buff2);
	return buff;
}


void main()
{
	char* buff1=strdup("Hello");
	char* buff2=strdup("World");
	char *buff=concat(buff1,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	buff=concat(buff,buff2);
	printf("\n%s",strdup(buff));
}

