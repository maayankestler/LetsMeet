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
        private string _userId = null;
        public string UserId
        {
            get
            {
                return (_userId == null) ? MainViewModel.GetInstance.CurrentUser.Id : _userId;
            }
            set
            {
                _userId = value;
            }
        }
        public User User 
        { 
            get
            {
                return UsersData.GetUser(UserId);
            }
        }
        public bool IsLoggedOnUser 
        { 
            get
            {
                return User.Equals(MainViewModel.GetInstance.CurrentUser);
            }
        }
        public bool IsFriend 
        { 
            get
            {
                return (User != null && MainViewModel.GetInstance.CurrentUser.IsFreind(User));
            }
        }
        public bool IsAdmin
        {
            get
            {
                return MainViewModel.GetInstance.CurrentUser.IsAdmin;
            }
        }
        public ICommand Logout { get; }
        public ICommand AddFriend { get; }
        public ICommand RemoveFriend { get; }
        public ICommand RemoveFriends { get; }
        public ICommand RemoveUser { get; }
        public ObservableCollection<object> SelectedObjects { get; set; }
        public List<User> SelectedMembers { get; set; }


        public UserDetailViewModel()
        {
            Logout = new Command(MainViewModel.GetInstance.LogOut);
            AddFriend = new Command(AddFriend_Button_Clicked);
            RemoveFriend = new Command(RemoveFriend_Button_Clicked);
            RemoveFriends = new Command(RemoveFriends_Button_Clicked);
            RemoveUser = new Command(RemoveUser_Button_Clicked);
            SelectedObjects = new ObservableCollection<object>();
            SelectedObjects.CollectionChanged += SelectedObjectsChanged;
            SelectedMembers = new List<User>();
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (query.ContainsKey("id"))
            {
                // Only a single query parameter is passed, which needs URL decoding.
                UserId = HttpUtility.UrlDecode(query["id"]);
            }
        }

        public void LoadUser()
        {
            OnPropertyChanged("User");
        }

        void AddFriend_Button_Clicked()
        {
            MainViewModel.GetInstance.CurrentUser.AddFriend(User);
            OnPropertyChanged("IsFriend");
            OnPropertyChanged("User");
        }

        void RemoveFriend_Button_Clicked()
        {
            MainViewModel.GetInstance.CurrentUser.RemoveFriend(User);
            OnPropertyChanged("IsFriend");
            OnPropertyChanged("User");
        }
        void RemoveFriends_Button_Clicked()
        {
            MainViewModel.GetInstance.CurrentUser.RemoveFriends(SelectedMembers);
            OnPropertyChanged("User");
        }

        async void RemoveUser_Button_Clicked()
        {
            bool WasLoggedOnUser = IsLoggedOnUser;
            UsersData.RemoveUser(UserId);
            if (WasLoggedOnUser)
                Logout.Execute(null);
            else
                await Shell.Current.GoToAsync("//users");
            OnPropertyChanged("Users");
        }

        void SelectedObjectsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
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
