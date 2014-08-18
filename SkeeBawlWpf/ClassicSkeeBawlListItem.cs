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
}