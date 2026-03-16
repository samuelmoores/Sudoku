using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using Sudoku.Models;

namespace Sudoku.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly List<SudokuPuzzle> _puzzles;
    private readonly SaveData _saveData;
    public GameViewModel GameViewModel { get; set; }
    public MenuViewModel MenuViewModel { get; set; }
    
    public Action? OnGameReady { get; set; }
    
    public MainWindowViewModel()
    {
        _puzzles = SudokuFileReader.ReadPuzzles("sudoku_puzzles.txt");
        var game = new GameViewModel(); // empty placeholder
        _saveData = SaveManager.Load() ?? new SaveData();
        MenuViewModel = new MenuViewModel(game, LoadGame, _saveData.Saves);
    }
    
    public void StartGame()
    {
        Console.WriteLine($"Looking for difficulty: '{MenuViewModel.CurrentDifficulty}'");
        foreach (var p in _puzzles)
            Console.WriteLine($"  Puzzle - Title: '{p.Title}', Difficulty: '{p.Difficulty}'");

        var puzzle = _puzzles.FirstOrDefault(p => 
            p.Difficulty == MenuViewModel.CurrentDifficulty);

        Console.WriteLine($"Found: {puzzle?.Title}");

        if (puzzle == null) return;
        GameViewModel = new GameViewModel(puzzle);
    }
    
    public void LoadGame(CurrentPuzzleState save)
    {
        Console.WriteLine($"LoadGame called - save null: {save == null}, PuzzleTitle: '{save?.PuzzleTitle}'");
        if (save?.PuzzleTitle == null) return;
        var puzzle = _puzzles.FirstOrDefault(p => p.Title == save.PuzzleTitle);
        if (puzzle == null) return;
        GameViewModel = new GameViewModel(puzzle);
        OnGameReady?.Invoke();
    }
}