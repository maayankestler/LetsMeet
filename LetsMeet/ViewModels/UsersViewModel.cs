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
using System.Collections;

namespace LetsMeet.ViewModels
{
    class UsersViewModel : INotifyPropertyChanged
    {
        public UsersViewModel()
        {
            FilterUsers();
            PageAppearingCommand = new Command(FilterUsers);
        }
        public ObservableCollection<User> _usersList { get; set; } = new ObservableCollection<User>(UsersData.Users);
        public IEnumerable AvailableUsers => _usersList;
        public ICommand PageAppearingCommand { get; }
        private bool _isFriend = false;
        public bool IsFriend
        {
            get
            {
                return _isFriend;
            }
            set
            {
                _isFriend = value;
                FilterUsers();
            }
        }

        private int _minAge = 0;
        public int MinAge
        {
            get
            {
                return _minAge;
            }
            set
            {
                _minAge = value;
                FilterUsers();
            }
        }

        private int _maxAge = 100;
        public int MaxAge
        {
            get
            {
                return _maxAge;
            }
            set
            {
                _maxAge = value;
                FilterUsers();
            }
        }

        private void FilterUsers()
        {
            ObservableCollection<User> temp_users_list = new ObservableCollection<User>();

            void VerifyAdd(User u)
            {
                if (!temp_users_list.Contains(u) && u.age < MaxAge && u.age > MinAge)
                    temp_users_list.Add(u);
            };

            if (IsFriend)
                UsersData.Users.FindAll(u =>MainViewModel.GetInstance.CurrentUser.IsFreind(u)).ForEach(VerifyAdd);
            if (!IsFriend)
                UsersData.Users.ForEach(VerifyAdd);

            _usersList = temp_users_list;
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
