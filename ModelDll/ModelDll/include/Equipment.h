#ifndef EQUIPMENT_H_INCLUDED
#define EQUIPMENT_H_INCLUDED
#include "Node.h"

namespace EP {

	///<summary>Représente un équipement. Cette classe abstraite est spécialisée par EquipmentKira et EquipmentFibaro.</summary>
	class __declspec(dllexport) Equipment : public Node {
	public:
		///<summary>Appelle le constructeur de Node, initialise m_roomParent et m_typeOf.</summary>
		///<param name="parent">La pièce contenant l'équipement</param>
		///<param name="typeOf">1: pour un équipement lié à la Kira, 2: pour un équipement lié à la Fibaro.</param>
		Equipment(char* name, char* ico, Node* parent, int typeOf) : Node(name, ico), m_roomParent(parent), m_typeOf(typeOf) {};

		virtual ~Equipment() {};

		///<summary>Envoie la requête HTTP permettant d'intéragir avec l'équipement.</summary>
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
		///<summary>Un pointeur vers la pièce contenant l'équipement.</summary>
		Node* m_roomParent;

		///<summary>1: pour un équipement lié à la Kira, 2: pour un équipement lié à la Fibaro.</summary>
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

	///<summary>Utiliser cette méthode plutôt que la méthode statique de la classe équipement lorsqu'on passe par la DLL.</summary>
	///<returns>L'adresse IP de la Kira.</returns>
	extern "C" __declspec(dllexport) char* Equipment_getIpKira();

	///<summary>Utiliser cette méthode plutôt que la méthode statique de la classe équipement lorsqu'on passe par la DLL.</summary>
	///<returns>L'adresse IP de la Fibaro.</returns>
	extern "C" __declspec(dllexport) char* Equipment_getIpFibaro();

	///<summary>Utiliser cette méthode plutôt que la méthode statique de la classe équipement lorsqu'on passe par la DLL.</summary>
	///<param name="new_ip">La nouvelle adresse IP de la Kira.</param>
	///<returns>0 si l'adresse est incorrecte, 1 sinon.</returns>
	extern "C" __declspec(dllexport) int Equipment_setIpKira(char* new_ip);

	///<summary>Utiliser cette méthode plutôt que la méthode statique de la classe équipement lorsqu'on passe par la DLL.</summary>
	///<param name="new_ip">La nouvelle adresse IP de la Fibaro.</param>
	///<returns>0 si l'adresse est incorrecte, 1 sinon.</returns>
	extern "C" __declspec(dllexport) int Equipment_setIpFibaro(char* new_ip);

	///<summary>Utiliser cette méthode plutôt que la méthode statique de la classe équipement lorsqu'on passe par la DLL.</summary>
	///<returns>Le login de la Fibaro.</returns>
	extern "C" __declspec(dllexport) char* Equipment_getLoginFibaro();

	///<summary>Utiliser cette méthode plutôt que la méthode statique de la classe équipement lorsqu'on passe par la DLL.</summary>
	///<returns>Le password de la Fibaro.</returns>
	extern "C" __declspec(dllexport) char* Equipment_getPasswordFibaro();

	///<summary>Utiliser cette méthode plutôt que la méthode statique de la classe équipement lorsqu'on passe par la DLL.</summary>
	///<param name="new_login">Le nouveau login de la Fibaro.</param>
	extern "C" __declspec(dllexport) int Equipment_setLoginFibaro(char* new_login);

	///<summary>Utiliser cette méthode plutôt que la méthode statique de la classe équipement lorsqu'on passe par la DLL.</summary>
	///<param name="new_password">Le nouveau password de la Fibaro.</param>
	extern "C" __declspec(dllexport) int Equipment_setPasswordFibaro(char* new_password);

