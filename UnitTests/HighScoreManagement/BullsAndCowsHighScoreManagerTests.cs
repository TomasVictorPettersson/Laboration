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

		private const string Separator = "#&#";
		private const string TestUserName = "TestUser";
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

		[TestCleanup]
		public void TestCleanup()
		{
			// Clean up any resources here
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