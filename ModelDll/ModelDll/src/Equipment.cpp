#include "..\include\Equipment.h"

#include <iostream>


#ifdef WIN32
#include <winsock2.h>
#endif // WIN32


using namespace std;

namespace EP {

	// STATIC METHODS

	int Equipment::setIpKira(char* new_ip){
		int a = 0, b = 0, c = 0, d = 0;
		int success = sscanf_s(new_ip, "%i.%i.%i.%i", &a, &b, &c, &d);
		if (success == 4 && a >= 0 && b >= 0 && c >= 0 && d >= 0 && a<255 && b<255 && c<255 && d<255) {
			success = 1;
			strcpy_s(IP_Kira, new_ip);
		}
		else {
			success = 0;
		}
		return success;
	}

	int Equipment::setIpFibaro(char* new_ip) {
		int a = 0, b = 0, c = 0, d = 0;
		int success = sscanf_s(new_ip, "%i.%i.%i.%i", &a, &b, &c, &d);

		if (success == 4 && a >= 0 && b >= 0 && c >= 0 && d >= 0 && a<255 && b<255 && c<255 && d<255) {
			success = 1;
			strcpy_s(IP_Fibaro, new_ip);
		}
		else {
			success = 0;
		}

		return success;
	}

	int Equipment::setLoginFibaro(char* new_login) {
		strcpy_s(Fibaro_login, new_login);
		return 0;
	}

	int Equipment::setPasswordFibaro(char* new_password) {
		strcpy_s(Fibaro_password, new_password);
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
		/*string s1(getIpKira()); //domaine, premiere partie de l'adree
		string s2("/remote" + m_pageNumber);
		string s3 = ".htm?button"; //impossible de le mettre directement dans la ligne suivante, je ne sais pas pourquoi

		s2 += s3 + std::to_string(m_buttonId) + "#";     // deuxieme partie de l'adresse  + 
		void requeteHttp(string s1, string s2);*/
		return 0;
	}

	int EquipmentKira::getButtonId() {
		return m_buttonId;
	}

	int EquipmentKira::setPagenumber(int new_pageNumber){
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
		strcpy_s(m_action, action);
	}

	EquipmentFibaro::~EquipmentFibaro() {
		// Nothing to do
	}

	int EquipmentFibaro::sendRequest() {
		// simple simple GET
		/*happyhttp::Connection conn(getIpFibaro(), 80);
		conn.setcallbacks(OnBegin, OnData, OnComplete, 0);
		string user = Fibaro_login;
		string pass = Fibaro_password;
		const string s = (user + ":" + pass);

		string encoded = "Basic " + base64_encode(reinterpret_cast<const unsigned char*>(s.c_str()), s.length());
		const char *auth = encoded.c_str();
		const char* headers[] =
		{
			"Authorization",auth,
			"Connection", "close",
			0
		};
		string s6 = "&";//impossible de le mettre directement dans la ligne suivante, je ne sais pas pourquoi
		string action = ("/api/callAction?deviceID=" + m_equipmentId + s6 + std::to_string(m_action[300]));
		conn.request("GET", action.c_str(), headers, 0, 0);

		while (conn.outstanding())
			conn.pump();*/
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
}

