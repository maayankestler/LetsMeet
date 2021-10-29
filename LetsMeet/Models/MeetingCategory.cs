
using LetsMeet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MeetingCategory {

    public MeetingCategory(string id, string name, string IconURL)
    {
        this.Id = id;
        this.Name = name;
        this.IconURL = IconURL;
    }

    public string Id { get; private set; }

    public string Name { get; set; }

    public string IconURL { get; set; }

    public List<MeetingType> Types
    {
        get
        {
            return MeetingTypesData.GetAllMeetingByCategory(Id);
        }
    }

}