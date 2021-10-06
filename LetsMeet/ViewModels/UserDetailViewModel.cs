using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using Xamarin.Forms;
using LetsMeet.Data;
using LetsMeet.Models;

namespace LetsMeet.ViewModels
{
    public class UserDetailViewModel : IQueryAttributable, INotifyPropertyChanged
    {
        public User User { get; private set; }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            // Only a single query parameter is passed, which needs URL decoding.
            string name = HttpUtility.UrlDecode(query["name"]);
            LoadUser(name);
        }

        void LoadUser(string name)
        {
            try
            {
                User = UsersData.Users.FirstOrDefault(a => a.Name == name);
                OnPropertyChanged("User");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load user.");
            }
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
