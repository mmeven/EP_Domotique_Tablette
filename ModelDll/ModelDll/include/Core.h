#ifndef CORE_H_INCLUDED
#define CORE_H_INCLUDED

#define DllExport __declspec(dllexport)
#define FILESAVE_NAME_SIZE 500
#include "Room.h"

namespace EP {

	/// \class Core
	/// \brief Le coeur de l'application, contient la liste des pièces,
	/// les paramètres de l'application ainsi que les méthodes pour gérer ceux-ci.
	class DllExport Core {
	public:

		/// \brief Le constructeur de Core.summary
		/// \param file Le nom du fichier de sauvegarde.
		Core(char* file);

		///
		///\brief Le destructeur de Core. Vide la liste de pièces.
		~Core();

		///
		///\brief Sauvegarde l'état actuel de l'application dans le fichier de sauvegarde.
		int save();

		///
		///\brief Charge l'application depuis le fichier de sauvegarde.
		int load();

		///\brief Ajoute le pointeur d'une pièce donnée dans m_listRooms.
		///Le nom d'une pièce doit être unique.
		///\param room Le pointeur de la pièce à ajouter.
		///\return 0 si tout s'est bien passé, 1 si le nom est déjà pris.
		int addRoom(Room* room);

		///\brief Supprime la pièce dont l'index est donné en paramètre.
		///\param index L'index de la pièce à supprimer de m_listRooms.
		///\return 0 si tout s'est bien passé, 1 si l'index ne correspond à aucune pièce.
		int deleteRoomByIndex(int index);

		///\brief Supprime la pièce dont le nom est donné en paramètre.
		///\param name Le nom de la pièce à supprimer de m_listRooms.
		///\return 0 si tout s'est bien passé, 1 si le nom ne correspond à aucune pièce.
		int deleteRoomByName(char* name);

		///\brief Ne pas utiliser cette méthode à partir de la DLL. A la place on peut itérer avec
		/// getNumberRooms et getRoomByIndex.
		///\return L'attribut m_listRooms.
		std::vector<Room*>* getRooms();

		///\brief Donne un pointeur vers la pièce dont le nom est donné en paramètre.
		///\param name Le nom de la pièce qu'on veut récupérer.
		///\return Un pointeur vers l'équipement demandé, NULL si aucun ne correspond.
		Room* getRoomByName(char* name);

		///\brief Donne un pointeur vers la pièce dont l'index est donné en paramètre.
		///\param index L'index de la pièce qu'on veut récupérer.
		///\return Un pointeur vers l'équipement demandé, NULL si aucun ne correspond.
		Room* getRoomByIndex(int index);

		///
		///\return Le nom du fichier de sauvegarde.
		char* getFileSave();

		///
		///\param name Le nouveau fichier de sauvegarde.
		void setFileSave(char* name);

		///
		///\return Le nombre de pièces de m_listRooms.
		int getNumberRooms();

		///
		///\return Le nom du port série utilisé pour communiquer avec le fauteuil.
		char* getCOMPort();

		///
		///\param port Le nom du port série à utiliser pour communiquer avec le fauteuil.
		void setCOMPort(char* port);

		///
		///\return L'identifiant du thème utilisé.
		int getThemeId();

		///
		///\return L'identifiant pour la taille des icônes.
		int getIconSize();

		///
		///\brief Permet de changer le thème utilisé.
		///\param id L'identifiant du nouveau thème.
		void setThemeId(int id);

		///
		///\brief Permet de changer la taille des icônes.
		///\param size Un chiffre correspondant à la nouvelle taille des icônes.
		void setIconSize(int size);
	private:

		///
		///\brief Le nom du fichier de sauvegarde.
		char m_coreSave[FILESAVE_NAME_SIZE];

		///
		///\brief La liste des pièces.
		std::vector<Room*> m_listRooms;

		///
		///\brief L'identifiant du thème utilisé.
		int m_themeId;

		///
		///\brief Un chiffre correspondant à la taille des icônes. Il ne s'agit pas de la taille exacte
		///mais d'un identifiant !
		int m_iconSize;

		///
		///\brief Le nom du port série utilisé pour communiquer avec le fauteuil.
		char* m_COMPort;
	};

	///\brief Constructeur statique utilisé pour permettre l'utilisation des objets Core en
	///passant par la DLL. Les paramètres sont les mêmes que ceux du constructeur.
	///\return Un pointeur vers l'objet créé.
	extern "C" DllExport Core* Core_New(char* file);

	///\brief Constructeur statique utilisé pour permettre l'utilisation des objets Core en
	///passant par la DLL. Les paramètres sont les mêmes que ceux du constructeur.
	///Après création de l'objet, la méthode Core::load() est appelée sur celui-ci.
	///\return Un pointeur vers l'objet créé.
	extern "C" DllExport Core* Core_NewFromSave(char* file);

	///\brief Destructeur statique, pour permettre la destruction des objets Core en passant par la DLL.
	///\param core Un pointeur vers l'objet à détruire.
	extern "C" DllExport void Core_Delete(Core* core);

	///\brief Destructeur statique, pour permettre la destruction des objets Core en passant par la DLL.
	///La méthode Core::save() est appelée avant sa destruction.
	///\param core Un pointeur vers l'objet à détruire.
	extern "C" DllExport void Core_SaveAndDelete(Core* core);
}

#endif // CORE_H_INCLUDED