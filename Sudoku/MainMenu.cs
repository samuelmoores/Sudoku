using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace Sudoku
{
    public partial class MainMenu : Form
    {


        public MainMenu()
        {
            InitializeComponent();
            this.ClientSize = new Size(500, 520);
            this.Text = "Sudoku";
        }

        
        private void button_new_game_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Start New Game");
            SudokuBoard board = new SudokuBoard();
            board.Show();
        }
    }
}
