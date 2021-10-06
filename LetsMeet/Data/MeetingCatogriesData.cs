using System;
using System.Collections.Generic;
using LetsMeet.Models;

namespace LetsMeet.Data
{
    public static class MeetingCatogriesData
    {
        public static IList<MeetingCategory> MeetingCategories { get; private set; }
        static MeetingCatogriesData()
        {
            MeetingCategories = new List<MeetingCategory>();

            MeetingCategories.Add(new MeetingCategory
            {

            });
        }
    }
}
