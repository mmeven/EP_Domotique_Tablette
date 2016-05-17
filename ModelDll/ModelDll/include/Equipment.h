#ifndef EQUIPMENT_H_INCLUDED
#define EQUIPMENT_H_INCLUDED
#include "Node.h"

namespace EP {

	///<summary>Repr�sente un �quipement. Cette classe abstraite est sp�cialis�e par EquipmentKira et EquipmentFibaro.</summary>
	class __declspec(dllexport) Equipment : public Node {
	public:
		///<summary>Appelle le constructeur de Node, initialise m_roomParent et m_typeOf.</summary>
		///<param name="parent">La pi�ce contenant l'�quipement</param>
		///<param name="typeOf">1: pour un �quipement li� � la Kira, 2: pour un �quipement li� � la Fibaro.</param>
		Equipment(char* name, char* ico, Node* parent, int typeOf) : Node(name, ico), m_roomParent(parent), m_typeOf(typeOf) {};

		virtual ~Equipment() {};

		///<summary>Envoie la requ�te HTTP permettant d'int�ragir avec l'�quipement.</summary>
		virtual int sendRequest() = 0;

		///<returns>L'attribut m_typeOf.</returns>
		int getTypeOf() { return m_typeOf; };

		///<returns>L'attribut m_roomParent.</returns>
		Node* getNodeParent() { return m_roomParent; };

		///<returns>L'adresse IP de la Kira.</returns>
		static char* getIpKira() { return IP_Kira; };

		///<returns>L'adresse IP de la Fibaro.</returns>
		static char* getIpFibaro() { return IP_Fibaro; };

		///<returns>Le login de la Fibaro.</returns>
		static char* getLoginFibaro() { return Fibaro_login; };

		///<returns>Le password de la Fibaro.</returns>
		static char* getPasswordFibaro() { return Fibaro_password; };

		///<param name="new_ip">La nouvelle adresse IP de la Kira.</param>
		///<returns>0 si l'adresse est incorrecte, 1 sinon.</returns>
		static int setIpKira(char* new_ip);

		///<param name="new_ip">La nouvelle adresse IP de la Fibaro.</param>
		///<returns>0 si l'adresse est incorrecte, 1 sinon.</returns>
		static int setIpFibaro(char* new_ip);

		///<param name="new_login">Le nouveau login de la Fibaro.</param>
		static int setLoginFibaro(char* new_login);

		///<param name="new_password">Le nouveau password de la Fibaro.</param>
		static int setPasswordFibaro(char* new_password);

	protected:
		///<summary>Un pointeur vers la pi�ce contenant l'�quipement.</summary>
		Node* m_roomParent;

		///<summary>1: pour un �quipement li� � la Kira, 2: pour un �quipement li� � la Fibaro.</summary>
		int m_typeOf;

		///<summary>L'adresse IP de la Kira.</summary>
		static char IP_Kira[15];

		///<summary>L'adresse IP de la Fibaro.</summary>
		static char IP_Fibaro[15];

		///<summary>Le login de la Fibaro.</summary>
		static char Fibaro_login[300];

		///<summary>Le password de la Fibaro.</summary>
		static char Fibaro_password[300];
	};

	///<summary>Utiliser cette m�thode plut�t que la m�thode statique de la classe �quipement lorsqu'on passe par la DLL.</summary>
	///<returns>L'adresse IP de la Kira.</returns>
	extern "C" __declspec(dllexport) char* Equipment_getIpKira();

	///<summary>Utiliser cette m�thode plut�t que la m�thode statique de la classe �quipement lorsqu'on passe par la DLL.</summary>
	///<returns>L'adresse IP de la Fibaro.</returns>
	extern "C" __declspec(dllexport) char* Equipment_getIpFibaro();

	///<summary>Utiliser cette m�thode plut�t que la m�thode statique de la classe �quipement lorsqu'on passe par la DLL.</summary>
	///<param name="new_ip">La nouvelle adresse IP de la Kira.</param>
	///<returns>0 si l'adresse est incorrecte, 1 sinon.</returns>
	extern "C" __declspec(dllexport) int Equipment_setIpKira(char* new_ip);

	///<summary>Utiliser cette m�thode plut�t que la m�thode statique de la classe �quipement lorsqu'on passe par la DLL.</summary>
	///<param name="new_ip">La nouvelle adresse IP de la Fibaro.</param>
	///<returns>0 si l'adresse est incorrecte, 1 sinon.</returns>
	extern "C" __declspec(dllexport) int Equipment_setIpFibaro(char* new_ip);

	///<summary>Utiliser cette m�thode plut�t que la m�thode statique de la classe �quipement lorsqu'on passe par la DLL.</summary>
	///<returns>Le login de la Fibaro.</returns>
	extern "C" __declspec(dllexport) char* Equipment_getLoginFibaro();

	///<summary>Utiliser cette m�thode plut�t que la m�thode statique de la classe �quipement lorsqu'on passe par la DLL.</summary>
	///<returns>Le password de la Fibaro.</returns>
	extern "C" __declspec(dllexport) char* Equipment_getPasswordFibaro();

