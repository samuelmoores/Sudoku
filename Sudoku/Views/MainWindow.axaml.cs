using System;
using Avalonia.Controls;
using Sudoku.ViewModels;

namespace Sudoku.Views;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    // Called after DataContext is set by App.axaml.cs
    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        
        if (DataContext is MainWindowViewModel vm)
        {
            MainContent.Content = new MainMenuView { DataContext = vm.MenuViewModel };
        }
    }

    public void NavigateTo(Control view)
    {
        MainContent.Content = view;
    }
}