
using LetsMeet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

public class MeetingCategory {

    public MeetingCategory(string id, string name, string IconURL)
    {
        this.Id = Interlocked.Increment(ref nextId).ToString();
        this.Name = name;
        this.IconURL = IconURL;
    }

    static int nextId = 0;
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