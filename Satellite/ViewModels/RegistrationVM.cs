using System;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia.ViewModels.Commands;
using ReactiveUI;
using Satellite.Models;
using Satellite.Views;

namespace Satellite.ViewModels;

public class RegistrationVM : ViewModelBase
{
        private User _user = new User();
        private string _password;
        private string _login;
        private string _lastname;
        private string _firstname;
        private string _secondname;
        private DateTimeOffset _birthdate = DateTimeOffset.Now;
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
        public string LastName
        {
            get => _lastname;
            set => this.RaiseAndSetIfChanged(ref _lastname, value);
        }
        public string Name
        {
            get => _firstname;
            set => this.RaiseAndSetIfChanged(ref _firstname, value);
        }
        public string Patronymic
        {
            get => _secondname;
            set => this.RaiseAndSetIfChanged(ref _secondname, value);
        }
        public DateTimeOffset BirthDate
        {
            get => _birthdate;
            set => this.RaiseAndSetIfChanged(ref _birthdate, value);
        }
        public ReactiveCommand<Window, Unit> SumbitRegistration { get; }
        

        public RegistrationVM()
        {
            SumbitRegistration = ReactiveCommand.Create<Window>(SumbitRegistrationImpl);
        }


        private void SumbitRegistrationImpl(Window obj)
        {
            var user = Helper.GetContext().Users.FirstOrDefault(x=> x.Login == Login);
            if (user == null)
            {
                _user.Login = _login;
            
                _user.Password = _password;
            
                _user.Lastname = _lastname;
            
                _user.Firstname = _firstname;
            
                _user.Secondname = _secondname;
            
                _user.Birthdate = DateOnly.FromDateTime(_birthdate.DateTime);
            
                Helper.GetContext().Users.Add(_user);
                try
                {
                    Helper.GetContext().SaveChanges();
                    MessageBoxManager.GetMessageBoxStandardWindow("Пользователь Зарегистрирован", "Зарегистрирован", ButtonEnum.Ok, Icon.Success).ShowDialog(obj);
                    obj.Close();
                }
                catch (Exception ex)
                {
                    MessageBoxManager.GetMessageBoxStandardWindow("Данные не заполнены", "Ошибка", ButtonEnum.Ok, Icon.Error).ShowDialog(obj);
                    return;
                }
                if (_user.Login != null & _user.Password != null & _user.Lastname != null & _user.Firstname != null & _user.Secondname != null & _user.Birthdate != null)
                {
                    MessageBoxManager.GetMessageBoxStandardWindow("Пользователь Зарегистрирован", "Зарегистрирован", ButtonEnum.Ok, Icon.Success).ShowDialog(obj);
                    AuthorizationWindow authorization = new AuthorizationWindow();
                }
            }
            else
            {
                MessageBoxManager.GetMessageBoxStandardWindow("Данный логин занят", "Ошибка", ButtonEnum.Ok, Icon.Error).ShowDialog(obj);
                return;
            }
        }
}