using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Satellite.Models;
using Satellite.ViewModels;

namespace Satellite.Views;

public partial class SelectedObjectWindow : Window
{
    public SelectedObjectWindow()
    {
        DataContext = new SelectedObjectVM(this);
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }
    public void DeleteToFavourites(Solar solar)
    {
        (DataContext as SelectedObjectVM).DeleteToFavouritesImpl(solar);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}