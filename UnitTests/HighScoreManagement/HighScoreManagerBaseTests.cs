using Laboration.GameResources.Constants;
using Laboration.HighScoreManagement.Implementations;
using Laboration.PlayerData.Implementations;
using Laboration.PlayerData.Interfaces;

namespace Laboration.UnitTests.HighScoreManagement
{
	[TestClass]
	public class HighScoreManagerBaseTests
	{
		private readonly TestHighScoreManager _highScoreManager = new();

		// Verifies that the SaveResult method creates a file and saves the result correctly.

		[TestMethod]
		public void SaveResult_ShouldCreateFileAndSaveResult()
		{
			// Act
			_highScoreManager.SaveResult(TestConstants.UserName, TestConstants.NumberOfGuesses);

			// Assert
			Assert.IsTrue(File.Exists(_highScoreManager.GetFilePath()), "The file should be created.");
			var lines = File.ReadAllLines(_highScoreManager.GetFilePath());
			Assert.IsTrue(lines.Length > 0, "The file should contain at least one line.");
			Assert.IsTrue(lines[0].Contains($"{TestConstants.UserName}{FileConstants.Separator}{TestConstants.NumberOfGuesses}"), "The first line should contain the correct formatted result.");
		}

		// Verifies that the SaveResult method appends new results to an existing file correctly.

		[TestMethod]
		public void SaveResult_ShouldAppendToExistingFile()
		{
			// Arrange
			var initialContent = $"{TestConstants.UserNameJohnDoe}{FileConstants.Separator}5\n";
			File.WriteAllText(_highScoreManager.GetFilePath(), initialContent);
			const string newUserName = TestConstants.UserNameJaneDoe;
			const int newNumberOfGuesses = 20;

			// Act
			_highScoreManager.SaveResult(newUserName, newNumberOfGuesses);

			// Assert
			var lines = File.ReadAllLines(_highScoreManager.GetFilePath());
			Assert.AreEqual(2, lines.Length, "The file should contain two lines.");
			Assert.IsTrue(lines[1].Contains($"{newUserName}{FileConstants.Separator}{newNumberOfGuesses}"), "The new line should be correctly appended.");
		}

		// Verifies that the ReadHighScoreResultsFromFile method reads results from a file correctly.

		[TestMethod]
		public void ReadHighScoreResultsFromFile_ShouldReadResultsCorrectly()
		{
			// Arrange
			var content = $"{TestConstants.UserName}{FileConstants.Separator}{TestConstants.NumberOfGuesses}\n";
			File.WriteAllText(_highScoreManager.GetFilePath(), content);

			// Act
			var results = _highScoreManager.ReadHighScoreResultsFromFile();

			// Assert
			Assert.AreEqual(1, results.Count, "The result list should contain exactly one player data.");
			Assert.AreEqual(TestConstants.UserName, results[0].UserName, "The user name should be 'TestUser'.");
			Assert.AreEqual(TestConstants.NumberOfGuesses, results[0].TotalGuesses, "The total guesses should be 10.");
		}

		// Verifies that the ReadHighScoreResultsFromFile method returns
		// an empty list when the file does not exist.

		[TestMethod]
		public void ReadHighScoreResultsFromFile_WhenFileDoesNotExist_ShouldReturnEmptyList()
		{
			// Arrange
			if (File.Exists(_highScoreManager.GetFilePath()))
			{
				File.Delete(_highScoreManager.GetFilePath());
			}

			// Act
			var results = _highScoreManager.ReadHighScoreResultsFromFile();

			// Assert
			Assert.AreEqual(0, results.Count, "The result list should be empty.");
		}

		// Verifies that the ParseLineToPlayerData method correctly parses a line into player data.

		[TestMethod]
		public void ParseLineToPlayerData_ShouldReturnPlayerData()
		{
			// Arrange
			var line = $"{TestConstants.UserName}{FileConstants.Separator}15";

			// Act
			var playerData = _highScoreManager.ParseLineToPlayerData(line);

			// Assert
			Assert.AreEqual(TestConstants.UserName, playerData.UserName, "The user name should be 'TestUser'.");
			Assert.AreEqual(15, playerData.TotalGuesses, "The total guesses should be 15.");
		}

		// Verifies that the ParseLineToPlayerData method throws a
		// FormatException when given an invalid format.

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void ParseLineToPlayerData_InvalidFormat_ShouldThrowFormatException()
		{
			// Arrange
			const string line = "InvalidFormatLine";

			// Act
			_highScoreManager.ParseLineToPlayerData(line);
		}

		// Verifies that the UpdateResultsList method adds a new player to the results list.

		[TestMethod]
		public void UpdateResultsList_ShouldAddNewPlayer()
		{
			// Arrange
			var results = new List<IPlayerData>();
			var playerData = new GamePlayerData(TestConstants.UserName, TestConstants.NumberOfGuesses);

			// Act
			var updatedResults = _highScoreManager.UpdateResultsList(results, playerData);

			// Assert
			Assert.AreEqual(1, updatedResults.Count, "The result list should contain one player.");
			Assert.AreEqual(playerData, updatedResults[0], "The player data should match.");
		}

		// Verifies that the UpdateResultsList method updates an existing player’s data in the results list.

		[TestMethod]
		public void UpdateResultsList_ShouldUpdateExistingPlayer()
		{
			// Arrange
			var playerData1 = new GamePlayerData(TestConstants.UserName, TestConstants.NumberOfGuesses);
			var results = new List<IPlayerData> { playerData1 };
			var playerData2 = new GamePlayerData(TestConstants.UserName, 20); // Same user but different guess count

			// Act
			var updatedResults = _highScoreManager.UpdateResultsList(results, playerData2);

			// Assert
			Assert.AreEqual(1, updatedResults.Count, "The result list should still contain one player.");
			Assert.AreEqual(15, updatedResults[0].CalculateAverageGuesses(), "The average number of guesses should be updated.");
		}

		// Verifies that the SortHighScoreList method sorts the results by number of guesses.

		[TestMethod]
		public void SortHighScoreList_ShouldSortResults()
		{
			// Arrange
			var playerData1 = new GamePlayerData("User1", 10);
			var playerData2 = new GamePlayerData("User2", 5);
			var results = new List<IPlayerData> { playerData1, playerData2 };

			// Act
			_highScoreManager.SortHighScoreList(results);

			// Assert
			Assert.AreEqual("User2", results[0].UserName, "The first player should be 'User2' with the lower number of guesses.");
			Assert.AreEqual("User1", results[1].UserName, "The second player should be 'User1' with the higher number of guesses.");
		}

		// Cleans up test resources by deleting the test file if it exists.

		[TestCleanup]
		public void Cleanup()
		{
			try
			{
				if (File.Exists(_highScoreManager.GetFilePath()))
				{
					File.Delete(_highScoreManager.GetFilePath());
				}
			}
			catch (Exception ex)
			{
				Assert.Fail($"Error cleaning up test resources: {ex.Message}");
			}
		}
	}
}