
using LetsMeet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class MeetingCategory {

    public MeetingCategory(string name, string IconURL)
    {
        this.Name = name;
        this.IconURL = IconURL;
    }

    [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; private set; }

    [BsonElement("Name")]
    public string Name { get; set; }

    [BsonElement("IconURL")]
    public string IconURL { get; set; }

    public List<MeetingType> Types
    {
        get
        {
            return MeetingTypesData.GetAllMeetingByCategory(Id);
        }
    }

}