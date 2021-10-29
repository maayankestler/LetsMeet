
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LetsMeet.Data;

public class MeetingType {

    public MeetingType(string id, string name, string IconURL, string category_id) {
        this.Id = id;
        this.Name = name;
        this.IconURL = IconURL;
        this.CategoryId = category_id;
    }

    public string Id { get; private set; }

    public string Name { get; set; }

    public string IconURL { get; set; }

    public string CategoryId { get; private set; }
    public MeetingCategory category
    {
        get
        {
            return MeetingCatogriesData.GetCategory(CategoryId);
        }
    }

    private string template_id;

}