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
        public string IconURL { get; set; } = "https://osxlatitude.com/uploads/monthly_2019_10/pngtree-vector-user-young-boy-avatar-icon-png-image_1538408.thumb.jpg.2ec631dd5e0e029b2c5a13fe37f2c122.jpg";

        public ICommand SignUp { get; }
        public ICommand SignIn { get; }
        public RegistrationPageViewModel()
        {
            SignUp = new Command(SignUpButtonClicked);
            SignIn = new Command(SignInButtonClicked);
        }

        async void SignUpButtonClicked()
        {
            User NewUser = new User(Name, UserName, Password, IconURL, BornDate);
            UsersData.CreateUser(NewUser);
            MainViewModel.GetInstance.Login(UserName, Password);
            await Shell.Current.GoToAsync("//profile");
        }

        async void SignInButtonClicked()
        {
            await Shell.Current.GoToAsync("//login");
        }
    }
}
