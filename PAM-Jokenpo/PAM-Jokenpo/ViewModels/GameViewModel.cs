using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PAM_Jokenpo.Models;
using Microsoft.Maui.Controls;

namespace PAM_Jokenpo.ViewModels
{
    public partial class GameViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _userChoice;

        [ObservableProperty]
        private string _opponentChoice;

        [ObservableProperty]
        private string _resultMessage;

        private int _victories;
        public int Victories
        {
            get => _victories;
            set
            {
                SetProperty(ref _victories, value);
                OnPropertyChanged(nameof(VictoriesText));
            }
        }

        private int _defeats;
        public int Defeats
        {
            get => _defeats;
            set
            {
                SetProperty(ref _defeats, value);
                OnPropertyChanged(nameof(DefeatsText));
            }
        }

        public ObservableCollection<string> Options { get; }

        public ICommand PlayCommand { get; private set; }

        public GameViewModel()
        {
            Options = new ObservableCollection<string> { "Pedra", "Papel", "Tesoura" };
            PlayCommand = new Command(Play);
        }

        private void Play()
        {
            if (string.IsNullOrWhiteSpace(UserChoice))
            {
                ResultMessage = "Selecione uma opção!";
                return;
            }

            var game = new Game();
            ResultMessage = game.Play(UserChoice);
            OpponentChoice = game.OpponentChoice;

            if (ResultMessage.Contains("Parabéns"))
            {
                Victories = Victories + 1;
            }
            else if (ResultMessage.Contains("perdeu"))
            {
                Defeats = Defeats + 1;
            }

            CheckGameOver();
        }

        private void CheckGameOver()
        {
            if (Victories >= 10)
            {
                ShowGameOver("Você venceu o jogo!");
            }
            else if (Defeats >= 10)
            {
                ShowGameOver("Você perdeu o jogo!");
            }
        }

        private async void ShowGameOver(string message)
        {
            await Application.Current.MainPage.DisplayAlert("Fim de Jogo", message, "Recomeçar");

            Victories = 0;
            Defeats = 0;
            UserChoice = string.Empty;
            OpponentChoice = string.Empty;
            ResultMessage = string.Empty;
        }

        public string VictoriesText => $"Vitórias: {Victories}";
        public string DefeatsText => $"Derrotas: {Defeats}";
    }
}
