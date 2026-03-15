using System.Collections.Generic;
using System.IO;

namespace Sudoku.Models;

public class SudokuFileReader
{
    public static List<SudokuPuzzle> ReadPuzzles(string filePath)
    {
        List<SudokuPuzzle> puzzles = new List<SudokuPuzzle>();
        string[] lines = File.ReadAllLines(filePath);
 
        SudokuPuzzle current = null;
        List<string> gridLines = new List<string>();
        bool readingSolution = false;
 
        foreach (string line in lines)
        {
            string trimmed = line.Trim();
 
            // Detect puzzle header e.g. "PUZZLE 1 (Easy)"
            if (trimmed.StartsWith("PUZZLE"))
            {
                current = new SudokuPuzzle(trimmed);
                gridLines.Clear();
                readingSolution = false;
                continue;
            }
 
            // Detect solution marker
            if (trimmed == "Solution:")
            {
                // Save puzzle grid collected so far
                if (current != null && gridLines.Count == 9)
                    current.Puzzle = SudokuPuzzle.ParseGrid(gridLines);
 
                gridLines.Clear();
                readingSolution = true;
                continue;
            }
 
            // Collect grid lines (rows of 9 space-separated values)
            if (current != null && trimmed.Length > 0 && !trimmed.StartsWith("="))
            {
                string[] tokens = trimmed.Split(' ');
                if (tokens.Length == 9)
                {
                    gridLines.Add(trimmed);
 
                    // Once we have 9 solution lines, save and finish this puzzle
                    if (readingSolution && gridLines.Count == 9)
                    {
                        current.Solution = SudokuPuzzle.ParseGrid(gridLines);
                        puzzles.Add(current);
                        current = null;
                        gridLines.Clear();
                        readingSolution = false;
                    }
                }
            }
        }
 
        return puzzles;
    }
}