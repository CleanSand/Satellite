using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;
using Satellite.Models;
using Satellite.Views;

namespace Satellite.ViewModels;

public class ProfileVM : ViewModelBase 
{
    private string _firstpassword;
    private string _secondpassword;
    private string _oldpassword;
    private static User AuthorizedUserNow { get; set; }

    
    public string OldPassword
    {
        get => _oldpassword;
        set => this.RaiseAndSetIfChanged(ref _oldpassword, value);
    }
    public string FirstPassword
    {
        get => _firstpassword;
        set => this.RaiseAndSetIfChanged(ref _firstpassword, value);
    }
    public string SecondPassword
    {
        get => _secondpassword;
        set => this.RaiseAndSetIfChanged(ref _secondpassword, value);
    }

    public ReactiveCommand<Window, Unit> OpenWindow { get; }
    public ReactiveCommand<Window, Unit> OpenWindowChangePassword { get; }
    public ReactiveCommand<Window, Unit> SubmitChange { get; }

    private void OpenWindowImpl(Window obj)
    {
            
        HomePageWindow homePage = new HomePageWindow();
        homePage.Show();
        obj.Close();
    }
    private void OpenWindowChangePasswordImpl(Window obj)
    {
            
        ChangePasswordWindow changePass = new ChangePasswordWindow();
        changePass.ShowDialog(obj);
    }
    private void SubmitChangeImpl(Window obj)
    {
        if (_oldpassword == AuthorizationVM.AuthorizedUser.Password)
        {
            if (_firstpassword == _secondpassword)
            {
                AuthorizationVM.AuthorizedUser.Password = _firstpassword;
                Helper.GetContext().Users.Update(AuthorizationVM.AuthorizedUser);
                Helper.GetContext().SaveChanges();
                MessageBoxManager.GetMessageBoxStandardWindow("Успешно","Пароль изменён",  ButtonEnum.Ok, Icon.Success).ShowDialog(obj);
            }
            else
            {
                MessageBoxManager.GetMessageBoxStandardWindow("Ошибка","Пароли не совпадают",  ButtonEnum.Ok, Icon.Error).ShowDialog(obj);
                return;
            }
        }
        else
        {
            MessageBoxManager.GetMessageBoxStandardWindow("Ошибка","Неверный текущий пароль",  ButtonEnum.Ok, Icon.Error).ShowDialog(obj);
            return;
        }
        
    }
    public ProfileVM()
    {
        AuthorizedUserNow = AuthorizationVM.AuthorizedUser;
        OpenWindow = ReactiveCommand.Create<Window>(OpenWindowImpl);
        OpenWindowChangePassword = ReactiveCommand.Create<Window>(OpenWindowChangePasswordImpl);
        SubmitChange = ReactiveCommand.Create<Window>(SubmitChangeImpl);
    }
}