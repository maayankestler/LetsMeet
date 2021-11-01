using System;
using System.Collections.Generic;
using LetsMeet.Models;
using Xamarin.Forms.Maps;
using LetsMeet.Data;
using LetsMeet.Data.DAL;
using MongoDB.Driver;

namespace LetsMeet.Data
{
    public static class MeetingsData
    {
        private static IMongoCollection<Meeting> collection = MongoDBConnection.GetInstance.DataBase.GetCollection<Meeting>("Meetings");
        public static List<Meeting> AllMeetings
        {
            get
            {
                IFindFluent<Meeting, Meeting> f = collection.Find(_ => true);
                List<Meeting> t = f.ToList();
                return t; // TODO delete
            }
        }
        
        public static List<Meeting> GetMeetingsByUser(User User)
        {
            return collection.Find(m => m.Members.Contains(User)).ToList();
        }

        public static Meeting GetMeetingById(string MeetingId)
        {
            return collection.Find(m => m.Id == MeetingId).SingleOrDefault();
        }

        public static List<Meeting> GetMeetingsByUser(string UserId)
        {
            return GetMeetingsByUser(UsersData.GetUser(UserId));
        }

        public static void CreateMeeting(Meeting m)
        {
            collection.InsertOne(m);
        }

        public static void RemoveMeeting(Meeting MeetingToRemove)
        {
            collection.DeleteOne(m => m.Id == MeetingToRemove.Id);
        }

        public static void RemoveMeetingByOwner(string OwnerId)
        {
            collection.DeleteMany(m => m.OwnerId == OwnerId);
        }

        public static List<Meeting> SearchMeetings(bool OwnedByMe, bool OwnedByFriend, bool Member, MeetingStatus status, User CurrenUser)
        {
            List<Meeting> temp_meetings_list = new List<Meeting>();

            void VerifyAdd(Meeting m)
            {
                if (!temp_meetings_list.Contains(m) && m.Status == status)
                    temp_meetings_list.Add(m);
            };

            if (OwnedByMe)
                collection.Find(m => m.OwnerId == CurrenUser.Id).ToList().ForEach(VerifyAdd);
            if (OwnedByFriend)
                collection.Find(m => CurrenUser.FriendsIds.Contains(m.OwnerId)).ToList().ForEach(VerifyAdd);
            if (Member)
                collection.Find(m => m.MembersIds.Contains(CurrenUser.Id)).ToList().ForEach(VerifyAdd);
            if (!OwnedByMe && !OwnedByFriend && !Member)
                MeetingsData.AllMeetings.ForEach(VerifyAdd);

            return temp_meetings_list;
        }

        public static void UpdateMeeting(Meeting Meeting)
        {
            collection.ReplaceOne(m => m.Id == Meeting.Id, Meeting);
        }
    }
}
