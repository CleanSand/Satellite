using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReactiveUI;
using Satellite.Models;
using Satellite.Views;

namespace Satellite.ViewModels;

public class HomePageVM : ViewModelBase
{
    private static ObservableCollection<Solar> _solars = new ObservableCollection<Solar>();
    public static ObservableCollection<Solar> Solars => _solars;
    
    private static ObservableCollection<Solar> _favouritessolars = new ObservableCollection<Solar>();
    public static ObservableCollection<Solar> Favouritessolars => _favouritessolars;
    private static Solar _objectSolar;
    private string _searchstring;
    public static Solar _serchingSolar;
    public static async Task Main()
    {
        using var client = new HttpClient();
        
        client.BaseAddress = new Uri("https://api.le-systeme-solaire.net/rest/");
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        var url = "bodies/";
        
        HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        var resp = await response.Content.ReadAsStringAsync();
        dynamic des = JObject.Parse(resp);
        foreach (var body in des.bodies)
        {

            _solars.Add(new Solar
            {
                bodyType = body.bodyType,
                englishName = body.englishName,
                id = body.id,
                isPlanet = body.isPlanet,
                perihelion = body.perihelion
            });
        }
        
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
            informationWindow.ShowDialog(_homePageWindow);
        }
    }
    public string Searchstring
    {
        get => _searchstring;
        set => this.RaiseAndSetIfChanged(ref _searchstring, value);
    }
    public ReactiveCommand<Window, Unit> SubmitSearch { get; }
    private void SubmitSearchImpl(Window obj)
    {
        _serchingSolar = Solars.FirstOrDefault(x => x.englishName == _searchstring);
        if (_serchingSolar != null)
        {
            _solars.Clear();
            _solars.Add(new Solar
            {
                bodyType = _serchingSolar.bodyType,
                englishName = _serchingSolar.englishName,
                id = _serchingSolar.id,
                isPlanet = _serchingSolar.isPlanet,
                perihelion = _serchingSolar.perihelion
            });
        }
        else
        {
            _solars.Clear();
            Main();
        }
    }
    private HomePageWindow _homePageWindow;
    public ReactiveCommand<Window, Unit> OpenWindow { get; }

    private void OpenWindowImpl(Window obj)
    {
        ProfileWindow Profilewindow = new ProfileWindow();
        Profilewindow.Show();
        obj.Close();
    }
    public ReactiveCommand<Window, Unit> OpenWindow2 { get; }

    private void OpenWindow2Impl(Window obj)
    {
        SelectedObjectWindow selectedWindow = new SelectedObjectWindow();
        selectedWindow.ShowDialog(obj);
    }
    public void AddToFavouritesImpl(Solar solar)
    {
        var edentity = _favouritessolars.SingleOrDefault(x => x.englishName == solar.englishName);
        if (edentity == null)
        {
            _favouritessolars.Add(new Solar
            {
                bodyType =  solar.bodyType,
                englishName = solar.englishName,
                id = solar.id,
                isPlanet = solar.isPlanet,
                perihelion = solar.perihelion
            });
        }
        
    }
    
    public HomePageVM(HomePageWindow window)
    {
        _homePageWindow = window;
        OpenWindow = ReactiveCommand.Create<Window>(OpenWindowImpl);
        OpenWindow2 = ReactiveCommand.Create<Window>(OpenWindow2Impl);
        SubmitSearch = ReactiveCommand.Create<Window>(SubmitSearchImpl);
    }
}