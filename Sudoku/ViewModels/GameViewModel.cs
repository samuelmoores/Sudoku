using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Sudoku.Models;

namespace Sudoku.ViewModels;

public partial class GameViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _difficulty = "Medium";
    [ObservableProperty]
    private string _title = "";
    
    private SaveData _saveData;
    
    public int[,]? LoadedPuzzle { get; set; }
    public int[,]? LoadedSolution { get; set; }
    public bool IsFromFile => LoadedPuzzle != null;
    
    public List<List<string>> SavedPlayerGrid { get; set; }

    public GameViewModel()
    {
        
    }
    
    // Constructor for file-loaded puzzles
    public GameViewModel(SudokuPuzzle puzzle)
    {
        _saveData = SaveManager.Load() ?? new SaveData();
        Title = puzzle.Title;
        Difficulty = puzzle.Difficulty;
        LoadedPuzzle = puzzle.Puzzle;
        LoadedSolution = puzzle.Solution;
        
        var saved = GetSavedState(puzzle.Title);
        if (saved != null)
        {
            // apply saved player grid
            SavedPlayerGrid = saved.PlayerGrid;
        }
    }
    
    public bool IsPuzzleSolved(string puzzleTitle)
        => _saveData.SolvedPuzzles.Contains(puzzleTitle);

    public CurrentPuzzleState? GetSavedState(string puzzleTitle)
    {
        return _saveData.Saves.FirstOrDefault(s => s.PuzzleTitle == puzzleTitle);
    }
    
    public void SaveCurrentPuzzle(List<List<string>> playerGrid)
    {
        var existing = _saveData.Saves.FirstOrDefault(s => s.PuzzleTitle == Title);
        if (existing != null)
            _saveData.Saves.Remove(existing);

        _saveData.Saves.Add(new CurrentPuzzleState
        {
            PuzzleTitle = Title,
            Difficulty = Difficulty,
            PlayerGrid = playerGrid,
            SavedAt = DateTime.Now
        });
        SaveManager.Save(_saveData);
    }
    
}