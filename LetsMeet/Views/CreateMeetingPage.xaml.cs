using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace LetsMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateMeetingPage : ContentPage
    {
        public string Name { get; set; }
        public int MinMembers { get; set; }
        public int MaxMembers { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Position Position { get; set; } = new Position();

        public string TpyeId = "1";
        public MeetingType Type
        {
            get
            {
                return Data.MeetingTypesData.GetMeetingType(this.TpyeId);
            }
        }
        private string _iconURL = null;
        public string IconURL
        {
            get
            {
                if (_iconURL == null)
                {
                    return Type.IconURL;
                }
                else
                {
                    return _iconURL;
                }
            }
            set
            {
                _iconURL = value;
            }
        }
        public CreateMeetingPage()
        {
            InitializeComponent();
            IconURL = Type.IconURL;
            // get location
            //var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            //var location = await Geolocation.GetLocationAsync(request);
            //if (location != null)
            //{
            //    position = new position(location.latitude, location.longitude);
            //}

            //var locator = CrossGeolocator.Current;
            //var position = await locator.GetPositionAsync(10000);
            //position = new Position(position.Latitude, position.Longitude);

            BindingContext = this;
        }

        async private void set_location_clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("ChooseLocation");
        }

        private void create_meeting_clicked(object sender, EventArgs e)
        {

        }

        protected override async void OnAppearing()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Low, TimeSpan.FromSeconds(10));
            CancellationTokenSource cts = new CancellationTokenSource();
            Location location = await Geolocation.GetLocationAsync(request, cts.Token);

            if (location != null)
            {
                Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                Position = new Position(location.Latitude, location.Longitude);
            }
            base.OnAppearing();
        }
    }
}