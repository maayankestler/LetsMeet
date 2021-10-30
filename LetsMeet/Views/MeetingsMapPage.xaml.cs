using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using LetsMeet.Data;
using LetsMeet.ViewModels;
using LetsMeet.Models;
using System.Collections.ObjectModel;
using System.Collections;
using Xamarin.Essentials;
using System.Threading;

namespace LetsMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeetingsMapPage : ContentPage, INotifyPropertyChanged
    {
        //public List<Meeting> AvailableMeetings { get; set; }
        public ObservableCollection<Meeting> _meetingsList { get; set; } = new ObservableCollection<Meeting>(MeetingsData.Meetings);
        public IEnumerable AvailableMeetings => _meetingsList;
        public MeetingsMapPage()
        {
            InitializeComponent();
            // _meetingsList = MeetingsData.Meetings; // TODO handle filters
            //BindingContext = new MeetingMapViewModel();
            BindingContext = this;
        }
        async private void Pin_InfoWindowClicked(object sender, PinClickedEventArgs e)
        {
            //string meetingId = (e.CurrentSelection.FirstOrDefault() as Meeting).Id;
            //await Shell.Current.GoToAsync($"MeetingDetails?id={meetingId}");
        }

        protected override async void OnAppearing()
        {
            Location location;
            location = await Geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                CancellationTokenSource cts = new CancellationTokenSource();
                location = await Geolocation.GetLocationAsync(request, cts.Token);
            }
            if (location != null)
            {
                Position CurrentPosition = new Position(location.Latitude, location.Longitude);
                MapControl.MoveToRegion(MapSpan.FromCenterAndRadius(CurrentPosition, Distance.FromMiles(1)));
            }
            base.OnAppearing();
        }
    }
}