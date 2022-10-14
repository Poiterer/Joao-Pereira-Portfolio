#pragma once
#include <SDL.h>
#include "string";
#include "RendererClass.h"

class TextureManager
{
public:
	TextureManager(RendererClass* renderer);
	~TextureManager();

	SDL_Texture* LoadTexture(std::string filepath);

private:
	RendererClass* ren;

};

