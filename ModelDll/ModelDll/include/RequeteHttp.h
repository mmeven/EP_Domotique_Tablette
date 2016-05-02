#pragma once
#define DllImport  __declspec(dllimport)

namespace EP {
	extern "C" {
		void DllImport requeteHttpKira(char* s1, char* s2);
		void DllImport requeteHttpFibaro(char* s1, char* s2, char* user, char* pass);
	}
}


