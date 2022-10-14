#include "TextureManager.h"


TextureManager::TextureManager(RendererClass* renderer)
{
	ren = renderer;
}

TextureManager::~TextureManager()
{
	ren = nullptr;
}

SDL_Texture* TextureManager::LoadTexture(std::string filePath)
{
	SDL_Texture* texture = nullptr;
	SDL_Surface* surface = SDL_LoadBMP(filePath.c_str());
	if (surface == NULL) {
		//MISSING ERROR THINGY
	}
	else
	{
		SDL_SetColorKey(surface, SDL_TRUE, SDL_MapRGB(surface->format, 255, 0, 255));
		texture = SDL_CreateTextureFromSurface(ren->GetSDLRend(), surface);
		if (texture == NULL) {
			//MISSING ERROR THINGY
		}
	}

	SDL_FreeSurface(surface);

	return texture;
}