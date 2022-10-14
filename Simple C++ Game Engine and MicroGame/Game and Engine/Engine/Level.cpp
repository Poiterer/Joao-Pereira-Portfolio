#include "Level.h"

Level::Level(float xgrav, float ygrav)
{
	b2Vec2 gravity(xgrav, ygrav);

	world = new b2World(gravity);
}

Level::~Level()
{
	SDL_DestroyTexture(background);
	SDL_DestroyTexture(background2);

	for (Actor* actor : actors)
	{
		delete actor;
	}
	actors.clear();
	for (Actor* actor : actorsToAdd)
	{
		delete actor;
	}
	actorsToAdd.clear();
	for (Actor* actor : actorsToRemove)
	{
		delete actor;
	}
	actorsToRemove.clear();


	delete &actors;
	delete &actorsToAdd;
	delete &actorsToRemove;
	delete world;
	background = nullptr;
	background2 = nullptr;
	world = nullptr;
}

b2Body* Level::CreateBody(b2BodyDef* bodyDef)
{
	b2Body* newBody = world->CreateBody(bodyDef);


	return newBody;
}

b2Body* Level::CreateBody(b2BodyDef* bodyDef, b2PolygonShape* shape, float density)
{
	b2Body* newBody = world->CreateBody(bodyDef);
	newBody->CreateFixture(shape, density);

	return newBody;
}

void Level::update(float deltaTime)
{
	for (Actor* actor : actors)
	{
		actor->update(deltaTime);
	}
	for (Actor* actor : actorsToRemove)
	{
		auto result = std::find(actors.begin(), actors.end(), actor);
		if (result != actorsToRemove.end())
		{
			actors.erase(result);
			delete actor;
		}
	}
	actorsToRemove.clear();
	for (Actor* actor : actorsToAdd)
	{
		actors.push_back(actor);
	}
	actorsToAdd.clear();

	
}

void Level::addActor(Actor* actor)
{
	actorsToAdd.push_back(actor);
	actor->setLevel(this);
}

void Level::removeActor(Actor* actor)
{
	actorsToRemove.push_back(actor);

}

void Level::render(RendererClass* ren, float deltaTime)
{
	if (background)
	{
		ren->AddToRender(background, nullptr, nullptr);
	}
	if (background2) 
	{
		ren->AddToRender(background2, nullptr, nullptr);
	}
	for (Actor* actor : actors)
	{
		actor->render(ren, deltaTime);
	}

}

void Level::TimeStep(float stepSize)
{
	world->Step(stepSize, 6, 3);
}


