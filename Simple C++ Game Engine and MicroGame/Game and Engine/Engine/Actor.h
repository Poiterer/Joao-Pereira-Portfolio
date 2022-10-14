#pragma once

#include "RendererClass.h"
#include "Vector2.h"
#include <box2d/box2d.h>
#include "Animations.h"
#include "string"
#include "TextureManager.h"

class Actor
{
	Actor(Vector2 loc);
	Actor();
	virtual ~Actor();

public:


	virtual void update(float deltaTime) {};

	virtual void render(RendererClass* ren, float deltaTime);

	virtual void animationEndedCallback() {};

	void setAnimation(class Animations* anim);

	void setLevel(class Level* lvl)
	{
		level = lvl;
	}

	Vector2 getLocation() const
	{
		return location;
	}

protected:
	Vector2 location;
	class Animations* animation;
	class Level* level;

	b2Body* body;

	/*
	std::string filepath;
	SDL_Texture* tex;
	RendererClass* ren;
	TextureManager* texMan = new TextureManager(ren);
	tex = texMan->LoadTexture(filepath);
	*/
	
	
	
	
	
	/*
public:
	Actor(Vector2 loc, char* textureSheet, RendererClass* render);
	~Actor();

	Vector2 position;

	virtual void render(RendererClass* ren, float deltaTime );
	virtual void update(float deltaTime);

	void setLevel(class Level* lvl) { level = lvl; }

	void setAnimation(class Animations* anim);
private:
	class Level* level;
	bool isAnimated;
	SDL_Texture* objText;
	SDL_Rect srcRect, destRect;
	RendererClass* renderer;
	*/
};

