using System;
using System.Collections.Generic;
using LetsMeet.Models;
using LetsMeet.Data.DAL;
using MongoDB.Driver;

namespace LetsMeet.Data
{
    public static class MeetingTypesData
    {
        private static IMongoCollection<MeetingType> collection = MongoDBConnection.GetInstance.DataBase.GetCollection<MeetingType>("MeetingsTypes");
        public static List<MeetingType> AllMeetingsTypes
        {
            get
            {
                return collection.Find(_ => true).ToList();
            }
        }

        public static MeetingType GetMeetingType(string id)
        {
            return collection.Find(m => m.Id == id).SingleOrDefault();
        }

        public static List<MeetingType> GetAllMeetingByCategory(string CategoryId)
        {
            return collection.Find(t => t.CategoryId == CategoryId).ToList();

        }

        public static void CreateMeetingType(MeetingType m)
        {
            collection.InsertOne(m);
        }

        public static MeetingType GetFirst()
        {
            return collection.Find(_ => true).Limit(1).Single();
        }
    }
}
