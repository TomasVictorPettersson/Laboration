using Laboration.Data.Interfaces;

namespace Laboration.HighScoreManagement.Interfaces
{
	public interface IHighScoreManager
	{
		void SaveResult(string userName, int numberOfGuesses);

		List<IPlayerData> ReadHighScoreResultsFromFile();

		IPlayerData ParseLineToPlayerData(string line);

		List<IPlayerData> UpdateResultsList(List<IPlayerData> results, IPlayerData playerData);

		void DisplayHighScoreList(string currentUserName);

		void SortHighScoreList(List<IPlayerData> results);

		void RenderHighScoreList(List<IPlayerData> results, string currentUserName);

		(int maxUserNameLength, int totalWidth) CalculateDisplayDimensions(List<IPlayerData> results);

		void DisplayHighScoreListHeader(int maxUserNameLength, int totalWidth);

		void PrintHighScoreResults(List<IPlayerData> results, string currentUserName, int maxUserNameLength);

		void DisplayRank(int rank, bool isCurrentUser);

		void DisplayPlayerData(IPlayerData player, bool isCurrentUser, int maxUserNameLength);
	}
}