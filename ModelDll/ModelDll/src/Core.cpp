#include "../include/Core.h"

#include <fstream>
#include <string>
#include <iostream>

using namespace std;

namespace EP {
	Core::Core(wchar_t* file) : m_currentRoom(0), m_iconSize(3), m_themeId(1)	{
		wcscpy_s(m_coreSave, file);
	}

	Core::~Core() {
		m_listRooms.clear();
	}

	int Core::save() {
		wofstream file(m_coreSave, wofstream::trunc);

		if (file) {
			// Core attributes
			file << m_themeId << "," << m_iconSize << "," << getNumberRooms() << endl;

			// Tmp vars
			int i, j;
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
		}

		return 0;
	}

	int Core::load() {
		wifstream file(m_coreSave, wifstream::in);
		if (file) {
			int nbRooms, nbEquip; // tmp vars
			Room* room;
			Equipment* eq;
			int i, j; // loop variables

			wchar_t tmp[300];
			wchar_t roomName[100], roomIco[100];
			wchar_t eqName[100], eqIco[100], eqAction[300];
			int eqTypeOf, eqId;

			// file >> m_themeId >> m_iconSize >> nbRooms;
			file.getline(tmp, 100, ',');
			m_themeId = wcstol(tmp, NULL, 10);

			file.getline(tmp, 100, ',');
			m_iconSize = wcstol(tmp, NULL, 10);

			file.getline(tmp, 100);
			nbRooms = wcstol(tmp, NULL, 10);

			for (i = 0; i < nbRooms; i++) {
				//file >> roomName >> roomIco >> nbEquip;
				file.getline(roomName, 100, ',');
				file.getline(roomIco, 100, ',');
				file.getline(tmp, 100);
				nbEquip = wcstol(tmp, NULL, 10);

				room = new Room(roomName, roomIco);

				for (j = 0; j < nbEquip; j++) {
					//file >> eqName >> eqIco >> eqTypeOf;

					file.getline(eqName, 100, ',');
					file.getline(eqIco, 100, ',');
					file.getline(tmp, 100, ',');
					eqTypeOf = wcstol(tmp, NULL, 10);

					if (eqTypeOf == 1) {
						//file >> eqId;

						file.getline(tmp, 100);
						eqId = wcstol(tmp, NULL, 10);

						eq = new EquipmentKira(eqName, eqIco, room, eqId);

						room->addEquipment(eq);
					}
					else if (eqTypeOf == 2) {
						//file >> eqId >> eqAction;

						file.getline(tmp, 100, ',');
						eqId = wcstol(tmp, NULL, 10);
						file.getline(eqAction, 100);

						eq = new EquipmentFibaro(eqName, eqIco, room, eqId, eqAction);

						room->addEquipment(eq);
					}
				}
				
				addRoom(room);
			}

			file.close();
		}
		return 0;
	}

	int Core::addRoom(Room* room) {
		
		m_listRooms.push_back(room);
		vector<Room*>::iterator it = m_listRooms.end();
		/*cout << "Ajout d'une piece de nom : ";
		wcout << (*it)->getName() << endl;*/
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
		wcout << name << endl;
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

	void Core::setCurrentRoom(Room* room) {
		m_currentRoom = room;
	}

	void Core::setThemeId(int id) {
		m_themeId = id;
	}

	void Core::setIconSize(int size) {
		m_iconSize = size;
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