using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Satellite.ViewModels;

namespace Satellite.Views;

public partial class RegistrationWindow : Window
{
    public RegistrationWindow()
    {
        DataContext = new RegistrationVM();
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}