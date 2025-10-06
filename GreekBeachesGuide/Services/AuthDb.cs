using System.Data.SQLite;

namespace GreekBeachesGuide.Services
{
    internal static class AuthDb
    {
        public static string ConnStr => Db.ConnStr;

        public static void EnsureUsersTable()
        {
            using var con = new SQLiteConnection(ConnStr);
            con.Open();

            string sql = @"
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Username TEXT UNIQUE NOT NULL,
                Password TEXT NOT NULL,
                CreatedAt TEXT NOT NULL
            );";

            using var cmd = new SQLiteCommand(sql, con);
            cmd.ExecuteNonQuery();
        }

        public static void CreateUser(string username, string password)
        {
            using var con = new SQLiteConnection(ConnStr);
            con.Open();

            using var cmd = new SQLiteCommand(
                "INSERT INTO Users (Username, Password, Role) VALUES (@u, @p, @r)", con);
            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@p", password); // απλό για demo, χωρίς hash
            cmd.Parameters.AddWithValue("@r", "user");
            cmd.ExecuteNonQuery();
        }

        public static bool Exists(string username)
        {
            using var con = new SQLiteConnection(ConnStr);
            con.Open();

            using var cmd = new SQLiteCommand("SELECT 1 FROM Users WHERE Username = @u LIMIT 1", con);
            cmd.Parameters.AddWithValue("@u", username);
            return cmd.ExecuteScalar() != null;
        }
    }
}

