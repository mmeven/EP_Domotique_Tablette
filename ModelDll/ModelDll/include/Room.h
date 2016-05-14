#ifndef ROOM_H_INCLUDED
#define ROOM_H_INCLUDED

#include <vector>
#include "Equipment.h"

namespace EP {
	/// <summary>Repr�sente une pi�ce de la maison ; H�rite de Node.
	/// On peut y retrouver les diff�rents �quipements li�s � une pi�ce et les g�rer.</summary>
	class __declspec(dllexport) Room : public Node {
	public:
		///<summary>Appele le constructeur de Node</summary>
		Room(char* name, char* ico);

		///<summary>D�truit la liste d'�quipements</summary>
		~Room();

		///<summary>Ajoute le pointeur d'un �quipement donn� dans m_listEquipments.
		///Le nom d'un �quipement doit �tre unique.</summary>
		///<param name="equip">Le pointeur de l'�quipement � ajouter.</param>
		///<returns>0 si tout s'est bien pass�, 1 si le nom est d�j� pris.</returns>
		int addEquipment(Equipment* equip);

		///<summary>Supprime l'�quipement dont l'index est donn� en param�tre.</summary>
		///<param name="index">L'index de l'�quipement � supprimer de m_listEquipments.</param>
		///<returns>0 si tout s'est bien pass�, 1 si l'index ne correspond � aucun �quipement.</returns>
		int deleteEquipmentByIndex(int index);

		///<summary>Supprime l'�quipement dont le nom est donn� en param�tre.</summary>
		///<param name="name">Le nom de l'�quipement � supprimer de m_listEquipments.</param>
		///<returns>0 si tout s'est bien pass�, 1 si le nom ne correspond � aucun �quipement.</returns>
		int deleteEquipmentByName(char* name);

		///<summary>Ne pas utiliser cette m�thode � partir de la DLL. A la place on peut it�rer avec
		/// getNumberEquipments et getEquipmentByIndex.</summary>
		///<returns>L'attribut m_listEquipments.</returns>
		std::vector<Equipment*>* getEquipments();

		///<summary>Donne un pointeur vers l'�quipement dont le nom est donn� en param�tre.</summary>
		///<param name="name">Le nom de l'�quipement qu'on veut r�cup�rer.</param>
		///<returns>Un pointeur vers l'�quipement demand�, NULL si aucun ne correspond.</returns>
		Equipment* getEquipmentByName(char* name);

		///<summary>Donne un pointeur vers l'�quipement dont l'index est donn� en param�tre.</summary>
		///<param name="index">L'index de l'�quipement qu'on veut r�cup�rer.</param>
		///<returns>Un pointeur vers l'�quipement demand�, NULL si aucun ne correspond.</returns>
		Equipment* getEquipmentByIndex(int index);

		///<returns>Le nombre d'�quipement que la pi�ce contient</returns>
		int getNumberEquipments();
	private:
		///<summary>La liste de tous les �quipements contenus par cette pi�ce.</summary>
		std::vector<Equipment*> m_listEquipments;
	};

	///<summary>Constructeur statique utilis� pour permettre l'utilisation des objets Room en
	///passant par la DLL. Les param�tres sont les m�mes que ceux du constructeur.</summary>
	///<returns>Un pointeur vers l'objet cr��.</returns>
	extern "C" __declspec(dllexport) Room* Room_New(char* name, char* ico);

	///<summary>Destructeur statique, pour permettre la destruction des objets Room en passant par la DLL.</summary>
	///<param name="room">Un pointeur vers l'objet � d�truire.</returns>
	extern "C" __declspec(dllexport) void Room_Delete(Room* room);
}

#endif ROOM_H_INCLUDED