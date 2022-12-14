using System.Net.Http;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Satellite.Models;
using Satellite.ViewModels;

namespace Satellite.Views;

public partial class HomePageWindow : Window
{
    
    public HomePageWindow()
    {
        DataContext = new HomePageVM(this);
        
        InitializeComponent();
        this.AttachDevTools();
        
    }

    public void AddToFavourites(Solar solar)
    {
        (DataContext as HomePageVM).AddToFavouritesImpl(solar);
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}