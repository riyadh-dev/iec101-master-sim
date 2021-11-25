using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IEC101MasterSim.ViewModel;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;

namespace IEC101MasterSim;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private int _dataGridSize;
    public MainWindow()
    {
        InitializeComponent();
        _dataGridSize = 0;

        PaletteHelper paletteHelper = new();
        var theme = paletteHelper.GetTheme();
        var enumBaseTheme = Properties.Settings.Default.Theme;
        var baseTheme = enumBaseTheme == BaseTheme.Light ? Theme.Light : Theme.Dark;
        theme.SetBaseTheme(baseTheme);
        paletteHelper.SetTheme(theme);
    }

    private void ToogleThemeBtn_Click(object sender, RoutedEventArgs e)
    {
        PaletteHelper paletteHelper = new();
        var theme = paletteHelper.GetTheme();
        var enumBaseTheme =
            theme.GetBaseTheme() == BaseTheme.Dark ? BaseTheme.Light : BaseTheme.Dark;
        var baseTheme = enumBaseTheme == BaseTheme.Light ? Theme.Light : Theme.Dark;
        theme.SetBaseTheme(baseTheme);
        paletteHelper.SetTheme(theme);
        Properties.Settings.Default.Theme = enumBaseTheme;
    }

    private void DataGrid_ScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        if (_dataGrid.Items.Count > 0 && _dataGridSize != _dataGrid.Items.Count)
        {
            if (VisualTreeHelper.GetChild(_dataGrid, 0) is Decorator border)
            {
                if (border.Child is ScrollViewer scroll)
                {
                    scroll.ScrollToEnd();
                }
            }
            _dataGridSize = _dataGrid.Items.Count;
        }
    }

    private void OpenJsonFileDialog(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new();
        openFileDialog.Filter = "Addresses File (*.json)|*.json";
        if (openFileDialog.ShowDialog() == true)
        {
            var jsonString = File.ReadAllText(openFileDialog.FileName);
            var dc = DataContext as MainViewModel;
            dc.LoadAddressNamesCommand.Execute(jsonString);
        }
    }

    private void OpenSaveCsvFileDialog(object sender, RoutedEventArgs e)
    {
        SaveFileDialog saveFileDialog = new();
        saveFileDialog.Filter = "Addresses archive(*.csv)|*.csv";
        saveFileDialog.FileName = DateTime.Now.ToString().Replace(':', '_');
        if (saveFileDialog.ShowDialog() == true)
        {
            var fileName = saveFileDialog.FileName;
            var dc = DataContext as MainViewModel;
            dc.SaveCurrentSignalsCommand.Execute(fileName);
        }
    }

    private void OpenHighlightJsonFileDialog(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new();
        openFileDialog.Filter = "Highlight File (*.json)|*.json";
        if (openFileDialog.ShowDialog() == true)
        {
            string jsonString = File.ReadAllText(openFileDialog.FileName);
            var dc = DataContext as MainViewModel;
            dc.LoadHighlightedAddressesCommand.Execute(jsonString);
        }
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        var dc = DataContext as MainViewModel;
        dc.CloseConnectionCommand.Execute(new object());
        Properties.Settings.Default.Save();
    }
}
