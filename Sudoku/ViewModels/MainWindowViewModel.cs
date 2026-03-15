using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using Sudoku.Models;

namespace Sudoku.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly List<SudokuPuzzle> _puzzles;
    public GameViewModel GameViewModel { get; set; }
    public MenuViewModel MenuViewModel { get; set; }
    
    public MainWindowViewModel()
    {
        _puzzles = SudokuFileReader.ReadPuzzles("sudoku_puzzles.txt");
        var game = new GameViewModel(); // empty placeholder
        MenuViewModel = new MenuViewModel(game);
    }
    
    public void StartGame()
    {
        var puzzle = _puzzles.FirstOrDefault(p => 
            p.Title.Contains(MenuViewModel.CurrentDifficulty));

        GameViewModel = puzzle != null
            ? new GameViewModel(puzzle)
            : new GameViewModel(MenuViewModel.CurrentDifficulty);
    }

}