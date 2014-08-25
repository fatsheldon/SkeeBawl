using System.Windows.Controls;

namespace SkeeBawlWpf
{
    public class ClassicSkeeBawlListItem : ListBoxItem, ISkeeBawlGameListItem
    {
        public ClassicSkeeBawlListItem()
        {
            GameImage = "pack://application:,,,/assets/menu/skeeballman.png";
        }
        public string GameImage { get; set; }

        public ISkeeGame GetGame()
        {
            return new ClassicGame();
        }
    }

    public class ThemeSkeeListItem : ListBoxItem, ISkeeBawlGameListItem
    {

        public string GameImage
        {
            get { return "pack://application:,,,/assets/menu/skeeballman2.png"; }
        }

        public ISkeeGame GetGame()
        {
            return new ThemeSkee();
        }
    }
}