using Laboration.GameResources.Constants;
using Laboration.HighScoreManagement.Implementations;
using Laboration.PlayerData.Implementations;
using Laboration.PlayerData.Interfaces;

namespace Laboration.UnitTests.HighScoreManagement
{
	[TestClass]
	public class HighScoreManagerTests
	{
		private readonly HighScoreManager _highScoreManager = new();
		private readonly List<IPlayerData> _results = [];

		// Verifies that SaveResult saves the result to a file.
		[TestMethod]
		public void SaveResult_SavesToFile()
		{
			// Act
			_highScoreManager.SaveResult(TestConstants.UserName, TestConstants.NumberOfGuesses);

			// Assert
			string[] lines = File.ReadAllLines(FileConstants.FilePath);
			Assert.IsTrue(lines.Length > 0, "The file should contain at least one line.");
			Assert.IsTrue(lines[0].Contains($"{TestConstants.UserName}{FileConstants.Separator}{TestConstants.NumberOfGuesses}"), "The first line should contain the correct formatted result.");
		}

		// Verifies that SaveResult appends new data to an existing file.
		[TestMethod]
		public void SaveResult_WithExistingFile_ShouldAppendData()
		{
			// Arrange
			var initialContent = $"{TestConstants.UserName}{FileConstants.Separator}{TestConstants.NumberOfGuesses}\n";
			File.WriteAllText(FileConstants.FilePath, initialContent);
			const string newUserName = "NewUser";
			const int newNumberOfGuesses = 20;

			// Act
			_highScoreManager.SaveResult(newUserName, newNumberOfGuesses);

			// Assert
			var lines = File.ReadAllLines(FileConstants.FilePath);
			Assert.AreEqual(2, lines.Length, "The file should contain two lines.");
			Assert.IsTrue(lines[1].Contains($"{newUserName}{FileConstants.Separator}{newNumberOfGuesses}"), "The new line should be correctly appended.");
		}

		// Verifies that ReadHighScoreResultsFromFile reads data from the file.
		[TestMethod]
		public void ReadHighScoreResultsFromFile_ReadsFromFile()
		{
			// Arrange
			File.WriteAllText(FileConstants.FilePath, $"{TestConstants.UserName}{FileConstants.Separator}{TestConstants.NumberOfGuesses}");

			// Act
			List<IPlayerData> results = _highScoreManager.ReadHighScoreResultsFromFile();

			// Assert
			Assert.AreEqual(1, results.Count, "The result list should contain exactly one player data.");
			Assert.AreEqual(TestConstants.UserName, results[0].UserName, "The user name should be 'TestUser'.");
			Assert.AreEqual(TestConstants.NumberOfGuesses, results[0].TotalGuesses, "The total guesses should be 10.");
		}

		// Verifies that ReadHighScoreResultsFromFile returns an empty list if the file does not exist.
		[TestMethod]
		public void ReadHighScoreResultsFromFile_FileDoesNotExist_ShouldReturnEmptyList()
		{
			// Arrange
			if (File.Exists(FileConstants.FilePath))
			{
				File.Delete(FileConstants.FilePath);
			}

			// Act
			var results = _highScoreManager.ReadHighScoreResultsFromFile();

			// Assert
			Assert.AreEqual(0, results.Count, "The result list should be empty.");
		}

		// Verifies that ParseLineToPlayerData correctly parses a line into player data.
		[TestMethod]
		public void ParseLineToPlayerData_ParsesLine()
		{
			// Arrange
			string line = $"{TestConstants.UserName}{FileConstants.Separator}{TestConstants.NumberOfGuesses}";

			// Act
			IPlayerData playerData = _highScoreManager.ParseLineToPlayerData(line);

			// Assert
			Assert.AreEqual(TestConstants.UserName, playerData.UserName, "The user name should be 'TestUser'.");
			Assert.AreEqual(TestConstants.NumberOfGuesses, playerData.TotalGuesses, "The total guesses should be 10.");
		}

		// Verifies that ParseLineToPlayerData throws a FormatException for invalid data.
		[TestMethod]
		public void ParseLineToPlayerData_InvalidData_ShouldThrowFormatException()
		{
			// Arrange
			var invalidLine = $"{TestConstants.UserName}{TestConstants.NumberOfGuesses}";

			// Act & Assert
			Assert.ThrowsException<FormatException>(() => _highScoreManager.ParseLineToPlayerData(invalidLine));
		}

		// Verifies that UpdateResultsList adds new player data to the results list.
		[TestMethod]
		public void UpdateResultsList_AddsNewData()
		{
			// Arrange
			IPlayerData playerData = new GamePlayerData(TestConstants.UserName, TestConstants.NumberOfGuesses);

			// Act
			List<IPlayerData> updatedResults = _highScoreManager.UpdateResultsList(_results, playerData);

			// Assert
			Assert.AreEqual(1, updatedResults.Count, "The updated results list should contain exactly one player data.");
			Assert.IsTrue(updatedResults.Contains(playerData), "The updated results list should contain the new player data.");
		}

