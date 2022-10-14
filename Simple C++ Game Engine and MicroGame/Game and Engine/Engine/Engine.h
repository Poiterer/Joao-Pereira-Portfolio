#pragma once

#include <string>


class Engine 
{
public:
	Engine();


	void init(std::string title, int width, int height);
	void startGame();

	void handleEvents();
	void update();
	void render();
	void clean();
	void inputs();

	bool running() { return isRunning;}


	~Engine();

private:
	bool isRunning = true;
	int frameStart;
	int frameEnd;
	float deltaTime;

	float updateInternalTimer;
	float updateTimeToCall = 1.0/60.0f;

	class SDLWrapper* sdl;
	class Window* window;
	class RendererClass* renderer;
	class Level* level;
	class TextureManager* textureManager;
	class InputManager* inputManager;
	





};

/*class Engine
{
public:

	Engine();
	~Engine();

	void StartGame();

	b2World* GetWorld();


private:
	

	b2World* world;

	void CreateWindow();

	b2World* CreateWorld();

	SDL_Event windowEvent;



};
*/

