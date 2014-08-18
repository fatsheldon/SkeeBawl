using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LedWiz;

namespace SkeeBawlWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ClassicGame : Window, ISkeeGame
    {
        public ClassicGame()
        {
            InitializeComponent();
        }

        #region ISkeeGame
        public event EventHandler GameStart;
        public event EventHandler AllBawlsInPlay;

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
                StartNewGame();
        }
        #endregion

        #region private eventhandlers
        private void Window_KeyUp(object sender, KeyEventArgs e)
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
                    StartNewGame();
                    break;
                case System.Windows.Input.Key.Escape: //killswitch
                    this.Close();
                    break;
            }
        }

        private void ClassicGame_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (GameStart != null)
                GameStart(this, new EventArgs());
        }
        #endregion

        #region privates
        private const int BallsToUse = 9;
        private int _ballsInPlay = 0;
        private int _ballsScored = 0;
        private int _score = 0;

        private void IncreaseScore(int howMuch)
        {
            if (_ballsScored < BallsToUse)
            {
                _score += howMuch;
                Dispatcher.Invoke(() => ScoreText.Text = _score.ToString());
            }
        }

        private void StartNewGame()
        {
            if (_ballsScored >= BallsToUse)
            {
                _ballsScored = 0;
                _ballsInPlay = 0;
                _score = 0;
                if (GameStart != null)
                    GameStart(this, new EventArgs());
                Dispatcher.Invoke(() => ScoreText.Text = _score.ToString());
                Dispatcher.Invoke(() => BallsScoredText.Text = _ballsScored.ToString());
                //Dispatcher.Invoke(() => BallsInPlayText.Text = _ballsInPlay.ToString());
            }
        }

        private void IncrementBallsScored()
        {
            if(_ballsScored < BallsToUse)
            _ballsScored++;
            Dispatcher.Invoke(() => BallsScoredText.Text = _ballsScored.ToString());
        }

        private void IncrementBallsInPlay()
        {
            _ballsInPlay++;
            //Dispatcher.Invoke(() => BallsInPlayText.Text = _ballsInPlay.ToString());
            if (_ballsInPlay >= BallsToUse && AllBawlsInPlay != null)
                AllBawlsInPlay(this, new EventArgs());
        }

        private void Exit()
        {
            if (_ballsScored >= BallsToUse)
                Dispatcher.Invoke(this.Close);
        //        this.Close();
        }
        #endregion
    }
}
