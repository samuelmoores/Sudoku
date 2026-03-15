using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Sudoku.ViewModels;

namespace Sudoku.Views;

public partial class GameView : UserControl
{
    private TextBox[,] cells = new TextBox[9, 9];
    private int[,] solution = new int[9, 9];
    private const int CellSize = 40;
    private const int Padding = 30;
    private bool _isFillingPuzzle = false;
    
    public GameView(GameViewModel vm)
    {
        InitializeComponent();
        BuildGrid();

        switch (vm.Difficulty)
        {
            case "Easy":
                FillGrid(20);
                break;
            case "Normal":
                FillGrid(40);
                break;
            case "Hard":
                FillGrid(60);
                break;
        }
    }

    private void BuildGrid()
    {
        // Set up 9 rows and columns
        for (int i = 0; i < 9; i++)
        {
            SudokuGrid.RowDefinitions.Add(new RowDefinition(GridLength.Star));
            SudokuGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
        }

        // Create each cell
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                // Calculate border thickness to create 3x3 box borders
                double left   = (col % 3 == 0) ? 2 : 0.5;
                double top    = (row % 3 == 0) ? 2 : 0.5;
                double right  = (col == 8)     ? 2 : 0.5;
                double bottom = (row == 8)     ? 2 : 0.5;

                var textBox = new TextBox
                {
                    TextAlignment = Avalonia.Media.TextAlignment.Center,
                    FontSize = 18,
                    BorderThickness = new Thickness(0),
                    Background = Brushes.White,
                    VerticalContentAlignment = Avalonia.Layout.VerticalAlignment.Center,
                    Padding = new Thickness(0),
                    MinWidth = 0,
                    MinHeight = 0,
                };

                var border = new Border
                {
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(left, top, right, bottom),
                    Child = textBox
                };

                Grid.SetRow(border, row);
                Grid.SetColumn(border, col);
                cells[row, col] = textBox;
                SudokuGrid.Children.Add(border);
            }
        }
    }
    
    private bool SolveBoard(int[,] board)
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (board[row, col] == 0)
                {
                    // Shuffle numbers for randomness
                    var nums = Enumerable.Range(1, 9).OrderBy(_ => Random.Shared.Next()).ToList();
                    foreach (int num in nums)
                    {
                        if (IsValid(board, row, col, num))
                        {
                            board[row, col] = num;
                            if (SolveBoard(board)) return true;
                            board[row, col] = 0; // backtrack
                        }
                    }
                    return false; // no valid number found
                }
            }
        }
        return true; // all cells filled
    }

    private bool IsValid(int[,] board, int row, int col, int num)
    {
        for (int i = 0; i < 9; i++)
        {
            if (board[row, i] == num) return false; // row
            if (board[i, col] == num) return false; // col
        }

        int boxRow = (row / 3) * 3;
        int boxCol = (col / 3) * 3;
        for (int r = boxRow; r < boxRow + 3; r++)
            for (int c = boxCol; c < boxCol + 3; c++)
                if (board[r, c] == num) return false; // box

        return true;
    }

    private void FillGrid(int emptyCells)
    {
        // Step 1: generate complete valid board
        SolveBoard(solution);

        // Step 2: copy to UI, then remove cells for puzzle
        var positions = Enumerable.Range(0, 81)
            .OrderBy(_ => Random.Shared.Next())
            .Take(emptyCells)
            .ToHashSet();

        _isFillingPuzzle = true;
        for (int row = 0; row < 9; row++)
            for (int col = 0; col < 9; col++)
            {
                int idx = row * 9 + col;
                if (positions.Contains(idx))
                {
                    cells[row, col].Text = ""; // leave empty for player
                    cells[row, col].IsReadOnly = false; // player can edit
                }
                else
                {
                    cells[row, col].Text = solution[row, col].ToString();
                    cells[row, col].Text = solution[row, col].ToString();
                    cells[row, col].IsReadOnly = true;  // locked
                    cells[row, col].Foreground = Brushes.Gray; // visually distinct                       
                }
            }
        _isFillingPuzzle = false;
    }
        
    private void CheckSolution_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            bool correct = true;

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (cells[row, col].Text != solution[row, col].ToString())
                    {
                        correct = false;
                        cells[row, col].Foreground = Brushes.Red; // highlight wrong cells
                    }
                    else
                    {
                        cells[row, col].Foreground = Brushes.Black;
                    }
                }
            }

            ResultText.Text = correct ? "Congratulations! 🎉" : "Some cells are incorrect.";
        }
}