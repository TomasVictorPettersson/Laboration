using Laboration.Classes;

namespace Laboration.Interfaces
{
	public interface IHighScoreManager
	{
		List<PlayerData> ReadHighScoreResultsFromFile();

		void SortAndDisplayHighScoreList(List<PlayerData> results, string currentUserName);

		void DisplayHighScoreListHeader();

		void DisplayHighScoreListResults(List<PlayerData> results, string currentUserName);

		void DisplayRank(int rank, bool isCurrentUser);

		void DisplayPlayerData(PlayerData player, bool isCurrentUser);

		void ShowHighScoreList(string currentUserName);
	}
}