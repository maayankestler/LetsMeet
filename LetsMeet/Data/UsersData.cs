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
        private static IMongoCollection<User> _collection = MongoDBConnection.GetInstance.DataBase.GetCollection<User>("Users");
        public static List<User> AllUsers
        {
            get
            {
                return GetAllUsers();
            }
        }

        public static List<User> GetAllUsers()
        {
            return _collection.Find(_ => true).ToList();
        }

        public static User GetUser(string userName, string password)
        {
            var User = _collection.Find(x => x.UserName == userName && x.Password == password).SingleOrDefault();
            if (User == null)
            {
                System.Diagnostics.Debug.WriteLine("login failed"); //todo make display alert
            }
            return User;
        }

        public static User GetUser(string userId)
        {
            return _collection.Find(x => x.Id == userId).SingleOrDefault();
        }

        public static void CreateUser(User newUser)
        {
            _collection.InsertOne(newUser);
        }

        public static void RemoveUser(User userToRemove)
        {
            RemoveUser(userToRemove.Id);
        }

        public static void RemoveUser(string userId)
        {
            // remove meetings
            MeetingsData.RemoveMeetingByOwner(userId);
            _collection.DeleteOne(u => u.Id == userId);
        }

        public static void UpdateUser(User user)
        {
            _collection.ReplaceOne(u => u.Id == user.Id, user);
        }
    }
}