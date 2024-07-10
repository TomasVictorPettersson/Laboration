using Laboration.Business.Classes;
using Laboration.Common.Interfaces;
using Laboration.UI.Interfaces;
using Moq;

namespace Laboration.Tests.Business
{
	[TestClass]
	public class GameLogicTests
	{
		private Mock<IHighScoreManager> _highScoreManagerMock;
		private Mock<IUserInterface> _userInterfaceMock;
		private GameLogic _gameLogic;

		[TestInitialize]
		public void Initialize()
		{
			_highScoreManagerMock = new Mock<IHighScoreManager>();
			_userInterfaceMock = new Mock<IUserInterface>();
			_gameLogic = new GameLogic(_highScoreManagerMock.Object, _userInterfaceMock.Object);
		}

		[TestMethod]
		public void DisplayWelcomeMessage_WritesCorrectMessagesToConsole()
		{
			// Arrange
			const string userName = "JohnDoe";
			StringWriter sw = new();
			Console.SetOut(sw);

			// Act
			_gameLogic.DisplayWelcomeMessage(userName);
			string consoleOutput = sw.ToString();

			// Assert
			StringAssert.Contains(consoleOutput, $"Welcome {userName} to Bulls and Cows!");
			StringAssert.Contains(consoleOutput, "The objective of the game is to guess a 4-digit number.");
			StringAssert.Contains(consoleOutput, "For each guess, you will receive feedback in the form of 'BBBB,CCCC',");
			StringAssert.Contains(consoleOutput, "where 'BBBB' represents the number of bulls (correct digits in the correct positions),");
			StringAssert.Contains(consoleOutput, "and 'CCCC' represents the number of cows (correct digits in the wrong positions).\n");
		}

		[TestCleanup]
		public void Cleanup()
		{
			Console.SetOut(Console.Out);
		}

		[TestMethod]
		public void MakeSecretNumber_GeneratesUnique4DigitNumber()
		{
			// Arrange
			string secretNumber1 = _gameLogic.MakeSecretNumber();
			string secretNumber2 = _gameLogic.MakeSecretNumber();

			// Act and Assert
			Assert.AreNotEqual(secretNumber1, secretNumber2);
			Assert.IsTrue(secretNumber1.Length == 4 && secretNumber2.Length == 4);
			Assert.IsTrue(int.TryParse(secretNumber1, out _) && int.TryParse(secretNumber2, out _));
		}

		[TestMethod]
		public void ProcessGuess_ValidInput_GeneratesBullsAndCowsFeedback()
		{
			// Arrange
			string secretNumber = "1234";
			int numberOfGuesses = 0;

			// Redirect Console output to capture it
			using (StringWriter sw = new StringWriter())
			{
				Console.SetOut(sw);

				// Act
				string guess = "1234";
				string result = _gameLogic.ProcessGuess(secretNumber, ref numberOfGuesses);

				// Assert
				Assert.AreEqual("BBBB,", result); // Assuming ProcessGuess returns the guess feedback
				Assert.AreEqual("BBBB,", sw.ToString().Split("\n")[1].Trim()); // Check the output printed to console
			}
		}

		[TestMethod]
		public void ProcessGuess_InvalidInput_ReturnsEmptyString()
		{
			// Arrange
			string secretNumber = "1234";
			int numberOfGuesses = 0;

			// Redirect Console output to capture it
			using (StringWriter sw = new StringWriter())
			{
				Console.SetOut(sw);

				// Act
				string guess = "abcd"; // Invalid input
				string result = _gameLogic.ProcessGuess(secretNumber, ref numberOfGuesses);

				// Assert
				Assert.AreEqual(string.Empty, result); // Ensure empty string is returned for invalid input
				StringAssert.Contains(sw.ToString(), "Invalid input. Please enter a 4-digit number."); // Check error message in console output
			}
		}

		[TestMethod]
		public void ProcessGuess_ValidGuessButIncorrect_GeneratesBullsAndCowsFeedback()
		{
			// Arrange
			string secretNumber = "1234";
			int numberOfGuesses = 0;

			// Redirect Console output to capture it
			using (StringWriter sw = new StringWriter())
			{
				Console.SetOut(sw);

				// Act
				string guess = "5678"; // Incorrect guess
				string result = _gameLogic.ProcessGuess(secretNumber, ref numberOfGuesses);

				// Assert
				Assert.AreEqual("0,0", result); // Assuming ProcessGuess returns the guess feedback
				Assert.AreEqual(1, numberOfGuesses); // Check if guess counter is incremented correctly
				Assert.AreEqual("0,0", sw.ToString().Split("\n")[1].Trim()); // Check the output printed to console
			}
		}

		[TestMethod]
		public void GenerateBullsAndCowsFeedback_CorrectGuess_ReturnsBBBB()
		{
			// Arrange
			const string secretNumber = "1234";
			const string guess = "1234";

			// Act
			string feedback = _gameLogic.GenerateBullsAndCowsFeedback(secretNumber, guess);

			// Assert
			Assert.AreEqual("BBBB,", feedback);
		}

		[TestMethod]
		public void GenerateBullsAndCowsFeedback_IncorrectGuess_ReturnsCorrectBullsAndCows()
		{
			// Arrange
			const string secretNumber = "1234";
			const string guess = "1568";

			// Act
			string feedback = _gameLogic.GenerateBullsAndCowsFeedback(secretNumber, guess);

			// Assert
			Assert.AreNotEqual("BBBB,", feedback);
		}
	}
}