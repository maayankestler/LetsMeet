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
using System.Collections.ObjectModel;
using System.Collections;

namespace LetsMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapGetLocationPage : ContentPage, INotifyPropertyChanged
    {
        public Position CurrentPosition { get; set; } = new Position();

        public ObservableCollection<Position> _positionsList { get; set; } = new ObservableCollection<Position>();
        public IEnumerable PositionsList => _positionsList;
       
        public MapGetLocationPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
        
        private void HandleMapClicked(object sender, MapClickedEventArgs e)
        {
            CurrentPosition = e.Position;
            //PositionsList = new List<Position>();
            _positionsList = new ObservableCollection<Position>();
            _positionsList.Add(CurrentPosition);
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
                _positionsList.Add(CurrentPosition);
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

        async private void ChooseLocation(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"..?Latitude={CurrentPosition.Latitude}&Longitude={CurrentPosition.Longitude}");
        }
    }
}