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
        public MongoDBConnection()
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(
                        new MongoUrl("mongodb://LetsmeetDB:qwe123@letsmeet-shard-00-00.4af2x.mongodb.net:27017,letsmeet-shard-00-01.4af2x.mongodb.net:27017,letsmeet-shard-00-02.4af2x.mongodb.net:27017/myFirstDatabase?ssl=true&replicaSet=atlas-ccuns6-shard-0&authSource=admin&retryWrites=true&w=majority")
                    );
            settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };
            settings.ConnectTimeout = new TimeSpan(0, 30, 0);
            Client = new MongoClient(settings);
            DataBase = Client.GetDatabase("LetsMeet");
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
