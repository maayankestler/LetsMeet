using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

using LetsMeet.Data;
using LetsMeet.Models;
using LetsMeet.ViewModels;

namespace LetsMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateMeetingPage : ContentPage, IQueryAttributable
    {
        public string Name { get; set; }
        public int MinMembers { get; set; }
        public int MaxMembers { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now.Add(new TimeSpan(1, 0, 0, 0));
        public DateTime EndTime { get; set; } = DateTime.Now.Add(new TimeSpan(1, 2, 0, 0));
        public Location Location { get; set; }

        private string _typeId = null;
        private MeetingType _type = null;
        public MeetingType Type
        {
            get
            {
                if (_typeId == null)
                {
                    _type = MeetingTypesData.GetFirst();
                    _typeId = _type.Id;
                }
                else if(_typeId != _type.Id)
                {
                    _type = MeetingTypesData.GetMeetingType(_typeId);
                }
                    
                
                return _type;
            }
            set
            {
                IconURL = value.IconURL;
                _typeId = value.Id;
            }
        }
        public string IconURL { get; set; }
        public CreateMeetingPage()
        {
            InitializeComponent();
            IconURL = Type.IconURL;
            BindingContext = this;
        }

        async private void set_location_clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("ChooseLocation");
        }

        async private void create_meeting_clicked(object sender, EventArgs e)
        {
            Meeting m = new Meeting(Name, IconURL, _typeId, StartTime, EndTime, MainViewModel.GetInstance.CurrentUser.Id, MinMembers, MaxMembers, MinAge, MaxAge, Location);
            MeetingsData.CreateMeeting(m);
            await Shell.Current.GoToAsync("//MeetingsList");
        }

        protected override async void OnAppearing()
        {
            // if Position not initialized
            if (Location == null)
            {
                // get current location
                var request = new GeolocationRequest(GeolocationAccuracy.Low, TimeSpan.FromSeconds(10));
                CancellationTokenSource cts = new CancellationTokenSource();
                Location location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    Location = location;
                }
            }
            base.OnAppearing();
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (query.ContainsKey("Latitude") && query.ContainsKey("Longitude"))
            {
                Location = new Location(Convert.ToDouble(HttpUtility.UrlDecode(query["Latitude"])),
                                        Convert.ToDouble(HttpUtility.UrlDecode(query["Longitude"])));
            }          
        }
    }
}