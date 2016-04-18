#ifndef CORE_H_INCLUDED
#define CORE_H_INCLUDED

#define FILESAVE_NAME_SIZE 300
#include "Room.h"
#include "Theme.h"

namespace EP {

	// The core of the application
	// Contains the tree's root (Home) and the parameters
	class __declspec(dllexport) Core {
	public:
		Core(wchar_t* file);
		~Core();

		// Saves the parameters and the tree into a file
		// NOT IMPLEMENTED
		int save();

		// Loads the parameters and the tree from a file
		// NOT IMPLEMENTED
		int load();

		// Adds the pointer of a given room into m_listRooms
		// The name of the Rooms must be unique
		// Returns 1 if the name is already used
		int addRoom(Room* room);

		// Deletes the room at the given position.
		// Returns 1 if the index given is out of bounds
		int deleteRoomByIndex(int index);

		// Deletes the room with the given name.
		// Returns 1 if the name given doesn't exist
		int deleteRoomByName(wchar_t* name);

		// Returns the vector containing all the Rooms
		// Wont work in C#, or I haven't found out yet
		std::vector<Room*>* getRooms();

		// Returns a pointer to the Room corresponding to the given name
		// Returns NULL if it doesn't exist
		Room* getRoomByName(wchar_t* name);

		// Returns a pointer to the Room corresponding to the given index
		// Returns NULL if the index is out of bounds
		Room* getRoomByIndex(int index);

		// Returns the name of the file used to load and save the application
		wchar_t* getFileSave();

		// Returns the number of equipments in the vector
		int getNumberRooms();

		// Returns m_currentRoom
		Room* getCurrentRoom();

		// Returns m_themeId
		int getThemeId();

		// Returns m_iconSize
		int getIconSize();

		// Sets m_currentRoom
		void setCurrentRoom(Room* room);

		// Sets m_themeId
		void setThemeId(int id);

		// Sets m_iconSize
		void setIconSize(int size);
	private:
		wchar_t m_coreSave[FILESAVE_NAME_SIZE];

		// The list of all the Rooms
		std::vector<Room*> m_listRooms;

		// Pointer to the current room
		// 0 if the current node is the root
		Room* m_currentRoom;

		// The number of the theme used
		int m_themeId;

		// 1 : big
		// 2 : medium
		// 3 : little
		int m_iconSize;

		// The list of all the Themes
		std::vector<Theme*> m_themes;

		// Pointer to the current Theme
		Theme* currentTheme;
	};

	extern "C" __declspec(dllexport) Core* Core_New(wchar_t* file);

	// Charger l'appli via le fichier de sauvegarde
	extern "C" __declspec(dllexport) Core* Core_NewFromSave(wchar_t* file);

	extern "C" __declspec(dllexport) void Core_Delete(Core* core);

	// Sauvegarder et quitter
	extern "C" __declspec(dllexport) void Core_SaveAndDelete(Core* core);
}

#endif // CORE_H_INCLUDED