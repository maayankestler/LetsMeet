using System;
using System.Collections.Generic;
using LetsMeet.Models;

namespace LetsMeet.Data
{
    public static class MeetingTypesData
    {
        public static IList<MeetingType> MeetingsTypes { get; private set; }
        static MeetingTypesData()
        {
            MeetingsTypes = new List<MeetingType>();

            MeetingsTypes.Add(new MeetingType
            {

            });
        }
    }
}
