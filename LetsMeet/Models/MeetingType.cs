
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using LetsMeet.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
public class MeetingType {

    public MeetingType(string name, string IconURL, string category_id) {
        this.Name = name;
        this.IconURL = IconURL;
        this.CategoryId = category_id;
    }

    [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; private set; }

    [BsonElement("Name")]
    public string Name { get; set; }

    [BsonElement("IconURL")]
    public string IconURL { get; set; }
    [BsonElement("CategoryId")]
    public string CategoryId { get; private set; }
    public MeetingCategory category
    {
        get
        {
            return MeetingCatogriesData.GetCategory(CategoryId);
        }
    }

    // private string template_id;

}