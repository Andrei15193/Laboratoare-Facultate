compile: Common.h Common.c Server.h Server.c Client.h Client.c
	gcc -c Common.c
	gcc -o Client Common.o Client.c
	gcc -o Server Common.o Server.c -lpthread
	rm Common.o

clean: Server Client
	rm Server Client
