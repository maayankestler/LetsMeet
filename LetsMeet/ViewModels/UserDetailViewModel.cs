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
        public User User { get; set; }
        public bool IsLoggedOnUser { get; set; }
        public bool IsFriend { get; set; }
        public ICommand Logout { get; }
        public ICommand AddFriend { get; }
        public ICommand RemoveFriend { get; }
        private string Id = null;

        public UserDetailViewModel()
        {
            Logout = new Command(MainViewModel.GetInstance.LogOut);
            AddFriend = new Command(AddFriend_Button_Clicked);
            RemoveFriend = new Command(RemoveFriend_Button_Clicked);
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (query.ContainsKey("id"))
            {
                // Only a single query parameter is passed, which needs URL decoding.
                Id = HttpUtility.UrlDecode(query["id"]);
            }
        }

        public void LoadUser()
        {
            try
            {
                string UserId = (Id == null) ? MainViewModel.GetInstance.CurrentUser.Id : Id;
                User = UsersData.Users.FirstOrDefault(a => a.Id == UserId);
                IsLoggedOnUser = User.Equals(MainViewModel.GetInstance.CurrentUser);
                IsFriend = MainViewModel.GetInstance.CurrentUser.IsFreind(User);
                OnPropertyChanged("User");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load user.");
            }
        }

        void AddFriend_Button_Clicked()
        {
            MainViewModel.GetInstance.CurrentUser.AddFriend(User);
            IsFriend = true;
            OnPropertyChanged("Friends");
        }

        void RemoveFriend_Button_Clicked()
        {
            MainViewModel.GetInstance.CurrentUser.RemoveFriend(User);
            IsFriend = false;
            OnPropertyChanged("Friends");
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
