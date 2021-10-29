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
        public Position Position { get; set; }

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
            // if Position not initialized
            if (Position.Latitude == 0 && Position.Latitude == 0)
            {
                // get current location
                var request = new GeolocationRequest(GeolocationAccuracy.Low, TimeSpan.FromSeconds(10));
                CancellationTokenSource cts = new CancellationTokenSource();
                Location location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    Position = new Position(location.Latitude, location.Longitude);
                }
            }
            base.OnAppearing();
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (query.ContainsKey("Latitude") && query.ContainsKey("Longitude"))
            {
                Position = new Position(Convert.ToDouble(HttpUtility.UrlDecode(query["Latitude"])),
                                        Convert.ToDouble(HttpUtility.UrlDecode(query["Longitude"])));
            }          
        }
    }
}