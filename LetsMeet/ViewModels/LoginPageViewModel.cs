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

        public LoginPageViewModel ()
        {
            Login = new Command(Login_Button_Clicked);
        }

        async void Login_Button_Clicked()
        {
            MainViewModel.GetInstance.Login(UserName, Password);
            if (MainViewModel.GetInstance.IsLoggedIn())
            {
                await Shell.Current.GoToAsync($"userdetails?id={MainViewModel.GetInstance.CurrentUser.Id}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("login filed");
            }
        }


        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            // await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}");
        }
    }
}
