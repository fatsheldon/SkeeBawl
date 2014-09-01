using System.Windows.Media;

namespace SkeeBawlWpf
{
    public class SkeeMediaPlayer : MediaPlayer
    {
        public SkeeMediaPlayer()
        {
            MediaOpened += (o, args) => IsBusy = true;
            MediaEnded += (o, args) => IsBusy = false;
        }

        public bool IsBusy { get; private set; }
    }
}