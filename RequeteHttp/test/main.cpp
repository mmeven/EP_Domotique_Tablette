#include <stdio.h>
#include <stdlib.h>
#include "RequeteHttp.h"

int main(int argc, char *argv[]) {
        char* s1("httpbin.org");
        char* s2("/ip");
        requeteHttpKira(s1,s2);
   return 0;
}


