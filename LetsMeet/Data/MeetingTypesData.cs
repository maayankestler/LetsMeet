using System;
using System.Collections.Generic;
using LetsMeet.Models;
using LetsMeet.Data.DAL;
using MongoDB.Driver;

namespace LetsMeet.Data
{
    public static class MeetingTypesData
    {
        private static IMongoCollection<MeetingType> _collection = MongoDBConnection.GetInstance.DataBase.GetCollection<MeetingType>("MeetingsTypes");
        public static List<MeetingType> AllMeetingsTypes
        {
            get
            {
                return _collection.Find(_ => true).ToList();
            }
        }

        public static MeetingType GetMeetingType(string id)
        {
            return _collection.Find(m => m.Id == id).SingleOrDefault();
        }

        public static List<MeetingType> GetAllMeetingByCategory(string categoryId)
        {
            return _collection.Find(t => t.CategoryId == categoryId).ToList();

        }

        public static void CreateMeetingType(MeetingType type)
        {
            _collection.InsertOne(type);
        }

        public static MeetingType GetFirst()
        {
            return _collection.Find(_ => true).Limit(1).Single();
        }
    }
}
