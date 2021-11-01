using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Collections;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;

using LetsMeet.Controls;
using LetsMeet.Data;
using LetsMeet.ViewModels;
using LetsMeet.Models;
using System.Windows.Input;

namespace LetsMeet.ViewModels
{
    class MeetingsViewModel: INotifyPropertyChanged
    {
        public MeetingsViewModel()
        {
            FilterMeetings();
            PageAppearingCommand = new Command(FilterMeetings);
        }

        public ObservableCollection<Meeting> _meetingsList { get; set; } = new ObservableCollection<Meeting>(MeetingsData.Meetings);
        public IEnumerable AvailableMeetings => _meetingsList;
        public ICommand PageAppearingCommand { get; }
        private bool _onwedByMe = false;
        public bool OwnedByMe
        {
            get
            {
                return _onwedByMe;
            }
            set
            {
                _onwedByMe = value;
                FilterMeetings();
            }
        }
        private bool _onwedByFriend = false;
        public bool OwnedByFriend
        {
            get
            {
                return _onwedByFriend;
            }
            set
            {
                _onwedByFriend = value;
                FilterMeetings();
            }
        }
        private bool _member = false;
        public bool Member
        {
            get
            {
                return _member;
            }
            set
            {
                _member = value;
                FilterMeetings();
            }
        }

        private void FilterMeetings()
        {
            ObservableCollection<Meeting> temp_meetings_list = new ObservableCollection<Meeting>();

            void VerifyAdd(Meeting m)
            {
                if (!temp_meetings_list.Contains(m) && m.Status == MeetingStatus.Available)
                    temp_meetings_list.Add(m);
            };

            if (OwnedByMe)
                MeetingsData.Meetings.FindAll(m => m.Owner != null && m.Owner.Id == MainViewModel.GetInstance.CurrentUser.Id).ForEach(VerifyAdd);
            if (OwnedByFriend)
                MeetingsData.Meetings.FindAll(m => MainViewModel.GetInstance.CurrentUser.Friends.Contains(m.Owner)).ForEach(VerifyAdd);
            if (Member)
                MeetingsData.Meetings.FindAll(m => m.Members.Contains(MainViewModel.GetInstance.CurrentUser)).ForEach(VerifyAdd);
            if (!OwnedByMe && !OwnedByFriend && !Member)
                MeetingsData.Meetings.ForEach(VerifyAdd);

            _meetingsList = temp_meetings_list;
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
