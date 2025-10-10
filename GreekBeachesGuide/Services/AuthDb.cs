using System.Data.SQLite;

namespace GreekBeachesGuide.Services
{
    // Handles user authentication and creation using SQLite
    internal static class AuthDb
    {
        // Use same connection string as main DB
        public static string ConnStr => Db.ConnStr;

        // Ensure the Users table exists (for login/registration)
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

        // Insert new user record (plain-text password for demo purposes)
        public static void CreateUser(string username, string password, string role)
        {
            using var con = new SQLiteConnection(ConnStr);
            con.Open();

            using var cmd = new SQLiteCommand(
                "INSERT INTO Users (Username, Password, Role) VALUES (@u, @p, @r)", con);
            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@p", password); // not hashed - only demo
            cmd.Parameters.AddWithValue("@r", role);
            cmd.ExecuteNonQuery();
        }

        // Check if a username already exists
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

