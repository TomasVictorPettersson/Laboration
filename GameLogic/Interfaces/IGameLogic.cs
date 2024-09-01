using GameResources.Enums;

namespace GameLogic.Interfaces
{
	// Defines the contract for game logic management.
	public interface IGameLogic
	{
		// Starts the game by displaying a welcome message, generating a secret number,
		// prompting the user with instructions, and running the main game loop.
		void PlayGame(string userName, bool isNewGame);

		// Generates and returns a random
		// -digit secret number for the game.
		string MakeSecretNumber();

		// Runs the main game loop, processing guesses and providing feedback to the user.
		void PlayGameLoop(string secretNumber, string userName);

		// Retrieves and processes the user's guess, updating the guess count.
		bool HandleUserGuess(string secretNumber, ref int numberOfGuesses);

		// Validates and processes the player's guess, providing feedback and updating the guess count.
		bool ProcessGuess(string secretNumber, string guess, ref int numberOfGuesses);

		// Ends the game by saving the player's result, displaying the correct number and number of guesses,
		// showing the updated high score list, and asking if the user wants to continue.
		void EndGame(string secretNumber, string userName, int numberOfGuesses);

		// Generates feedback for the user's guess compared to the secret number.
		string GenerateFeedback(string secretNumber, string guess);

		// Counts the number of bulls (correct digits in correct positions).
		int CountBulls(string secretNumber, string guess);

		// Counts the number of cows (correct digits in incorrect positions).
		int CountCows(string secretNumber, string guess);

		// Returns the type of game (e.g., MasterMind, BullsAndCows).
		GameTypes GetGameType();
	}
}