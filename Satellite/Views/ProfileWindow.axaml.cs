using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Satellite.ViewModels;

namespace Satellite.Views;

public partial class ProfileWindow : Window
{
    public ProfileWindow()
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