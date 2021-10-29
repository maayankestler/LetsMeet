using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using LetsMeet.Views;

namespace LetsMeet
{
    public partial class AppShell : Shell
    {
        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();
        public ICommand HelpCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
            BindingContext = this;
        }

        void RegisterRoutes()
        {
            Routes.Add("UserDetails", typeof(UserDetailPage));
            Routes.Add("MeetingDetails", typeof(MeetingDetailPage));
            Routes.Add("users", typeof(UserPage));
            Routes.Add("ChooseLocation", typeof(MapGetLocationPage));


            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }

        private void Tab_Appearing(object sender, EventArgs e)
        {

        }
    }
}
