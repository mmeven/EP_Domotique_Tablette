#ifndef NODE_H_INCLUDED
#define NODE_H_INCLUDED
#include <wchar.h>

namespace EP {

	// Represents a node of the tree used to modelize the rooms
	// and their equiments
	class __declspec(dllexport) Node {
	public:
		// Basic constructor used only for the root of the tree, which doesn't need any icon
		Node(wchar_t* name);

		// Not used
		Node(wchar_t* name, wchar_t* ico);

		// Used by Room, Equipment
		Node(wchar_t* name, wchar_t* ico, EP::Node* parent);

		//Destructor
		virtual ~Node(void);

		// Returns the name of the node
		wchar_t* getName();

		// Changes the name of the node with the given parameter
		void setName(wchar_t* name);

		// Returns the path to the icon used by this node
		wchar_t* getIco();

	protected:
		wchar_t m_name[100]; // Name of the room/equipment
		wchar_t m_ico[100]; // Name of the icone's file
		EP::Node* m_parent; // Parent of the node
	};

	extern "C" __declspec(dllexport) Node* Node_NewNoIco(wchar_t* name);
	extern "C" __declspec(dllexport) Node* Node_NewNoParent(wchar_t* name, wchar_t* ico);
	extern "C" __declspec(dllexport) Node* Node_New(wchar_t* name, wchar_t* ico, EP::Node* parent);
	extern "C" __declspec(dllexport) void Node_Delete(Node* node);
}
#endif