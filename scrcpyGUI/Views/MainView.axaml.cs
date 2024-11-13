using Avalonia.Controls;
using Avalonia.Interactivity;

namespace scrcpyGUI.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    void UseCustomArgumentsClicked(object o, RoutedEventArgs routedEventArgs)
    {
        CustomArguments.IsEnabled = UseCustomArguments.IsChecked.GetValueOrDefault();
    }
}
