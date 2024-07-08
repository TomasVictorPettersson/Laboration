using Laboration.Data.Interfaces;

namespace Laboration.Common.Interfaces
{
	public interface IHighScoreManager
	{
		void SaveResult(string userName, int numberOfGuesses);

		List<IPlayerData> ReadHighScoreResultsFromFile();

		IPlayerData ParseLineToPlayerData(string line);

		List<IPlayerData> UpdateResultsList(List<IPlayerData> results, IPlayerData playerData);

		void SortAndDisplayHighScoreList(List<IPlayerData> results, string currentUserName);

		void DisplayHighScoreListHeader();

		void DisplayHighScoreListResults(List<IPlayerData> results, string currentUserName);

		void DisplayRank(int rank, bool isCurrentUser);

		void DisplayPlayerData(IPlayerData player, bool isCurrentUser);

		void ShowHighScoreList(string currentUserName);
	}
}