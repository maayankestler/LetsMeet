using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Authentication;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

using LetsMeet.Models;
using LetsMeet.Data;
using Xamarin.Forms.Maps;

namespace LetsMeet.Data.DAL
{
    class MongoDBConnection
    {
        public MongoClient Client { get; }
        public IMongoDatabase DataBase { get; }
        //{
        //    get
        //    {
        //        return Client.GetDatabase("LetsMeet");
        //    }
        //}
        public MongoDBConnection()
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(
                        new MongoUrl("mongodb://LetsmeetDB:qwe123@letsmeet-shard-00-00.4af2x.mongodb.net:27017,letsmeet-shard-00-01.4af2x.mongodb.net:27017,letsmeet-shard-00-02.4af2x.mongodb.net:27017/myFirstDatabase?ssl=true&replicaSet=atlas-ccuns6-shard-0&authSource=admin&retryWrites=true&w=majority")
                    );
            settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };
            Client = new MongoClient(settings);
            DataBase = Client.GetDatabase("LetsMeet");

            //IMongoCollection<MeetingCategory> collection1 = DataBase.GetCollection<MeetingCategory>("MeetingsCategories");
            //List<MeetingCategory> MeetingCategoriesList = new List<MeetingCategory>();

            //MeetingCategoriesList.Add(new MeetingCategory("Sport", "https://png.pngtree.com/png-clipart/20190613/original/pngtree-cartoon-sports-fitness-equipment-png-image_3594503.jpg"));

            //MeetingCategoriesList.Add(new MeetingCategory("Board and cards games", "https://png.pngtree.com/png-clipart/20190920/original/pngtree-card-game-cartoon-illustration-png-image_4651168.jpg"));

            //MeetingCategoriesList.Add(new MeetingCategory("Social", "https://png.pngtree.com/png-clipart/20190604/original/pngtree-friend-png-image_1280871.jpg"));

            //collection1.InsertMany(MeetingCategoriesList);


            //IMongoCollection<Meeting> collection2 = DataBase.GetCollection<Meeting>("Meetings");

            //List<Meeting> MeetingsList = new List<Meeting>();
            //MeetingsList.Add(new Meeting(
            //    "soccer brother",
            //    "https://png.pngtree.com/png-clipart/20190611/original/pngtree-beautiful-blue-cartoon-soccer-field-png-image_2749691.jpg",
            //    "1",
            //    new DateTime(2021, 10, 23, 12, 0, 0),
            //    new DateTime(2021, 10, 23, 15, 0, 0),
            //    "1",
            //    10,
            //    15,
            //    15,
            //    30,
            //    new Position(32.2271155528261, 34.9893602728844)));
            //MeetingsList.Add(new Meeting(
            //    "catan kings",
            //    "https://e7.pngegg.com/pngimages/692/115/png-clipart-catan-boardgamegeek-dice-board-game-dice-game-dice-thumbnail.png",
            //    "5",
            //    new DateTime(2021, 10, 24, 20, 0, 0),
            //    new DateTime(2021, 10, 24, 22, 0, 0),
            //    "3",
            //    3,
            //    4,
            //    16,
            //    60,
            //    new Position(32.2155361580173, 34.9895503744483)));
            //MeetingsList.Add(new Meeting(
            //    "pokerrrr",
            //    "",
            //    "4",
            //    new DateTime(2021, 11, 1, 20, 0, 0),
            //    new DateTime(2021, 11, 1, 22, 0, 0),
            //    "2",
            //    4,
            //    6,
            //    18,
            //    80,
            //    new Position(32.2289114408791, 35.0048205256462)));

            //collection2.InsertMany(MeetingsList);
            //IMongoCollection<MeetingType> collection3 = DataBase.GetCollection<MeetingType>("MeetingsTypes");

            //List<MeetingType> MeetingsTypesList = new List<MeetingType>();

            //MeetingsTypesList.Add(new MeetingType(
            //    "1",
            //    "soccer",
            //    "https://png.pngtree.com/png-clipart/20200225/original/pngtree-soccer-ball-icon-circle-png-image_5278517.jpg",
            //    "1"));
            //MeetingsTypesList.Add(new MeetingType(
            //    "2",
            //    "basketball",
            //    "https://png.pngtree.com/element_our/20200702/ourmid/pngtree-cartoon-vector-basketball-hand-drawn-image_2283539.jpg",
            //    "1"));
            //MeetingsTypesList.Add(new MeetingType(
            //    "3",
            //    "baseball",
            //    "https://png.pngtree.com/png-clipart/20190916/original/pngtree-white-cartoon-baseball-illustration-png-image_4594709.jpg",
            //    "1"));

            //MeetingsTypesList.Add(new MeetingType(
            //    "4",
            //    "poker",
            //    "https://png.pngtree.com/png-clipart/20190604/original/pngtree-poker-png-image_1398414.jpg",
            //    "2"));
            //MeetingsTypesList.Add(new MeetingType(
            //    "5",
            //    "catan",
            //    "https://m.media-amazon.com/images/I/81+okm4IpfL._AC_SL1500_.jpg",
            //    "2"));
            //MeetingsTypesList.Add(new MeetingType(
            //    "6",
            //    "monopoly",
            //    "https://www.merchandisingplaza.co.uk/9507/2/T-shirts---Monopoly-Logo-Tee-Shirt-l.jpg",
            //    "2"));
            //MeetingsTypesList.Add(new MeetingType(
            //    "7",
            //    "drink",
            //    "https://png.pngtree.com/png-clipart/20200710/ourmid/pngtree-man-drinking-beer-illustration-png-image_2305051.jpg",
            //    "3"));

            //collection3.InsertMany(MeetingsTypesList);
            //IMongoCollection<User> collection4 = DataBase.GetCollection<User>("Users");

            //List<User> UsersList = new List<User>();

            //UsersList.Add(new User(
            //    "maayan",
            //    "t",
            //    "a",
            //    "https://www.onlyou.co.il/data/EIw2xdZedc_500572.jpg",
            //    new DateTime(1997, 3, 17)
            //));

            //UsersList.Add(new User(
            //    "ido",
            //    "ido5",
            //    "555",
            //    "https://png.pngtree.com/png-vector/20191101/ourmid/pngtree-cartoon-color-simple-male-avatar-png-image_1934459.jpg",
            //    new DateTime(1999, 4, 10)
            //));

            //UsersList.Add(new User(
            //    "tomer",
            //    "t",
            //    "t",
            //    "https://aux.iconspalace.com/uploads/1867938351348566395.png",
            //    new DateTime(1990, 11, 7)
            //));

            //collection4.InsertMany(UsersList);
            //int i = 1;
        }

        // singleton
        private static MongoDBConnection instance = null;
        public static MongoDBConnection GetInstance
        {
            get
            {
                if (instance == null)
                    instance = new MongoDBConnection();
                return instance;
            }
        }
    }
}
