using CommunityToolkit.Mvvm.Input;

namespace Sudoku.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";

    [RelayCommand]
    private void StartGame()
    {
        
    }
}