using System;
using System.Collections.Generic;
using LetsMeet.Models;
using Xamarin.Forms.Maps;

namespace LetsMeet.Data
{
    public static class MeetingsData
    {
        public static List<Meeting> Meetings { get; private set; }
        static MeetingsData()
        {
            Meetings = GetAllMeetings(); 
        }

        public static List<Meeting> GetAllMeetings()
        {
            List<Meeting> MeetingsList = new List<Meeting>();
            MeetingsList.Add(new Meeting(
                "soccer brother",
                "https://png.pngtree.com/png-clipart/20190611/original/pngtree-beautiful-blue-cartoon-soccer-field-png-image_2749691.jpg",
                "1",
                new DateTime(2021, 10, 23, 12, 0, 0),
                new DateTime(2021, 10, 23, 15, 0, 0),
                "1",
                10,
                15,
                15,
                30,
                new Position(32.2271155528261, 34.9893602728844)));
            MeetingsList.Add(new Meeting(
                "catan kings",
                "https://e7.pngegg.com/pngimages/692/115/png-clipart-catan-boardgamegeek-dice-board-game-dice-game-dice-thumbnail.png",
                "5",
                new DateTime(2021, 10, 24, 20, 0, 0),
                new DateTime(2021, 10, 24, 22, 0, 0),
                "3",
                3,
                4,
                16,
                60,
                new Position(32.2155361580173, 34.9895503744483)));
            MeetingsList.Add(new Meeting(
                "pokerrrr",
                null,
                "4",
                new DateTime(2021, 11, 1, 20, 0, 0),
                new DateTime(2021, 11, 1, 22, 0, 0),
                "2",
                4,
                6,
                18,
                80,
                new Position(32.2289114408791, 35.0048205256462)));

            return MeetingsList; // TODO return only available
        }
        
        public static List<Meeting> GetMeetingsByUser(User User)
        {
            return Meetings.FindAll(m => m.Members.Contains(User));
        }

        public static Meeting GetMeetingById(string MeetingId)
        {
            return Meetings.Find(m => m.Id == MeetingId);
        }

        public static List<Meeting> GetMeetingsByUser(string UserId)
        {
            return Meetings.FindAll(m => m.Members.Exists(member => member.Id == UserId));
        }

        public static void CreateMeeting(Meeting m)
        {
            Meetings.Add(m);
        }
    }
}
