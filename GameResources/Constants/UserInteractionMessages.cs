﻿namespace Laboration.GameResources.Constants
{
	// Contains constants related to user interaction messages and prompts.
	public static class UserInteractionMessages
	{
		// The game selection options for user interaction.
		public const string GameSelectionOptions = "Select a game to play:\n1. Bulls and Cows\n2. MasterMind\n3. Exit";

		// Prompt for the user to enter the game he wants to play.
		public const string ChoseGamePrompt = "\nEnter the game you want to play: ";

		// Message shown when an invalid game selection is made.
		public const string InvalidSelectionMessage = "Invalid selection. Please enter a valid number.";

		// Prompt for the user to enter their username.
		public const string UserNamePrompt = "Enter your username: ";

		// Message shown when the user enters an empty username.
		public const string EmptyUsernameMessage = "Empty values are not allowed. Please enter a valid username.\n";

		// Message shown when the username length is not within the acceptable range.
		public const string UsernameLengthMessage = "Username must be between 2 and 20 characters long.\n";

		// Prompt for the user to enter their 4-digit guess.
		public const string GuessPrompt = "Enter your 4-digit guess: ";

		// Message shown when the user enters an invalid input for a guess.
		public const string BullsAndCowsInvalidInputMessage = "Invalid input. Please enter a 4-digit number with unique digits.\n";

		// Message shown when the user enters an invalid input for a guess.
		public const string MasterMindInvalidInputMessage = "Invalid input. Please enter a 4-digit number.\n";

		// Prompt asking the user if they want to play the game again.
		public const string PlayAgainPrompt = "Do you want to play again? (y/n): ";

		// Message shown when the user provides an invalid response to the play-again prompt.
		public const string InvalidPlayAgainResponse = "Invalid input. Please enter y for yes or n for no.\n";
	}
}