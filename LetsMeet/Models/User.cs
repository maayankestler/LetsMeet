
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LetsMeet.Models;
using LetsMeet.Data;
using System.Threading;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class User 
{
    public User(string name, string UserName, string Password, string IconURL, DateTime BornDate)
    {
        this.Name = name;
        this.UserName = UserName;
        this.Password = Password;
        this.IconURL = IconURL;
        this.BornDate = BornDate;
        this.FriendsIds = new HashSet<string>();
        this.type = UserType.User;
    }

    [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; }

    [BsonElement("UserName")]
    public string UserName { get; set; }

    [BsonElement("Password")]
    public string Password { get; set; }
    [BsonElement("Type")]
    public UserType type { get; private set; }
    public bool IsAdmin 
    { 
        get
        {
            return (type == UserType.Admin);
        }
    }

    //private List<string> _favoriteTypesIds = new List<string>();

    //public HashSet<MeetingType> FavoriteTypes
    //{
    //    get
    //    {
    //        List<MeetingType> types = _favoriteTypesIds.ConvertAll(new Converter<string, MeetingType>(MeetingTypesData.GetMeetingType));
    //        return new HashSet<MeetingType>(types);
    //    }
    //}

    [BsonElement("FriendsIds")]
    public HashSet<string> FriendsIds = new HashSet<string>();

    public HashSet<User> Friends
    {
        get
        {
            List<User> friends = FriendsIds.ToList().ConvertAll(new Converter<string, User>(UsersData.GetUser));
            HashSet<User> FriendsHashSet = new HashSet<User>(friends);
            FriendsHashSet.Remove(null);
            return FriendsHashSet;
        }
    }

    [BsonElement("IconURL")]
    public string IconURL { get; set; }

    [BsonElement("BornDate")]
    public DateTime BornDate { get; set; }

    public int age {
        get
        {
            DateTime start = this.BornDate;
            DateTime end = DateTime.Now;
            return (end.Year - start.Year - 1) +
            (((end.Month > start.Month) ||
            ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
        }
    }

    public override bool Equals(object obj) => this.Equals(obj as User);

    public bool Equals(User U)
    {
        if (U is null || this.GetType() != U.GetType())
        {
            return false;
        }

        return (Id == U.Id);
    }

    //public void AddFavoriteType (MeetingType meeting_type)
    //{
    //    if (!this.FavoriteTypes.Contains(meeting_type))
    //    {
    //        this.FavoriteTypes.Add(meeting_type);
    //        // TODO add to DB
    //    }
    //}

    //public void RemoveFavoriteType(MeetingType meeting_type)
    //{
    //    if (this.FavoriteTypes.Contains(meeting_type))
    //    {
    //        this.FavoriteTypes.Remove(meeting_type);
    //        // TODO remove from db
    //    }
    //    else
    //    {
    //        // TODO can't find user
    //    }
    //}

    public void AddFriend(User user)
    {
        if (user != null && !this.FriendsIds.Contains(user.Id))
        {
            FriendsIds.Add(user.Id);
            user.AddFriend(this);
            UsersData.UpdateUser(this);
        }
    }

    public bool IsFreind(User user)
    {
        return user != null && !this.Equals(user) && FriendsIds.Contains(user.Id);
    }

    public void RemoveFriend(User friend)
    {
        if (IsFreind(friend))
        {
            FriendsIds.Remove(friend.Id);
            friend.RemoveFriend(this);
            UsersData.UpdateUser(this);
        }    
    }

    public void RemoveFriends(List<User> Friends)
    {
        Friends.ForEach(f => RemoveFriend(f));
    }

}

public enum UserType
{
    Admin,
    User
}