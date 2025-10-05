using GreekBeachesGuide.Forms;
using GreekBeachesGuide.Services;

namespace GreekBeachesGuide
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Global crash log 
            Application.ThreadException += (s, e) => LogCrash(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) => LogCrash(e.ExceptionObject as Exception);

            // ΠΡΩΤΑ: DB init - ΠΡΙΝ ανοίξει οποιαδήποτε φόρμα
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
                return; // Σταμάτα την εφαρμογή
            }

            // ΜΕΤΑ: Άνοιξε το Login
            Application.Run(new FormLogin());
        }

        static void LogCrash(Exception ex)
        {
            try
            {
                var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DebugLog.txt");
                System.IO.File.AppendAllText(path, $"[{DateTime.Now}] {ex}\r\n\r\n");
                MessageBox.Show("Σφάλμα. Καταγράφηκε στο DebugLog.txt");
            }
            catch { }
        }
    }
}