namespace LetsMeet.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using LetsMeet.Data;
    using Xamarin.Forms.Maps;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Bson.Serialization.IdGenerators;
    using Xamarin.Essentials;

    public class Meeting
    {
        public Meeting(string name, string iconURL, string typeId, DateTime startTime, DateTime endTime,
                       string ownerId, int minMembers, int maxMembers, int minAge, int maxAge, Location location)
        {
            this.Name = name;
            this.IconURL = iconURL;
            this._tpyeId = typeId;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.OwnerId = ownerId;
            this.MembersIds = new HashSet<string>();
            this.MembersIds.Add(OwnerId);
            this.Status = MeetingStatus.Available;
            this.MinMembers = minMembers;
            this.MaxMembers = maxMembers;
            this.MinAge = minAge;
            this.MaxAge = maxAge;
            this.Location = location;
        }

        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("TypeId")]
        private string _tpyeId;
        

        [BsonElement("MinMembers")]
        public int MinMembers { get; set; }

        [BsonElement("MaxMembers")]
        public int MaxMembers { get; set; }

        [BsonElement("MinAge")]
        public int MinAge { get; set; }

        [BsonElement("MaxAge")]
        public int MaxAge { get; set; }

        private string _iconURL = null;
        [BsonElement("IconURL")]
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

        [BsonElement("MembersIds")]
        public HashSet<string> MembersIds = new HashSet<string>();
        public HashSet<User> Members
        {
            get
            {
                List<User> members = MembersIds.ToList().ConvertAll(new Converter<string, User>(UsersData.GetUser));
                HashSet<User> MembersHashSet = new HashSet<User>(members);
                MembersHashSet.Remove(null);
                return MembersHashSet;
            }
        }

        [BsonElement("StartTime")]
        public DateTime StartTime { get; set; }

        [BsonElement("EndTime")]
        public DateTime EndTime { get; set; }

        [BsonElement("Status")]
        public MeetingStatus Status { get; private set; }

        [BsonElement("OwnerId")]
        public string OwnerId;

        public User Owner
        {
            get
            {
                return UsersData.GetUser(OwnerId);
            }
        }

        [BsonElement("Location")]
        public Location Location { get; set; }

        public void Cancel()
        {
            this.Status = MeetingStatus.Cancelled;
            MeetingsData.UpdateMeeting(this);
        }

        public void AddMember(User member)
        {
            if (member.age >= MinAge && member.age <= MaxAge && MembersIds.Count < MaxMembers)
            {
                MembersIds.Add(member.Id);
                MeetingsData.UpdateMeeting(this);
            }
            else
                Console.WriteLine("can't join meeting");
        }
        public void RemoveMember(User member)
        {
            MembersIds.Remove(member.Id);
            MeetingsData.UpdateMeeting(this);
        }

        public void RemoveMembers(List<User> members)
        {
            members.ForEach(m => RemoveMember(m));
            MeetingsData.UpdateMeeting(this);
        }

    }
}

public enum MeetingStatus
{
    Available,
    Cancelled,
    Done,
    InProgress,
    Template
}