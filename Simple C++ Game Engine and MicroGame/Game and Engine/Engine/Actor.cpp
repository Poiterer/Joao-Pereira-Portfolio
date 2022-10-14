#include "Actor.h"

Actor::Actor(Vector2 loc) : location(loc)
{

}

Actor::Actor() : location(Vector2(0.0f,0.0f))
{

}

Actor::~Actor()
{
}

void Actor::render(RendererClass* ren, float deltaTime)
{
	animation->render(ren, deltaTime, );
}

void Actor::setAnimation(Animations* anim)
{
	animation = anim;
	animation->setOwner(this);

}
