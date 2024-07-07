namespace Laboration.Business.Interfaces
{
	public interface IGameLogic
	{
		void PlayGame(string userName);

		string MakeSecretNumber();

		string GenerateBullsAndCowsFeedback(string secretNumber, string guess);
	}
}