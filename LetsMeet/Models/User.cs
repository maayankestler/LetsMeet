
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class User {

    public User() {
    }

    private string Id { get; }

    public string Name { get; set; }

    public string UserName { get; set; }

    public string Passwrod { get; set; }

    private string[] FavoriteTypesIds;

    public HashSet<MeetingType> FavoriteTypes { get; } = new HashSet<MeetingType>();

    private string[] FriendsIds;

    public HashSet<User> Friends { get; private set; } = new HashSet<User>();

    public string IconURL { get; set; }

    public DateTime born_date { get; set; }

    public int age {
        get
        {
            DateTime start = this.born_date;
            DateTime end = DateTime.Now;
            return (end.Year - start.Year - 1) +
            (((end.Month > start.Month) ||
            ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
        }
    }


    public Meeting[] GetMeetings() {
        // TODO implement here
        return new Meeting[] { };
    }

    public void LogIn() {
        // TODO implement here
    }

    public void LogOut() {
        // TODO implement here
    }

    public void AddFavoriteType (MeetingType meeting_type)
    {
        if (!this.FavoriteTypes.Contains(meeting_type))
        {
            this.FavoriteTypes.Add(meeting_type);
            // add to DB
        }
    }

    public void RemoveFavoriteType(MeetingType meeting_type)
    {
        if (this.FavoriteTypes.Contains(meeting_type))
        {
            this.FavoriteTypes.Remove(meeting_type);
            // remove from db
        }
        else
        {
            // can't find user
        }
    }

    public void AddFriend(User user)
    {
        if (!this.Friends.Contains(user))
        {
            this.Friends.Add(user);
            // add to DB
        }
    }

    public void RemoveFriend(User user)
    {
        if (this.Friends.Contains(user))
        {
            this.Friends.Remove(user);
            // remove from db
        }
        else
        {
            // can't find user
        }
    }

}