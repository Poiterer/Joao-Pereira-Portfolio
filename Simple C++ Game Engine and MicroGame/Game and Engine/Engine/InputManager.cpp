#include "InputManager.h"

InputManager::InputManager() {

	//GameController
	for (int i = 0; i < SDL_NumJoysticks(); ++i) {
		if (SDL_IsGameController(i)) {
			controller = SDL_GameControllerOpen(i);
			char* mapping = SDL_GameControllerMapping(controller);
			SDL_free(mapping);
		}
	}
}

InputManager::~InputManager() {}

InputManager* InputManager::sInstance = NULL;

InputManager* InputManager::Instance() {
	if (sInstance == NULL)
		sInstance = new InputManager();

	return sInstance;
}

void InputManager::Release() {
	delete sInstance;
	sInstance = NULL;
}

bool InputManager::KeyDown(SDL_Scancode scanCode) {
	return keyboardState[scanCode];
}

void InputManager::Update() {
	keyboardState = SDL_GetKeyboardState(NULL);
}