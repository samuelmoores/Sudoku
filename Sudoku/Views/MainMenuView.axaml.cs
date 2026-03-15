using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Sudoku.Views;

public partial class MainMenuView : UserControl
{
    public MainMenuView()
    {
        InitializeComponent();
    }
    
    private void StartGame_Click(object sender, RoutedEventArgs e)
    {
        var window = (MainWindow)this.VisualRoot!;
        window.NavigateTo(new GameView()); // swap in your game view
    }
}