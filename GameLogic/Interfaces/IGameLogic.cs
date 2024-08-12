namespace Laboration.GameLogic.Interfaces
{
	// Defines the contract for the game logic.
	public interface IGameLogic
	{
		// Starts the game by displaying a welcome message, generating a secret number,
		// and then running the main game loop.
		void PlayGame(string userName);

		// Generates a random 4-digit secret number for the game.
		string MakeSecretNumber();

		// Executes the main game loop, processing guesses and providing feedback.
		void PlayGameLoop(string secretNumber, string userName);

		// Retrieves the user's guess from the console UI and processes it.
		bool HandleUserGuess(string secretNumber, ref int numberOfGuesses);

		// Processes the player's guess, validates it, and provides feedback.
		bool ProcessGuess(string secretNumber, string guess, ref int numberOfGuesses);

		// Ends the game by saving the player's result, displaying a message with the
		// correct number and number of guesses, and showing the updated high score list.
		void EndGame(string secretNumber, string userName, int numberOfGuesses);
	}
}