using System;
using System.Linq;
using System.Net.Http;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Metsys.Bson;
using Satellite.Models;
using Satellite.Views;
using System.Windows;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using ReactiveUI;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace Satellite.ViewModels;

public class AuthorizationVM : ViewModelBase
{
        private string _password;
        private string _login;
        public static User AuthorizedUser { get; set; }
        private User _user;
        public User User
        {
            get => _user;
            set => this.RaiseAndSetIfChanged(ref _user, value);
        }
        public string Login
        {
            get => _login;
            set => this.RaiseAndSetIfChanged(ref _login, value);
        }
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }
        public ReactiveCommand<Window, Unit> OpenWindow { get; }
        public ReactiveCommand<Window, Unit> OpenWindow2 { get; }
        public AuthorizationVM()
        {
            
            OpenWindow = ReactiveCommand.Create<Window>(OpenWindowImpl);
            OpenWindow2 = ReactiveCommand.Create<Window>(OpenWindowImpl2);
        }

        private void OpenWindowImpl(Window obj)
        {
            
            RegistrationWindow registration = new RegistrationWindow();
            registration.ShowDialog(obj);
        }
        private async void OpenWindowImpl2(Window obj)
        {
            var user = Helper.GetContext().Users.SingleOrDefault(x => x.Login == Login & x.Password == Password);
            if (user != null)
            {
                AuthorizedUser = user;
                await HomePageVM.Main().WaitAsync(TimeSpan.FromMinutes(5));
                HomePageWindow homePage = new HomePageWindow();
                homePage.Show();
                obj.Close();
                
            }
            else
            {
                MessageBoxManager.GetMessageBoxStandardWindow("Ошибка","Данные введены неверно",  ButtonEnum.Ok, Icon.Error).ShowDialog(obj);
                return;
            }

            
        }
        
}