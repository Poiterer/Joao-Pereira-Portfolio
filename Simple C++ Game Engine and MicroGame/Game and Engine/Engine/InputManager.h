#pragma once
#include <SDL.h>

class InputManager
{
private:

	InputManager();
	~InputManager();

	static InputManager* sInstance;

	const Uint8* keyboardState;

public:

	static InputManager* Instance();
	static void Release();


	bool KeyDown(SDL_Scancode scanCode);

	void Update();

	SDL_GameController* controller = nullptr;

};

