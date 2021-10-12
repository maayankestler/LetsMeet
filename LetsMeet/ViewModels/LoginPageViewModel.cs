using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using LetsMeet.Views;

namespace LetsMeet.ViewModels
{
    class LoginPageViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public ICommand Login { get; }
        public ICommand Register { get; }

        public LoginPageViewModel ()
        {
            Login = new Command(Login_Button_Clicked);
            Register = new Command(Register_Button_Clicked);
        }

        async void Login_Button_Clicked()
        {
            MainViewModel.GetInstance.Login(UserName, Password);
            if (MainViewModel.GetInstance.IsLoggedIn())
            {
                await Shell.Current.GoToAsync("//profile");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("login filed");
            }
        }

        async void Register_Button_Clicked()
        {
            await Shell.Current.GoToAsync("//register");
        }
    }
}
