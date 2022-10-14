#pragma once
#include "SDL.h"
#include "Window.h"

class RendererClass
{
public:
	RendererClass(Window* window, int index, Uint32 flags);
	~RendererClass();

	void ClearRender();
	void SetRenderBaseColor(Uint8 r, Uint8 g, Uint8 b, Uint8 a);
	void AddToRender(SDL_Texture* tex, SDL_Rect* srect, SDL_Rect* drect);
	void UpdateRender();
	SDL_Renderer* GetSDLRend();

private:
	SDL_Renderer* myRenderer;
};

