using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sudoku.Models;

public enum GameState
{
    Playing,
    Paused,
    Completed
}

// Models/SaveData.cs
public class SaveData
{
    public ObservableCollection<CurrentPuzzleState> Saves { get; set; } = new();
    public List<string> SolvedPuzzles { get; set; } = new(); // e.g. "PUZZLE 1 (Easy)"
    public CurrentPuzzleState? CurrentPuzzle { get; set; }
}

public class CurrentPuzzleState
{
    public string SaveName { get; set; }
    public string PuzzleTitle { get; set; }
    public string Difficulty { get; set; }
    public List<List<string>> PlayerGrid { get; set; }
    public DateTime SavedAt { get; set; }
}