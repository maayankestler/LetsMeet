using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using Xamarin.Forms;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Xamarin.Forms.Maps;
//using Xamarin.Essentials;

using LetsMeet.Data;
using LetsMeet.Models;

namespace LetsMeet.ViewModels
{
    public class MeetingMapViewModel : INotifyPropertyChanged
    {
        // public List<Position> MeetingsLocations;
        public List<Meeting> AvailableMeetings;
        public Command<MapClickedEventArgs> MapClickedCommand =>
        new Command<MapClickedEventArgs>(args =>
        {
            //Pins.Add(new Pin
            //{
            //    Label = $"Pin{Pins.Count}",
            //    Position = args.Point
            //});
        });
        //public Map MapControl { get; set; }

        public MeetingMapViewModel()
        {
            AvailableMeetings = MeetingsData.Meetings; // TODO handle filters
            //MeetingsLocations = MeetingsData.Meetings.Select(m => m.position).ToList(); 
            //MapControl.MapClicked += HandleMapClicked;
            //HandleMapClickedCommand = new Command(OnMapClicked);
        }

        //private void HandleMapClicked(object sender, MapClickedEventArgs e)
        //{
        //    var postion = e.Position;
        //    // ...
        //}

        //void OnMapClicked(object sender, MapClickedEventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine($"MapClick: {e.Position.Latitude}, {e.Position.Longitude}");
        //}

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
