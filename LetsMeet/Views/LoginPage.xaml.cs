using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LetsMeet.ViewModels;

namespace LetsMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginPageViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if(MainViewModel.GetInstance.IsLoggedIn())
                //await Shell.Current.GoToAsync($"userdetails?name={MainViewModel.GetInstance.CurrentUser.Name}");
                await Shell.Current.GoToAsync("//users"); 

        }
    }
}