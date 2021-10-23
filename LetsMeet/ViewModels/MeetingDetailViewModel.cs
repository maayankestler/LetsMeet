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
    class MeetingDetailViewModel : IQueryAttributable, INotifyPropertyChanged
    {
        public Meeting Meeting { get; set; }
        public bool IsInMeeting { get; private set; }
        public bool IsOwner { get; private set; }
        public ICommand JoinMeeting { get; }
        public ICommand QuitMeeting { get; }
        public ICommand CancelMeeting { get; }

        public MeetingDetailViewModel()
        {
            JoinMeeting = new Command(JoinMeeting_Button_Clicked);
            QuitMeeting = new Command(QuitMeeting_Button_Clicked);
            CancelMeeting = new Command(CancelMeeting_Button_Clicked);
        }
        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (query.ContainsKey("id"))
            {
                // Only a single query parameter is passed, which needs URL decoding.
                LoadMeeting(HttpUtility.UrlDecode(query["id"]));
            }
        }

        public void LoadMeeting(string id)
        {
            try
            {
                Meeting = MeetingsData.Meetings.FirstOrDefault(a => a.Id == id);
                IsInMeeting = (Meeting.Members.Contains(MainViewModel.GetInstance.CurrentUser));
                IsOwner = (Meeting.Owner.Id == MainViewModel.GetInstance.CurrentUser.Id);
                //OnPropertyChanged("Meeting");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load Meeting.");
            }
        }

        void JoinMeeting_Button_Clicked()
        {
            Meeting.AddMember(MainViewModel.GetInstance.CurrentUser);
            IsInMeeting = true;
            OnPropertyChanged("Meeting");
        }

        void QuitMeeting_Button_Clicked()
        {
            Meeting.RemoveMember(MainViewModel.GetInstance.CurrentUser);
            IsInMeeting = false;
            OnPropertyChanged("Meeting");
        }

        void CancelMeeting_Button_Clicked()
        {
            Meeting.Cancel();
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
