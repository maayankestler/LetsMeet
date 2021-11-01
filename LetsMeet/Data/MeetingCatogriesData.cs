using System;
using System.Collections.Generic;
using LetsMeet.Models;
using LetsMeet.Data.DAL;
using MongoDB.Driver;

namespace LetsMeet.Data
{
    public static class MeetingCatogriesData
    {
        private static IMongoCollection<MeetingCategory> collection = MongoDBConnection.GetInstance.DataBase.GetCollection<MeetingCategory>("MeetingsCategories");
        public static List<MeetingCategory> AllMeetingCategories 
        { 
            get
            {
                return collection.Find(_ => true).ToList();
            }
        }

        public static MeetingCategory GetCategory(string id)
        {
            return collection.Find(x => x.Id == id).SingleOrDefault();
        }

        public static MeetingCategory GetFirst()
        {
            return collection.Find(_ => true).Limit(1).Single();
        }

        public static void CreateMeetingCategory(MeetingCategory m)
        {
            collection.InsertOne(m);
        }
    }
}
