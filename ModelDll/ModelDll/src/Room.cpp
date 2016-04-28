#include "../include/Room.h"

using namespace std;

namespace EP {
	Room::Room(char* name, char* ico) : Node(name, ico)
	{}

	Room::~Room() {
		m_listEquipments.clear();
	}

	int Room::addEquipment(Equipment* equip) {
		m_listEquipments.push_back(equip);
		return 0;
	}

	int Room::deleteEquipmentByIndex(int index) {
		if (index >= ((int)m_listEquipments.size()) || index < 0) return 1; // out of bounds

		vector<Equipment*>::iterator it = m_listEquipments.begin();
		it += index;

		m_listEquipments.erase(it);

		return 0;
	}

	int Room::deleteEquipmentByName(char* name) {
		if (Room::getEquipmentByName(name) == NULL) return 1; // does not exist

		vector<Equipment*>::iterator it;
		for (it = m_listEquipments.begin(); it != m_listEquipments.end(); it++) {
			if (strcmp(name, (*it)->getName()) == 0) break;
		}

		m_listEquipments.erase(it);

		return 0;
	}

	vector<Equipment*>* Room::getEquipments() {
		return &m_listEquipments;
	}

	Equipment* Room::getEquipmentByName(char* name) {
		vector<Equipment*>::iterator it;
		for (it = m_listEquipments.begin(); it != m_listEquipments.end(); it++) {
			if (strcmp(name, (*it)->getName()) == 0) {
				return (*it);
			}
		}
		return NULL;
	}

	Equipment* Room::getEquipmentByIndex(int index) {
		if (index < ((int)m_listEquipments.size()) && index >= 0) return m_listEquipments[index];
		else return NULL;
	}

	int Room::getNumberEquipments() {
		return m_listEquipments.size();
	}

	extern "C" __declspec(dllexport) Room* Room_New(char* name, char* ico) {
		return new Room(name, ico);
	}

	extern "C" __declspec(dllexport) void Room_Delete(Room* room) {
		delete room;
	}
}
