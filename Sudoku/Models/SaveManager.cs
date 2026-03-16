using System;
using System.IO;

namespace Sudoku.Models;

// Models/SaveManager.cs
using System.Text.Json;

public static class SaveManager
{
    private static readonly string SavePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "Sudoku",
        "save.json"
    );

    public static void Save(SaveData data)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(SavePath)!);
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(SavePath, json);
    }

    public static SaveData? Load()
    {
        if (!File.Exists(SavePath))
            return null; // fresh save

        var json = File.ReadAllText(SavePath);
        return JsonSerializer.Deserialize<SaveData>(json) ?? new SaveData();
    }
}