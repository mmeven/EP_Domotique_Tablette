#ifndef CORE_H_INCLUDED
#define CORE_H_INCLUDED

#define FILESAVE_NAME_SIZE 500
#include "Room.h"

namespace EP {

	///<summary>Le coeur de l'application, contient la liste des pi�ces,
	///les param�tres de l'application ainsi que les m�thodes pour g�rer ceux-ci.</summary>
	class __declspec(dllexport) Core {
	public:
		///<summary>Le constructeur de Core.</summary>
		///<param name="file">Le nom du fichier de sauvegarde.</param>
		Core(char* file);

		///<summary>Le destructeur de Core. Vide la liste de pi�ces.</summary>
		~Core();

		///<summary>Sauvegarde l'�tat actuel de l'application dans le fichier de sauvegarde.</summary>
		int save();

		///<summary>Charge l'application depuis le fichier de sauvegarde.</summary>
		int load();

		///<summary>Ajoute le pointeur d'une pi�ce donn�e dans m_listRooms.
		///Le nom d'une pi�ce doit �tre unique.</summary>
		///<param name="equip">Le pointeur de la pi�ce � ajouter.</param>
		///<returns>0 si tout s'est bien pass�, 1 si le nom est d�j� pris.</returns>
		int addRoom(Room* room);

		///<summary>Supprime la pi�ce dont l'index est donn� en param�tre.</summary>
		///<param name="index">L'index de la pi�ce � supprimer de m_listRooms.</param>
		///<returns>0 si tout s'est bien pass�, 1 si l'index ne correspond � aucune pi�ce.</returns>
		int deleteRoomByIndex(int index);

		///<summary>Supprime la pi�ce dont le nom est donn� en param�tre.</summary>
		///<param name="name">Le nom de la pi�ce � supprimer de m_listRooms.</param>
		///<returns>0 si tout s'est bien pass�, 1 si le nom ne correspond � aucune pi�ce.</returns>
		int deleteRoomByName(char* name);

		///<summary>Ne pas utiliser cette m�thode � partir de la DLL. A la place on peut it�rer avec
		/// getNumberRooms et getRoomByIndex.</summary>
		///<returns>L'attribut m_listRooms.</returns>
		std::vector<Room*>* getRooms();

		///<summary>Donne un pointeur vers la pi�ce dont le nom est donn� en param�tre.</summary>
		///<param name="name">Le nom de la pi�ce qu'on veut r�cup�rer.</param>
		///<returns>Un pointeur vers l'�quipement demand�, NULL si aucun ne correspond.</returns>
		Room* getRoomByName(char* name);

		///<summary>Donne un pointeur vers la pi�ce dont l'index est donn� en param�tre.</summary>
		///<param name="index">L'index de la pi�ce qu'on veut r�cup�rer.</param>
		///<returns>Un pointeur vers l'�quipement demand�, NULL si aucun ne correspond.</returns>
		Room* getRoomByIndex(int index);

		///<returns>Le nom du fichier de sauvegarde.</returns>
		char* getFileSave();

		///<returns>Le nombre de pi�ces de m_listRooms.</returns>
		int getNumberRooms();

		///<returns>L'identifiant du th�me utilis�.</returns>
		int getThemeId();

		///<returns>L'identifiant pour la taille des ic�nes.</returns>
		int getIconSize();

		///<param name="id">L'identifiant du nouveau th�me.</param>
		void setThemeId(int id);

		///<param name="size">Un chiffre correspondant � la nouvelle taille des ic�nes.</param>
		void setIconSize(int size);
	private:
		///<summary>Le nom du fichier de sauvegarde.</summary>
		char m_coreSave[FILESAVE_NAME_SIZE];

		///<summary>La liste des pi�ces.</summary>
		std::vector<Room*> m_listRooms;

		///<summary>L'identifiant du th�me utilis�.</summary>
		int m_themeId;

		///<summary>Un chiffre correspondant � la taille des ic�nes. Il ne s'agit pas de la taille exacte
		///mais d'un identifiant !</summary>
		int m_iconSize;
	};

	///<summary>Constructeur statique utilis� pour permettre l'utilisation des objets Core en
	///passant par la DLL. Les param�tres sont les m�mes que ceux du constructeur.</summary>
	///<returns>Un pointeur vers l'objet cr��.</returns>
	extern "C" __declspec(dllexport) Core* Core_New(char* file);

	///<summary>Constructeur statique utilis� pour permettre l'utilisation des objets Core en
	///passant par la DLL. Les param�tres sont les m�mes que ceux du constructeur.
	///Apr�s cr�ation de l'objet, la m�thode Core::load() est appel�e sur celui-ci.</summary>
	///<returns>Un pointeur vers l'objet cr��.</returns>
	extern "C" __declspec(dllexport) Core* Core_NewFromSave(char* file);

	///<summary>Destructeur statique, pour permettre la destruction des objets Core en passant par la DLL.</summary>
	///<param name="core">Un pointeur vers l'objet � d�truire.</returns>
	extern "C" __declspec(dllexport) void Core_Delete(Core* core);

	///<summary>Destructeur statique, pour permettre la destruction des objets Core en passant par la DLL.
	///La m�thode Core::save() est appel�e avant sa destruction.</summary>
	///<param name="core">Un pointeur vers l'objet � d�truire.</returns>
	extern "C" __declspec(dllexport) void Core_SaveAndDelete(Core* core);
}

#endif // CORE_H_INCLUDED