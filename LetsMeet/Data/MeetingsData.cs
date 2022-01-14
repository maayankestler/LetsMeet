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
        private static IMongoCollection<Meeting> _collection = MongoDBConnection.GetInstance.DataBase.GetCollection<Meeting>("Meetings");
        public static List<Meeting> AllMeetings
        {
            get
            {
                return _collection.Find(_ => true).ToList();
            }
        }
        
        public static List<Meeting> GetMeetingsByUser(User user)
        {
            return _collection.Find(m => m.Members.Contains(user)).ToList();
        }

        public static Meeting GetMeetingById(string meetingId)
        {
            return _collection.Find(m => m.Id == meetingId).SingleOrDefault();
        }

        public static List<Meeting> GetMeetingsByUser(string userId)
        {
            return GetMeetingsByUser(UsersData.GetUser(userId));
        }

        public static void CreateMeeting(Meeting meeting)
        {
            _collection.InsertOne(meeting);
        }

        public static void RemoveMeeting(Meeting meetingToRemove)
        {
            _collection.DeleteOne(m => m.Id == meetingToRemove.Id);
        }

        public static void RemoveMeetingByOwner(string ownerId)
        {
            _collection.DeleteMany(m => m.OwnerId == ownerId);
        }

        public static List<Meeting> SearchMeetings(bool ownedByMe, bool ownedByFriend, bool member, MeetingStatus status, User currenUser)
        {
            List<Meeting> temp_meetings_list = new List<Meeting>();

            void VerifyAdd(Meeting m)
            {
                if (!temp_meetings_list.Contains(m) && m.Status == status)
                    temp_meetings_list.Add(m);
            };

            if (ownedByMe)
                _collection.Find(m => m.OwnerId == currenUser.Id).ToList().ForEach(VerifyAdd);
            if (ownedByFriend)
                _collection.Find(m => currenUser.FriendsIds.Contains(m.OwnerId)).ToList().ForEach(VerifyAdd);
            if (member)
                _collection.Find(m => m.MembersIds.Contains(currenUser.Id)).ToList().ForEach(VerifyAdd);
            if (!ownedByMe && !ownedByFriend && !member)
                MeetingsData.AllMeetings.ForEach(VerifyAdd);

            return temp_meetings_list;
        }

        public static void UpdateMeeting(Meeting meeting)
        {
            _collection.ReplaceOne(m => m.Id == meeting.Id, meeting);
        }
    }
}
