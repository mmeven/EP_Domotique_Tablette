#include "..\include\Equipment.h"
#include <iostream>
namespace EP {

	EquipmentKira::EquipmentKira(wchar_t* name, wchar_t* ico, Node* parent, int buttonId) : Equipment(name, ico, parent, 1), m_buttonId(buttonId) {
		// Nothing to do
	}

	EquipmentKira::~EquipmentKira() {
		printf("EquipmentKira detruit\n");
	}

	int EquipmentKira::sendRequest() {
		// Code de la requete HTTP
		return 0;
	}

	int EquipmentKira::getButtonId() {
		return m_buttonId;
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
		// Code de la requete HTTP
		return 0;
	}

	int EquipmentFibaro::getEquipmentId() {
		return m_equipmentId;
	}

	wchar_t* EquipmentFibaro::getAction() {
		return m_action;
	}

	extern "C" __declspec(dllexport) EquipmentFibaro* EquipmentFibaro_New(wchar_t* name, wchar_t* ico, Node* parent, int equipmentId, wchar_t* action) {
		return new EquipmentFibaro(name, ico, parent, equipmentId, action);
	}
	extern "C" __declspec(dllexport) void EquipmentFibaro_Delete(EquipmentFibaro* eq) {
		delete eq;
	}
}

