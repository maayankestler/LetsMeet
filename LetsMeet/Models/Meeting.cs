
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Meeting {

    public Meeting() {
    }

    public string Id;

    public string Name;

    private string _tpyeId;

    public MeetingType Tpye
    {
        get
        {
            //return GetMeetingTypeById(this._tpyeId);
            return new MeetingType();
        }
    }

    //public GeoCoordinate Location;

    private string[] _membersIds = { };

    public User[] Members;

    public DateTime StartTime;

    public DateTime EndTime;

    public string Status { get; }

    private string _ownerId;

    public User Owner;



    public void cancel() {
        // TODO implement here
    }

    public void start() {
        // TODO implement here
    }

    public void get_members() {
        // TODO implement here
    }

}