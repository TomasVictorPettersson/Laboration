using Laboration.ConsoleUI.Interfaces;
using Laboration.ConsoleUI.Utils;
using Laboration.GameResources.Constants;
using Laboration.GameResources.Enums;

namespace Laboration.ConsoleUI.Implementations
{
	public class GameSelector : IGameSelector
	{
		public GameTypes SelectGameType()
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

		private static void DisplayGameOptions()
		{
			Console.WriteLine(UserInteractionMessages.GameSelectionPrompt);
			Console.WriteLine(FormatUtils.CreateSeparatorLine());
			Console.WriteLine(UserInteractionMessages.GameSelectionOptions);
			Console.WriteLine(FormatUtils.CreateSeparatorLine());
			Console.WriteLine(UserInteractionMessages.ExitOption);
			Console.WriteLine(FormatUtils.CreateSeparatorLine());
		}

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