#include<string>
#include "RequeteHttp.h"
#include "happyhttp.h"
#include "Requete.h"
#include "base64.h"
#define DllExport   __declspec(dllexport)
#ifdef WIN32
#include <winsock2.h>
#endif // WIN32



void DllExport requeteHttpKira(char* s1, char* s2)
{

#ifdef WIN32
    WSAData wsaData;
    int code = WSAStartup(MAKEWORD(1, 1), &wsaData);
    if( code != 0 )
    {
        fprintf(stderr, "shite. %d\n",code);
        return ;
    }
#endif //WIN32
    try
    {
        Requete requete1;
        requete1.envoyerRequete(s1,s2);
    }

    catch( happyhttp::Wobbly& e )
    {
        fprintf(stderr, "Exception:\n%s\n", e.what() );
    }
#ifdef WIN32
    WSACleanup();
#endif // WIN32
    return;
}


void  DllExport requeteHttpFibaro(char* s1, char* s2,char* user, char* pass)
{


#ifdef WIN32
    WSAData wsaData;
    int code = WSAStartup(MAKEWORD(1, 1), &wsaData);
    if( code != 0 )
    {
        fprintf(stderr, "shite. %d\n",code);
        return ;
    }
#endif //WIN32
    try
    {

        // simple simple GET
        happyhttp::Connection conn(s1, 80);
        std::string u(user);
        std::string p(pass);
        const std::string s = (u + ":" + p);

        std::string encoded = "Basic " + base64_encode(reinterpret_cast<const unsigned char*>(s.c_str()), s.length());
        const char *auth = encoded.c_str();
        const char* headers[] =
        {
            "Authorization",auth,
            "Connection", "close",
            0
        };

        conn.request("GET", s2, headers, 0, 0);

        while (conn.outstanding())
            conn.pump();
    }
    catch( happyhttp::Wobbly& e )
    {
        fprintf(stderr, "Exception:\n%s\n", e.what() );
    }
#ifdef WIN32
    WSACleanup();
#endif // WIN32
    return;
}


