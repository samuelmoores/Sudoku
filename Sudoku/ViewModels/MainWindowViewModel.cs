using CommunityToolkit.Mvvm.Input;

namespace Sudoku.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public GameViewModel GameViewModel { get; set; } = new GameViewModel();
    public MenuViewModel MenuViewModel { get; set; }
    
    public MainWindowViewModel()
    {
        MenuViewModel = new MenuViewModel(GameViewModel); // fix: create it here
    }

    [RelayCommand]
    private void StartGame()
    {
        
    }
}