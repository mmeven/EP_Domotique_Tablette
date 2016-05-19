#include "../include/Node.h"
#include <iostream>
namespace EP {

	Node::Node(char* name, char* ico) : m_parent(0) {
		strcpy(m_name, name);
		strcpy(m_ico, ico);
	}

	Node::~Node(void) {
		printf("Node detruit\n");
	}

	char* Node::getName(){
		return m_name;
	}

	char* Node::getIco() {
		return m_ico;
	}

	void Node::setName(char* name) {
		strcpy(m_name, name);
	}

	void Node::setIco(char* ico) {
		strcpy(m_ico, ico);
	}

	extern "C" __declspec(dllexport) Node* Node_New(char* name, char* ico) {
		return new Node(name, ico);
	}

	extern "C" __declspec(dllexport) void Node_Delete(Node* node) {
		delete node;
	}
}