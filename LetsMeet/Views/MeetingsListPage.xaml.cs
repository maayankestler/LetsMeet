using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LetsMeet.Models;
using LetsMeet.ViewModels;

namespace LetsMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeetingsListPage : ContentPage
    {
        public MeetingsListPage()
        {
            InitializeComponent();
            BindingContext = new MeetingsViewModel();
        }

        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string meetingId = (e.CurrentSelection.FirstOrDefault() as Meeting).Id;
            //// The following route works because route names are unique in this application.
            await Shell.Current.GoToAsync($"MeetingDetails?id={meetingId}");
        }
    }
}