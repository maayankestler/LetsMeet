
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LetsMeet.Data;

public class User {

    public User() {
    }

    public string Id { get; set; } // TODO add auto genrator

    public string Name { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    private List<string> _favoriteTypesIds = new List<string>();

    public HashSet<MeetingType> FavoriteTypes
    {
        get
        {
            List<MeetingType> types = _favoriteTypesIds.ConvertAll(new Converter<string, MeetingType>(MeetingTypesData.GetMeetingType));
            return new HashSet<MeetingType>(types);
        }
    }

    private List<string> _friendsIds = new List<string>();

    public HashSet<User> Friends
    {
        get
        {
            List<User> friends = _friendsIds.ConvertAll(new Converter<string, User>(UsersData.GetUser));
            return new HashSet<User>(friends);
        }
    }

    public string IconURL { get; set; }

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

    public Meeting[] GetMeetings() {
        // TODO implement here
        return new Meeting[] { };
    }

    public void AddFavoriteType (MeetingType meeting_type)
    {
        if (!this.FavoriteTypes.Contains(meeting_type))
        {
            this.FavoriteTypes.Add(meeting_type);
            // TODO add to DB
        }
    }

    public void RemoveFavoriteType(MeetingType meeting_type)
    {
        if (this.FavoriteTypes.Contains(meeting_type))
        {
            this.FavoriteTypes.Remove(meeting_type);
            // TODO remove from db
        }
        else
        {
            // TODO can't find user
        }
    }

    public void AddFriend(User user)
    {
        if (!this.Friends.Contains(user))
        {
            //this.Friends.Add(user);
            _friendsIds.Add(user.Id);
            user.AddFriend(this);
            // TODO add to DB
        }
    }

    public bool IsFreind(User user)
    {
        return !this.Equals(user) && _friendsIds.Contains(user.Id);
    }

    public void RemoveFriend(User friend)
    {
        if (IsFreind(friend))
        {
            _friendsIds.Remove(friend.Id);
            friend.RemoveFriend(this);
        }    
    }

    public void RemoveFriends(List<User> Friends)
    {
        Friends.ForEach(f => RemoveFriend(f));
    }

}