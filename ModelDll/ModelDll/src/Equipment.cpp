#include "..\include\Equipment.h"
#include "..\include\RequeteHttp.h"
#include <iostream>


#ifdef WIN32
#include <winsock2.h>
#endif // WIN32


using namespace std;

namespace EP {

	// STATIC METHODS

	int Equipment::setIpKira(char* new_ip){
		int a = 0, b = 0, c = 0, d = 0;
		int success = sscanf(new_ip, "%i.%i.%i.%i", &a, &b, &c, &d);
		if (success == 4 && a >= 0 && b >= 0 && c >= 0 && d >= 0 && a<255 && b<255 && c<255 && d<255) {
			success = 1;
			strcpy(IP_Kira, new_ip);
		}
		else {
			success = 0;
		}
		return success;
	}

	int Equipment::setIpFibaro(char* new_ip) {
		int a = 0, b = 0, c = 0, d = 0;
		int success = sscanf(new_ip, "%i.%i.%i.%i", &a, &b, &c, &d);

		if (success == 4 && a >= 0 && b >= 0 && c >= 0 && d >= 0 && a<255 && b<255 && c<255 && d<255) {
			success = 1;
			strcpy(IP_Fibaro, new_ip);
		}
		else {
			success = 0;
		}

		return success;
	}

	int Equipment::setLoginFibaro(char* new_login) {
		if(new_login[0] != '\0')
			strcpy(Fibaro_login, new_login);
		return 0;
	}

	int Equipment::setPasswordFibaro(char* new_password) {
		if (new_password[0] != '\0')
			strcpy(Fibaro_password, new_password);
		return 0;
	}

	char Equipment::IP_Kira[15] = "192.168.1.31";
	char Equipment::IP_Fibaro[15] = "192.168.81.1";
	char Equipment::Fibaro_login[300] = "admin";
	char Equipment::Fibaro_password[300] = "admin";

	// KIRA

	EquipmentKira::EquipmentKira(char* name, char* ico, Node* parent, int buttonId, int page) : Equipment(name, ico, parent, 1), m_buttonId(buttonId), m_pageNumber(page) {
		// Nothing to do
	}

	EquipmentKira::~EquipmentKira() {
		printf("EquipmentKira detruit\n");
	}

	int EquipmentKira::sendRequest() {
		char* s1(getIpKira()); //domaine, premiere partie de l'adree
		char s2[500];

		// deuxieme partie de l'adresse
		if (m_buttonId < 10) {
			sprintf(s2, "/remote%d.htm?button00%d#", m_pageNumber, m_buttonId);
		}
		else if (m_buttonId < 100) {
			sprintf(s2, "/remote%d.htm?button0%d#", m_pageNumber, m_buttonId);
		}
		else {
			sprintf(s2, "/remote%d.htm?button%d#", m_pageNumber, m_buttonId);
		}

		cerr << s1 << " ; " << s2 << endl;

		requeteHttpKira(s1, s2);
		return 0;
	}

	int EquipmentKira::getButtonId() {
		return m_buttonId;
	}

	int EquipmentKira::getPageNumber() {
		return m_pageNumber;
	}

	int EquipmentKira::setButtonId(int new_id) {
		m_buttonId = new_id;
		return 0;
	}

	int EquipmentKira::setPageNumber(int new_pageNumber){
		m_pageNumber = new_pageNumber;
		return 0;
	}

	extern "C" __declspec(dllexport) EquipmentKira* EquipmentKira_New(char* name, char* ico, Node* parent, int buttonId, int page) {
		return new EquipmentKira(name, ico, parent, buttonId, page);
	}
	extern "C" __declspec(dllexport) void EquipmentKira_Delete(EquipmentKira* eq) {
		delete eq;
	}

	// FIBARO

	EquipmentFibaro::EquipmentFibaro(char* name, char* ico, Node* parent, int equipmentId, char* action) : Equipment(name, ico, parent, 2), m_equipmentId(equipmentId) {
		strcpy(m_action, action);
	}

	EquipmentFibaro::~EquipmentFibaro() {
		// Nothing to do
	}

	int EquipmentFibaro::sendRequest() {
		char s6 = '&'; //impossible de le mettre directement dans la ligne suivante, je ne sais pas pourquoi
		char action[500];

		sprintf(action, "/api/callAction?deviceID=%d&name=%s", m_equipmentId, m_action);

		cerr << getIpFibaro() << " ; " << action << " ; " << Fibaro_login << " ; " << Fibaro_password << endl;

		requeteHttpFibaro(getIpFibaro(), action, Fibaro_login, Fibaro_password);

		return 0;
	}

	int EquipmentFibaro::getEquipmentId() {
		return m_equipmentId;
	}

	char* EquipmentFibaro::getAction() {
		return m_action;
	}

	extern "C" __declspec(dllexport) EquipmentFibaro* EquipmentFibaro_New(char* name, char* ico, Node* parent, int equipmentId, char* action) {
		return new EquipmentFibaro(name, ico, parent, equipmentId, action);
	}
	extern "C" __declspec(dllexport) void EquipmentFibaro_Delete(EquipmentFibaro* eq) {
		delete eq;
	}
	extern "C" __declspec(dllexport) int Equipment_setIpKira(char* new_ip) {
		return Equipment::setIpKira(new_ip);
	}
	extern "C" __declspec(dllexport) int Equipment_setIpFibaro(char* new_ip) {
		return Equipment::setIpFibaro(new_ip);
	}
	extern "C" __declspec(dllexport) int Equipment_setLoginFibaro(char* new_login) {
		return Equipment::setLoginFibaro(new_login);
	}
	extern "C" __declspec(dllexport) int Equipment_setPasswordFibaro(char* new_password) {
		return Equipment::setPasswordFibaro(new_password);
	}
	extern "C" __declspec(dllexport) char* Equipment_getPasswordFibaro() {
		return Equipment::getPasswordFibaro();
	}
	extern "C" __declspec(dllexport) char* Equipment_getLoginFibaro() {
		return Equipment::getLoginFibaro();
	}
	extern "C" __declspec(dllexport) char* Equipment_getIpFibaro() {
		return Equipment::getIpFibaro();
	}
	extern "C" __declspec(dllexport) char* Equipment_getIpKira() {
		return Equipment::getIpKira();
	}
}

