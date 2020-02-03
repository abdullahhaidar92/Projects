#include <stdlib.h>
#include<string.h>
char* concat(char* buff1,char* buff2)
{
	int i=0;
	int N1=strlen(buff1);
	int N2=strlen(buff2);
	char* buff=(char*)malloc(N1+N2+2);
	for(i=0;i<N1;i++)
	{
		buff[i]=buff1[i];
	}
	for(i=0;i<N2;i++)
	{
		buff[N1+i]=buff2[i];
	}
	buff[N1+N2]='\0';
	return buff;
}
char* append(char* buff,char c)
{
	int i=0;
	int N=strlen(buff);
	char* result=(char*)malloc(N+2);
	for(i=0;i<N;i++)
	{
		result[i]=buff[i];
	}
    result[N]=c;
	result[N+1]='\0';
	return result;
}
static char *int_to_string_helper(char *dest, size_t n, int x) {
  if (n == 0) {
    return NULL;
  }
  if (x <= -10) {
    dest = int_to_string_helper(dest, n - 1, x / 10);
    if (dest == NULL) return NULL;
  }
  *dest = (char) ('0' - x % 10);
  return dest + 1;
}

char *stringValue(char *dest, size_t n, int x) {
  char *p = dest;
  if (n == 0) {
    return NULL;
  }
  n--;
  if (x < 0) {
    if (n == 0) return NULL;
    n--;
    *p++ = '-';
  } else {
    x = -x;
  }
  p = int_to_string_helper(p, n, x);
  if (p == NULL) return NULL;
  *p = 0;
  return dest;
}  

int isNumeric(const char *str,int * value) 
{
	int flag=0,p=1, i=0;
	*value=0;
	if(*str=='-'){
		str++;
		flag=1;
	}
    for(i=strlen(str)-1; i>=0 ;i--)
    {
        if(str[i] < '0' || str[i] > '9')
            return -1;
		*value = *value + (str[i]-'0')*p;
		p*=10;
       
    }
	if(flag)
		*value = - *value;
    return 1;
}





