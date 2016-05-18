#include "../include/Core.h"

#include <fstream>
#include <string>
#include <iostream>

using namespace std;

namespace EP {
	Core::Core(char* file) : m_iconSize(2), m_themeId(1)	{
		strcpy(m_coreSave, file);
	}

	Core::~Core() {
		m_listRooms.clear();
	}

	int Core::save() {
		ofstream file(m_coreSave, ofstream::trunc);

		if (file) {
			// Core attributes
			file << m_themeId << "," << m_iconSize << "," << getNumberRooms() << "\n";

			file << Equipment::getIpKira() << "," << Equipment::getIpFibaro() << "," << Equipment::getLoginFibaro() << "," << Equipment::getPasswordFibaro() << "\n";

			// Tmp vars
			int i, j;
			Room* room;
			Equipment* eq;

			
			for (i = 0; i < getNumberRooms(); i++) {
				room = m_listRooms[i];

				// Room's attributes
				file << room->getName() << "," << room->getIco() << "," << room->getNumberEquipments() << "\n";

				// For each of its equipments
				for (j = 0; j < room->getNumberEquipments(); j++) {
					eq = room->getEquipmentByIndex(j);

					file << eq->getName() << "," << eq->getIco() << "," << eq->getTypeOf() << ",";
					if (eq->getTypeOf() == 1) {
						file << ((EquipmentKira*)eq)->getButtonId() << "," << ((EquipmentKira*)eq)->getPageNumber();
					}
					else if (eq->getTypeOf() == 2) {
						file << ((EquipmentFibaro*)eq)->getEquipmentId() << "," << ((EquipmentFibaro*)eq)->getAction();
					}

					file << "\n";
				}
			}
			file.close();
		}

		return 0;
	}

	int Core::load() {
		ifstream file(m_coreSave, ifstream::in);
		if (file) {
			int nbRooms, nbEquip; // tmp vars
			Room* room;
			Equipment* eq;
			int i, j; // loop variables

			char tmp[300];
			char ipKira[300], ipFibaro[300], login[300], passwd[300];
			char roomName[100], roomIco[100];
			char eqName[100], eqIco[100], eqAction[300];
			int eqTypeOf, eqId, eqKiraPage;

			// file >> m_themeId >> m_iconSize >> nbRooms;
			file.getline(tmp, 100, ',');
			m_themeId = strtol(tmp, NULL, 10);

			file.getline(tmp, 100, ',');
			m_iconSize = strtol(tmp, NULL, 10);

			file.getline(tmp, 100);
			nbRooms = strtol(tmp, NULL, 10);

			// file >> ip kira >> ip fibaro >> login fibaro >> login mdp

			file.getline(ipKira, 100, ',');
			Equipment::setIpKira(ipKira);

			file.getline(ipFibaro, 100, ',');
			Equipment::setIpFibaro(ipFibaro);

			file.getline(login, 100, ',');
			Equipment::setLoginFibaro(login);

			file.getline(passwd, 100);
			Equipment::setPasswordFibaro(passwd);

			for (i = 0; i < nbRooms; i++) {
				//file >> roomName >> roomIco >> nbEquip;

				roomName[0] = '\0';
				roomIco[0] = '\0';
				tmp[0] = '\0';

				file.getline(roomName, 100, ',');
				file.getline(roomIco, 100, ',');
				file.getline(tmp, 100, '\n');
				nbEquip = strtol(tmp, NULL, 10);

				room = new Room(roomName, roomIco);

				for (j = 0; j < nbEquip; j++) {
					//file >> eqName >> eqIco >> eqTypeOf;
					eqName[0] = '\0';
					eqIco[0] = '\0';
					eqAction[0] = '\0';
					tmp[0] = '\0';

					file.getline(eqName, 100, ',');
					file.getline(eqIco, 100, ',');
					file.getline(tmp, 100, ',');
					eqTypeOf = strtol(tmp, NULL, 10);

					if (eqTypeOf == 1) {
						//file >> eqId >> eqKiraPage;

						file.getline(tmp, 100, ',');
						eqId = strtol(tmp, NULL, 10);
						file.getline(tmp, 100);
						eqKiraPage = strtol(tmp, NULL, 10);

						eq = new EquipmentKira(eqName, eqIco, room, eqId, eqKiraPage);

						room->addEquipment(eq);
					}
					else if (eqTypeOf == 2) {
						//file >> eqId >> eqAction;

						file.getline(tmp, 100, ',');
						eqId = strtol(tmp, NULL, 10);
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
		return 0;
	}

	int Core::deleteRoomByIndex(int index) {
		if (index >= ((int)m_listRooms.size()) || index < 0) return 1;

		m_listRooms.erase(m_listRooms.begin()+index);

		return 0;
	}

	int Core::deleteRoomByName(char* name) {
		if (getRoomByName(name) == NULL) return 1; // does not exist

		vector<Room*>::iterator it;
		for (it = m_listRooms.begin(); it != m_listRooms.end(); it++) {
			if (strcmp(name, (*it)->getName()) == 0) break;
		}

		m_listRooms.erase(it);

		return 0;
	}

	vector<Room*>* Core::getRooms() {
		return &m_listRooms;
	}

	Room* Core::getRoomByName(char* name) {
		vector<Room*>::iterator it;
		for (it = m_listRooms.begin(); it != m_listRooms.end(); it++) {
			if (strcmp(name, (*it)->getName()) == 0) {
				return (*it);
			}
		}
		return NULL;
	}

	Room* Core::getRoomByIndex(int index) {
		if (index < ((int)m_listRooms.size()) && index >= 0) return m_listRooms[index];
		else return NULL;
	}

	char* Core::getFileSave() {
		return m_coreSave;
	}

	int Core::getNumberRooms() {
		return m_listRooms.size();
	}

	int Core::getThemeId() {
		return m_themeId;
	}

	int Core::getIconSize() {
		return m_iconSize;
	}

	void Core::setThemeId(int id) {
		m_themeId = id;
	}

	void Core::setIconSize(int size) {
		m_iconSize = size;
	}

	extern "C" __declspec(dllexport) Core* Core_New(char* file) {
		return new Core(file);
	}

	extern "C" __declspec(dllexport) Core* Core_NewFromSave(char* file) {
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