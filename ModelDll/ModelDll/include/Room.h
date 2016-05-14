#ifndef ROOM_H_INCLUDED
#define ROOM_H_INCLUDED

#include <vector>
#include "Equipment.h"

namespace EP {
	/// <summary>Représente une pièce de la maison ; Hérite de Node.
	/// On peut y retrouver les différents équipements liés à une pièce et les gérer.</summary>
	class __declspec(dllexport) Room : public Node {
	public:
		///<summary>Appele le constructeur de Node</summary>
		Room(char* name, char* ico);

		///<summary>Détruit la liste d'équipements</summary>
		~Room();

		///<summary>Ajoute le pointeur d'un équipement donné dans m_listEquipments.
		///Le nom d'un équipement doit être unique.</summary>
		///<param name="equip">Le pointeur de l'équipement à ajouter.</param>
		///<returns>0 si tout s'est bien passé, 1 si le nom est déjà pris.</returns>
		int addEquipment(Equipment* equip);

		///<summary>Supprime l'équipement dont l'index est donné en paramètre.</summary>
		///<param name="index">L'index de l'équipement à supprimer de m_listEquipments.</param>
		///<returns>0 si tout s'est bien passé, 1 si l'index ne correspond à aucun équipement.</returns>
		int deleteEquipmentByIndex(int index);

		///<summary>Supprime l'équipement dont le nom est donné en paramètre.</summary>
		///<param name="name">Le nom de l'équipement à supprimer de m_listEquipments.</param>
		///<returns>0 si tout s'est bien passé, 1 si le nom ne correspond à aucun équipement.</returns>
		int deleteEquipmentByName(char* name);

		///<summary>Ne pas utiliser cette méthode à partir de la DLL. A la place on peut itérer avec
		/// getNumberEquipments et getEquipmentByIndex.</summary>
		///<returns>L'attribut m_listEquipments.</returns>
		std::vector<Equipment*>* getEquipments();

		///<summary>Donne un pointeur vers l'équipement dont le nom est donné en paramètre.</summary>
		///<param name="name">Le nom de l'équipement qu'on veut récupérer.</param>
		///<returns>Un pointeur vers l'équipement demandé, NULL si aucun ne correspond.</returns>
		Equipment* getEquipmentByName(char* name);

		///<summary>Donne un pointeur vers l'équipement dont l'index est donné en paramètre.</summary>
		///<param name="index">L'index de l'équipement qu'on veut récupérer.</param>
		///<returns>Un pointeur vers l'équipement demandé, NULL si aucun ne correspond.</returns>
		Equipment* getEquipmentByIndex(int index);

		///<returns>Le nombre d'équipement que la pièce contient</returns>
		int getNumberEquipments();
	private:
		///<summary>La liste de tous les équipements contenus par cette pièce.</summary>
		std::vector<Equipment*> m_listEquipments;
	};

	///<summary>Constructeur statique utilisé pour permettre l'utilisation des objets Room en
	///passant par la DLL. Les paramètres sont les mêmes que ceux du constructeur.</summary>
	///<returns>Un pointeur vers l'objet créé.</returns>
	extern "C" __declspec(dllexport) Room* Room_New(char* name, char* ico);

	///<summary>Destructeur statique, pour permettre la destruction des objets Room en passant par la DLL.</summary>
	///<param name="room">Un pointeur vers l'objet à détruire.</returns>
	extern "C" __declspec(dllexport) void Room_Delete(Room* room);
}

#endif ROOM_H_INCLUDED