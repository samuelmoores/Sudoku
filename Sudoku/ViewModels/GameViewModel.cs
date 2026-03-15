using CommunityToolkit.Mvvm.ComponentModel;

namespace Sudoku.ViewModels;

public partial class GameViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _difficulty = "Normal";
}