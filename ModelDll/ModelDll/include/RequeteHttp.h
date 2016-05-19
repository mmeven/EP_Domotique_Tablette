#pragma once
#define DllImport  __declspec(dllimport)

namespace EP {
	extern "C" {
		/// \brief Cette méthode permet d'envoyer une requête HTTP pour un équipement
		/// lié à la Kira. Cette méthode est un import de la DLL RequeteHttp.dll et est utilisée
		/// uniquement par la méthode EquipmentKira::sendRequest() ; les paramètres sont construits
		/// automatiquement à partir des informations du l'équipement.
		/// \param s1 L'adresse IP de la Kira.
		/// \param s2 La deuxième partie de l'adresse correspondant à la requête HTTP.
		void DllImport requeteHttpKira(char* s1, char* s2);

		/// \brief Cette méthode permet d'envoyer une requête HTTP pour un équipement
		/// lié à la Fibaro. Cette méthode est un import de la DLL RequeteHttp.dll et est utilisée
		/// uniquement par la méthode EquipmentFibaro::sendRequest() ; les paramètres sont construits
		/// automatiquement à partir des informations du l'équipement.
		/// \param s1 L'adresse IP de la Fibaro.
		/// \param s2 La deuxième partie de l'adresse correspondant à la requête HTTP.
		/// \param user Le login utilisé pour s'authentifier auprès de la Fibaro.
		/// \param pass Le login utilisé pour s'authentifier auprès de la Fibaro.
		void DllImport requeteHttpFibaro(char* s1, char* s2, char* user, char* pass);
	}
}


