using System;
using System.Collections.Generic;
using LetsMeet.Models;
using LetsMeet.Data;
using LetsMeet.Data.DAL;
using MongoDB.Driver;

namespace LetsMeet.Data
{
    public static class UsersData
    {
        private static IMongoCollection<User> collection = MongoDBConnection.GetInstance.DataBase.GetCollection<User>("Users");
        public static List<User> AllUsers
        {
            get
            {
                return GetAllUsers();
            }
        }

        public static List<User> GetAllUsers()
        {
            return collection.Find(_ => true).ToList();
        }

        public static User GetUser(string UserName, string Password)
        {
            var User = collection.Find(x => x.UserName == UserName && x.Password == Password).SingleOrDefault();
            if (User == null)
            {
                System.Diagnostics.Debug.WriteLine("login failed"); //todo make display alert
            }
            return User;
        }

        public static User GetUser(string UserId)
        {
            return collection.Find(x => x.Id == UserId).SingleOrDefault();
        }

        public static void CreateUser(User NewUser)
        {
            collection.InsertOne(NewUser);
        }

        public static void RemoveUser(User UserToRemove)
        {
            RemoveUser(UserToRemove.Id);
        }

        public static void RemoveUser(string UserId)
        {
            // remove meetings
            MeetingsData.RemoveMeetingByOwner(UserId);
            collection.DeleteOne(u => u.Id == UserId);
        }

        public static void UpdateUser(User User)
        {
            collection.ReplaceOne(u => u.Id == User.Id, User);
        }
    }
}