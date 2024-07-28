using Laboration.PlayerData.Implementations;

namespace Laboration.Tests.PlayerData
{
	[TestClass]
	public class PlayerDataTests
	{
		[TestMethod]
		public void AddGuess_IncreasesTotalGuessesAndGamesPlayed()
		{
			// Arrange
			BullsAndCowsPlayerData bullsAndCowsPlayerData = new("JohnDoe", 10);

			// Act
			bullsAndCowsPlayerData.AddGuess(5);

			// Assert
			Assert.AreEqual(15, bullsAndCowsPlayerData.TotalGuesses);
			Assert.AreEqual(2, bullsAndCowsPlayerData.TotalGamesPlayed);
		}

		[TestMethod]
		public void CalculateAverageGuesses_ReturnsCorrectAverage()
		{
			// Arrange
			BullsAndCowsPlayerData bullsAndCowsPlayerData = new("JohnDoe", 10);
			bullsAndCowsPlayerData.AddGuess(8);
			bullsAndCowsPlayerData.AddGuess(12);

			// Act
			double averageGuesses = bullsAndCowsPlayerData.CalculateAverageGuesses();

			// Assert
			Assert.AreEqual(10, averageGuesses);
		}

		[TestMethod]
		public void Equals_ReturnsTrueForSameUserName()
		{
			// Arrange
			BullsAndCowsPlayerData bullsAndCowsPlayerData1 = new("JohnDoe", 10);
			BullsAndCowsPlayerData bullsAndCowsPlayerData2 = new("JohnDoe", 5);

			// Act
			bool areEqual = bullsAndCowsPlayerData1.Equals(bullsAndCowsPlayerData2);

			// Assert
			Assert.IsTrue(areEqual);
		}

		[TestMethod]
		public void Equals_ReturnsFalseForDifferentUserName()
		{
			// Arrange
			BullsAndCowsPlayerData bullsAndCowsPlayerData1 = new("JohnDoe", 10);
			BullsAndCowsPlayerData playerData2 = new("JaneDoe", 5);

			// Act
			bool areEqual = bullsAndCowsPlayerData1.Equals(playerData2);

			// Assert
			Assert.IsFalse(areEqual);
		}

		[TestMethod]
		public void GetHashCode_ReturnsSameHashCodeForSameUserName()
		{
			// Arrange
			BullsAndCowsPlayerData bullsAndCowsPlayerData1 = new("JohnDoe", 10);
			BullsAndCowsPlayerData playerData2 = new("JohnDoe", 5);

			// Act
			int hashCode1 = bullsAndCowsPlayerData1.GetHashCode();
			int hashCode2 = playerData2.GetHashCode();

			// Assert
			Assert.AreEqual(hashCode1, hashCode2);
		}
	}
}