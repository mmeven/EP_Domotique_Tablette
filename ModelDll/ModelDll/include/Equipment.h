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
		Equipment(char* name, char* ico, Node* parent, int typeOf) : Node(name, ico), m_roomParent(parent), m_typeOf(typeOf) {};
		virtual ~Equipment() {};

		// Sends the HTTP Request corresponding to the Equipment
		virtual int sendRequest() = 0;

		// Returns m_typeOf
		int getTypeOf() { return m_typeOf; };

		// Returns m_roomParent
		Node* getNodeParent() { return m_roomParent; };

		// To get the Kira's IP address
		static char* getIpKira() { return IP_Kira; };

		// To get the Fibaro's IP address
		static char* getIpFibaro() { return IP_Fibaro; };

		// To set the Kira's IP address (0 : ip incorrect, 1 : ok)
		static int setIpKira(char* new_ip);

		// To set the Fibaro's IP address (0 : ip incorrect, 1 : ok)
		static int setIpFibaro(char* new_ip);

		static char* getLoginFibaro() { return Fibaro_login; };

		static char* getPasswordFibaro() { return Fibaro_password; };

		static int setLoginFibaro(char* new_login);

		static int setPasswordFibaro(char* new_password);

	protected:
		// The room containing the equipment
		Node* m_roomParent;

		// 1 : Kira
		// 2 : Fibaro
		int m_typeOf;

		// Kira's adress
		static char IP_Kira[15];

		// Kira's adress
		static char IP_Fibaro[15];

		// Login of the Fibaro
		static char Fibaro_login[300];

		// Password of the Fibaro
		static char Fibaro_password[300];

	private:
		
	};

	class __declspec(dllexport) EquipmentKira : public Equipment
	{
	public:
		// See Equipment
		// Initializes m_buttonId
		EquipmentKira(char* name, char* ico, Node* parent, int buttonId, int page);

		virtual ~EquipmentKira();

		// See Equipment
		virtual int sendRequest();

		// Returns m_buttonId
		int getButtonId();
		int getPageNumber() { return m_pageNumber; };
		int setPageNumber(int new_PageNumber);
	protected:

	private:
		// The id of the button which will be activated by sendRequest()
		int m_buttonId;

		// Number of the page on which the button corresponding to the equipment is registered on the Kira
		int m_pageNumber;
	};

	extern "C" __declspec(dllexport) EquipmentKira* EquipmentKira_New(char* name, char* ico, Node* parent, int buttonId, int page);
	extern "C" __declspec(dllexport) void EquipmentKira_Delete(EquipmentKira* eq);

	class __declspec(dllexport) EquipmentFibaro : public Equipment
	{
	public:
		// See Equipment
		// Initializes m_equipmentId, and m_action
		EquipmentFibaro(char* name, char* ico, Node* parent, int equipmentId, char* action);
		virtual ~EquipmentFibaro();
		// See Equipment
		virtual int sendRequest();

		// Returns m_equipmentId
		int getEquipmentId();

		// Returns m_action
		char* getAction();
	protected:
	private:
		// The id of the equipment which will be activated by sendRequest()
		int m_equipmentId;

		// The action realized by sendRequest()
		char m_action[300];
	};

	extern "C" __declspec(dllexport) EquipmentFibaro* EquipmentFibaro_New(char* name, char* ico, Node* parent, int equipmentId, char* action);
	extern "C" __declspec(dllexport) void EquipmentFibaro_Delete(EquipmentFibaro* eq);
}
#endif
