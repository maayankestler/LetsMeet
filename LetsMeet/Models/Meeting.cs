namespace LetsMeet.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using LetsMeet.Data;
    using Xamarin.Forms.Maps;

    public class Meeting
    {
        public Meeting(string name, string IconURL, string type_id, DateTime StartTime, DateTime EndTime,
                       string owner_id, int min_members, int max_members, int min_age, int max_age, Position position)
        {
            this.Id = Interlocked.Increment(ref nextId).ToString();
            this.Name = name;
            this.IconURL = IconURL;
            this._tpyeId = type_id;
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            this._ownerId = owner_id;
            this._membersIds = new List<string>();
            this._membersIds.Add(_ownerId);
            this.Status = MeetingStatus.Available;  //"Available"; // TODO manage statuses (Available/Cancelled/Done/Inprogress/Template .....)
            this.MinMembers = min_members;
            this.MaxMembers = max_members;
            this.MinAge = min_age;
            this.MaxAge = max_age;
            this.Position = position;
        }

        static int nextId = 0;
        public string Id { get; private set; }

        public string Name { get; set; }

        private string _tpyeId;
        private string _iconURL = null;
        public int MinMembers { get; set; }
        public int MaxMembers { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public string IconURL
        {
            get
            {
                if (_iconURL == null)
                {
                    return Type.IconURL;
                }
                else
                {
                    return _iconURL;
                }
            }
            set
            {
                _iconURL = value;
            }
        }

        public MeetingType Type
        {
            get
            {
                return MeetingTypesData.GetMeetingType(this._tpyeId);
            }
        }

        //public GeoCoordinate Location;

        private List<string> _membersIds = new List<string>();
        public List<User> Members
        {
            get
            {
                List<User> members = _membersIds.ConvertAll(new Converter<string, User>(UsersData.GetUser));
                return members;
            }
        }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public MeetingStatus Status { get; private set; }

        private string _ownerId;

        public User Owner
        {
            get
            {
                return UsersData.GetUser(_ownerId);
            }
        }

        public Position Position { get; set; }

        public void Cancel()
        {
            this.Status = MeetingStatus.Cancelled;
        }

        public void AddMember(User Member)
        {
            if (Member.age >= MinAge && Member.age <= MaxAge && _membersIds.Count < MaxMembers)
                _membersIds.Add(Member.Id);
            else
                Console.WriteLine("can't join meeting");
        }
        public void RemoveMember(User Member)
        {
            _membersIds.Remove(Member.Id);
        }

        public void RemoveMembers(List<User> Members)
        {
            Members.ForEach(m => RemoveMember(m));
        }

    }
}

public enum MeetingStatus
{
    Available,
    Cancelled,
    Done,
    Inprogress,
    Template
}