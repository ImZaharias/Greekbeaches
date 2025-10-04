using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace GreekBeachesGuide.Services
{
    internal class TtsService
    {
        private static SpeechSynthesizer _s;
        public static void Speak(string text)
        {
            Stop();
            _s = new SpeechSynthesizer();
            _s.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.NotSet, 0, System.Globalization.CultureInfo.GetCultureInfo("el-GR"));
            _s.SpeakAsync(text);
        }
        public static void Stop()
        { if (_s != null) { _s.SpeakAsyncCancelAll(); _s.Dispose(); _s = null; } }
    }
}
