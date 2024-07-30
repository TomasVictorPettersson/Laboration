namespace Laboration.GameLogic.Interfaces
{
	// Defines the contract for the game logic in a Bulls and Cows game.
	public interface IGameLogic
	{
		// Starts the game, initializing game state and playing the game.
		void PlayGame(string userName);

		// Initializes the game, displaying a welcome message to the user.
		void InitializeGame(string userName);

		// Generates a random 4-digit secret number for the game.
		string MakeSecretNumber();

		// Executes the main game loop, processing guesses and providing feedback.
		void PlayGameLoop(string secretNumber, string userName);

		// Processes the player's guess, validates it, and provides feedback.
		string ProcessGuess(string secretNumber, ref int numberOfGuesses);

		// Ends the game, saving the result and displaying high scores.
		void EndGame(string secretNumber, string userName, int numberOfGuesses);
	}
}