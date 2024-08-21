using Laboration.GameResources.Constants;
using Laboration.GameResources.Enums;

namespace Laboration.ConsoleUI.GameSelection
{
	// This static class handles user interactions for selecting a game type.
	// It displays available game options, prompts the user for input, and
	// returns the corresponding GameType based on the user's choice.
	public static class GameSelector
	{
		// Prompts the user to select a game type and returns the corresponding GameType.
		// Continues prompting until the user makes a valid selection.
		public static GameTypes SelectGameType()
		{
			DisplayGameOptions();

			while (true)
			{
				Console.Write(UserInteractionMessages.ChoseGamePrompt);
				var input = Console.ReadLine();
				var selectedGameType = ParseUserInput(input);

				if (selectedGameType.HasValue)
				{
					return selectedGameType.Value;
				}

				Console.WriteLine(UserInteractionMessages.InvalidSelectionMessage);
			}
		}

		// Displays the available game options to the user.
		private static void DisplayGameOptions()
		{
			Console.WriteLine(UserInteractionMessages.GameSelectionOptions);
		}

		// Parses the user's input and returns the corresponding GameType.
		// Returns null if the input is invalid.
		private static GameTypes? ParseUserInput(string? input)
		{
			return input switch
			{
				"1" => GameTypes.BullsAndCows,
				"2" => GameTypes.MasterMind,
				"3" => GameTypes.Quit,
				_ => null
			};
		}
	}
}