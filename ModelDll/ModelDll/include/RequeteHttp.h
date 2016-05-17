#pragma once
#define DllImport  __declspec(dllimport)

namespace EP {
	extern "C" {
		/// <summary>Cette méthode permet d'envoyer une requête HTTP pour un équipement
		/// lié à la Kira. Cette méthode est un import de la DLL RequeteHttp.dll et est utilisée
		/// uniquement par la méthode EquipmentKira::sendRequest() ; les paramètres sont construits
		/// automatiquement à partir des informations du l'équipement.</summary>
		/// <param name="s1">L'adresse IP de la Kira.</param>
		/// <param name="s2">La deuxième partie de l'adresse correspondant à la requête HTTP.</param>
		void DllImport requeteHttpKira(char* s1, char* s2);

		/// <summary>Cette méthode permet d'envoyer une requête HTTP pour un équipement
		/// lié à la Fibaro. Cette méthode est un import de la DLL RequeteHttp.dll et est utilisée
		/// uniquement par la méthode EquipmentFibaro::sendRequest() ; les paramètres sont construits
		/// automatiquement à partir des informations du l'équipement.</summary>
		/// <param name="s1">L'adresse IP de la Fibaro.</param>
		/// <param name="s2">La deuxième partie de l'adresse correspondant à la requête HTTP.</param>
		/// <param name="user">Le login utilisé pour s'authentifier auprès de la Fibaro.</param>
		/// <param name="pass">Le login utilisé pour s'authentifier auprès de la Fibaro.</param>
		void DllImport requeteHttpFibaro(char* s1, char* s2, char* user, char* pass);
	}
}


