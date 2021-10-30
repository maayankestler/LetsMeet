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
        //public List<Meeting> AvailableMeetings { get; set; }
        public ObservableCollection<Meeting> _meetingsList { get; set; } = new ObservableCollection<Meeting>(MeetingsData.Meetings);
        public IEnumerable AvailableMeetings => _meetingsList;
        private bool _onwedByMe = false;
        public bool OwnedByMe { 
            get
            {
                return _onwedByMe;
            } 
            set
            {
                _onwedByMe = value;
                FilterMeetings();
            } 
        }
        private bool _onwedByFriend = false;
        public bool OwnedByFriend
        {
            get
            {
                return _onwedByFriend;
            }
            set
            {
                _onwedByFriend = value;
                FilterMeetings();
            }
        }
        private bool _member = false;
        public bool Member
        {
            get
            {
                return _member;
            }
            set
            {
                _member = value;
                FilterMeetings();
            }
        }
        public MeetingsMapPage()
        {
            InitializeComponent();
            FilterMeetings(); // TODO handle filters
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

        private void FilterMeetings()
        {
            ObservableCollection<Meeting> temp_meetings_list = new ObservableCollection<Meeting>();

            void VerifyAdd(Meeting m)
            {
                if (!temp_meetings_list.Contains(m))
                    temp_meetings_list.Add(m);
            };

            if (OwnedByMe)
                MeetingsData.Meetings.FindAll(m => m.Owner.Id == MainViewModel.GetInstance.CurrentUser.Id).ForEach(VerifyAdd);
            if (OwnedByFriend)
                MeetingsData.Meetings.FindAll(m => MainViewModel.GetInstance.CurrentUser.Friends.Contains(m.Owner)).ForEach(VerifyAdd);
            if (Member)
                MeetingsData.Meetings.FindAll(m => m.Members.Contains(MainViewModel.GetInstance.CurrentUser)).ForEach(VerifyAdd);
            if (!OwnedByMe && !OwnedByFriend && !Member)
                temp_meetings_list = new ObservableCollection<Meeting>(MeetingsData.Meetings);
            //MeetingsData.Meetings.FindAll(m => m.Members.Contains(MainViewModel.GetInstance.CurrentUser)).ForEach(
            //    i => {
            //        if (!temp_meetings_list.Contains(i))
            //            temp_meetings_list.Add(i);
            //    });
            //else
            //    temp_meetings_list = new ObservableCollection<Meeting>(MeetingsData.Meetings);
            _meetingsList = temp_meetings_list;
        }
    }
}