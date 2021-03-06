#include <stdio.h>
#include <stdlib.h>
typedef struct node {
    char* val;
    struct node * next;
} node_t;
void push(node_t ** head, char* val) {
    node_t * new_node;
    new_node = malloc(sizeof(node_t));

    new_node->val = val;
    new_node->next = *head;
    *head = new_node;
}

void pushBack(node_t ** head, char* val) {
	 node_t * current = *head;
	if(*head==NULL)
	{
		 *head = malloc(sizeof(node_t));
         (*head)->val = val;
         (*head)->next = NULL;
	}
   else
   {
	    while (current->next != NULL) {
        current = current->next;
      }
    current->next = malloc(sizeof(node_t));
    current->next->val = val;
    current->next->next = NULL;
   }
   
}
char* pop(node_t ** head) {
    char* retval = NULL;
    node_t * next_node = NULL;

    if (*head == NULL) {
        return NULL;
    }

    next_node = (*head)->next;
    retval = (*head)->val;
    free(*head);
    *head = next_node;

    return retval;
}

int contains(node_t * head,char* value) {
    node_t * current = head;
    while (current != NULL) {
        if(strcmp(value,current->val)==0)
            return 1;
        current = current->next;
    }
    return -1;
}

void print_list(node_t * head) {
    node_t * current = head;

    while (current != NULL) {
        printf("%s\n", current->val);
        current = current->next;
    }
}
