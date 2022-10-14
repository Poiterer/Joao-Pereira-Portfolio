#include "Animations.h"


Animations::Animations(std::string path, SDL_Texture* tex)
{
	texture = tex;

}

Animations::~Animations()
{
}

void Animations::update(float deltaTime)
{
	if (!isDone)
	{
		frameTime += deltaTime;

		if (frameTime >= speed);
		{
			if (isloopable)
			{

			}
		}
	}
}

void Animations::render(RendererClass* ren, float deltaTime, SDL_Rect* srect, SDL_Rect* drect)
{
	ren->AddToRender(texture, srect, drect);
}

void Animations::setOwner(Actor* actor)
{
	ownerActor = actor;
}
