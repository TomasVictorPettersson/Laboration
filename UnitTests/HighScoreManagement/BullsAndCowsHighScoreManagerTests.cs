using Laboration.Data.Interfaces;
using Laboration.HighScoreManagement.Implementations;
using Laboration.PlayerData.Implementations;

namespace Laboration.Tests.HighScoreManagement
{
	[TestClass]
	public class BullsAndCowsHighScoreManagerTests
	{
		private readonly BullsAndCowsHighScoreManager _highScoreManager = new();
		private readonly List<IPlayerData> _results;

		public BullsAndCowsHighScoreManagerTests()
		{
			_results = [];
		}

		[TestMethod]
		public void SaveResult_SavesToFile()
		{
			// Arrange
			const string userName = "TestUser";
			const int numberOfGuesses = 10;

			// Act
			_highScoreManager.SaveResult(userName, numberOfGuesses);

			// Assert
			const string filePath = "result.txt";
			string[] lines = File.ReadAllLines(filePath);
			Assert.IsTrue(lines.Length > 0, "The file should contain at least one line.");
			Assert.IsTrue(lines[0].Contains("TestUser#&#10"), "The first line should contain 'TestUser#&#10'.");
		}

		[TestMethod]
		public void ReadResultsFromFile_ReadsFromFile()
		{
			// Arrange
			File.WriteAllText("result.txt", "TestUser#&#10");
			// Act
			List<IPlayerData> results = _highScoreManager.ReadHighScoreResultsFromFile();

			// Assert
			Assert.AreEqual(1, results.Count, "The result list should contain exactly one player data.");
			Assert.AreEqual("TestUser", results[0].UserName, "The user name should be 'TestUser'.");
			Assert.AreEqual(10, results[0].TotalGuesses, "The total guesses should be 10.");
		}

		[TestMethod]
		public void ParseLine_ParsesLine()
		{
			// Arrange
			const string line = "TestUser#&#10";

			// Act
			IPlayerData playerData = _highScoreManager.ParseLineToPlayerData(line);

			// Assert
			Assert.AreEqual("TestUser", playerData.UserName, "The user name should be 'TestUser'.");
			Assert.AreEqual(10, playerData.TotalGuesses, "The total guesses should be 10.");
		}

		[TestMethod]
		public void UpdateList_AddsNewData()
		{
			// Arrange
			IPlayerData playerData = new BullsAndCowsPlayerData("TestUser", 10);

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
			IPlayerData playerData1 = new BullsAndCowsPlayerData("TestUser", 10);
			IPlayerData playerData2 = new BullsAndCowsPlayerData("TestUser", 15);
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
			const string filePath = "result.txt";
			try
			{
				if (File.Exists(filePath))
				{
					File.Delete(filePath);
				}
			}
			catch (Exception ex)
			{
				Assert.Inconclusive($"Error cleaning up test resources: {ex.Message}");
			}
		}
	}
}