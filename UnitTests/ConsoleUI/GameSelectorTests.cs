using ConsoleUI.Implementations;
using ConsoleUI.Utils;
using GameResources.Constants;
using GameResources.Enums;

namespace UnitTests.ConsoleUI
{
	[TestClass]
	public class GameSelectorTests
	{
		private readonly GameSelector gameSelector = new();
		private readonly StringWriter consoleOutput = new();
		private StringReader consoleInput = null!;

		[TestInitialize]
		public void Setup()
		{
			Console.SetOut(consoleOutput);
		}

		// Helper method to simulate user input for console interaction
		private void SimulateInput(string input)
		{
			consoleInput = new StringReader(input);
			Console.SetIn(consoleInput);
		}

		// Tests if valid input for 'Bulls and Cows' correctly returns the corresponding GameType.
		[TestMethod]
		public void SelectGameType_ValidInput_ReturnsCorrectGameType()
		{
			// Arrange
			SimulateInput("1\n");

			// Act
			var result = gameSelector.SelectGameType();

			// Assert
			Assert.AreEqual(GameTypes.BullsAndCows, result, "Expected GameType to be BullsAndCows for input '1'.");
		}

		// Tests if invalid input followed by a valid input shows an error message and returns the correct GameType.
		[TestMethod]
		public void SelectGameType_InvalidInput_ShowsErrorMessage()
		{
			// Arrange
			SimulateInput("invalid\n1\n");

			// Act
			var result = gameSelector.SelectGameType();

			// Assert
			StringAssert.Contains(consoleOutput.ToString(), UserInteractionMessages.GameSelectionInvalidMessage, "Expected error message for invalid input.");
			Assert.AreEqual(GameTypes.BullsAndCows, result, "Expected GameType to be BullsAndCows after invalid input followed by '1'.");
		}

		// Tests if input for quitting the game correctly returns the Quit GameType.
		[TestMethod]
		public void SelectGameType_ValidInput_Quit_ReturnsQuitGameType()
		{
			// Arrange
			SimulateInput("3\n");

			// Act
			var result = gameSelector.SelectGameType();

			// Assert
			Assert.AreEqual(GameTypes.Quit, result, "Expected GameType to be Quit for input '3'.");
		}

		// Tests if the game options are correctly displayed.
		[TestMethod]
		public void DisplayGameOptions_CorrectlyDisplaysOptions()
		{
			// Arrange
			var expectedOutput =
				$"{FormatUtils.CreateSeparatorLine()}\n" +
				$"{UserInteractionMessages.GameSelectionOptions}\n" +
				$"{FormatUtils.CreateSeparatorLine()}\n" +
				$"{UserInteractionMessages.ExitOption}\n" +
				$"{FormatUtils.CreateSeparatorLine()}";

			// Act
			gameSelector.DisplayGameOptions();

			var actualOutput = consoleOutput.ToString().Trim();

			// Assert
			Assert.AreEqual(expectedOutput, actualOutput, "Console output does not match the expected format.");
		}

		// Cleanup after each test method is run.
		[TestCleanup]
		public void Cleanup()
		{
			consoleOutput.Dispose();
		}
	}
}