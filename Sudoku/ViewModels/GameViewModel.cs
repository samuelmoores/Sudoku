using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Sudoku.Models;

namespace Sudoku.ViewModels;

public partial class GameViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _difficulty = "Medium";
    
    public int[,]? LoadedPuzzle { get; set; }
    public int[,]? LoadedSolution { get; set; }
    public bool IsFromFile => LoadedPuzzle != null;

    public GameViewModel()
    {
        LoadedPuzzle = null;
        LoadedSolution = null;  
        Console.WriteLine("null loaded puzzle" + Environment.StackTrace);
    }
    
    // Constructor for generated puzzles (existing behavior)
    public GameViewModel(string difficulty)
    {
        Difficulty = difficulty;
    }

    // Constructor for file-loaded puzzles
    public GameViewModel(SudokuPuzzle puzzle)
    {
        Difficulty = puzzle.Title;
        LoadedPuzzle = puzzle.Puzzle;
        LoadedSolution = puzzle.Solution;
    }
    
}