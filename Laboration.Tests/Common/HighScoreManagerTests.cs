using Laboration.Common.Classes;
using Laboration.Data.Interfaces;

namespace Laboration.Common.Tests
{
	[TestClass]
	public class HighScoreManagerTests
	{
		private HighScoreManager _highScoreManager;

		[TestInitialize]
		public void Setup()
		{
			_highScoreManager = new HighScoreManager();
		}

		[TestMethod]
		public void ReadHighScoreResultsFromFile_ValidFile_ReturnsCorrectData()
		{
			// Arrange
			// Create a mock file with some data
			string[] fileContent = ["User1#&#10", "User2#&#20", "User1#&#15"];
			File.WriteAllLines("result.txt", fileContent);

			// Act
			List<IPlayerData> results = _highScoreManager.ReadHighScoreResultsFromFile();

			// Assert
			Assert.AreEqual(2, results.Count);
			Assert.AreEqual("User1", results[0].UserName);
			Assert.AreEqual(12.5, results[0].CalculateAverageGuesses());
			Assert.AreEqual("User2", results[1].UserName);
			Assert.AreEqual(20, results[1].CalculateAverageGuesses());

			// Clean up
			File.Delete("result.txt");
		}

		[TestMethod]
		public void ReadHighScoreResultsFromFile_EmptyFile_ReturnsEmptyList()
		{
			// Arrange
			// Create an empty mock file
			File.WriteAllText("result.txt", string.Empty);

			// Act
			List<IPlayerData> results = _highScoreManager.ReadHighScoreResultsFromFile();

			// Assert
			Assert.AreEqual(0, results.Count);

			// Clean up
			File.Delete("result.txt");
		}
	}
}