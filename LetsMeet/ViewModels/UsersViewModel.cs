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
            PageAppearingCommand = new Command(FilterUsers);
        }
        public ObservableCollection<User> _usersList { get; set; }
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
            ObservableCollection<User> tempUsersList = new ObservableCollection<User>();

            void VerifyAdd(User u)
            {
                if (!tempUsersList.Contains(u) && u.age < MaxAge && u.age > MinAge)
                    tempUsersList.Add(u);
            };

            if (IsFriend)
                UsersData.AllUsers.FindAll(u =>MainViewModel.GetInstance.CurrentUser.IsFreind(u)).ForEach(VerifyAdd);
            if (!IsFriend)
                UsersData.AllUsers.ForEach(VerifyAdd);

            _usersList = tempUsersList;
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
