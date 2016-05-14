#pragma once
#define DllImport  __declspec(dllimport)

namespace EP {
	extern "C" {
		/// <summary>Cette m�thode permet d'envoyer une requ�te HTTP pour un �quipement
		/// li� � la Kira. Cette m�thode est un import de la DLL RequeteHttp.dll et est utilis�e
		/// uniquement par la m�thode EquipmentKira::sendRequest() ; les param�tres sont construits
		/// automatiquement � partir des informations du l'�quipement.</summary>
		/// <param name="s1">L'adresse IP de la Kira.</param>
		/// <param name="s2">La deuxi�me partie de l'adresse correspondant � la requ�te HTTP.</param>
		void DllImport requeteHttpKira(char* s1, char* s2);

		/// <summary>Cette m�thode permet d'envoyer une requ�te HTTP pour un �quipement
		/// li� � la Fibaro. Cette m�thode est un import de la DLL RequeteHttp.dll et est utilis�e
		/// uniquement par la m�thode EquipmentFibaro::sendRequest() ; les param�tres sont construits
		/// automatiquement � partir des informations du l'�quipement.</summary>
		/// <param name="s1">L'adresse IP de la Fibaro.</param>
		/// <param name="s2">La deuxi�me partie de l'adresse correspondant � la requ�te HTTP.</param>
		/// <param name="user">Le login utilis� pour s'authentifier aupr�s de la Fibaro.</param>
		/// <param name="pass">Le login utilis� pour s'authentifier aupr�s de la Fibaro.</param>
		void DllImport requeteHttpFibaro(char* s1, char* s2, char* user, char* pass);
	}
}


