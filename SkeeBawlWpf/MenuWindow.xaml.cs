using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using LedWiz;

namespace SkeeBawlWpf
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class MenuWindow : SkeeBawlWindow
    {
        public delegate void GameStartHandler(object sender, GameStartEventArgs e);
        public event GameStartHandler GameStart;
        public MenuWindow()
        {
            InitializeComponent();

            GamesList.SelectedItem = GamesList.Items[0];
            GamesList.Focus();

            //   new testwindow().Show();
        }

        public void ledWiz_InputChange(object sender, LedWizInputArgs e)
        {
            foreach (var update in e.LedWizUpdates.Where(x => x.JoystickButton == JoystickButton.Button10 && x.Value > 0))
            {
                Dispatcher.Invoke(() =>
                    GamesList.SelectedItem = GamesList.SelectedIndex == GamesList.Items.Count - 1
                        ? GamesList.Items[0]
                        : GamesList.Items[GamesList.SelectedIndex + 1]);
            }
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.Button1 && x.Value > 0))
                Dispatcher.Invoke(() => GameStart(this, new GameStartEventArgs { Game = ((ISkeeBawlGameListItem)GamesList.SelectedItem).GetGame() }));
        }

        private void MenuWindow_OnKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D8:
                    GamesList.SelectedItem = GamesList.SelectedIndex == GamesList.Items.Count - 1 ? GamesList.Items[0] : GamesList.Items[GamesList.SelectedIndex + 1];
                    break;
                case Key.D0:
                case Key.Enter:
                    GameStart(this, new GameStartEventArgs { Game = ((ISkeeBawlGameListItem)GamesList.SelectedItem).GetGame() });
                    break;
                case Key.Escape: //killswitch. only known way out.
                    this.Close();
                    break;
            }
        }

        private void GamesList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GameImage.Source = new BitmapImage(new Uri(((ISkeeBawlGameListItem)GamesList.SelectedItem).GameImage));
        }
    }

    public class GameStartEventArgs : EventArgs
    {
        public ISkeeGame Game { get; set; }
    }
}
