using System;
using System.Collections.Generic;
using LetsMeet.Models;

namespace LetsMeet.Data
{
    public static class MeetingsData
    {
        public static IList<Meeting> Meetings { get; private set; }
        static MeetingsData()
        {
            Meetings = new List<Meeting>();

            Meetings.Add(new Meeting
            {
                
            });
        }
    }
}
