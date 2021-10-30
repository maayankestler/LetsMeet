using LetsMeet.Models;
using Microsoft.EntityFrameworkCore;

namespace LetsMeet.Data.Dal
{
    public class SqlLiteConnection: DbContext
    {
        public DbSet<MeetingType> MeetingTypes { get; set; }
        public DbSet<MeetingCategory> MeetingCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        
        public string DbPath { get; private set; }

        public SqlLiteConnection(string dbPath)
        {
            DbPath = dbPath;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}