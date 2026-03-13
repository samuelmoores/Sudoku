using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class SudokuBoard : Form
    {
        private TextBox[,] cells = new TextBox[9, 9];
        private int[,] solution = new int[9, 9];

        private const int CellSize = 40;
        private const int Padding = 30;
        private bool _isFillingPuzzle = false;

        public SudokuBoard()
        {
            InitializeComponent();
            CreateGrid();
        }

        private void CreateGrid()
        {
            int difficulty = 0;
            int emptyCells = 30; // default to easy

            switch (difficulty)
            {
                case 0:
                    emptyCells = 1;
                    break;
                case 1:
                    emptyCells = 45;
                    break;
                case 2:
                    emptyCells = 70;
                    break;
            }

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {

                    int xOffset = (col / 3) * 6;
                    int yOffset = (row / 3) * 2;

                    var cell = new TextBox
                    {
                        Size = new Size(CellSize, CellSize),
                        Location = new Point(Padding + col * CellSize + xOffset,
                                            Padding + row * CellSize + yOffset),
                        TextAlign = HorizontalAlignment.Center,
                        Font = new Font("Arial", 18, FontStyle.Bold),
                        MaxLength = 1,
                        BackColor = GetBoxColor(row, col)
                    };

                    cell.KeyPress += Cell_KeyPress;
                    cell.Tag = (row, col);
                    cell.TextChanged += Cell_TextChanged;
                    cells[row, col] = cell;
                    this.Controls.Add(cell);

                }
            }

            FillGrid(emptyCells);

        }

        // Alternate background colors for 3x3 boxes
        private Color GetBoxColor(int row, int col)
        {
            int boxRow = row / 3;
            int boxCol = col / 3;
            return (boxRow + boxCol) % 2 == 0
                ? Color.White
                : Color.LightSteelBlue;
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
                        cells[row, col].Text = ""; // leave empty for player
                    else
                        cells[row, col].Text = solution[row, col].ToString();
                }
            _isFillingPuzzle = false;
        }

        // Only allow digits 1-9
        private void Cell_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) || e.KeyChar == '0')
            {
                if (e.KeyChar != (char)Keys.Back) // Allow backspace
                    e.Handled = true;
            }
        }

        private void Cell_TextChanged(object sender, EventArgs e)
        {
            if (_isFillingPuzzle) return;

            var box = (TextBox)sender;
            var (row, col) = ((int, int))box.Tag;
            string value = box.Text;

            if (!CheckEntry(value, row, col))
                box.Text = "";
        }

        private void button_quit_Click(object sender, EventArgs e)
        {
            Close();
        }

        bool CheckEntry(string newEntry, int row, int col)
        {
            if (newEntry == "") return true; // empty is always valid

            for (int i = 0; i < 9; i++)
            {
                if (newEntry == cells[row, i].Text && i != col) return false; // row
                if (newEntry == cells[i, col].Text && i != row) return false; // col
            }

            int boxRow = (row / 3) * 3;
            int boxCol = (col / 3) * 3;
            for (int r = boxRow; r < boxRow + 3; r++)
                for (int c = boxCol; c < boxCol + 3; c++)
                    if ((r != row || c != col) && cells[r, c].Text == newEntry)
                        return false;

            return true;
        }
    }
}
