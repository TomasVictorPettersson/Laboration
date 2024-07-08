using Laboration.Data.Classes;
using Laboration.Data.Interfaces;

namespace Laboration.Tests.Data
{
	[TestClass]
	public class PlayerDataTests
	{
		[TestMethod]
		public void AddGuess_IncreasesTotalGuessesAndTotalGamesPlayed()
		{
			// Arrange
			IPlayerData playerData = new PlayerData("JohnDoe", 10);

			// Act
			playerData.AddGuess(5);

			// Assert
			Assert.AreEqual(15, playerData.TotalGuesses);
			Assert.AreEqual(2, playerData.TotalGamesPlayed);
		}

		[TestMethod]
		public void CalculateAverageGuesses_ReturnsCorrectAverage()
		{
			// Arrange
			IPlayerData playerData = new PlayerData("JohnDoe", 10);
			playerData.AddGuess(8);
			playerData.AddGuess(12);

			// Act
			double averageGuesses = playerData.CalculateAverageGuesses();

			// Assert
			Assert.AreEqual(10, averageGuesses);
		}

		[TestMethod]
		public void Equals_ReturnsTrueForSameUserName()
		{
			// Arrange
			IPlayerData playerData1 = new PlayerData("JohnDoe", 10);
			IPlayerData playerData2 = new PlayerData("JohnDoe", 5);

			// Act
			bool areEqual = playerData1.Equals(playerData2);

			// Assert
			Assert.IsTrue(areEqual);
		}

		[TestMethod]
		public void Equals_ReturnsFalseForDifferentUserName()
		{
			// Arrange
			IPlayerData playerData1 = new PlayerData("JohnDoe", 10);
			IPlayerData playerData2 = new PlayerData("JaneDoe", 5);

			// Act
			bool areEqual = playerData1.Equals(playerData2);

			// Assert
			Assert.IsFalse(areEqual);
		}

		[TestMethod]
		public void GetHashCode_ReturnsSameHashCodeForSameUserName()
		{
			// Arrange
			IPlayerData playerData1 = new PlayerData("JohnDoe", 10);
			IPlayerData playerData2 = new PlayerData("JohnDoe", 5);

			// Act
			int hashCode1 = playerData1.GetHashCode();
			int hashCode2 = playerData2.GetHashCode();

			// Assert
			Assert.AreEqual(hashCode1, hashCode2);
		}
	}
}