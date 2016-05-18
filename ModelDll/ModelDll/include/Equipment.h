#ifndef EQUIPMENT_H_INCLUDED
#define EQUIPMENT_H_INCLUDED

#ifndef DllExport
#define DllExport  __declspec(dllexport)
#endif

#include "Node.h"

namespace EP {

	///\class Equipment
	///\brief Représente un équipement. Cette classe abstraite est spécialisée par EquipmentKira et EquipmentFibaro.
	class DllExport Equipment : public Node {
	public:
		///\brief Appelle le constructeur de Node, initialise m_roomParent et m_typeOf.
		///\param parent La pièce contenant l'équipement
		///\param typeOf 1: pour un équipement lié à la Kira, 2: pour un équipement lié à la Fibaro.
		Equipment(char* name, char* ico, Node* parent, int typeOf) : Node(name, ico), m_roomParent(parent), m_typeOf(typeOf) {};

		virtual ~Equipment() {};

		///\brief Envoie la requête HTTP permettant d'intéragir avec l'équipement.
		virtual int sendRequest() = 0;

		///\return L'attribut m_typeOf.
		int getTypeOf() { return m_typeOf; };

		///\return L'attribut m_roomParent.
		Node* getNodeParent() { return m_roomParent; };

		///\return L'adresse IP de la Kira.
		static char* getIpKira() { return IP_Kira; };

		///\return L'adresse IP de la Fibaro.
		static char* getIpFibaro() { return IP_Fibaro; };

		///\return Le login de la Fibaro.
		static char* getLoginFibaro() { return Fibaro_login; };

		///\return Le password de la Fibaro.
		static char* getPasswordFibaro() { return Fibaro_password; };

		///\param new_ip La nouvelle adresse IP de la Kira.
		///\return 0 si l'adresse est incorrecte, 1 sinon.
		static int setIpKira(char* new_ip);

		///\param new_ip La nouvelle adresse IP de la Fibaro.
		///\return 0 si l'adresse est incorrecte, 1 sinon.
		static int setIpFibaro(char* new_ip);

		///\param new_login Le nouveau login de la Fibaro.
		static int setLoginFibaro(char* new_login);

		///\param new_password Le nouveau password de la Fibaro.
		static int setPasswordFibaro(char* new_password);

	protected:
		///\brief Un pointeur vers la pièce contenant l'équipement.
		Node* m_roomParent;

		///\brief 1: pour un équipement lié à la Kira, 2: pour un équipement lié à la Fibaro.
		int m_typeOf;

		///\brief L'adresse IP de la Kira.
		static char IP_Kira[15];

		///\brief L'adresse IP de la Fibaro.
		static char IP_Fibaro[15];

		///\brief Le login de la Fibaro.
		static char Fibaro_login[300];

		///\brief Le password de la Fibaro.
		static char Fibaro_password[300];
	};

	///\brief Utiliser cette méthode plutôt que la méthode statique de la classe équipement lorsqu'on passe par la DLL.
	///\return L'adresse IP de la Kira.
	extern "C" DllExport char* Equipment_getIpKira();

	///\brief Utiliser cette méthode plutôt que la méthode statique de la classe équipement lorsqu'on passe par la DLL.
	///\return L'adresse IP de la Fibaro.
	extern "C" DllExport char* Equipment_getIpFibaro();

	///\brief Utiliser cette méthode plutôt que la méthode statique de la classe équipement lorsqu'on passe par la DLL.
	///\param new_ip La nouvelle adresse IP de la Kira.
	///\return 0 si l'adresse est incorrecte, 1 sinon.
	extern "C" DllExport int Equipment_setIpKira(char* new_ip);

	///\brief Utiliser cette méthode plutôt que la méthode statique de la classe équipement lorsqu'on passe par la DLL.
	///\param new_ip La nouvelle adresse IP de la Fibaro.
	///\return 0 si l'adresse est incorrecte, 1 sinon.
	extern "C" DllExport int Equipment_setIpFibaro(char* new_ip);

	///\brief Utiliser cette méthode plutôt que la méthode statique de la classe équipement lorsqu'on passe par la DLL.
	///\return Le login de la Fibaro.
	extern "C" DllExport char* Equipment_getLoginFibaro();

	///\brief Utiliser cette méthode plutôt que la méthode statique de la classe équipement lorsqu'on passe par la DLL.
	///\return Le password de la Fibaro.
	extern "C" DllExport char* Equipment_getPasswordFibaro();

