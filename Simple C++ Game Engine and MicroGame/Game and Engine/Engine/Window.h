#pragma once

#include <string>
#include <SDL.h>

class Window
{
public:
	Window(std::string title, int windowWidth, int windowHeight);
	SDL_Surface* getSurface();
	void updateSurface();
	SDL_Window* getSDLWindow();
	~Window();

private:
	SDL_Window* window;
};

