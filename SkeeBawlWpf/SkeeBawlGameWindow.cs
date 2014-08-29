using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using LedWiz;
using SkeeBawlWpf.Properties;

namespace SkeeBawlWpf
{
    public abstract class SkeeBawlGameWindow : SkeeBawlWindow
    {
        protected SkeeBawlGameWindow()
        {
            KeyUp += Window_KeyUp;
            Loaded += Game_OnLoaded;
            MediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
        }

        private void MediaPlayer_MediaEnded(object sender, System.EventArgs e)
        {
            ((MediaPlayer)sender).Close();
        }
        protected abstract void IncrementBallsInPlay();
        protected abstract void IncrementBallsScored();
        protected abstract void IncreaseScore(int howMuch);
        protected abstract void CheckStartNewGame();
        protected abstract void StartNewGame();
        protected abstract void Exit();
        protected MediaPlayer MediaPlayer = new MediaPlayer();

        protected void PlaySound(Uri uri)
        {
            Dispatcher.Invoke(() => MediaPlayer.Open(uri));
            Dispatcher.Invoke(() => MediaPlayer.Play());
        }


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
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.JoyUp && x.Value > 0))
                RunMethod(Settings.Default.JoyUp);
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.JoyDown && x.Value > 0))
                RunMethod(Settings.Default.JoyDown);
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.JoyLeft && x.Value > 0))
                RunMethod(Settings.Default.JoyLeft);
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.JoyRight && x.Value > 0))
                RunMethod(Settings.Default.JoyRight);
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.Button12 && x.Value > 0))
                RunMethod(Settings.Default.Button12);
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.Button11 && x.Value > 0))
                RunMethod(Settings.Default.Button11);
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.Button10 && x.Value > 0))
                RunMethod(Settings.Default.Button10);
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.Button9 && x.Value > 0))
                RunMethod(Settings.Default.Button9);
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.Button8 && x.Value > 0))
                RunMethod(Settings.Default.Button8);
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.Button7 && x.Value > 0))
                RunMethod(Settings.Default.Button7);
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.Button6 && x.Value > 0))
                RunMethod(Settings.Default.Button6);
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.Button5 && x.Value > 0))
                RunMethod(Settings.Default.Button5);
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.Button4 && x.Value > 0))
                RunMethod(Settings.Default.Button4);
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.Button3 && x.Value > 0))
                RunMethod(Settings.Default.Button3);
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.Button2 && x.Value > 0))
                RunMethod(Settings.Default.Button2);
            if (e.LedWizUpdates.Any(x => x.JoystickButton == JoystickButton.Button1 && x.Value > 0))
                RunMethod(Settings.Default.Button1);
        }
        private void RunMethod(int whichOne)
        {
            switch ((InputMap)whichOne)
            {
                case InputMap.Start:
                    CheckStartNewGame();
                    break;
                case InputMap.Score10:
                    IncreaseScore(10);
                    break;
                case InputMap.Score20:
                    IncreaseScore(20);
                    break;
                case InputMap.Score30:
                    IncreaseScore(30);
                    break;
                case InputMap.Score40:
                    IncreaseScore(40);
                    break;
                case InputMap.Score50:
                    IncreaseScore(50);
                    break;
                case InputMap.CountScoredBalls:
                    IncrementBallsScored();
                    break;
                case InputMap.CountBalls:
                    IncrementBallsInPlay();
                    break;
                case InputMap.Select:
                    Exit();
                    break;
            }
        }
    }
}