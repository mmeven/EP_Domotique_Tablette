#include "../include/Node.h"

namespace EP {
	Node::Node(wchar_t* name) : m_ico(L""), m_parent(0)	{
		wcscpy_s(m_name, name);
	}

	Node::Node(wchar_t* name, wchar_t* ico) :  m_parent(0)	{
		wcscpy_s(m_name, name);
		wcscpy_s(m_ico, ico);
	}

	Node::Node(wchar_t* name, wchar_t* ico, Node* parent) : m_parent(parent)	{
		wcscpy_s(m_name, name);
		wcscpy_s(m_ico, ico);
	}

	Node::~Node(void) {}

	wchar_t* Node::getName() {
		return m_name;
	}

	wchar_t* Node::getIco() {
		return m_ico;
	}

	void Node::setName(wchar_t* name) {
		wcscpy_s(m_name, name);
	}

	extern "C" __declspec(dllexport) Node* Node_NewNoIco(wchar_t* name) {
		return new Node(name);
	}

	extern "C" __declspec(dllexport) Node* Node_NewNoParent(wchar_t* name, wchar_t* ico) {
		return new Node(name, ico);
	}

	extern "C" __declspec(dllexport) Node* Node_New(wchar_t* name, wchar_t* ico, EP::Node* parent) {
		return new Node(name, ico, parent);
	}

	extern "C" __declspec(dllexport) void Node_Delete(Node* node) {
		delete node;
	}
}