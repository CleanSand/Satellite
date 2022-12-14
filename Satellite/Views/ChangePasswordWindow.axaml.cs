using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Satellite.ViewModels;

namespace Satellite.Views;

public partial class ChangePasswordWindow : Window
{
    public ChangePasswordWindow()
    {
        DataContext = new ProfileVM();
        InitializeComponent();
        this.AttachDevTools();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}