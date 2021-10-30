using LetsMeet.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

namespace LetsMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDetailPage : ContentPage
    {
        private UserDetailViewModel udvm;
        public UserDetailPage()
        {
            InitializeComponent();
            udvm = new UserDetailViewModel();
            BindingContext = udvm;
        }

        protected override async void OnAppearing()
        {
            //InitializeComponent();
            base.OnAppearing();
            udvm.LoadUser();
        }
    }
}