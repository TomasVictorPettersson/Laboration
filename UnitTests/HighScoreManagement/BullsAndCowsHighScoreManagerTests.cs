using Laboration.HighScoreManagement.Implementations;
using Laboration.PlayerData.Implementations;
using Laboration.PlayerData.Interfaces;

namespace Laboration.UnitTests.HighScoreManagement
{
	[TestClass]
	public class BullsAndCowsHighScoreManagerTests
	{
		// Constants
		private const string FilePath = "result.txt";

		private const string TestUserName = "TestUser";
		private const string Separator = "#&#";
		private const int TestNumberOfGuesses = 10;

		private readonly BullsAndCowsHighScoreManager _highScoreManager = new();
		private readonly List<IPlayerData> _results = [];

		[TestMethod]
		public void SaveResult_SavesToFile()
		{
			// Act
			_highScoreManager.SaveResult(TestUserName, TestNumberOfGuesses);

			// Assert
			string[] lines = File.ReadAllLines(FilePath);
			Assert.IsTrue(lines.Length > 0, "The file should contain at least one line.");
			Assert.IsTrue(lines[0].Contains($"{TestUserName}{Separator}{TestNumberOfGuesses}"), "The first line should contain the correct formatted result.");
		}

		[TestMethod]
		public void SaveResult_WithExistingFile_ShouldAppendData()
		{
			// Arrange
			var initialContent = $"{TestUserName}{Separator}{TestNumberOfGuesses}\n";
			File.WriteAllText(FilePath, initialContent);
			var newUserName = "NewUser";
			var newNumberOfGuesses = 20;

			// Act
			_highScoreManager.SaveResult(newUserName, newNumberOfGuesses);

			// Assert
			var lines = File.ReadAllLines(FilePath);
			Assert.AreEqual(2, lines.Length, "The file should contain two lines.");
			Assert.IsTrue(lines[1].Contains($"{newUserName}{Separator}{newNumberOfGuesses}"), "The new line should be correctly appended.");
		}

		[TestMethod]
		public void ReadResultsFromFile_ReadsFromFile()
		{
			// Arrange
			File.WriteAllText(FilePath, $"{TestUserName}{Separator}{TestNumberOfGuesses}");

			// Act
			List<IPlayerData> results = _highScoreManager.ReadHighScoreResultsFromFile();

			// Assert
			Assert.AreEqual(1, results.Count, "The result list should contain exactly one player data.");
			Assert.AreEqual(TestUserName, results[0].UserName, "The user name should be 'TestUser'.");
			Assert.AreEqual(TestNumberOfGuesses, results[0].TotalGuesses, "The total guesses should be 10.");
		}

		[TestMethod]
		public void ReadHighScoreResultsFromFile_FileDoesNotExist_ShouldReturnEmptyList()
		{
			// Arrange
			if (File.Exists(FilePath))
			{
				File.Delete(FilePath);
			}

			// Act
			var results = _highScoreManager.ReadHighScoreResultsFromFile();

			// Assert
			Assert.AreEqual(0, results.Count, "The result list should be empty.");
		}

		[TestMethod]
		public void ParseLine_ParsesLine()
		{
			// Arrange
			string line = $"{TestUserName}{Separator}{TestNumberOfGuesses}";

			// Act
			IPlayerData playerData = _highScoreManager.ParseLineToPlayerData(line);

			// Assert
			Assert.AreEqual(TestUserName, playerData.UserName, "The user name should be 'TestUser'.");
			Assert.AreEqual(TestNumberOfGuesses, playerData.TotalGuesses, "The total guesses should be 10.");
		}

		[TestMethod]
		public void ParseLineToPlayerData_InvalidData_ShouldThrowFormatException()
		{
			// Arrange
			var invalidLine = $"{TestUserName}{TestNumberOfGuesses}";

			// Act & Assert
			Assert.ThrowsException<FormatException>(() => _highScoreManager.ParseLineToPlayerData(invalidLine));
		}

		[TestMethod]
		public void UpdateList_AddsNewData()
		{
			// Arrange
			IPlayerData playerData = new BullsAndCowsPlayerData(TestUserName, TestNumberOfGuesses);

			// Act
			List<IPlayerData> updatedResults = _highScoreManager.UpdateResultsList(_results, playerData);

			// Assert
			Assert.AreEqual(1, updatedResults.Count, "The updated results list should contain exactly one player data.");
			Assert.IsTrue(updatedResults.Contains(playerData), "The updated results list should contain the new player data.");
		}

