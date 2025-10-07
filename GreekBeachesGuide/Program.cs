using GreekBeachesGuide.Forms;
using GreekBeachesGuide.Services;

namespace GreekBeachesGuide
{
    internal static class Program
    {
        [STAThread] // Windows Forms requires STA
        static void Main()
        {
            Application.EnableVisualStyles();                 // Modern UI
            Application.SetCompatibleTextRenderingDefault(false);

            // Global exception logging (UI & non-UI threads)
            Application.ThreadException += (s, e) => LogCrash(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) => LogCrash(e.ExceptionObject as Exception);

            // DB init before any forms
            try
            {
                Db.EnsureCreated();
                MessageBox.Show($"Βάση δημιουργήθηκε στο:\n{Db.DbPath}\n\nΠαραλίες: {Db.SearchBeaches("").Count}",
                    "Debug Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ΚΡΙΣΙΜΟ ΣΦΑΛΜΑ:\n{ex.Message}\n\nStack:\n{ex.StackTrace}",
                    "Σφάλμα DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Abort if DB failed
            }

            // Start with Login
            Application.Run(new FormLogin());
        }

        // Append crash info to file and notify user
        static void LogCrash(Exception ex)
        {
            try
            {
                var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DebugLog.txt");
                System.IO.File.AppendAllText(path, $"[{DateTime.Now}] {ex}\r\n\r\n");
                MessageBox.Show("Σφάλμα. Καταγράφηκε στο DebugLog.txt");
            }
            catch { /* ignore logging failures */ }
        }
    }
}

