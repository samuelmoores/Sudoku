using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Sudoku.Models;
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
        var mainVm = (MainWindowViewModel)window.DataContext!;
        mainVm.StartGame();
        window.NavigateTo(new GameView(mainVm.GameViewModel));
    }
    
    private void SaveSlotsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ListBox lb && lb.SelectedItem is CurrentPuzzleState save)
        {
            var vm = (MenuViewModel)DataContext!;
            vm.SelectSaveCommand.Execute(save);
        }
    }
    
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        var window = (MainWindow)this.VisualRoot!;
        var mainVm = (MainWindowViewModel)window.DataContext!;
        mainVm.OnGameReady = () =>
        {
            window.NavigateTo(new GameView(mainVm.GameViewModel));
        };
    }
    
}