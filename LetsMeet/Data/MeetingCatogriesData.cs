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
            MeetingCategories = GetAllMeetingCategories();
        }

        public static List<MeetingCategory> GetAllMeetingCategories()
        {
            List<MeetingCategory> MeetingCategoriesList = new List<MeetingCategory>();

            MeetingCategoriesList.Add(new MeetingCategory("1", "Sport", "https://png.pngtree.com/png-clipart/20190613/original/pngtree-cartoon-sports-fitness-equipment-png-image_3594503.jpg"));

            MeetingCategoriesList.Add(new MeetingCategory("2", "Board and cards games", "https://png.pngtree.com/png-clipart/20190920/original/pngtree-card-game-cartoon-illustration-png-image_4651168.jpg"));

            MeetingCategoriesList.Add(new MeetingCategory("3", "Social", "https://png.pngtree.com/png-clipart/20190604/original/pngtree-friend-png-image_1280871.jpg"));

            return MeetingCategoriesList;
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

        public static void CreateMeetingCategory(MeetingCategory m)
        {
            MeetingCategories.Add(m);
        }
    }
}
