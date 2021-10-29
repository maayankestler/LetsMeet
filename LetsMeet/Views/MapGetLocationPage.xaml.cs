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
using Xamarin.Essentials;
using System.Threading;

namespace LetsMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapGetLocationPage : ContentPage, INotifyPropertyChanged
    {
        public Position CurrentPosition { get; set; } = new Position(32, 35);
        public List<Position> PositionsList { get; set; } = new List<Position>();
        public MapGetLocationPage()
        {
            InitializeComponent();
            PositionsList.Add(CurrentPosition);
            BindingContext = this;
        }
        
        private void HandleMapClicked(object sender, MapClickedEventArgs e)
        {
            CurrentPosition = e.Position;
            //PositionsList = new List<Position>();
            PositionsList.Add(CurrentPosition);
            // OnPropertyChanged("CurrentPosition");
            OnPropertyChanged("PositionsList");
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
                CurrentPosition = new Position(location.Latitude, location.Longitude);
                PositionsList.Add(CurrentPosition);
                ChooseMapControl.MoveToRegion(MapSpan.FromCenterAndRadius(CurrentPosition, Distance.FromMiles(1)));
            }
            base.OnAppearing();
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}