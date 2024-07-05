using Laboration.Data.Classes;

namespace Laboration.Data.Tests
{
	[TestClass]
	public class PlayerDataTests
	{
		[TestMethod]
		public void CalculateAverageGuesses_MultipleGuesses_ReturnsCorrectAverage()
		{
			// Arrange
			PlayerData player = new("User1", 10);
			player.AddGuess(15);
			player.AddGuess(20);

			// Act
			double averageGuesses = player.CalculateAverageGuesses();

			// Assert
			Assert.AreEqual(15, averageGuesses);
		}

		[TestMethod]
		public void Equals_SameUserName_ReturnsTrue()
		{
			// Arrange
			PlayerData player1 = new("User1", 10);
			PlayerData player2 = new("User1", 20);

			// Act
			bool areEqual = player1.Equals(player2);

			// Assert
			Assert.IsTrue(areEqual);
		}

		[TestMethod]
		public void Equals_DifferentUserName_ReturnsFalse()
		{
			// Arrange
			PlayerData player1 = new("User1", 10);
			PlayerData player2 = new("User2", 20);

			// Act
			bool areEqual = player1.Equals(player2);

			// Assert
			Assert.IsFalse(areEqual);
		}

		[TestMethod]
		public void GetHashCode_SameUserName_ReturnsSameHashCode()
		{
			// Arrange
			PlayerData player1 = new("User1", 10);
			PlayerData player2 = new("User1", 20);

			// Act
			int hashCode1 = player1.GetHashCode();
			int hashCode2 = player2.GetHashCode();

			// Assert
			Assert.AreEqual(hashCode1, hashCode2);
		}
	}
}