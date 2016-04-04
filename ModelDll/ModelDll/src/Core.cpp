#include "../include/Core.h"

#include <fstream>

using namespace std;

namespace EP {
	Core::Core(wchar_t* file) : m_currentRoom(0), m_iconSize(0), m_themeId(0)
	{
		wcscpy_s(m_coreSave, file);
		//load();
	}

	Core::~Core() {
		//save();
		m_listRooms.clear();
	}

	int Core::save() {
		wofstream file(m_coreSave, wofstream::out);

		// Core attributes
		file << m_themeId << "," << m_iconSize << "," << getNumberRooms() << endl;

		// Tmp vars
		int i,j;
		Room* room;
		Equipment* eq;


		for (i = 0; i < getNumberRooms(); i++) {
			room = m_listRooms[i];
			
			// Room's attributes
			file << room->getName() << "," << room->getIco() << "," << room->getNumberEquipments() << endl;

			// For each of its equipments
			for (j = 0; j < room->getNumberEquipments(); j++) {
				eq = room->getEquipmentByIndex(i);

				file << eq->getName() << "," << eq->getIco() << "," << eq->getTypeOf() << ",";
				if (eq->getTypeOf() == 1) {
					file << ((EquipmentKira*)eq)->getButtonId();
				}
				else if (eq->getTypeOf() == 2) {
					file << ((EquipmentFibaro*)eq)->getEquipmentId() << "," << ((EquipmentFibaro*)eq)->getAction();
				}

				file << endl;
			}
		}
		file.close();
		return 0;
	}

	int Core::load() {
		return 0;
	}

	int Core::addRoom(Room* room) {
		m_listRooms.push_back(room);
		return 0;
	}

	int Core::deleteRoomByIndex(int index) {
		if (index < ((int)m_listRooms.size()) && index >= 0) return 1;

		vector<Room*>::iterator it = m_listRooms.begin();
		it += index;
		m_listRooms.erase(it);

		return 0;
	}

	int Core::deleteRoomByName(wchar_t* name) {
		if (getRoomByName(name) == NULL) return 1; // does not exist

		vector<Room*>::iterator it;
		for (it = m_listRooms.begin(); it != m_listRooms.end(); it++) {
			if (name == (*it)->getName()) break;
		}

		m_listRooms.erase(it);

		return 0;
	}

	vector<Room*>* Core::getRooms() {
		return &m_listRooms;
	}

	Room* Core::getRoomByName(wchar_t* name) {
		vector<Room*>::iterator it;
		for (it = m_listRooms.begin(); it != m_listRooms.end(); it++) {
			if (name == (*it)->getName()) {
				return (*it);
			}
		}
		return NULL;
	}

	Room* Core::getRoomByIndex(int index) {
		if (index < ((int)m_listRooms.size()) && index >= 0) return m_listRooms[index];
		else return NULL;
	}

	wchar_t* Core::getFileSave() {
		return m_coreSave;
	}

	int Core::getNumberRooms() {
		return m_listRooms.size();
	}

	Room* Core::getCurrentRoom() {
		return m_currentRoom;
	}

	int Core::getThemeId() {
		return m_themeId;
	}

	int Core::getIconSize() {
		return m_iconSize;
	}

	extern "C" __declspec(dllexport) Core* Core_New(wchar_t* file) {
		return new Core(file);
	}

	extern "C" __declspec(dllexport) Core* Core_NewFromSave(wchar_t* file) {
		Core* core = new Core(file);

		core->load();

		return core;
	}

	extern "C" __declspec(dllexport) void Core_Delete(Core* core) {
		delete core;
	}

	extern "C" __declspec(dllexport) void Core_SaveAndDelete(Core* core) {
		core->save();
		delete core;
	}
}