	///<summary>Utiliser cette m�thode plut�t que la m�thode statique de la classe �quipement lorsqu'on passe par la DLL.</summary>
	///<param name="new_login">Le nouveau login de la Fibaro.</param>
	extern "C" __declspec(dllexport) int Equipment_setLoginFibaro(char* new_login);

	///<summary>Utiliser cette m�thode plut�t que la m�thode statique de la classe �quipement lorsqu'on passe par la DLL.</summary>
	///<param name="new_password">Le nouveau password de la Fibaro.</param>
	extern "C" __declspec(dllexport) int Equipment_setPasswordFibaro(char* new_password);

	///<summary>Repr�sente un �quipement li� � une Kira, h�rite de Equipment.</summary>
	class __declspec(dllexport) EquipmentKira : public Equipment
	{
	public:
		///<summary>Appelle le constructeur de Equipment et initialise m_buttonId et m_page.</summary>
		///<param name="buttonId">Le num�ro du bouton correspondant � l'�quipement dans l'interface web de la Kira.</param>
		///<param name="page">La page du bouton correspondant � l'�quipement dans l'interface web de la Kira.</param>
		EquipmentKira(char* name, char* ico, Node* parent, int buttonId, int page);

		virtual ~EquipmentKira();

		///<summary>Ex�cute la requ�te HTTP correspondant � cet �quipement.</summary>
		virtual int sendRequest();

		///<returns>Le num�ro du bouton correspondant � l'�quipement dans l'interface web de la Kira.</returns>
		int getButtonId();

		///<returns>Le num�ro de page du bouton correspondant � l'�quipement dans l'interface web de la Kira.</returns>
		int getPageNumber();

		///<param name="new_id">Le nouveau num�ro du bouton.</param>
		int setButtonId(int new_id);

		///<param name="new_PageNumber">Le nouveau num�ro de page du bouton.</param>
		int setPageNumber(int new_PageNumber);
	protected:

	private:
		///<summary>Le num�ro du bouton correspondant � l'�quipement dans l'interface web de la Kira.</summary>
		int m_buttonId;

		///<summary>Le num�ro de page du bouton correspondant � l'�quipement dans l'interface web de la Kira.</summary>
		int m_pageNumber;
	};

	///<summary>Constructeur statique utilis� pour permettre l'utilisation des objets EquipmentKira en
	///passant par la DLL. Les param�tres sont les m�mes que ceux du constructeur.</summary>
	///<returns>Un pointeur vers l'objet cr��.</returns>
	extern "C" __declspec(dllexport) EquipmentKira* EquipmentKira_New(char* name, char* ico, Node* parent, int buttonId, int page);

	///<summary>Destructeur statique, pour permettre la destruction des objets EquipmentKira en passant par la DLL.</summary>
	///<param name="eq">Un pointeur vers l'objet � d�truire.</returns>
	extern "C" __declspec(dllexport) void EquipmentKira_Delete(EquipmentKira* eq);

	///<summary>Repr�sente un �quipement li� � une Fibaro, h�rite de Equipment</summary>
	class __declspec(dllexport) EquipmentFibaro : public Equipment
	{
	public:
		///<summary>Appelle le constructeur de Equipment et initialise m_equipmentId et m_action.</summary>
		///<param name="buttonId">L'identifiant correspondant � l'�quipement dans l'interface web de la Fibaro.</param>
		///<param name="page">L'action que l'�quipement devra effectuer.</param>
		EquipmentFibaro(char* name, char* ico, Node* parent, int equipmentId, char* action);

		virtual ~EquipmentFibaro();

		///<summary>Ex�cute la requ�te HTTP correspondant � cet �quipement.</summary>
		virtual int sendRequest();

		///<returns>L'identifiant correspondant � l'�quipement dans l'interface web de la Fibaro.</returns>
		int getEquipmentId();

		///<returns>L'action que l'�quipement devra effectuer.</returns>
		char* getAction();
	protected:
	private:
		///<summary>L'identifiant correspondant � l'�quipement dans l'interface web de la Fibaro.</summary>
		int m_equipmentId;

		///<summary>L'action que l'�quipement devra effectuer.</summary>
		char m_action[300];
	};

	///<summary>Constructeur statique utilis� pour permettre l'utilisation des objets EquipmentFibaro en
	///passant par la DLL. Les param�tres sont les m�mes que ceux du constructeur.</summary>
	///<returns>Un pointeur vers l'objet cr��.</returns>
	extern "C" __declspec(dllexport) EquipmentFibaro* EquipmentFibaro_New(char* name, char* ico, Node* parent, int equipmentId, char* action);

	///<summary>Destructeur statique, pour permettre la destruction des objets EquipmentFibaro en passant par la DLL.</summary>
	///<param name="eq">Un pointeur vers l'objet � d�truire.</returns>
	extern "C" __declspec(dllexport) void EquipmentFibaro_Delete(EquipmentFibaro* eq);
}
#endif