		// Verifies that UpdateResultsList updates existing player data in the results list.
		[TestMethod]
		public void UpdateResultsList_UpdatesExistingData()
		{
			// Arrange
			IPlayerData playerData1 = new GamePlayerData(TestConstants.UserName, TestConstants.NumberOfGuesses);
			IPlayerData playerData2 = new GamePlayerData(TestConstants.UserName, 15);
			_results.Add(playerData1);

			// Act
			List<IPlayerData> updatedResults = _highScoreManager.UpdateResultsList(_results, playerData2);

			// Assert
			Assert.AreEqual(1, updatedResults.Count, "The updated results list should contain exactly one player data.");
			Assert.AreEqual(15, updatedResults[0].TotalGuesses, "The total guesses should be updated to 15.");
		}

		// Verifies that SortHighScoreList correctly sorts the results list by number of guesses.
		[TestMethod]
		public void SortHighScoreList_ValidData_SortsListCorrectly()
		{
			// Arrange
			var results = new List<IPlayerData>
			{
				new GamePlayerData("Player1", 10),
				new GamePlayerData("Player2", 5),
				new GamePlayerData("Player3", 8)
			};

			// Act
			_highScoreManager.SortHighScoreList(results);

			// Assert
			Assert.AreEqual(3, results.Count, "The results list should contain exactly three entries.");
			Assert.AreEqual("Player2", results[0].UserName, "The player with the lowest number of guesses should be in the first position after sorting.");
			Assert.AreEqual(5, results[0].TotalGuesses, "The number of guesses for Player2 should be 5.");

			Assert.AreEqual("Player3", results[1].UserName, "The player with the middle number of guesses should be in the second position after sorting.");
			Assert.AreEqual(8, results[1].TotalGuesses, "The number of guesses for Player3 should be 8.");

			Assert.AreEqual("Player1", results[2].UserName, "The player with the highest number of guesses should be in the last position after sorting.");
			Assert.AreEqual(10, results[2].TotalGuesses, "The number of guesses for Player1 should be 10.");
		}

		// Verifies that SortHighScoreList does not alter an empty list.
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

		// Verifies that SortHighScoreList does not alter a list with a single entry.
		[TestMethod]
		public void SortHighScoreList_SingleEntry_ShouldRemainUnchanged()
		{
			// Arrange
			var results = new List<IPlayerData>
			{
				new GamePlayerData(TestConstants.UserName, TestConstants.NumberOfGuesses)
			};

			// Act
			_highScoreManager.SortHighScoreList(results);

			// Assert
			Assert.AreEqual(1, results.Count, "The results list should contain exactly one entry.");
			Assert.AreEqual(TestConstants.UserName, results[0].UserName, "The single entry should be unchanged.");
			Assert.AreEqual(TestConstants.NumberOfGuesses, results[0].TotalGuesses, "The number of guesses should be 10.");
		}

		// Verifies that SortHighScoreList maintains order for entries with the same number of guesses.
		[TestMethod]
		public void SortHighScoreList_SameNumberOfGuesses_ShouldNotAlterOrder()
		{
			// Arrange
			var results = new List<IPlayerData>
			{
				new GamePlayerData("Player1", 10),
				new GamePlayerData("Player2", 10)
			};

			// Act
			_highScoreManager.SortHighScoreList(results);

			// Assert
			Assert.AreEqual("Player1", results[0].UserName, "Order should be consistent for same number of guesses.");
			Assert.AreEqual("Player2", results[1].UserName, "Order should be consistent for same number of guesses.");
		}

		// Verifies that SortHighScoreList correctly sorts when there are large numbers of guesses.
		[TestMethod]
		public void SortHighScoreList_WithLargeNumberOfGuesses_ShouldSortCorrectly()
		{
			// Arrange
			var results = new List<IPlayerData>
			{
				new GamePlayerData("Player1", int.MaxValue),
				new GamePlayerData("Player2", 5)
			};

			// Act
			_highScoreManager.SortHighScoreList(results);

			// Assert
			Assert.AreEqual("Player2", results[0].UserName, "The player with fewer guesses should be first.");
			Assert.AreEqual("Player1", results[1].UserName, "The player with large number of guesses should be last.");
		}

		// Cleans up test resources by deleting the file if it exists.
		[TestCleanup]
		public void TestCleanup()
		{
			try
			{
				if (File.Exists(FileConstants.FilePath))
				{
					File.Delete(FileConstants.FilePath);
				}
			}
			catch (Exception ex)
			{
				Assert.Fail($"Error cleaning up test resources: {ex.Message}");
			}
		}
	}
}