using System.Diagnostics;

namespace Sudoku
{
    partial class MainMenu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button_new_game = new Button();
            SuspendLayout();
            // 
            // button_new_game
            // 
            button_new_game.Location = new Point(215, 141);
            button_new_game.Name = "button_new_game";
            button_new_game.Size = new Size(75, 23);
            button_new_game.TabIndex = 0;
            button_new_game.Text = "New Game";
            button_new_game.UseVisualStyleBackColor = true;
            button_new_game.Click += button_new_game_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 481);
            Controls.Add(button_new_game);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button button_new_game;
    }
}
