#include "..\include\Equipment.h"
#include "..\include\Requete.h"
#include "..\include\base64.h"
#include "..\include\happyhttp.h"
#include <iostream>
#include <string>

#ifdef WIN32
#include <winsock2.h>
#endif // WIN32

namespace EP {

	int Equipment::setIp(std::string new_ip){

		const char *cstr = new_ip.c_str();
		int a = 0, b = 0, c = 0, d = 0;
		int success = sscanf(cstr, "%i.%i.%i.%i", &a, &b, &c, &d);
		if (success == 4 && a >= 0 && b >= 0 && c >= 0 && d >= 0 && a<255 && b<255 && c<255 && d<255) {
			success = 1;
			m_ip = new_ip;
		}
		else {
			success = 0;
		}
		return success;

		}




	EquipmentKira::EquipmentKira(wchar_t* name, wchar_t* ico, Node* parent, int buttonId) : Equipment(name, ico, parent, 1), m_buttonId(buttonId) {
		// Nothing to do
	}

	EquipmentKira::~EquipmentKira() {
		printf("EquipmentKira detruit\n");
	}

	int EquipmentKira::sendRequest() {
		std::string s1(getIp()); //domaine, premiere partie de l'adree
		std::string s2;
		std::string s3 = ".htm?button"; //impossible de le mettre directement dans la ligne suivante, je ne sais pas pourquoi
		s2=("/remote" + m_pageNumber  + s3 + std::to_string(m_buttonId) + "#");          //deuxieme partie de l'adresse
		void requeteHttp(std::string s1, std::string s2);
		return 0;
	}

	int EquipmentKira::getButtonId() {
		return m_buttonId;
	}

	int EquipmentKira::setPagenumber(int new_pageNumber){
		m_pageNumber = new_pageNumber;
	}

	extern "C" __declspec(dllexport) EquipmentKira* EquipmentKira_New(wchar_t* name, wchar_t* ico, Node* parent, int buttonId) {
		return new EquipmentKira(name, ico, parent, buttonId);
	}
	extern "C" __declspec(dllexport) void EquipmentKira_Delete(EquipmentKira* eq) {
		delete eq;
	}

	EquipmentFibaro::EquipmentFibaro(wchar_t* name, wchar_t* ico, Node* parent, int equipmentId, wchar_t* action) : Equipment(name, ico, parent, 2), m_equipmentId(equipmentId) {
		wcscpy_s(m_action, action);
	}

	EquipmentFibaro::~EquipmentFibaro() {
		// Nothing to do
	}

	int EquipmentFibaro::sendRequest() {
		// simple simple GET
		happyhttp::Connection conn(getIp().c_str(), 80);
		conn.setcallbacks(OnBegin, OnData, OnComplete, 0);
		std::string user = m_login;
		std::string pass = m_password;
		const std::string s = (user + ":" + pass);

		std::string encoded = "Basic " + base64_encode(reinterpret_cast<const unsigned char*>(s.c_str()), s.length());
		const char *auth = encoded.c_str();
		const char* headers[] =
		{
			"Authorization",auth,
			"Connection", "close",
			0
		};
		std::string s6 = "&";//impossible de le mettre directement dans la ligne suivante, je ne sais pas pourquoi
		std::string action = ("/api/callAction?deviceID=" + m_equipmentId + s6 + std::to_string(m_action[300]));
		conn.request("GET", action.c_str(), headers, 0, 0);

		while (conn.outstanding())
			conn.pump();
		return 0;
	}

	int EquipmentFibaro::getEquipmentId() {
		return m_equipmentId;
	}

	wchar_t* EquipmentFibaro::getAction() {
		return m_action;
	}

	int EquipmentFibaro::setLogin(std::string new_login) {
		m_login = new_login;
	}

	int EquipmentFibaro::setPassword(std::string new_password) {
		m_password = new_password;
	}

	extern "C" __declspec(dllexport) EquipmentFibaro* EquipmentFibaro_New(wchar_t* name, wchar_t* ico, Node* parent, int equipmentId, wchar_t* action) {
		return new EquipmentFibaro(name, ico, parent, equipmentId, action);
	}
	extern "C" __declspec(dllexport) void EquipmentFibaro_Delete(EquipmentFibaro* eq) {
		delete eq;
	}
}

