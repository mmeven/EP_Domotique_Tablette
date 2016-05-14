#include "Requete.h"
#include "happyhttp.h"
#include <cstdio>
#include <cstring>

#ifdef WIN32
#include <winsock2.h>
#endif // WIN32


int count=0;

void OnBegin( const happyhttp::Response* r, void* userdata )
{
	printf( "BEGIN (%d %s)\n", r->getstatus(), r->getreason() );
	count = 0;
}

void OnData( const happyhttp::Response* r, void* userdata, const unsigned char* data, int n )
{
	fwrite( data,1,n, stdout );
	count += n;
}

void OnComplete( const happyhttp::Response* r, void* userdata )
{
	printf( "COMPLETE (%d bytes)\n", count );
}


Requete::Requete(char* s1)
{
    m_adresse=s1;
}
Requete::Requete()
{
}
Requete::~Requete()
{
    //dtor
}


int Requete::envoyerRequete(char* s1, char* s2)
{
		puts("-----------------Test1------------------------" );
	// simple simple GET
	happyhttp::Connection conn( (const char*) s1, 80 );
	conn.setcallbacks( OnBegin, OnData, OnComplete, 0 );

	conn.request( "GET", (const char*) s2, 0, 0,0 );

	while( conn.outstanding() )
		conn.pump();
}
