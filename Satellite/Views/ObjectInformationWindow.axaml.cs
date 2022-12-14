using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Satellite.Views;

public partial class ObjectInformationWindow : Window
{
    public ObjectInformationWindow()
    {
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