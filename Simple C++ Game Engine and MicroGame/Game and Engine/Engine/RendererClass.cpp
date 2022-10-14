#include "RendererClass.h"
#include "InitError.h"


RendererClass::RendererClass(Window* window, int index, Uint32 flags)
{
	SDL_Window* sdlWindow = window->getSDLWindow();
	myRenderer = SDL_CreateRenderer(sdlWindow, index, flags);
	if (sdlWindow == nullptr) {
		throw InitError();

	}
}

RendererClass::~RendererClass()
{
	SDL_DestroyRenderer(myRenderer);
	myRenderer = nullptr;
}

void RendererClass::ClearRender()
{
	SDL_RenderClear(myRenderer);
}

void RendererClass::SetRenderBaseColor(Uint8 r, Uint8 g, Uint8 b, Uint8 a)
{
	SDL_SetRenderDrawColor(myRenderer, r, g, b, a);
}

void RendererClass::AddToRender(SDL_Texture* tex,SDL_Rect* srect, SDL_Rect* drect )
{
	SDL_RenderCopy(myRenderer,tex,srect,drect);
}

void RendererClass::UpdateRender()
{
	SDL_RenderPresent(myRenderer);
}

SDL_Renderer* RendererClass::GetSDLRend()
{
	return myRenderer;
}
