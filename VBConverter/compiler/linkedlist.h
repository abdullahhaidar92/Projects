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

node_t* pushBack(node_t * head, char* val) {
	 node_t * current = head;
	if(head==NULL)
	{
		 head = malloc(sizeof(node_t));
         head->val = val;
         head->next = NULL;
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
	return head;
   
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

void print_list(node_t * head) {
    node_t * current = head;

    while (current != NULL) {
        printf("%s\n", current->val);
        current = current->next;
    }
}
