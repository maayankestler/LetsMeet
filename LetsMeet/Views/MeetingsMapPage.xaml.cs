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


namespace LetsMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeetingsMapPage : ContentPage, INotifyPropertyChanged
    {
        public List<Meeting> AvailableMeetings;
        public MeetingsMapPage()
        {
            InitializeComponent();
            AvailableMeetings = MeetingsData.Meetings; // TODO handle filters
            //BindingContext = new MeetingMapViewModel();
        }

        private void HandleMapClicked(object sender, MapClickedEventArgs e)
        {
            var postion = e.Position;
            // ...
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