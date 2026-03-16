using System;
using System.Collections.Generic;

namespace Sudoku.Models;

public class SudokuPuzzle
{
    public string Title { get; set; }
    public string Difficulty { get; set; }

    public int[,] Puzzle { get; set; }   // 0 = empty
    public int[,] Solution { get; set; }
 
    public SudokuPuzzle(string title, string difficulty)
    {
        Title = title;
        Difficulty = difficulty;
        Puzzle = new int[9, 9];
        Solution = new int[9, 9];
    }
 
    // Parse a grid from 9 lines of space-separated values, '.' = 0
    public static int[,] ParseGrid(List<string> lines)
    {
        int[,] grid = new int[9, 9];
        for (int row = 0; row < 9; row++)
        {
            string[] tokens = lines[row].Trim().Split(' ');
            for (int col = 0; col < 9; col++)
            {
                grid[row, col] = tokens[col] == "." ? 0 : int.Parse(tokens[col]);
            }
        }
        return grid;
    }
 
    public void PrintGrid(int[,] grid)
    {
        for (int row = 0; row < 9; row++)
        {
            if (row == 3 || row == 6)
                Console.WriteLine("------+-------+------");
 
            for (int col = 0; col < 9; col++)
            {
                if (col == 3 || col == 6)
                    Console.Write("| ");
 
                string cell = grid[row, col] == 0 ? "." : grid[row, col].ToString();
                Console.Write(cell + " ");
            }
            Console.WriteLine();
        }
    }
 
    public void Print()
    {
        Console.WriteLine($"\n=== {Title} ===");
        Console.WriteLine("\nPuzzle:");
        PrintGrid(Puzzle);
        Console.WriteLine("\nSolution:");
        PrintGrid(Solution);
    }
}