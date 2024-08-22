﻿using Laboration.ConsoleUI.Interfaces;
using Laboration.GameLogic.Implementations;
using Laboration.GameResources.Enums;
using Laboration.HighScoreManagement.Interfaces;
using Laboration.Validation.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Laboration.UnitTests.GameLogic
{
	[TestClass]
	public class BullsAndCowsGameLogicTests
	{
		private BullsAndCowsGameLogic _gameLogic;
		private Mock<IHighScoreManager> _mockHighScoreManager;
		private Mock<IConsoleUI> _mockConsoleUI;
		private Mock<IValidation> _mockValidation;

		[TestInitialize]
		public void Setup()
		{
			_mockHighScoreManager = new Mock<IHighScoreManager>();
			_mockConsoleUI = new Mock<IConsoleUI>();
			_mockValidation = new Mock<IValidation>();
			_gameLogic = new BullsAndCowsGameLogic(
				_mockHighScoreManager.Object,
				_mockConsoleUI.Object,
				_mockValidation.Object
			);
		}

		[TestMethod]
		public void MakeSecretNumber_ShouldGenerateUnique4DigitNumber()
		{
			// Act
			string secretNumber = _gameLogic.MakeSecretNumber();

			// Assert
			Assert.AreEqual(4, secretNumber.Length, "Secret number should be 4 digits long.");
			Assert.IsTrue(HasUniqueDigits(secretNumber), "Secret number should have unique digits.");
		}

		[TestMethod]
		public void CountCows_ShouldReturnCorrectNumberOfCows()
		{
			// Arrange
			string secretNumber = "1234";
			string guess = "1325";

			// Act
			int cows = _gameLogic.CountCows(secretNumber, guess);

			// Assert
			Assert.AreEqual(2, cows, "CountCows should return 2 for this guess.");
		}

		[TestMethod]
		public void GetGameType_ShouldReturnBullsAndCows()
		{
			// Act
			GameTypes gameType = _gameLogic.GetGameType();

			// Assert
			Assert.AreEqual(GameTypes.BullsAndCows, gameType, "GetGameType should return BullsAndCows.");
		}

		private bool HasUniqueDigits(string number)
		{
			HashSet<char> digits = new HashSet<char>(number);
			return digits.Count == number.Length;
		}
	}
}