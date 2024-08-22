using Laboration.ConsoleUI.Interfaces;
using Laboration.GameLogic.Implementations;
using Laboration.GameResources.Constants;
using Laboration.GameResources.Enums;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Validation.Interfaces;
using Moq;

[TestClass]
public class BullsAndCowsGameLogicTests
{
	private BullsAndCowsGameLogic _gameLogic;
	private Mock<IHighScoreManager> _mockHighScoreManager;
	private Mock<IConsoleUI> _mockConsoleUI;
	private Mock<IValidation> _mockValidation;

	[TestInitialize]
	public void Setup()
	{
		_mockHighScoreManager = new Mock<IHighScoreManager>();
		_mockConsoleUI = new Mock<IConsoleUI>();
		_mockValidation = new Mock<IValidation>();
		_gameLogic = new BullsAndCowsGameLogic(
			_mockHighScoreManager.Object,
			_mockConsoleUI.Object,
			_mockValidation.Object
		);
	}

	[TestMethod]
	public void MakeSecretNumber_ShouldGenerateUnique4DigitNumber()
	{
		// Act
		string secretNumber = _gameLogic.MakeSecretNumber();

		// Assert
		Assert.AreEqual(4, secretNumber.Length, "Secret number should be 4 digits long.");
		Assert.IsTrue(HasUniqueDigits(secretNumber), "Secret number should have unique digits.");
	}

	[TestMethod]
	public void CountBulls_ShouldReturnCorrectNumberOfBulls()
	{
		// Arrange

		string guess = "1212";

		// Act
		int bulls = _gameLogic.CountBulls(TestConstants.SecretNumber, guess);

		// Assert
		Assert.AreEqual(2, bulls, "CountCows should return 2 for this guess.");
	}

	[TestMethod]
	public void CountCows_ShouldReturnCorrectNumberOfCows()
	{
		// Arrange

		string guess = "1325";

		// Act
		int cows = _gameLogic.CountCows(TestConstants.SecretNumber, guess);

		// Assert
		Assert.AreEqual(2, cows, "CountCows should return 2 for this guess.");
	}

	[TestMethod]
	public void GenerateFeedback_ShouldReturnBBBB_ForCorrectGuess()
	{
		// Act
		string feedback = _gameLogic.GenerateFeedback(TestConstants.SecretNumber, TestConstants.Guess);

		// Assert
		Assert.AreEqual("BBBB,", feedback, "Feedback should be 'BBBB,' for a correct guess.");
	}

	[TestMethod]
	public void GenerateFeedback_ShouldHandleGuessWithRepeatingDigits()
	{
		// Arrange
		string secretNumber = "5699";
		string guess = "1299";

		// Act
		string feedback = _gameLogic.GenerateFeedback(secretNumber, guess);

		// Assert
		Assert.AreEqual("BB,", feedback, "Feedback should correctly handle guesses with repeating digits.");
	}

	[TestMethod]
	public void GenerateFeedback_ShouldReturnCorrectFeedback_ForIncorrectGuess()
	{
		// Arrange
		const string guess = "1568";

		// Act
		string feedback = _gameLogic.GenerateFeedback(TestConstants.SecretNumber, guess);

		// Assert
		Assert.AreNotEqual(TestConstants.FeedbackBBBB, feedback, "Feedback should not be 'BBBB,' for incorrect guess.");
		Assert.IsTrue(feedback.Contains('B') || feedback.Contains('C'), "Feedback should contain 'B' or 'C' for incorrect guess.");
	}

	[TestMethod]
	public void GenerateFeedback_ShouldReturnCCCC_ForCorrectCows()
	{
		// Arrange
		const string guess = "4321";

		// Act
		string feedback = _gameLogic.GenerateFeedback(TestConstants.SecretNumber, guess);

		// Assert
		Assert.AreEqual(TestConstants.FeedbackCCCC, feedback, "Feedback should be ',CCCC' for correct cows.");
	}

	[TestMethod]
	public void GenerateFeedback_ShouldReturnMixedBullsAndCows_ForPartialMatch()
	{
		// Arrange
		const string guess = "1243";

		// Act
		string feedback = _gameLogic.GenerateFeedback(TestConstants.SecretNumber, guess);

		// Assert
		Assert.AreEqual(TestConstants.FeedbackBBCC, feedback, "Feedback should be 'BB,CC' for partial match.");
	}

	[TestMethod]
	public void GenerateFeedback_ShouldReturnBB_ForOnlyBulls()
	{
		// Arrange
		const string guess = "1259";

		// Act
		string feedback = _gameLogic.GenerateFeedback(TestConstants.SecretNumber, guess);

		// Assert
		Assert.AreEqual(TestConstants.FeedbackBBComma, feedback, "Feedback should be 'BB,' for only bulls.");
	}

	[TestMethod]
	public void GenerateFeedback_ShouldReturnCC_ForOnlyCows()
	{
		// Arrange
		const string guess = "3498";

		// Act
		string feedback = _gameLogic.GenerateFeedback(TestConstants.SecretNumber, guess);

		// Assert
		Assert.AreEqual(TestConstants.FeedbackCommaCC, feedback, "Feedback should be ',CC' for only cows.");
	}

	[TestMethod]
	public void GenerateFeedback_ShouldReturnComma_ForNoBullsOrCows()
	{
		// Arrange
		const string guess = "5678"; // A guess with no matching digits to the secret number.

		// Act
		string feedback = _gameLogic.GenerateFeedback(TestConstants.SecretNumber, guess);

		// Assert
		Assert.AreEqual(TestConstants.FeedbackComma, feedback, "Feedback should be ',' when no bulls or cows are found.");
	}

	[TestMethod]
	public void GetGameType_ShouldReturnBullsAndCows()
	{
		// Act
		GameTypes gameType = _gameLogic.GetGameType();

		// Assert
		Assert.AreEqual(GameTypes.BullsAndCows, gameType, "GetGameType should return BullsAndCows.");
	}

	private bool HasUniqueDigits(string number)
	{
		HashSet<char> digits = new HashSet<char>(number);
		return digits.Count == number.Length;
	}
}