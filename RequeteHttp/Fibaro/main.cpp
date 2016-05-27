#include "happyhttp.h"
#include "INCLUDE/Requete.h"
#include <cstdio>
#include <iostream>
#include <cstring>
#include "base64.h"
#ifdef WIN32
#include <winsock2.h>
#endif // WIN32
int i;



void Test1()
{
	puts("-----------------Test1------------------------" );
	// simple simple GET
	happyhttp::Connection conn( "192.168.81.1", 80 );
	conn.setcallbacks( OnBegin, OnData, OnComplete, 0 );
    std::string user = "admin" ;
    std::string pass="admin";
    const std::string s = (user+":"+pass) ;

    std::string encoded = base64_encode(reinterpret_cast<const unsigned char*>(s.c_str()), s.length());
    printf("%s\n",("Basic "+encoded).c_str());
	const char* headers[] =
	{
        "Authorization:",("Basic "+encoded).c_str(),
		"Connection", "close",
		0
	};

	conn.request( "GET", "/api/callAction?deviceID=5&name=turnOn", headers, 0,0 );

	while( conn.outstanding() )
		conn.pump();
}



void Test2()
{
	puts("-----------------Test2------------------------" );
	// POST using high-level request interface

    std::string user = "admin" ;
    std::string pass="admin";
    const std::string s = (user+":"+pass) ;

    std::string encoded = base64_encode(reinterpret_cast<const unsigned char*>(s.c_str()), s.length());
    printf("%s\n",("Basic "+encoded).c_str());

	const char* headers[] =
	{
        "Authorization:",("Basic "+encoded).c_str(),
		"Connection", "close",
		0
	};

	const char* body = ""; //si on met deviceID=5&name=turnOn ici on obtient 401 BAD REQUEST

	happyhttp::Connection conn( "192.168.81.1", 80 );
	conn.setcallbacks( OnBegin, OnData, OnComplete, 0 );
	conn.request( "POST",
			"/api/callAction?deviceID=5&name=turnOn",
			headers,
			(const unsigned char*)body,
			strlen(body) );

	while( conn.outstanding() )
		conn.pump();
}

void Test3()
{
	puts("-----------------Test3------------------------" );
	// POST example using lower-level interface

	const char* params = "deviceID=5&name=turnOn";
	int l = strlen(params)+i;

	happyhttp::Connection conn( "192.168.81.1", 80 );
	conn.setcallbacks( OnBegin, OnData, OnComplete, 0 );

	conn.putrequest( "GET", "/api/callAction" );;
  std::string user = "admin" ;
  std::string pass="admin";
    const std::string s = (user+":"+pass) ;

  std::string encoded = base64_encode(reinterpret_cast<const unsigned char*>(s.c_str()), s.length());
  printf("%s\n",("Basic "+encoded).c_str());
conn.putheader( "Content-Length", l );
	conn.putheader( "Authorization:",("Basic "+encoded).c_str() );
	conn.putheader("Connection:","close");
	conn.endheaders();
	conn.send( (const unsigned char*)params, l );

	while( conn.outstanding() )
		conn.pump();
}



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
		//Test1();
		Test2();
		//Test3();
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
