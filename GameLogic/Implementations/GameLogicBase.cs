using Laboration.ConsoleUI.Interfaces;
using Laboration.GameLogic.Interfaces;
using Laboration.GameResources.Constants;
using Laboration.GameResources.Enums;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Validation.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboration.GameLogic.Implementations
{
	// Base class for game logic, providing common methods and properties for different game types.
	public abstract class GameLogicBase : IGameLogic
	{
		private readonly IHighScoreManager _highScoreManager;
		private readonly IConsoleUI _consoleUI;
		private readonly IValidation _validation;

		// Constructor initializes the base class with required dependencies.
		protected GameLogicBase(IHighScoreManager highScoreManager, IConsoleUI consoleUI, IValidation validation)
		{
			_highScoreManager = highScoreManager;
			_consoleUI = consoleUI;
			_validation = validation;
		}

		// Starts the game by displaying a welcome message, generating a secret number,
		// prompting the user, optionally showing the secret number for practice,
		// and then running the main game loop.
		public void PlayGame(string userName, bool isNewGame)
		{
			try
			{
				_consoleUI.DisplayWelcomeMessage(GetGameType(), userName, isNewGame);

				string secretNumber = MakeSecretNumber();

				_consoleUI.WaitForUserToContinue(
					isNewGame
						? $"\n{PromptMessages.InstructionsReadMessage}"
						: $"\n{PromptMessages.StartPlayingPrompt}"
				);

				// Comment out or remove the next line to play the real game!
				_consoleUI.DisplaySecretNumberForPractice(secretNumber);
				PlayGameLoop(secretNumber, userName);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error playing game: {ex.Message}");
				throw;
			}
		}

		// Generates a random secret number for the game.
		public abstract string MakeSecretNumber();

		// Main game loop that continues until the user guesses the secret number.
		public void PlayGameLoop(string secretNumber, string userName)
		{
			try
			{
				int numberOfGuesses = 0;
				bool isGuessCorrect = false;

				while (!isGuessCorrect)
				{
					isGuessCorrect = HandleUserGuess(secretNumber, ref numberOfGuesses);
				}

				EndGame(secretNumber, userName, numberOfGuesses);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in game loop: {ex.Message}");
				throw;
			}
		}

		// Retrieves the user's guess from the console UI and processes it.
		public bool HandleUserGuess(string secretNumber, ref int numberOfGuesses)
		{
			string guess = _consoleUI.GetValidGuessFromUser(GetGameType());
			return ProcessGuess(secretNumber, guess, ref numberOfGuesses);
		}

		// Processes the user's guess, generates feedback, and updates the number of guesses.
		public bool ProcessGuess(string secretNumber, string guess, ref int numberOfGuesses)
		{
			string guessFeedback = GenerateFeedback(secretNumber, guess);
			_consoleUI.DisplayGuessFeedback(guessFeedback);
			numberOfGuesses++;
			return _validation.IsCorrectGuess(guess, secretNumber);
		}

		// Concludes the game by saving the player's result to the high score list,
		// displaying a message with the correct number and the number of guesses,
		// showing the updated high score list, and prompting the user to continue.
		public void EndGame(string secretNumber, string userName, int numberOfGuesses)
		{
			try
			{
				_highScoreManager.SaveResult(userName, numberOfGuesses);
				_consoleUI.DisplayCorrectMessage(secretNumber, numberOfGuesses);
				_consoleUI.WaitForUserToContinue(PromptMessages.FinishGamePrompt);
				_consoleUI.DisplayHighScoreList(userName);
				_consoleUI.WaitForUserToContinue(PromptMessages.ContinuePrompt);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error ending game: {ex.Message}");
				throw;
			}
		}

		// Generates feedback for the player's guess. This method should be overridden
		// by derived classes to provide specific feedback formats.
		protected abstract string GenerateFeedback(string secretNumber, string guess);

		// Abstract method to get the game type, to be implemented by derived classes.
		protected abstract GameTypes GetGameType();
	}
}