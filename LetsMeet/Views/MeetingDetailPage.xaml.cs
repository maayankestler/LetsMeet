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
    public partial class MeetingDetailPage : ContentPage
    {
        public MeetingDetailPage()
        {
            InitializeComponent();
            BindingContext = new MeetingDetailViewModel();
        }

        //async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    
        //    //string userId = (e.CurrentSelection.FirstOrDefault() as User).Id;
        //    //await Shell.Current.GoToAsync($"UserDetails?id={userId}");
        //}
    }
}