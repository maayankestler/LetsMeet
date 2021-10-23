
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
        this._categoryId = category_id;
    }

    public string Id;

    public string Name { get; set; }

    public string IconURL { get; set; }

    private string _categoryId;
    public MeetingCategory category
    {
        get
        {
            return MeetingCatogriesData.GetCategory(_categoryId);
        }
    }

    private string template_id;

}