	///<summary>Représente un équipement lié à une Kira, hérite de Equipment.</summary>
	class __declspec(dllexport) EquipmentKira : public Equipment
	{
	public:
		///<summary>Appelle le constructeur de Equipment et initialise m_buttonId et m_page.</summary>
		///<param name="buttonId">Le numéro du bouton correspondant à l'équipement dans l'interface web de la Kira.</param>
		///<param name="page">La page du bouton correspondant à l'équipement dans l'interface web de la Kira.</param>
		EquipmentKira(char* name, char* ico, Node* parent, int buttonId, int page);

		virtual ~EquipmentKira();

		///<summary>Exécute la requête HTTP correspondant à cet équipement.</summary>
		virtual int sendRequest();

		///<returns>Le numéro du bouton correspondant à l'équipement dans l'interface web de la Kira.</returns>
		int getButtonId();

		///<returns>Le numéro de page du bouton correspondant à l'équipement dans l'interface web de la Kira.</returns>
		int getPageNumber();

		///<param name="new_id">Le nouveau numéro du bouton.</param>
		int setButtonId(int new_id);

		///<param name="new_PageNumber">Le nouveau numéro de page du bouton.</param>
		int setPageNumber(int new_PageNumber);
	protected:

	private:
		///<summary>Le numéro du bouton correspondant à l'équipement dans l'interface web de la Kira.</summary>
		int m_buttonId;

		///<summary>Le numéro de page du bouton correspondant à l'équipement dans l'interface web de la Kira.</summary>
		int m_pageNumber;
	};

	///<summary>Constructeur statique utilisé pour permettre l'utilisation des objets EquipmentKira en
	///passant par la DLL. Les paramètres sont les mêmes que ceux du constructeur.</summary>
	///<returns>Un pointeur vers l'objet créé.</returns>
	extern "C" __declspec(dllexport) EquipmentKira* EquipmentKira_New(char* name, char* ico, Node* parent, int buttonId, int page);

	///<summary>Destructeur statique, pour permettre la destruction des objets EquipmentKira en passant par la DLL.</summary>
	///<param name="eq">Un pointeur vers l'objet à détruire.</returns>
	extern "C" __declspec(dllexport) void EquipmentKira_Delete(EquipmentKira* eq);

	///<summary>Représente un équipement lié à une Fibaro, hérite de Equipment</summary>
	class __declspec(dllexport) EquipmentFibaro : public Equipment
	{
	public:
		///<summary>Appelle le constructeur de Equipment et initialise m_equipmentId et m_action.</summary>
		///<param name="buttonId">L'identifiant correspondant à l'équipement dans l'interface web de la Fibaro.</param>
		///<param name="page">L'action que l'équipement devra effectuer.</param>
		EquipmentFibaro(char* name, char* ico, Node* parent, int equipmentId, char* action);

		virtual ~EquipmentFibaro();

		///<summary>Exécute la requête HTTP correspondant à cet équipement.</summary>
		virtual int sendRequest();

		///<returns>L'identifiant correspondant à l'équipement dans l'interface web de la Fibaro.</returns>
		int getEquipmentId();

		///<returns>L'action que l'équipement devra effectuer.</returns>
		char* getAction();
	protected:
	private:
		///<summary>L'identifiant correspondant à l'équipement dans l'interface web de la Fibaro.</summary>
		int m_equipmentId;

		///<summary>L'action que l'équipement devra effectuer.</summary>
		char m_action[300];
	};

	///<summary>Constructeur statique utilisé pour permettre l'utilisation des objets EquipmentFibaro en
	///passant par la DLL. Les paramètres sont les mêmes que ceux du constructeur.</summary>
	///<returns>Un pointeur vers l'objet créé.</returns>
	extern "C" __declspec(dllexport) EquipmentFibaro* EquipmentFibaro_New(char* name, char* ico, Node* parent, int equipmentId, char* action);

	///<summary>Destructeur statique, pour permettre la destruction des objets EquipmentFibaro en passant par la DLL.</summary>
	///<param name="eq">Un pointeur vers l'objet à détruire.</returns>
	extern "C" __declspec(dllexport) void EquipmentFibaro_Delete(EquipmentFibaro* eq);
}
#endif
