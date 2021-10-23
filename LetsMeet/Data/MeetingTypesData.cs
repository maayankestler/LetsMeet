using System;
using System.Collections.Generic;
using LetsMeet.Models;

namespace LetsMeet.Data
{
    public static class MeetingTypesData
    {
        public static List<MeetingType> MeetingsTypes { get; private set; }
        static MeetingTypesData()
        {
            MeetingsTypes = new List<MeetingType>();

            MeetingsTypes.Add(new MeetingType(
                "1",
                "soccer", 
                "https://png.pngtree.com/png-clipart/20200225/original/pngtree-soccer-ball-icon-circle-png-image_5278517.jpg", 
                "1"));
            MeetingsTypes.Add(new MeetingType(
                "2",
                "basketball",
                "https://png.pngtree.com/element_our/20200702/ourmid/pngtree-cartoon-vector-basketball-hand-drawn-image_2283539.jpg",
                "1"));
            MeetingsTypes.Add(new MeetingType(
                "3",
                "baseball",
                "https://png.pngtree.com/png-clipart/20190916/original/pngtree-white-cartoon-baseball-illustration-png-image_4594709.jpg",
                "1"));

            MeetingsTypes.Add(new MeetingType(
                "4",
                "poker",
                "https://png.pngtree.com/png-clipart/20190604/original/pngtree-poker-png-image_1398414.jpg",
                "2"));
            MeetingsTypes.Add(new MeetingType(
                "5",
                "catan",
                "https://m.media-amazon.com/images/I/81+okm4IpfL._AC_SL1500_.jpg",
                "2"));
            MeetingsTypes.Add(new MeetingType(
                "6",
                "monopoly",
                "https://www.merchandisingplaza.co.uk/9507/2/T-shirts---Monopoly-Logo-Tee-Shirt-l.jpg",
                "2"));
        }

        public static MeetingType GetMeetingType(string id)
        {
            var Type = MeetingsTypes.Find(x => x.Id == id);
            if (Type == null)
            {
                System.Diagnostics.Debug.WriteLine("can't find Type"); // TODO make display alert
            }
            return Type;
        }
    }
}
