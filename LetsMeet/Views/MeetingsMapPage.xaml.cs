using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Collections;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;

using LetsMeet.Controls;
using LetsMeet.Data;
using LetsMeet.ViewModels;
using LetsMeet.Models;

namespace LetsMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeetingsMapPage : ContentPage, INotifyPropertyChanged
    {
        public MeetingsMapPage()
        {
            InitializeComponent();
            BindingContext = new MeetingsViewModel();
        }
        async private void Pin_InfoWindowClicked(object sender, PinClickedEventArgs e)
        {
            MeetingPin meeting_pin = sender as MeetingPin;
            await Shell.Current.GoToAsync($"MeetingDetails?id={meeting_pin.Meeting.Id}");
        }

        protected override async void OnAppearing()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            CancellationTokenSource cts = new CancellationTokenSource();
            Location location = await Geolocation.GetLocationAsync(request, cts.Token);
            Position CurrentPosition = new Position(location.Latitude, location.Longitude);
            ChooseMapControl.MoveToRegion(MapSpan.FromCenterAndRadius(CurrentPosition, Distance.FromMiles(1)));
            base.OnAppearing();
        }
    }
}