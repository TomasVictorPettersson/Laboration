namespace Laboration.GameLogic.Interfaces
{
	// Defines the contract for the game logic.
	public interface IGameLogic
	{
		// Starts the game by displaying a welcome message based on whether it's a new game,
		// generating a secret number, prompting the user with instructions,
		// optionally showing the secret number for practice and then running the main game loop.
		void PlayGame(string userName, bool isNewGame);

		// Generates a random 4-digit secret number for the game.
		string MakeSecretNumber();

		// Executes the main game loop, processing guesses and providing feedback.
		void PlayGameLoop(string secretNumber, string userName);

		// Retrieves the user's guess from the console UI and processes it.
		bool HandleUserGuess(string secretNumber, ref int numberOfGuesses);

		// Processes the player's guess, validates it, and provides feedback.
		bool ProcessGuess(string secretNumber, string guess, ref int numberOfGuesses);

		// Concludes the game by saving the player's result to the high score list,
		// displaying a message with the correct number and the number of guesses,
		// showing the updated high score list, and prompting the user to continue.
		void EndGame(string secretNumber, string userName, int numberOfGuesses);
	}
}