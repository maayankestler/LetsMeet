using System;
using System.Collections.Generic;
using LetsMeet.Models;

namespace LetsMeet.Data
{
    public static class MeetingCatogriesData
    {
        public static List<MeetingCategory> MeetingCategories { get; private set; }
        static MeetingCatogriesData()
        {
            MeetingCategories = new List<MeetingCategory>();

            MeetingCategories.Add(new MeetingCategory
            {
                Id="1",
                Name="Sport",
                IconURL= "https://png.pngtree.com/png-clipart/20190613/original/pngtree-cartoon-sports-fitness-equipment-png-image_3594503.jpg"
            });

            MeetingCategories.Add(new MeetingCategory
            {
                Id = "2",
                Name = "Board and cards games",
                IconURL = "https://png.pngtree.com/png-clipart/20190920/original/pngtree-card-game-cartoon-illustration-png-image_4651168.jpg"
            });

            MeetingCategories.Add(new MeetingCategory
            {
                Id = "3",
                Name = "Social",
                IconURL = "https://png.pngtree.com/png-clipart/20190604/original/pngtree-friend-png-image_1280871.jpg"
            });
        }

        public static MeetingCategory GetCategory(string id)
        {
            var Category = MeetingCategories.Find(x => x.Id == id);
            if (Category == null)
            {
                System.Diagnostics.Debug.WriteLine("can't find Category"); // TODO make display alert
            }
            return Category;
        }
    }
}
