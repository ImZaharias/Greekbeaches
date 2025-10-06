using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace GreekBeachesGuide.Services
{
    internal class TtsService
    {
        private static SpeechSynthesizer _s;
        public static void Speak(string text)
        {
            
            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show("Δεν υπάρχει κείμενο για ανάγνωση.");
                return;
            }

            Stop();
            _s = new SpeechSynthesizer { Volume = 100, Rate = 0 };

            // Προσπάθησε να επιλέξεις ελληνική φωνή (αν υπάρχει)
            var el = _s.GetInstalledVoices()
                       .FirstOrDefault(v => v.VoiceInfo.Name.Contains("Stefanos"));
            if (el != null) _s.SelectVoice(el.VoiceInfo.Name);
            

            // Ρητή γλώσσα -> σωστό word-breaking στα ελληνικά
            var pb = new PromptBuilder(new CultureInfo("el-GR"));
            pb.AppendText(text);
            _s.SpeakAsync(pb);
        }
        public static void Stop()
        { if (_s != null) { _s.SpeakAsyncCancelAll(); _s.Dispose(); _s = null; } }

    }
}