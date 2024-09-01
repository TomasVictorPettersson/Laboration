using ConsoleUI.Implementations;
using ConsoleUI.Utils;
using GameResources.Constants;
using GameResources.Enums;
using Validation.Implementations;

namespace UnitTests.ConsoleUI
{
	[TestClass]
	public class GameSelectionTests
	{
		private GameSelection gameSelector = null!;
		private StringWriter consoleOutput = null!;
		private StringReader consoleInput = null!;

		[TestInitialize]
		public void Setup()
		{
			gameSelector = new GameSelection(new GameSelectionValidation());
			consoleOutput = new StringWriter();
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
		public void SelectGameType_ValidInput_ReturnsBullsAndCowsGameType()
		{
			// Arrange
			SimulateInput(UserInputConstants.BullsAndCowsInput + "\n");

			// Act
			var result = gameSelector.SelectGameType();

			// Assert
			Assert.AreEqual(GameTypes.BullsAndCows, result, "Expected GameType to be BullsAndCows for input '1'.");
		}

		// Tests if valid input for 'MasterMind' correctly returns the corresponding GameType.
		[TestMethod]
		public void SelectGameType_ValidInput_ReturnsMasterMindGameType()
		{
			// Arrange
			SimulateInput(UserInputConstants.MasterMindInput + "\n");

			// Act
			var result = gameSelector.SelectGameType();

			// Assert
			Assert.AreEqual(GameTypes.MasterMind, result, "Expected GameType to be MasterMind for input '2'.");
		}

		// Tests if invalid input followed by a valid input shows an error message and returns the correct GameType.
		[TestMethod]
		public void SelectGameType_InvalidInput_ShowsErrorMessage()
		{
			// Arrange
			SimulateInput(TestConstants.InvalidInput + "\n" + UserInputConstants.BullsAndCowsInput + "\n");

			// Act
			var result = gameSelector.SelectGameType();

			// Assert
			StringAssert.Contains(consoleOutput.ToString(), UserInteractionMessages.GameSelectionInvalidMessage, "Expected error message for invalid input.");
			Assert.AreEqual(GameTypes.BullsAndCows, result, "Expected GameType to be BullsAndCows after invalid input followed by '1'.");
		}

		// Tests if input for quitting the game correctly returns the Quit GameType.
		[TestMethod]
		public void SelectGameType_InputQuit_ReturnsQuitGameType()
		{
			// Arrange
			SimulateInput(UserInputConstants.QuitInput + "\n");

			// Act
			var result = gameSelector.SelectGameType();

			// Assert
			Assert.AreEqual(GameTypes.Quit, result, "Expected GameType to be Quit for input '3'.");
		}

		// Tests if the game options are correctly displayed.
		[TestMethod]
		public void DisplayGameOptions_DisplaysOptionsCorrectly()
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
			Console.SetIn(Console.In);
		}
	}
}