#include "happyhttp.h"
#include "include/Requete.h"
#include <cstdio>
#include <cstring>

#ifdef WIN32
#include <winsock2.h>
#endif // WIN32



int main( int argc, char* argv[] )
{
#ifdef WIN32
    WSAData wsaData;
    int code = WSAStartup(MAKEWORD(1, 1), &wsaData);
	if( code != 0 )
	{
		fprintf(stderr, "shite. %d\n",code);
		return 0;
	}
#endif //WIN32


	try
	{
        Requete requete1;
		std::string s1("httpbin.org"); //domaine, premiere partie de l'adree
        std::string s2("/ip");          //deuxieme partie de l'adresse
		requete1.envoyerRequete(s1,s2);
	}

	catch( happyhttp::Wobbly& e )
	{
		fprintf(stderr, "Exception:\n%s\n", e.what() );
	}



#ifdef WIN32
    WSACleanup();
#endif // WIN32

	return 0;
}


