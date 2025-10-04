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

            // DB init
            try { Db.EnsureCreated(); }
            catch (Exception ex) { MessageBox.Show("DB init error: " + ex.Message); }

            // Start με Login
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