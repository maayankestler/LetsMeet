using System;
using System.Collections.Generic;
using LetsMeet.Models;
using LetsMeet.Data;

namespace LetsMeet.Data
{
    public static class UsersData
    {
        public static List<User> Users { get; private set; }
        static UsersData()
        {
            Users = GetAllUsers();
        }

        public static List<User> GetAllUsers()
        {
            List<User> UsersList = new List<User>();

            UsersList.Add(new User(
                "maayan",
                "t",
                "a",
                "https://www.onlyou.co.il/data/EIw2xdZedc_500572.jpg",
                new DateTime(1997, 3, 17)
            ));

            UsersList.Add(new User(
                "ido",
                "ido5",
                "555",
                "https://png.pngtree.com/png-vector/20191101/ourmid/pngtree-cartoon-color-simple-male-avatar-png-image_1934459.jpg",
                new DateTime(1999, 4, 10)
            ));

            UsersList.Add(new User(
                "tomer",
                "t",
                "t",
                "https://aux.iconspalace.com/uploads/1867938351348566395.png",
                new DateTime(1990, 11, 7)
            ));

            return UsersList;
        }

        public static User GetUser(string UserName, string Password)
        {
            var User = Users.Find(x => x.UserName == UserName && x.Password == Password);
            if (User == null)
            {
                System.Diagnostics.Debug.WriteLine("login failed"); //todo make display alert
            }
            return User;
        }

        public static User GetUser(string UserId)
        {
            var User = Users.Find(x => x.Id == UserId);
            if (User == null)
            {
                System.Diagnostics.Debug.WriteLine("can't find user"); // TODO make display alert
            }
            return User;
        }

        public static void CreateUser(User NewUser)
        {
            Users.Add(NewUser);
        }

        public static bool RemoveUser(User UserToRemove)
        {
            return Users.Remove(UserToRemove);
        }

        public static bool RemoveUser(string UserId)
        {
            // remove meetings
            MeetingsData.Meetings.FindAll(m => m.Owner.Id == UserId).ForEach(m => MeetingsData.RemoveMeeting(m));
            User UserToRemove = GetUser(UserId);
            return RemoveUser(UserToRemove);
        }
    }
}