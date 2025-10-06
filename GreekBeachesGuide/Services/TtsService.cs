using System.Linq;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using Windows.Media.Playback;
using Windows.Media.Core;

namespace GreekBeachesGuide.Services
{
    internal static class TtsService
    {
        private static SpeechSynthesizer _synth;
        private static MediaPlayer _player;
        private static bool _isSpeaking;

        private static void EnsureInit()
        {
            if (_synth != null) return;
            _synth = new SpeechSynthesizer();
            _player = new MediaPlayer();

            var stef = SpeechSynthesizer.AllVoices
                .FirstOrDefault(v => v.DisplayName.Contains("Stefanos", System.StringComparison.OrdinalIgnoreCase));
            if (stef != null) _synth.Voice = stef;
            else
            {
                var el = SpeechSynthesizer.AllVoices
                    .FirstOrDefault(v => v.Language.Equals("el-GR", System.StringComparison.OrdinalIgnoreCase));
                if (el != null) _synth.Voice = el;
            }

            _player.MediaEnded += (s, e) => { _isSpeaking = false; };

        }

        public static async Task SpeakAsync(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return;
            EnsureInit();

            if (_isSpeaking)
            {
                _player.Pause();
                _player.Source = null;
                _isSpeaking = false;
                return;
            }

            var stream = await _synth.SynthesizeTextToStreamAsync(text);
            _player.Source = MediaSource.CreateFromStream(stream, stream.ContentType);
            _player.Play();
            _isSpeaking = true;
        }

        public static void Stop()
        {
            if (_player == null) return;

            _player.Pause();          // σταματάει την αναπαραγωγή
            _player.Source = null;    // αποδεσμεύει το stream
            _isSpeaking = false;
        }
    }
}




