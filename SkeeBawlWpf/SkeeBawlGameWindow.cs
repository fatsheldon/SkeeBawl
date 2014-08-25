using System.Linq;
using System.Windows;
using System.Windows.Input;
using LedWiz;

namespace SkeeBawlWpf
{
    public abstract class SkeeBawlGameWindow : SkeeBawlWindow
    {
        protected SkeeBawlGameWindow()
        {
            KeyUp += Window_KeyUp;
            Loaded += Game_OnLoaded;
        }
        protected abstract void IncrementBallsInPlay();
        protected abstract void IncrementBallsScored();
        protected abstract void IncreaseScore(int howMuch);
        protected abstract void CheckStartNewGame();
        protected abstract void StartNewGame();
        protected abstract void Exit();

        protected void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.D8:
                    Exit();
                    break;
                case System.Windows.Input.Key.D7:
                    IncrementBallsInPlay();
                    break;
                case System.Windows.Input.Key.D6:
                    IncrementBallsScored();
                    break;
                case System.Windows.Input.Key.D5:
                    IncreaseScore(50);
                    break;
                case System.Windows.Input.Key.D4:
                    IncreaseScore(40);
                    break;
                case System.Windows.Input.Key.D3:
                    IncreaseScore(30);
                    break;
                case System.Windows.Input.Key.D2:
                    IncreaseScore(20);
                    break;
                case System.Windows.Input.Key.D1:
                    IncreaseScore(10);
                    break;
                case System.Windows.Input.Key.D0:
                    CheckStartNewGame();
                    break;
                case System.Windows.Input.Key.Escape: //killswitch
                    this.Close();
                    break;
            }
        }
        protected void Game_OnLoaded(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        public void LedWizInputChange(object sender, LedWizInputArgs e)
        {
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.Button10 && x.Value > 0))
                Exit();
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.Button8 && x.Value > 0))
                IncrementBallsInPlay();
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.Button7 && x.Value > 0))
                IncrementBallsScored();
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.Button6 && x.Value > 0))
                IncreaseScore(50);
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.Button5 && x.Value > 0))
                IncreaseScore(40);
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.Button4 && x.Value > 0))
                IncreaseScore(30);
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.Button3 && x.Value > 0))
                IncreaseScore(20);
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.Button2 && x.Value > 0))
                IncreaseScore(10);
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.Button1 && x.Value > 0))
                CheckStartNewGame();
        }
    }
}