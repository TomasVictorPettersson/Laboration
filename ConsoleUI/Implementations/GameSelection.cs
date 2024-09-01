using ConsoleUI.Interfaces;
using ConsoleUI.Utils;
using GameResources.Constants;
using GameResources.Enums;
using Validation.Interfaces;

namespace ConsoleUI.Implementations
{
	// Implementation of the IGameSelector interface for selecting game types.
	public class GameSelection(IGameSelectionValidation validation) : IGameSelection
	{
		private readonly IGameSelectionValidation _validation = validation;

		// Prompts user to select a game type and returns the selected type.
		public GameTypes SelectGameType()
		{
			DisplayGameOptions();

			while (true)
			{
				Console.Write(PromptMessages.GameSelectionPrompt);
				var input = Console.ReadLine();

				// Use the validation class to parse user input.
				var selectedGameType = _validation.ParseGameTypeInput(input);

				if (selectedGameType.HasValue)
				{
					return selectedGameType.Value;
				}

				Console.WriteLine(UserInteractionMessages.GameSelectionInvalidMessage);
			}
		}

		// Displays game options and formatting.
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
	}
}