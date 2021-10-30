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
using LetsMeet.Controls;

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