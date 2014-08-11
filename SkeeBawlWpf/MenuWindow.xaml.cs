using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LedWiz;

namespace SkeeBawlWpf
{

    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public delegate void GameStartHandler(object sender, GameStartEventArgs e);
        public event GameStartHandler GameStart;
        public MenuWindow()
        {
            InitializeComponent();

            GamesList.SelectedItem = GamesList.Items[0];

            //   new testwindow().Show();
        }

        public void ledWiz_InputChange(object sender, LedWizInputArgs e)
        {
            foreach (var update in e.LedWizUpdates.Where(x => x.JoystickButton == JoystickButton.Button9 && x.Value > 0))
            {
                Dispatcher.Invoke(() =>
                    GamesList.SelectedItem = GamesList.SelectedIndex == GamesList.Items.Count - 1
                        ? GamesList.Items[0]
                        : GamesList.Items[GamesList.SelectedIndex + 1]);
            }
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.Button1 && x.Value > 0))
                Dispatcher.Invoke(() => GameStart(this, new GameStartEventArgs { Game = new ClassicGame() }));
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
                    GameStart(this, new GameStartEventArgs { Game = new ClassicGame() });
                    break;
                case Key.Escape: //killswitch. only known way out.
                    this.Close();
                    break;
            }
        }
    }

    public class GameStartEventArgs : EventArgs
    {
        public ISkeeGame Game { get; set; }
    }
}
