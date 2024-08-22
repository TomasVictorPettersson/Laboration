﻿using Laboration.ConsoleUI.Interfaces;
using Laboration.GameLogic.Interfaces;
using Laboration.GameResources.Constants;
using Laboration.GameResources.Enums;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Validation.Interfaces;

namespace Laboration.GameLogic.Implementations
{
	// Abstract base class for game logic, providing common functionality for different game types.
	// Implements the IGameLogic interface and defines the core game loop and helper methods.
	public abstract class GameLogicBase(IHighScoreManager highScoreManager, IConsoleUI consoleUI, IValidation validation) : IGameLogic
	{
		// Dependency injection for high score management, console UI, and validation.
		private readonly IHighScoreManager _highScoreManager = highScoreManager;

		private readonly IConsoleUI _consoleUI = consoleUI;
		private readonly IValidation _validation = validation;

		// Starts the game by displaying a welcome message, generating a secret number,
		// and running the main game loop. Handles exceptions and displays error messages.
		public void PlayGame(string userName, bool isNewGame)
		{
			try
			{
				_consoleUI.DisplayWelcomeMessage(GetGameType(), userName, isNewGame);
				string secretNumber = MakeSecretNumber();
				_consoleUI.WaitForUserToContinue(isNewGame ? $"\n{PromptMessages.InstructionsReadMessage}" : $"\n{PromptMessages.StartPlayingPrompt}");

				// Optionally display the secret number for practice mode.
				_consoleUI.DisplaySecretNumberForPractice(secretNumber);

				PlayGameLoop(secretNumber, userName);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error playing game: {ex.Message}");
				throw;
			}
		}

		// Abstract method for generating a secret number, to be implemented by derived classes.
		public abstract string MakeSecretNumber();

		// Main game loop that processes user guesses until the correct guess is made.
		// Ends the game once the correct guess is found.
		public void PlayGameLoop(string secretNumber, string userName)
		{
			int numberOfGuesses = 0;
			bool isGuessCorrect = false;

			while (!isGuessCorrect)
			{
				isGuessCorrect = HandleUserGuess(secretNumber, ref numberOfGuesses);
			}

			EndGame(secretNumber, userName, numberOfGuesses);
		}

		// Handles user input for a guess and processes it.
		public bool HandleUserGuess(string secretNumber, ref int numberOfGuesses)
		{
			string guess = _consoleUI.GetValidGuessFromUser(GetGameType());
			return ProcessGuess(secretNumber, guess, ref numberOfGuesses);
		}

		// Processes the user's guess, generates feedback, and updates the guess count.
		public bool ProcessGuess(string secretNumber, string guess, ref int numberOfGuesses)
		{
			string feedback = GenerateFeedback(secretNumber, guess);
			_consoleUI.DisplayGuessFeedback(feedback);
			numberOfGuesses++;
			return _validation.IsCorrectGuess(guess, secretNumber);
		}

		// Ends the game by saving the result, displaying the correct number and number of guesses,
		// showing the high score list, and asking the user if they want to continue.
		public void EndGame(string secretNumber, string userName, int numberOfGuesses)
		{
			_highScoreManager.SaveResult(userName, numberOfGuesses);
			_consoleUI.DisplayCorrectMessage(secretNumber, numberOfGuesses);
			_consoleUI.WaitForUserToContinue(PromptMessages.FinishGamePrompt);
			_consoleUI.DisplayHighScoreList(userName);
			_consoleUI.WaitForUserToContinue(PromptMessages.ContinuePrompt);
		}

		// Generates feedback for the user's guess compared to the secret number,
		// formatted as "Bulls,Cows".
		public string GenerateFeedback(string secretNumber, string guess)
		{
			int bulls = CountBulls(secretNumber, guess);
			int cows = CountCows(secretNumber, guess);
			return $"{new string('B', bulls)},{new string('C', cows)}";
		}

		// Counts the number of bulls (correct digits in the correct positions).
		public int CountBulls(string secretNumber, string guess)
		{
			int bulls = 0;
			for (int i = 0; i < 4; i++)
			{
				if (secretNumber[i] == guess[i])
				{
					bulls++;
				}
			}
			return bulls;
		}

		// Abstract method to count the number of cows (correct digits in incorrect positions).
		// To be implemented by derived classes based on specific game rules.
		public abstract int CountCows(string secretNumber, string guess);

		// Abstract method to get the game type (e.g., MasterMind, BullsAndCows).
		// To be implemented by derived classes to specify the type of game.
		public abstract GameTypes GetGameType();
	}
}