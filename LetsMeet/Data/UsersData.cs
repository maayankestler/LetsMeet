using System;
using System.Collections.Generic;
using LetsMeet.Models;

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

            UsersList.Add(new User
            {
                Name = "maayan",
                UserName = "maayan3002",
                Passwrod="123456",
                IconURL = "https://www.onlyou.co.il/data/EIw2xdZedc_500572.jpg",
                born_date = new DateTime(1997, 3, 17)
            });

            UsersList.Add(new User
            {
                Name = "ido",
                UserName = "ido5",
                Passwrod = "555",
                IconURL = "https://png.pngtree.com/png-vector/20191101/ourmid/pngtree-cartoon-color-simple-male-avatar-png-image_1934459.jpg",
                born_date = new DateTime(1999, 4, 10)
            });

            UsersList.Add(new User
            {
                Name = "tomer",
                UserName = "tomerico",
                Passwrod = "qwe123",
                IconURL = "https://lh3.googleusercontent.com/proxy/X82ZMY5qQWrRcs7o6ljRXYMH09hM7digrGRUPnonFhAx3S1862iLocDw5g1GJ4vmjHcNdY69QXkUYSflYj2iWPfCuTkNLUtCkT2aGdUXqCqKi14_g2TWMQ",
                born_date = new DateTime(1990, 11, 7)
            });

            return UsersList;
        }

        public static User GetUser(string UserName, string Password)
        {
            List<User> AllUsers = GetAllUsers();
            var User = AllUsers.Find(x => x.UserName == UserName && x.Passwrod == Password);
            if (User == null)
            {
                System.Diagnostics.Debug.WriteLine("login failed"); //todo make display alert
            }
            return User;
        }
    }
}