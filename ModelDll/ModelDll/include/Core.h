#ifndef CORE_H_INCLUDED
#define CORE_H_INCLUDED

#define FILESAVE_NAME_SIZE 500
#include "Room.h"

namespace EP {

	///<summary>Le coeur de l'application, contient la liste des pièces,
	///les paramètres de l'application ainsi que les méthodes pour gérer ceux-ci.</summary>
	class __declspec(dllexport) Core {
	public:
		///<summary>Le constructeur de Core.</summary>
		///<param name="file">Le nom du fichier de sauvegarde.</param>
		Core(char* file);

		///<summary>Le destructeur de Core. Vide la liste de pièces.</summary>
		~Core();

		///<summary>Sauvegarde l'état actuel de l'application dans le fichier de sauvegarde.</summary>
		int save();

		///<summary>Charge l'application depuis le fichier de sauvegarde.</summary>
		int load();

		///<summary>Ajoute le pointeur d'une pièce donnée dans m_listRooms.
		///Le nom d'une pièce doit être unique.</summary>
		///<param name="equip">Le pointeur de la pièce à ajouter.</param>
		///<returns>0 si tout s'est bien passé, 1 si le nom est déjà pris.</returns>
		int addRoom(Room* room);

		///<summary>Supprime la pièce dont l'index est donné en paramètre.</summary>
		///<param name="index">L'index de la pièce à supprimer de m_listRooms.</param>
		///<returns>0 si tout s'est bien passé, 1 si l'index ne correspond à aucune pièce.</returns>
		int deleteRoomByIndex(int index);

		///<summary>Supprime la pièce dont le nom est donné en paramètre.</summary>
		///<param name="name">Le nom de la pièce à supprimer de m_listRooms.</param>
		///<returns>0 si tout s'est bien passé, 1 si le nom ne correspond à aucune pièce.</returns>
		int deleteRoomByName(char* name);

		///<summary>Ne pas utiliser cette méthode à partir de la DLL. A la place on peut itérer avec
		/// getNumberRooms et getRoomByIndex.</summary>
		///<returns>L'attribut m_listRooms.</returns>
		std::vector<Room*>* getRooms();

		///<summary>Donne un pointeur vers la pièce dont le nom est donné en paramètre.</summary>
		///<param name="name">Le nom de la pièce qu'on veut récupérer.</param>
		///<returns>Un pointeur vers l'équipement demandé, NULL si aucun ne correspond.</returns>
		Room* getRoomByName(char* name);

		///<summary>Donne un pointeur vers la pièce dont l'index est donné en paramètre.</summary>
		///<param name="index">L'index de la pièce qu'on veut récupérer.</param>
		///<returns>Un pointeur vers l'équipement demandé, NULL si aucun ne correspond.</returns>
		Room* getRoomByIndex(int index);

		///<returns>Le nom du fichier de sauvegarde.</returns>
		char* getFileSave();

		///<returns>Le nombre de pièces de m_listRooms.</returns>
		int getNumberRooms();

		///<returns>L'identifiant du thème utilisé.</returns>
		int getThemeId();

		///<returns>L'identifiant pour la taille des icônes.</returns>
		int getIconSize();

		///<param name="id">L'identifiant du nouveau thème.</param>
		void setThemeId(int id);

		///<param name="size">Un chiffre correspondant à la nouvelle taille des icônes.</param>
		void setIconSize(int size);
	private:
		///<summary>Le nom du fichier de sauvegarde.</summary>
		char m_coreSave[FILESAVE_NAME_SIZE];

		///<summary>La liste des pièces.</summary>
		std::vector<Room*> m_listRooms;

		///<summary>L'identifiant du thème utilisé.</summary>
		int m_themeId;

		///<summary>Un chiffre correspondant à la taille des icônes. Il ne s'agit pas de la taille exacte
		///mais d'un identifiant !</summary>
		int m_iconSize;
	};

	///<summary>Constructeur statique utilisé pour permettre l'utilisation des objets Core en
	///passant par la DLL. Les paramètres sont les mêmes que ceux du constructeur.</summary>
	///<returns>Un pointeur vers l'objet créé.</returns>
	extern "C" __declspec(dllexport) Core* Core_New(char* file);

	///<summary>Constructeur statique utilisé pour permettre l'utilisation des objets Core en
	///passant par la DLL. Les paramètres sont les mêmes que ceux du constructeur.
	///Après création de l'objet, la méthode Core::load() est appelée sur celui-ci.</summary>
	///<returns>Un pointeur vers l'objet créé.</returns>
	extern "C" __declspec(dllexport) Core* Core_NewFromSave(char* file);

	///<summary>Destructeur statique, pour permettre la destruction des objets Core en passant par la DLL.</summary>
	///<param name="core">Un pointeur vers l'objet à détruire.</returns>
	extern "C" __declspec(dllexport) void Core_Delete(Core* core);

	///<summary>Destructeur statique, pour permettre la destruction des objets Core en passant par la DLL.
	///La méthode Core::save() est appelée avant sa destruction.</summary>
	///<param name="core">Un pointeur vers l'objet à détruire.</returns>
	extern "C" __declspec(dllexport) void Core_SaveAndDelete(Core* core);
}

#endif // CORE_H_INCLUDED