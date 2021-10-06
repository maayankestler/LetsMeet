using System;
using System.Collections.Generic;
using LetsMeet.Models;

namespace LetsMeet.Data
{
    public static class UsersData
    {
        public static IList<User> Users { get; private set; }
        static UsersData()
        {
            Users = new List<User>();

            Users.Add(new User
            {
                Name = "maayan",
                UserName = "maayan3002",
                IconURL = "https://www.onlyou.co.il/data/EIw2xdZedc_500572.jpg",
                born_date = new DateTime(1997, 3, 17)
            });

            Users.Add(new User
            {
                Name = "ido",
                UserName = "ido5",
                IconURL = "https://png.pngtree.com/png-vector/20191101/ourmid/pngtree-cartoon-color-simple-male-avatar-png-image_1934459.jpg",
                born_date = new DateTime(1999, 4, 10)
            });

            Users.Add(new User
            {
                Name = "tomer",
                UserName = "tomerico",
                IconURL = "https://lh3.googleusercontent.com/proxy/X82ZMY5qQWrRcs7o6ljRXYMH09hM7digrGRUPnonFhAx3S1862iLocDw5g1GJ4vmjHcNdY69QXkUYSflYj2iWPfCuTkNLUtCkT2aGdUXqCqKi14_g2TWMQ",
                born_date = new DateTime(1990, 11, 7)
            });
        }
    }
}