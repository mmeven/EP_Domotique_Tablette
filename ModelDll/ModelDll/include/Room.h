#ifndef ROOM_H_INCLUDED
#define ROOM_H_INCLUDED

#ifndef DllExport
#define DllExport  __declspec(dllexport)
#endif

#include <vector>
#include "Equipment.h"

namespace EP {
	/// \class Room
	/// \brief Représente une pièce de la maison ; Hérite de Node.
	/// On peut y retrouver les différents équipements liés à une pièce et les gérer.
	class DllExport Room : public Node {
	public:
		///\brief Appele le constructeur de Node
		Room(char* name, char* ico);

		///\brief Détruit la liste d'équipements
		~Room();

		///\brief Ajoute le pointeur d'un équipement donné dans m_listEquipments.
		///Le nom d'un équipement doit être unique.
		///\param equip Le pointeur de l'équipement à ajouter.
		///\return 0 si tout s'est bien passé, 1 si le nom est déjà pris.
		int addEquipment(Equipment* equip);

		///\brief Supprime l'équipement dont l'index est donné en paramètre.
		///\param index L'index de l'équipement à supprimer de m_listEquipments.
		///\return 0 si tout s'est bien passé, 1 si l'index ne correspond à aucun équipement.
		int deleteEquipmentByIndex(int index);

		///\brief Supprime l'équipement dont le nom est donné en paramètre.
		///\param name Le nom de l'équipement à supprimer de m_listEquipments.
		///\return 0 si tout s'est bien passé, 1 si le nom ne correspond à aucun équipement.
		int deleteEquipmentByName(char* name);

		///\brief Ne pas utiliser cette méthode à partir de la DLL. A la place on peut itérer avec
		/// getNumberEquipments et getEquipmentByIndex.
		///\return L'attribut m_listEquipments.
		std::vector<Equipment*>* getEquipments();

		///\brief Donne un pointeur vers l'équipement dont le nom est donné en paramètre.
		///\param name Le nom de l'équipement qu'on veut récupérer.
		///\return Un pointeur vers l'équipement demandé, NULL si aucun ne correspond.
		Equipment* getEquipmentByName(char* name);

		///\brief Donne un pointeur vers l'équipement dont l'index est donné en paramètre.
		///\param index L'index de l'équipement qu'on veut récupérer.
		///\return Un pointeur vers l'équipement demandé, NULL si aucun ne correspond.
		Equipment* getEquipmentByIndex(int index);

		///\return Le nombre d'équipement que la pièce contient
		int getNumberEquipments();
	private:
		///\brief La liste de tous les équipements contenus par cette pièce.
		std::vector<Equipment*> m_listEquipments;
	};

	///\brief Constructeur statique utilisé pour permettre l'utilisation des objets Room en
	///passant par la DLL. Les paramètres sont les mêmes que ceux du constructeur.
	///\return Un pointeur vers l'objet créé.
	extern "C" DllExport Room* Room_New(char* name, char* ico);

	///\brief Destructeur statique, pour permettre la destruction des objets Room en passant par la DLL.
	///\param room Un pointeur vers l'objet à détruire.
	extern "C" DllExport void Room_Delete(Room* room);
}

#endif ROOM_H_INCLUDED