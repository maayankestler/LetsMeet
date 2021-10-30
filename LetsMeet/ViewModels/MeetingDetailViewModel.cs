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
        public Meeting Meeting { get; set; }
        public bool IsInMeeting
        {
            get
            {
                return (Meeting != null && Meeting.Members.Contains(MainViewModel.GetInstance.CurrentUser));
            }
        }
        public bool IsOwner 
        { 
            get
            {
                return (Meeting != null && Meeting.Owner == MainViewModel.GetInstance.CurrentUser);
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
            JoinMeeting = new Command(JoinMeeting_Button_Clicked);
            QuitMeeting = new Command(QuitMeeting_Button_Clicked);
            CancelMeeting = new Command(CancelMeeting_Button_Clicked);
            RemoveMembers = new Command(RemoveMembers_Button_Clicked);
            SelectedObjects = new ObservableCollection<object>();
            SelectedObjects.CollectionChanged += SelectedObjectsChanged;
            SelectedMembers = new List<User>();
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
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load Meeting.");
            }
        }

        void JoinMeeting_Button_Clicked()
        {
            Meeting.AddMember(MainViewModel.GetInstance.CurrentUser);
            OnPropertyChanged("IsInMeeting"); 
            OnPropertyChanged("Meeting");
        }

        void QuitMeeting_Button_Clicked()
        {
            Meeting.RemoveMember(MainViewModel.GetInstance.CurrentUser);
            OnPropertyChanged("IsInMeeting");
            OnPropertyChanged("Meeting");
        }

        void RemoveMembers_Button_Clicked()
        {
            Meeting.RemoveMembers(SelectedMembers);
            OnPropertyChanged("Meeting");
        }

        void CancelMeeting_Button_Clicked()
        {
            Meeting.Cancel();
        }

        void SelectedObjectsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SelectedMembers = SelectedObjects.OfType<User>().ToList();
            // remove owner
            var itemToRemove = SelectedMembers.SingleOrDefault(m => m.Id == Meeting.Owner.Id);
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
