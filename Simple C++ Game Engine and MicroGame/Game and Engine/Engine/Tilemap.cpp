#include "Tilemap.h"

Tilemap::Tilemap(SDL_Texture* texture, int col, int row)
{
	SDL_QueryTexture(texture, NULL, NULL, &textureWidth, &textureHeight);

	frameWidth = textureWidth / col;
	frameHeight = textureHeight / row;

}

Tilemap::~Tilemap()
{
	textureWidth = NULL;
	textureHeight = NULL;
	frameWidth = NULL;
	frameHeight = NULL;
}

Vector2 Tilemap::GetTextureWidhtHeight()
{
	return Vector2(textureWidth, textureHeight);
}

Vector2 Tilemap::GetFrameWidthHeight()
{
	return Vector2(frameWidth, frameHeight);
}

