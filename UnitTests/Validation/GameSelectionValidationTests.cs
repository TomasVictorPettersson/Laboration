using GameResources.Constants;
using GameResources.Enums;
using Validation.Implementations;

namespace UnitTests.Validation
{
	[TestClass]
	public class GameSelectionValidationTests
	{
		private readonly GameSelectionValidation _validation = new();

		// Verifies that the method correctly maps the input '1' to the GameTypes.BullsAndCows enum value.
		[TestMethod]
		public void ParseGameTypeInput_ShouldReturnBullsAndCows_ForInput1()
		{
			// Act
			var result = _validation.ParseGameTypeInput(UserInputConstants.BullsAndCowsInput);

			// Assert
			Assert.AreEqual(GameTypes.BullsAndCows, result, "Input '1' should map to GameTypes.BullsAndCows.");
		}

		// Verifies that the method correctly maps the input '2' to the GameTypes.MasterMind enum value.
		[TestMethod]
		public void ParseGameTypeInput_ShouldReturnMasterMind_ForInput2()
		{
			// Act
			var result = _validation.ParseGameTypeInput(UserInputConstants.MasterMindInput);

			// Assert
			Assert.AreEqual(GameTypes.MasterMind, result, "Input '2' should map to GameTypes.MasterMind.");
		}

		// Verifies that the method correctly maps the input '3' to the GameTypes.Quit enum value.
		[TestMethod]
		public void ParseGameTypeInput_ShouldReturnQuit_ForInput3()
		{
			// Act
			var result = _validation.ParseGameTypeInput(UserInputConstants.QuitInput);

			// Assert
			Assert.AreEqual(GameTypes.Quit, result, "Input '3' should map to GameTypes.Quit.");
		}

		// Verifies that the method returns null for an invalid input that does not correspond to any game type.
		[TestMethod]
		public void ParseGameTypeInput_ShouldReturnNull_ForInvalidInput()
		{
			// Act
			var result = _validation.ParseGameTypeInput(TestConstants.InvalidInput);

			// Assert
			Assert.IsNull(result, "Invalid input should return null.");
		}

		// Verifies that the method returns null when the input is null.
		[TestMethod]
		public void ParseGameTypeInput_ShouldReturnNull_ForNullInput()
		{
			// Act
			var result = _validation.ParseGameTypeInput(null);

			// Assert
			Assert.IsNull(result, "Null input should return null.");
		}
	}
}