using System.Collections.ObjectModel;
using ReactiveUI;
using Satellite.Models;
using Satellite.Views;

namespace Satellite.ViewModels;

public class SelectedObjectVM : ViewModelBase
{
    private static ObservableCollection<Solar> _selectedSolars = new ObservableCollection<Solar>();
    private static Solar _objectSolar;
    private SelectedObjectWindow _selectedObjectWindow;
    public static ObservableCollection<Solar> SelectedSolars
    {
        get => _selectedSolars;
        set => _selectedSolars = value;
    }
    public Solar ObjectSolar
    {
        get => _objectSolar;
        set
        {
            this.RaiseAndSetIfChanged(ref _objectSolar, value);
            ObjectInformationWindow informationWindow = new ObjectInformationWindow()
            {
                DataContext = new ObjectInformationVM(_objectSolar)
            };
            informationWindow.ShowDialog(_selectedObjectWindow);
        }
    }
    public void DeleteToFavouritesImpl(Solar solar)
    {
        SelectedSolars.Remove(solar);
    }
    public SelectedObjectVM(SelectedObjectWindow window)
    {
        _selectedObjectWindow = window;
        SelectedSolars = HomePageVM.Favouritessolars;
    }
}