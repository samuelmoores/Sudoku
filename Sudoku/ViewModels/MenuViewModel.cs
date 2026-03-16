using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sudoku.Models;

namespace Sudoku.ViewModels;

public partial class MenuViewModel : ViewModelBase
{
    private readonly GameViewModel _game;
    private readonly Action<CurrentPuzzleState> _onLoadGame;
    public string CurrentDifficulty => _game.Difficulty;
    
    [ObservableProperty]
    private bool _isLoadDialogOpen = false;

    public ObservableCollection<CurrentPuzzleState> SaveSlots { get; } = new();


    public MenuViewModel(GameViewModel game, Action<CurrentPuzzleState> onLoadGame,  ObservableCollection<CurrentPuzzleState> saveSlots)
    {
        _game = game;
        _onLoadGame = onLoadGame;
        SaveSlots = saveSlots;
    }

    [RelayCommand]
    private void SetDifficulty(string difficulty)
    {
        _game.Difficulty = difficulty;
        OnPropertyChanged(nameof(CurrentDifficulty)); // notify UI to update
    }

    [RelayCommand]
    private void OpenLoadDialog()
    {
        var freshData = SaveManager.Load() ?? new SaveData();
        Console.WriteLine($"Saves found: {freshData.Saves.Count}");
        foreach (var s in freshData.Saves)
            Console.WriteLine($"  PuzzleTitle: '{s.PuzzleTitle}', Difficulty: '{s.Difficulty}'");
    
        SaveSlots.Clear();
        foreach (var save in freshData.Saves)
            SaveSlots.Add(save);
    
        Console.WriteLine($"SaveSlots count after update: {SaveSlots.Count}");
        IsLoadDialogOpen = true;
    }

    [RelayCommand]
    private void CloseLoadDialog() => IsLoadDialogOpen = false;

    [RelayCommand]
    private void LoadSave(CurrentPuzzleState save)
    {
        _onLoadGame(save);
        IsLoadDialogOpen = false;
    }
    
    [RelayCommand]
    private void SelectSave(CurrentPuzzleState? save)
    {
        if (save == null) return;
        _onLoadGame(save);
        IsLoadDialogOpen = false;
    }
}