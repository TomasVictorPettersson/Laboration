using Laboration.Common.Classes;
using Laboration.Data.Classes;
using Laboration.Data.Interfaces;

namespace Laboration.Tests.Common.Classes
{
	[TestClass]
	public class HighScoreManagerTests
	{
		private HighScoreManager _highScoreManager;

		[TestInitialize]
		public void Initialize()
		{
			_highScoreManager = new HighScoreManager();
		}

		[TestMethod]
		public void SaveResult_SavesResultToFile()
		{
			// Arrange
			const string userName = "TestUser";
			const int numberOfGuesses = 10;

			// Act
			_highScoreManager.SaveResult(userName, numberOfGuesses);

			// Assert
			string[] lines = File.ReadAllLines("result.txt");
			Assert.IsTrue(lines.Length > 0);
			Assert.IsTrue(lines[0].Contains("TestUser#&#10"));
		}

		[TestMethod]
		public void ReadHighScoreResultsFromFile_ReadsResultsFromFile()
		{
			// Arrange
			File.WriteAllText("result.txt", "TestUser#&#10");

			// Act
			List<IPlayerData> results = _highScoreManager.ReadHighScoreResultsFromFile();

			// Assert
			Assert.AreEqual(1, results.Count);
			Assert.AreEqual("TestUser", results[0].UserName);
			Assert.AreEqual(10, results[0].TotalGuesses);
		}

		[TestMethod]
		public void ParseLineToPlayerData_ParsesLineCorrectly()
		{
			// Arrange
			const string line = "TestUser#&#10";

			// Act
			IPlayerData playerData = _highScoreManager.ParseLineToPlayerData(line);

			// Assert
			Assert.AreEqual("TestUser", playerData.UserName);
			Assert.AreEqual(10, playerData.TotalGuesses);
		}

		[TestMethod]
		public void UpdateResultsList_AddsNewPlayerData()
		{
			// Arrange
			List<IPlayerData> results = new();
			IPlayerData playerData = new PlayerData("TestUser", 10);

			// Act
			List<IPlayerData> updatedResults = _highScoreManager.UpdateResultsList(results, playerData);

			// Assert
			Assert.AreEqual(1, updatedResults.Count);
			Assert.IsTrue(updatedResults.Contains(playerData));
		}

		[TestMethod]
		public void UpdateResultsList_UpdatesExistingPlayerData()
		{
			// Arrange
			List<IPlayerData> results = new();
			IPlayerData playerData1 = new PlayerData("TestUser", 10);
			IPlayerData playerData2 = new PlayerData("TestUser", 15);
			results.Add(playerData1);

			// Act
			List<IPlayerData> updatedResults = _highScoreManager.UpdateResultsList(results, playerData2);

			// Assert
			Assert.AreEqual(1, updatedResults.Count);
			Assert.AreEqual(25, updatedResults[0].TotalGuesses);
		}

		[TestMethod]
		public void SortAndDisplayHighScoreList_SortsAndDisplaysCorrectly()
		{
			// Arrange
			List<IPlayerData> results = new()
			{
				new PlayerData("User1", 10),
				new PlayerData("User2", 15),
				new PlayerData("User3", 5)
			};

			using StringWriter sw = new();
			Console.SetOut(sw);

			// Act
			_highScoreManager.SortAndDisplayHighScoreList(results, "CurrentUser");

			// Assert
			string expectedOutput = "=== High Score List ===\r\n";
			expectedOutput += "Rank     Player     Games     Average Guesses\r\n";
			expectedOutput += "---------------------------------------------\r\n";
			expectedOutput += "1        User3         1          5,00\r\n";
			expectedOutput += "2        User1         1         10,00\r\n";
			expectedOutput += "3        User2         1         15,00\r\n";

			Assert.AreEqual(expectedOutput, sw.ToString());
		}
	}
}