		[TestMethod]
		public void UpdateList_UpdatesExistingData()
		{
			// Arrange
			IPlayerData playerData1 = new BullsAndCowsPlayerData(TestUserName, TestNumberOfGuesses);
			IPlayerData playerData2 = new BullsAndCowsPlayerData(TestUserName, 15);
			_results.Add(playerData1);

			// Act
			List<IPlayerData> updatedResults = _highScoreManager.UpdateResultsList(_results, playerData2);

			// Assert
			Assert.AreEqual(1, updatedResults.Count, "The updated results list should contain exactly one player data.");
			Assert.AreEqual(25, updatedResults[0].TotalGuesses, "The total guesses should be updated to 25.");
		}

		[TestMethod]
		public void SortHighScoreList_ValidData_SortsListCorrectly()
		{
			// Arrange
			var results = new List<IPlayerData>
			{
				new BullsAndCowsPlayerData("Player1", 10),
				new BullsAndCowsPlayerData("Player2", 5),
				new BullsAndCowsPlayerData("Player3", 8)
			};

			// Act
			_highScoreManager.SortHighScoreList(results);

			// Assert
			Assert.AreEqual(3, results.Count, "The results list should contain exactly three entries.");
			Assert.AreEqual("Player2", results[0].UserName,
				"The player with the lowest number of guesses should be in the first position after sorting.");
			Assert.AreEqual(5, results[0].TotalGuesses,
				"The number of guesses for Player2 should be 5.");

			Assert.AreEqual("Player3", results[1].UserName,
				"The player with the middle number of guesses should be in the second position after sorting.");
			Assert.AreEqual(8, results[1].TotalGuesses,
				"The number of guesses for Player3 should be 8.");

			Assert.AreEqual("Player1", results[2].UserName,
				"The player with the highest number of guesses should be in the last position after sorting.");
			Assert.AreEqual(10, results[2].TotalGuesses,
				"The number of guesses for Player1 should be 10.");
		}

		[TestMethod]
		public void SortHighScoreList_EmptyList_ShouldRemainEmpty()
		{
			// Arrange
			var results = new List<IPlayerData>();

			// Act
			_highScoreManager.SortHighScoreList(results);

			// Assert
			Assert.AreEqual(0, results.Count, "The results list should remain empty.");
		}

		[TestMethod]
		public void SortHighScoreList_SingleEntry_ShouldRemainUnchanged()
		{
			// Arrange
			var results = new List<IPlayerData>
	{
		new BullsAndCowsPlayerData(TestUserName, TestNumberOfGuesses)
	};

			// Act
			_highScoreManager.SortHighScoreList(results);

			// Assert
			Assert.AreEqual(1, results.Count, "The results list should contain exactly one entry.");
			Assert.AreEqual(TestUserName, results[0].UserName, "The single entry should be unchanged.");
			Assert.AreEqual(TestNumberOfGuesses, results[0].TotalGuesses, "The number of guesses should be 10.");
		}

		[TestMethod]
		public void SortHighScoreList_SameNumberOfGuesses_ShouldNotAlterOrder()
		{
			// Arrange
			var results = new List<IPlayerData>
		 {
			 new BullsAndCowsPlayerData("Player1", 10),
			 new BullsAndCowsPlayerData("Player2", 10)
		 };

			// Act
			_highScoreManager.SortHighScoreList(results);

			// Assert
			Assert.AreEqual("Player1", results[0].UserName, "Order should be consistent for same number of guesses.");
			Assert.AreEqual("Player2", results[1].UserName, "Order should be consistent for same number of guesses.");
		}

		[TestMethod]
		public void SortHighScoreList_WithLargeNumberOfGuesses_ShouldSortCorrectly()
		{
			// Arrange
			var results = new List<IPlayerData>
		 {
			 new BullsAndCowsPlayerData("Player1", int.MaxValue),
			 new BullsAndCowsPlayerData("Player2", 5)
		 };

			// Act
			_highScoreManager.SortHighScoreList(results);

			// Assert
			Assert.AreEqual("Player2", results[0].UserName, "The player with fewer guesses should be first.");
			Assert.AreEqual("Player1", results[1].UserName, "The player with large number of guesses should be last.");
		}

		[TestCleanup]
		public void TestCleanup()
		{
			try
			{
				if (File.Exists(FilePath))
				{
					File.Delete(FilePath);
				}
			}
			catch (Exception ex)
			{
				Assert.Fail($"Error cleaning up test resources: {ex.Message}");
			}
		}
	}
}