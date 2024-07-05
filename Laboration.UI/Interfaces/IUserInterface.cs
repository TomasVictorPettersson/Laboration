namespace Laboration.UI.Interfaces
{
    public interface IUserInterface
    {
        string GetUserName();

        void DisplayCorrectMessage(string secretNumber, int numberOfGuesses);

        bool AskToContinue();
    }
}