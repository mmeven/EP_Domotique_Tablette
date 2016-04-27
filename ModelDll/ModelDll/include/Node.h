#ifndef NODE_H_INCLUDED
#define NODE_H_INCLUDED
#include <wchar.h>

namespace EP {

	// Represents a node of the tree used to modelize the rooms
	// and their equiments
	class __declspec(dllexport) Node {
	public:
		// Constructor
		Node(char* name, char* ico);

		//Destructor
		virtual ~Node(void);

		// Returns the name of the node
		char* getName();

		// Changes the name of the node with the given parameter
		void setName(char* name);

		// Returns the path to the icon used by this node
		char* getIco();

		// Sets m_ico
		void setIco(char* ico);

	protected:
		char m_name[100]; // Name of the room/equipment
		char m_ico[100]; // Name of the icone's file
		EP::Node* m_parent; // Parent of the node
	};

	extern "C" __declspec(dllexport) Node* Node_New(char* name, char* ico);
	extern "C" __declspec(dllexport) void Node_Delete(Node* node);
}
#endif