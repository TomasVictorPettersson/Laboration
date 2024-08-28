using ConsoleUI.Interfaces;
using ConsoleUI.Utils;
using GameResources.Constants;
using GameResources.Enums;

namespace ConsoleUI.Implementations
{
	// Implementation of the IGameSelector interface for selecting game types
	public class GameSelector : IGameSelector
	{
		// Prompts user to select a game type and returns the selected type
		public GameTypes SelectGameType()
		{
			DisplayGameOptions();

			while (true)
			{
				Console.Write(PromptMessages.GameSelectionPrompt);
				var input = Console.ReadLine();
				var selectedGameType = ParseUserInput(input);

				if (selectedGameType.HasValue)
				{
					return selectedGameType.Value;
				}

				Console.WriteLine(UserInteractionMessages.GameSelectionInvalidMessage);
			}
		}

		// Displays game options and formatting
		public void DisplayGameOptions()
		{
			Console.WriteLine(
				$"{FormatUtils.CreateSeparatorLine()}\n" +
				$"{UserInteractionMessages.GameSelectionOptions}\n" +
				$"{FormatUtils.CreateSeparatorLine()}\n" +
				$"{UserInteractionMessages.ExitOption}\n" +
				$"{FormatUtils.CreateSeparatorLine()}"
			);
		}

		// Parses user input and returns corresponding GameType enum value
		public GameTypes? ParseUserInput(string? input)
		{
			return input switch
			{
				"1" => GameTypes.BullsAndCows, // Option 1: Bulls and Cows game
				"2" => GameTypes.MasterMind,   // Option 2: MasterMind game
				"3" => GameTypes.Quit,         // Option 3: Exit the application
				_ => null                      // Invalid input
			};
		}
	}
}