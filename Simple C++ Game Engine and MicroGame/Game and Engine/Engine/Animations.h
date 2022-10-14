#pragma once
#include "string"
#include <SDL.h>
#include "RendererClass.h"
#include "Actor.h"

class Animations
{
	Animations(SDL_Texture* tex);
	~Animations();

public:
	void update(float deltaTime);
	void render(RendererClass* ren, float deltaTime, SDL_Rect* srect, SDL_Rect* drect);
	void setOwner(Actor* actor);

private:
	SDL_Rect playerRect;
	SDL_Rect playerPosition;

	SDL_Texture* texture;

	Actor* ownerActor;

	float frameTime{ 0 };
	int speed{ 0 };
	int textureWidth{ 0 };
	int textureHeight{ 0 };
	int frameWidth{ 0 };
	int frameHeight{ 0 };
	int isloopable;
	bool isDone;



};

