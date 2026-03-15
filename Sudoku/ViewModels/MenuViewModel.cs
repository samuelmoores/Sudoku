using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Sudoku.ViewModels;

public partial class MenuViewModel : ViewModelBase
{
    private readonly GameViewModel _game;

    public MenuViewModel(GameViewModel game)
    {
        _game = game;
    }

    [RelayCommand]
    private void SetDifficulty(string difficulty)
    {
        _game.Difficulty = difficulty;
        OnPropertyChanged(nameof(CurrentDifficulty)); // notify UI to update
    }

    public string CurrentDifficulty => _game.Difficulty;
}