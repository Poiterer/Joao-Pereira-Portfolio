#pragma once
#include "SDL.h"
#include "string"
#include "Vector2.h"

class Tilemap
{
public:
	Tilemap(SDL_Texture* texture, int col, int row);
	~Tilemap();


	Vector2 GetTextureWidhtHeight();
	Vector2 GetFrameWidthHeight();

private:
	int textureWidth;
	int textureHeight;
	int frameWidth;
	int frameHeight;



};

