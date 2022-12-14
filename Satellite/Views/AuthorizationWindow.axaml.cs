using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Satellite.ViewModels;

namespace Satellite.Views;

public partial class AuthorizationWindow : Window
{
    public AuthorizationWindow()
    {
        DataContext = new AuthorizationVM();
        InitializeComponent();
        this.AttachDevTools();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}