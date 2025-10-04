using GreekBeachesGuide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace GreekBeachesGuide.Services
{
    internal class Db
    {
        public static string DbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "beaches.db");
        public static string ConnStr => $"Data Source={DbPath};Version=3;";

        public static void EnsureCreated()
        {
            var dir = Path.GetDirectoryName(DbPath);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            var firstTime = !File.Exists(DbPath);
            if (firstTime) SQLiteConnection.CreateFile(DbPath);


            using (var con = new SQLiteConnection(ConnStr))
            {
                con.Open();
                using (var cmd = new SQLiteCommand(@"
CREATE TABLE IF NOT EXISTS Users (
Id INTEGER PRIMARY KEY AUTOINCREMENT,
Username TEXT UNIQUE NOT NULL,
Password TEXT NOT NULL,
Role TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS Beaches (
Id INTEGER PRIMARY KEY AUTOINCREMENT,
Name TEXT NOT NULL,
Region TEXT NOT NULL,
Description TEXT NOT NULL,
Features TEXT,
ImagePath TEXT,
AudioPath TEXT,
VideoPath TEXT,
Rating INTEGER DEFAULT 0,
IsTop INTEGER DEFAULT 0
);
CREATE TABLE IF NOT EXISTS History (
Id INTEGER PRIMARY KEY AUTOINCREMENT,
Username TEXT,
BeachId INTEGER,
ViewedAt TEXT
);", con))
                { cmd.ExecuteNonQuery(); }


                if (firstTime) Seed(con);
            }
        }

        private static void Seed(SQLiteConnection con)
        {
            using (var cmd = new SQLiteCommand("INSERT OR IGNORE INTO Users(Username,Password,Role) VALUES('demo','1234','User');", con))
                cmd.ExecuteNonQuery();
            // TODO: Insert sample Beaches like in the doc (10-20).
        }

        public static List<Beach> SearchBeaches(string q)
        {
            var list = new List<Beach>();
            using (var con = new SQLiteConnection(ConnStr))
            {
                con.Open();
                string sql = string.IsNullOrWhiteSpace(q)
                ? "SELECT * FROM Beaches ORDER BY IsTop DESC, Name"
                : "SELECT * FROM Beaches WHERE Name LIKE @q OR Region LIKE @q OR Features LIKE @q ORDER BY IsTop DESC, Name";
                using (var cmd = new SQLiteCommand(sql, con))
                {
                    if (!string.IsNullOrWhiteSpace(q)) cmd.Parameters.AddWithValue("@q", "%" + q + "%");
                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            list.Add(new Beach
                            {
                                Id = Convert.ToInt32(r["Id"]),
                                Name = r["Name"].ToString(),
                                Region = r["Region"].ToString(),
                                Description = r["Description"].ToString(),
                                Features = r["Features"].ToString(),
                                ImagePath = r["ImagePath"].ToString(),
                                AudioPath = r["AudioPath"].ToString(),
                                VideoPath = r["VideoPath"].ToString(),
                                Rating = Convert.ToInt32(r["Rating"]),
                                IsTop = Convert.ToInt32(r["IsTop"]) == 1
                            });
                        }
                    }
                }
            }
            return list;
        }  
    }
}
