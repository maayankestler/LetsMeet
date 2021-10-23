
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LetsMeet.Data;

public class Meeting {

    public Meeting(string id, string name, string IconURL, string type_id, DateTime StartTime, DateTime EndTime, 
                   string owner_id, int min_members, int max_members, int min_age, int max_age) 
    {
        this.Id = id;
        this.Name = name;
        this.IconURL = IconURL;
        this._tpyeId = type_id;
        this.StartTime = StartTime;
        this.EndTime = EndTime;
        this._ownerId = owner_id;
        this._membersIds = new List<string>();
        this._membersIds.Add(_ownerId);
        this.Status = "Available"; // TODO manage statuses (Available/Cancelled/Done/Inprogress/Template .....)
        this.MinMembers = min_members;
        this.MaxMembers = max_members;
        this.MinAge = min_age;
        this.MaxAge = max_age;
    }

    public string Id { get; set; }

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

    public string Status { get; }

    private string _ownerId;

    public User Owner
    {
        get
        {
            return UsersData.GetUser(_ownerId);
        }
    }



    public void Cancel() {
        // TODO implement here
    }

    public void start() {
        // TODO implement here
    }

    public void AddMember(User Member) {
        _membersIds.Add(Member.Id);
    }
    public void RemoveMember(User Member)
    {
        _membersIds.Remove(Member.Id);
    }

}