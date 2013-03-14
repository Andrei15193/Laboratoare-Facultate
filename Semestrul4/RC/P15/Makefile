compile: Common.h Server.h Server.c Client.h Client.c
	gcc -o Client Client.c
	gcc -o Server Server.c -lpthread

clean: Server Client
	rm Server Client
