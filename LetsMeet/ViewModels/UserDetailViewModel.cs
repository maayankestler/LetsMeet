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
using System.Collections.ObjectModel;

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
        public ICommand RemoveFriends { get; }
        public ObservableCollection<object> SelectedObjects { get; set; }
        public List<User> SelectedMembers { get; set; }
        private string Id = null;

        public UserDetailViewModel()
        {
            Logout = new Command(MainViewModel.GetInstance.LogOut);
            AddFriend = new Command(AddFriend_Button_Clicked);
            RemoveFriend = new Command(RemoveFriend_Button_Clicked);
            RemoveFriends = new Command(RemoveFriends_Button_Clicked);
            SelectedObjects = new ObservableCollection<object>();
            SelectedObjects.CollectionChanged += SelectedObjectsChanged;
            SelectedMembers = new List<User>();
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
            OnPropertyChanged("User");
        }

        void RemoveFriend_Button_Clicked()
        {
            MainViewModel.GetInstance.CurrentUser.RemoveFriend(User);
            IsFriend = false;
            OnPropertyChanged("User");
        }
        void RemoveFriends_Button_Clicked()
        {
            MainViewModel.GetInstance.CurrentUser.RemoveFriends(SelectedMembers);
            OnPropertyChanged("User");
        }

        async void SelectedObjectsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SelectedMembers = SelectedObjects.OfType<User>().ToList();
            // remove owner
            var itemToRemove = SelectedMembers.SingleOrDefault(m => m.Id == MainViewModel.GetInstance.CurrentUser.Id);
            if (itemToRemove != null)
            {
                SelectedMembers.Remove(itemToRemove);
                // SelectedObjects.Remove(itemToRemove);
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
