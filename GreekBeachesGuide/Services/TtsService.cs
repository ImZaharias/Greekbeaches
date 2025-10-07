using System.Linq;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using Windows.Media.Playback;
using Windows.Media.Core;

namespace GreekBeachesGuide.Services
{
    // Simple TTS wrapper using UWP SpeechSynthesizer + MediaPlayer
    internal static class TtsService
    {
        private static SpeechSynthesizer _synth; // TTS engine
        private static MediaPlayer _player;      // Audio output
        private static bool _isSpeaking;         // Playback flag

        // Lazy init: pick "Stefanos" or any Greek voice (el-GR)
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

            _player.MediaEnded += (s, e) => _isSpeaking = false; // reset on finish
        }

        // Speak or stop-if-already-speaking (toggle behavior)
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

        // Hard stop + release current audio
        public static void Stop()
        {
            if (_player == null) return;
            _player.Pause();
            _player.Source = null;
            _isSpeaking = false;
        }
    }
}





