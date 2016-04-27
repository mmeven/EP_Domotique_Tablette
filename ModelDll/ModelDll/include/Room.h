#ifndef ROOM_H_INCLUDED
#define ROOM_H_INCLUDED

#include <vector>
#include "Equipment.h"

namespace EP {
	// Represents a node of the tree used to modelize the rooms
	// and their equiments
	class __declspec(dllexport) Room : public Node {
	public:
		// See Node.h
		Room(char* name, char* ico);

		// Deletes the list of Equipments
		~Room();

		// Adds the pointer of an given equipment into m_listEquipments
		// The name of the Equipments must be unique
		// Returns 1 if the name is already used
		int addEquipment(Equipment* equip);

		// Deletes the equipment at the given position.
		// Returns 1 if the index given is out of bounds
		int deleteEquipmentByIndex(int index);

		// Deletes the equipment with the given name.
		// Returns 1 if the name given doesn't exist
		int deleteEquipmentByName(char* name);

		// Returns a pointer of the first element of an array containing all Equipments
		// Use this in a C# code
		// NOT IMPLEMENTED
		//Equipment* getEquipmentsAsArray();

		// Returns the vector containing all the Equipments of the Room
		// Wont work in C#, or I haven't found out yet
		std::vector<Equipment*>* getEquipments();

		// Returns a pointer to the Equipment corresponding to the given name
		// Returns NULL if it doesn't exist
		Equipment* getEquipmentByName(char* name);

		// Returns a pointer to the Equipment corresponding to the given index
		// Returns NULL if the index is out of bounds
		Equipment* getEquipmentByIndex(int index);

		// Returns the number of Equipments in the vector
		int getNumberEquipments();
	private:
		// The list of all the Equipments contained by this Room
		std::vector<Equipment*> m_listEquipments;
	};

	extern "C" __declspec(dllexport) Room* Room_New(char* name, char* ico);
	extern "C" __declspec(dllexport) void Room_Delete(Room* room);
}

#endif ROOM_H_INCLUDED