namespace Sudoku
{
    partial class SudokuBoard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button_quit = new Button();
            SuspendLayout();
            // 
            // button_quit
            // 
            button_quit.Location = new Point(197, 422);
            button_quit.Name = "button_quit";
            button_quit.Size = new Size(75, 23);
            button_quit.TabIndex = 0;
            button_quit.Text = "Quit";
            button_quit.UseVisualStyleBackColor = true;
            button_quit.Click += button_quit_Click;
            // 
            // SudokuBoard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 481);
            Controls.Add(button_quit);
            Name = "SudokuBoard";
            Text = "SudokuBoard";
            ResumeLayout(false);
        }

        #endregion

        private Button button_quit;
    }
}