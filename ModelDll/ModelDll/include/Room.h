#ifndef ROOM_H_INCLUDED
#define ROOM_H_INCLUDED

#include <vector>
#include "Equipment.h"

namespace EP {
	// Represents a node of the tree used to modelize the rooms
	// and their equiments
	class Room : public Node {
	public:
		// See Node.h
		Room(std::string name, std::string ico, Node * parent);

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
		int deleteEquipmentByName(std::string name);

		// Returns a pointer of the first element of an array containing all Equipments
		// Use this in a C# code
		Equipment* getEquipmentsAsArray();

		// Returns the vector containing all the Equipments of the Room
		std::vector<Equipment*>* getEquipments();

		// Returns a pointer to the Equipment corresponding to the given name
		// Returns NULL if it doesn't exist
		Equipment* getEquipmentByName(std::string name);

		// Returns a pointer to the Equipment corresponding to the given index
		// Returns NULL if the index is out of bounds
		Equipment* getEquipmentByIndex(int index);

		// Returns the number of Equipments in the vector
		int getNumberEquipments();
	private:
		// The list of all the Equipments contained by this Room
		std::vector<Equipment*> m_listEquipments;
	};
}

#endif ROOM_H_INCLUDED