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
    class MeetingDetailViewModel : IQueryAttributable, INotifyPropertyChanged
    {
        private string _meetingId = null;
        public string MeetingId
        {
            get
            {
                return _meetingId;
            }
            set
            {
                if (_meetingId != value)
                {
                    _meetingId = value;
                    Meeting = MeetingsData.GetMeetingById(_meetingId);
                }
            }
        }
        public Meeting Meeting { get; private set; }
        public bool IsInMeeting
        {
            get
            {
                return (Meeting != null && Meeting.MembersIds.Contains(MainViewModel.GetInstance.CurrentUser.Id));
            }
        }
        public bool IsOwner
        {
            get
            {
                return (Meeting != null && Meeting.OwnerId == MainViewModel.GetInstance.CurrentUser.Id);
            }
        }
        public ICommand JoinMeeting { get; }
        public ICommand QuitMeeting { get; }
        public ICommand CancelMeeting { get; }
        public ICommand RemoveMembers { get; }
        public ObservableCollection<object> SelectedObjects { get; set; }
        public List<User> SelectedMembers { get; set; }

        public MeetingDetailViewModel()
        {
            JoinMeeting = new Command(JoinMeetingButtonClicked);
            QuitMeeting = new Command(QuitMeetingButtonClicked);
            CancelMeeting = new Command(CancelMeetingButtonClicked);
            RemoveMembers = new Command(RemoveMembersButtonClicked);
            SelectedObjects = new ObservableCollection<object>();
            SelectedObjects.CollectionChanged += SelectedObjectsChanged;
            SelectedMembers = new List<User>();
        }
        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (query.ContainsKey("id"))
            {
                MeetingId = HttpUtility.UrlDecode(query["id"]);
                OnPropertyChanged("Meeting");
            }
        }

        void JoinMeetingButtonClicked()
        {
            Meeting.AddMember(MainViewModel.GetInstance.CurrentUser);
            OnPropertyChanged("IsInMeeting"); 
            OnPropertyChanged("Meeting");
        }

        void QuitMeetingButtonClicked()
        {
            Meeting.RemoveMember(MainViewModel.GetInstance.CurrentUser);
            OnPropertyChanged("IsInMeeting");
            OnPropertyChanged("Meeting");
        }

        void RemoveMembersButtonClicked()
        {
            Meeting.RemoveMembers(SelectedMembers);
            OnPropertyChanged("Meeting");
        }

        async void CancelMeetingButtonClicked()
        {
            Meeting.Cancel();
            await Shell.Current.GoToAsync("..");
        }

        void SelectedObjectsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SelectedMembers = SelectedObjects.OfType<User>().ToList();
            // remove owner
            var itemToRemove = SelectedMembers.SingleOrDefault(m => m.Id == Meeting.Owner.Id);
            if (itemToRemove != null)
            {
                SelectedMembers.Remove(itemToRemove);
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
