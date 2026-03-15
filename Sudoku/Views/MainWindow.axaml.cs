using Avalonia.Controls;

namespace Sudoku.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainContent.Content = new MainMenuView();
    }
    
    public void NavigateTo(UserControl view)
    {
        MainContent.Content = view;
    }
}