#ifndef EQUIPMENT_H_INCLUDED
#define EQUIPMENT_H_INCLUDED
#include "Node.h"

namespace EP {

	// Represents a node of the tree used to modelize the rooms
	// and their equiments
	class __declspec(dllexport) Equipment : public Node {
	public:
		// See Node.h
		// Also initializes m_typeOf
		Equipment(wchar_t* name, wchar_t* ico, Node* parent, int typeOf) : Node(name, ico), m_roomParent(parent), m_typeOf(typeOf) {};
		virtual ~Equipment() {};

		// Sends the HTTP Request corresponding to the Equipment
		virtual int sendRequest() = 0;

		// Returns m_typeOf
		int getTypeOf() { return m_typeOf; };

		// Returns m_roomParent
		Node* getNodeParent() { return m_roomParent; };
	protected:
		// The room containing the equipment
		Node* m_roomParent;

		// 1 : Kira
		// 2 : Fibaro
		int m_typeOf;
	};

	class __declspec(dllexport) EquipmentKira : public Equipment
	{
	public:
		// See Equipment
		// Initializes m_buttonId
		EquipmentKira(wchar_t* name, wchar_t* ico, Node* parent, int buttonId);

		virtual ~EquipmentKira();

		// See Equipment
		virtual int sendRequest();

		// Returns m_buttonId
		int getButtonId();
	protected:
	private:
		// The id of the button which will be activated by sendRequest()
		int m_buttonId;
	};

	extern "C" __declspec(dllexport) EquipmentKira* EquipmentKira_New(wchar_t* name, wchar_t* ico, Node* parent, int buttonId);
	extern "C" __declspec(dllexport) void EquipmentKira_Delete(EquipmentKira* eq);

	class __declspec(dllexport) EquipmentFibaro : public Equipment
	{
	public:
		// See Equipment
		// Initializes m_equipmentId, and m_action
		EquipmentFibaro(wchar_t* name, wchar_t* ico, Node* parent, int equipmentId, wchar_t* action);
		virtual ~EquipmentFibaro();
		// See Equipment
		virtual int sendRequest();

		// Returns m_equipmentId
		int getEquipmentId();

		// Returns m_action
		wchar_t* getAction();
	protected:
	private:
		// The id of the equipment which will be activated by sendRequest()
		int m_equipmentId;

		// The action realized by sendRequest()
		wchar_t m_action[300];
	};

	extern "C" __declspec(dllexport) EquipmentFibaro* EquipmentFibaro_New(wchar_t* name, wchar_t* ico, Node* parent, int equipmentId, wchar_t* action);
	extern "C" __declspec(dllexport) void EquipmentFibaro_Delete(EquipmentFibaro* eq);
}
#endif
