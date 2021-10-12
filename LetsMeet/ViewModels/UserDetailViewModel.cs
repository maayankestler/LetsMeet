using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using Xamarin.Forms;
using LetsMeet.Data;
using LetsMeet.Models;
using System.Windows.Input;

namespace LetsMeet.ViewModels
{
    public class UserDetailViewModel : IQueryAttributable, INotifyPropertyChanged
    {
        public User User { get; private set; }
        public bool IsLoggedOnUser { get; private set; } = false;
        public ICommand Logout { get; }

        public UserDetailViewModel()
        {
            Logout = new Command(MainViewModel.GetInstance.LogOut);
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (query.ContainsKey("id"))
            {
                // Only a single query parameter is passed, which needs URL decoding.
                string id = HttpUtility.UrlDecode(query["id"]);
                LoadUser(id);
            }
            else
            {
                LoadUser();
            }
        }

        void LoadUser(string id)
        {
            try
            {
                User = UsersData.Users.FirstOrDefault(a => a.Id == id);
                IsLoggedOnUser = User.Equals(MainViewModel.GetInstance.CurrentUser);
                OnPropertyChanged("User");
                OnPropertyChanged("IsLoggedOnUser");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load user.");
            }
        }

        void LoadUser()
        {
            LoadUser(MainViewModel.GetInstance.CurrentUser.Id);
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
