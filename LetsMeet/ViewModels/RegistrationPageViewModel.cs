using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using LetsMeet.Data;

namespace LetsMeet.ViewModels
{
    class RegistrationPageViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime BornDate { get; set; } = DateTime.Now.AddYears(-18);
        public string IconURL { get; set; } = "https://www.kindpng.com/picc/m/495-4952535_create-digital-profile-icon-blue-user-profile-icon.png";

        public ICommand SignUp { get; }
        public ICommand SignIn { get; }
        public RegistrationPageViewModel()
        {
            SignUp = new Command(SignUp_Button_Clicked);
            SignIn = new Command(SignIn_Button_Clicked);
        }

        async void SignUp_Button_Clicked()
        {
            User NewUser = new User(Name, UserName, Password, IconURL, BornDate);
            UsersData.CreateUser(NewUser);
            MainViewModel.GetInstance.Login(UserName, Password);
            await Shell.Current.GoToAsync("//profile");
        }

        async void SignIn_Button_Clicked()
        {
            await Shell.Current.GoToAsync("//login");
        }
    }
}
