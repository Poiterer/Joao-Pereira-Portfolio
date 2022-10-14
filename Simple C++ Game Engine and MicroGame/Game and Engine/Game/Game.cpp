// Game.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <Engine.h>



int main(int argc, char* argv[])
{
	Engine engine;
	engine.init("Xennon CLONE!!", 800, 600);
	engine.startGame();

	return 0;
}



