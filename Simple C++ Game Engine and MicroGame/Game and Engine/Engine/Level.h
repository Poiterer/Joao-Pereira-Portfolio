#pragma once

#include "box2d/box2d.h"
#include <vector>
#include "Actor.h"
#include "RendererClass.h"
#include "SDL.h"

class Level
{
public:
	Level(float xgrav,float ygrav);
	~Level();

	b2Body* CreateBody(b2BodyDef* bodyDef);
	b2Body* CreateBody(b2BodyDef* bodyDef, b2PolygonShape* shape, float density = 0);

	void update(float deltaTime);
	void setBackground();
	void setBackground2();
	void addActor(Actor* actor);
	void removeActor(Actor* actor);
	void render(RendererClass* ren, float deltaTime);

	void TimeStep(float stepSize);

private:
	b2World* world;
	std::vector<Actor*> actors;
	std::vector<Actor*> actorsToAdd;
	std::vector<Actor*> actorsToRemove;
	SDL_Texture* background;
	SDL_Texture* background2;


};