	///\brief Utiliser cette méthode plutôt que la méthode statique de la classe équipement lorsqu'on passe par la DLL.
	///\param new_login Le nouveau login de la Fibaro.
	extern "C" DllExport int Equipment_setLoginFibaro(char* new_login);

	///\brief Utiliser cette méthode plutôt que la méthode statique de la classe équipement lorsqu'on passe par la DLL.
	///\param new_password Le nouveau password de la Fibaro.
	extern "C" DllExport int Equipment_setPasswordFibaro(char* new_password);

	///\class EquipmentKira
	///\brief Représente un équipement lié à une Kira, hérite de Equipment.
	class DllExport EquipmentKira : public Equipment
	{
	public:
		///\brief Appelle le constructeur de Equipment et initialise m_buttonId et m_page.
		///\param buttonId Le numéro du bouton correspondant à l'équipement dans l'interface web de la Kira.
		///\param page La page du bouton correspondant à l'équipement dans l'interface web de la Kira.
		EquipmentKira(char* name, char* ico, Node* parent, int buttonId, int page);

		virtual ~EquipmentKira();

		///\brief Exécute la requête HTTP correspondant à cet équipement.
		virtual int sendRequest();

		///\return Le numéro du bouton correspondant à l'équipement dans l'interface web de la Kira.
		int getButtonId();

		///\return Le numéro de page du bouton correspondant à l'équipement dans l'interface web de la Kira.
		int getPageNumber();

		///\param new_id Le nouveau numéro du bouton.
		int setButtonId(int new_id);

		///\param new_PageNumber Le nouveau numéro de page du bouton.
		int setPageNumber(int new_PageNumber);
	protected:

	private:
		///\brief Le numéro du bouton correspondant à l'équipement dans l'interface web de la Kira.
		int m_buttonId;

		///\brief Le numéro de page du bouton correspondant à l'équipement dans l'interface web de la Kira.
		int m_pageNumber;
	};

	///\brief Constructeur statique utilisé pour permettre l'utilisation des objets EquipmentKira en
	///passant par la DLL. Les paramètres sont les mêmes que ceux du constructeur.
	///\return Un pointeur vers l'objet créé.
	extern "C" DllExport EquipmentKira* EquipmentKira_New(char* name, char* ico, Node* parent, int buttonId, int page);

	///\brief Destructeur statique, pour permettre la destruction des objets EquipmentKira en passant par la DLL.
	///\param eq Un pointeur vers l'objet à détruire.
	extern "C" DllExport void EquipmentKira_Delete(EquipmentKira* eq);

	///\class EquipmentFibaro
	///\brief Représente un équipement lié à une Fibaro, hérite de Equipment
	class DllExport EquipmentFibaro : public Equipment
	{
	public:
		///\brief Appelle le constructeur de Equipment et initialise m_equipmentId et m_action.
		///\param equipmentId L'identifiant correspondant à l'équipement dans l'interface web de la Fibaro.
		///\param action L'action que l'équipement devra effectuer.
		EquipmentFibaro(char* name, char* ico, Node* parent, int equipmentId, char* action);

		virtual ~EquipmentFibaro();

		///\brief Exécute la requête HTTP correspondant à cet équipement.
		virtual int sendRequest();

		///\return L'identifiant correspondant à l'équipement dans l'interface web de la Fibaro.
		int getEquipmentId();

		///\return L'action que l'équipement devra effectuer.
		char* getAction();
	protected:
	private:
		///\brief L'identifiant correspondant à l'équipement dans l'interface web de la Fibaro.
		int m_equipmentId;

		///\brief L'action que l'équipement devra effectuer.
		char m_action[300];
	};

	///\brief Constructeur statique utilisé pour permettre l'utilisation des objets EquipmentFibaro en
	///passant par la DLL. Les paramètres sont les mêmes que ceux du constructeur.
	///\return Un pointeur vers l'objet créé.
	extern "C" DllExport EquipmentFibaro* EquipmentFibaro_New(char* name, char* ico, Node* parent, int equipmentId, char* action);

	///\brief Destructeur statique, pour permettre la destruction des objets EquipmentFibaro en passant par la DLL.
	///\param eq Un pointeur vers l'objet à détruire.
	extern "C" DllExport void EquipmentFibaro_Delete(EquipmentFibaro* eq);
}
#endif
