namespace PAM_Jokenpo.Models;

public class Game
{
    public string UserChoice { get; set; }
    public string OpponentChoice { get; set; }

    public string Play(string userChoice)
    {
        string[] choices = { "Pedra", "Papel", "Tesoura" };
        var random = new Random();
        OpponentChoice = choices[random.Next(choices.Length)];
        UserChoice = userChoice;

        if (UserChoice == OpponentChoice)
        {
            return $"Empate! Ambos escolheram {UserChoice}.";
        }
        else if ((UserChoice == "Pedra" && OpponentChoice == "Tesoura") ||
                 (UserChoice == "Papel" && OpponentChoice == "Pedra") ||
                 (UserChoice == "Tesoura" && OpponentChoice == "Papel"))
        {
            return $"Parabéns, você venceu! Você escolheu {UserChoice} e o oponente escolheu {OpponentChoice}.";
        }
        else
        {
            return $"Você perdeu! Você escolheu {UserChoice} e o oponente escolheu {OpponentChoice}.";
        }
    }
}
