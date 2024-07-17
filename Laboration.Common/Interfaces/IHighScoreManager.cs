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

		void DisplayHighScoreListHeader(int maxUserNameLength, int totalWidth);

		void DisplayHighScoreListResults(List<IPlayerData> results, string currentUserName,
			int maxUserNameLength, int totalWidth);

		void DisplayRank(int rank, bool isCurrentUser);

		void DisplayPlayerData(IPlayerData player, bool isCurrentUser, int maxUserNameLength);

		void ShowHighScoreList(string currentUserName);
	}
}