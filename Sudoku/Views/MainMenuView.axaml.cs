using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Sudoku.ViewModels;

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
        var vm = (MainWindowViewModel)window.DataContext!;

        vm.StartGame();
        
        window.NavigateTo(new GameView(vm.GameViewModel));
    }
    
}