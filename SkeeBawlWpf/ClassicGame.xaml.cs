using System;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace SkeeBawlWpf
{
    public partial class ClassicGame : SkeeBawlGameWindow, ISkeeGame
    {
        private readonly string _themeDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\classic");

        public ClassicGame()
        {
            InitializeComponent();
        }

        #region ISkeeGame
        public event EventHandler GameStart;
        public event EventHandler AllBawlsInPlay;
        #endregion

        #region privates
        private const int BallsToUse = 9;
        private int _ballsInPlay = 0;
        private int _ballsScored = 0;
        private int _score = 0;
        #endregion

        #region SkeeBawlGameWindow
        protected override void IncreaseScore(int howMuch)
        {
            if (_ballsScored < BallsToUse)
            {
                var mp = new MediaPlayer();
                switch (howMuch)
                {
                    case 10:
                        mp.Open(new Uri(Path.Combine(_themeDir, "Ding10.mp3")));
                        break;
                    case 20:
                        mp.Open(new Uri(Path.Combine(_themeDir, "Ding20.mp3")));
                        break;
                    case 30:
                        mp.Open(new Uri(Path.Combine(_themeDir, "Ding30.mp3")));
                        break;
                    case 40:
                        mp.Open(new Uri(Path.Combine(_themeDir, "Ding40.mp3")));
                        break;
                    case 50:
                        mp.Open(new Uri(Path.Combine(_themeDir, "Ding50.mp3")));
                        break;
                }
                mp.Play();
                _score += howMuch;
                Dispatcher.Invoke(() => ScoreText.Text = _score.ToString());
            }
        }

        protected override void CheckStartNewGame()
        {
            if (_ballsScored >= BallsToUse)
            {
                _ballsScored = 0;
                _ballsInPlay = 0;
                _score = 0;

                Dispatcher.Invoke(() => ScoreText.Text = _score.ToString());
                Dispatcher.Invoke(() => BallsScoredText.Text = _ballsScored.ToString());
                StartNewGame();
            }
        }

        protected override void StartNewGame()
        {
            var mp = new MediaPlayer();
            mp.Open(new Uri(Path.Combine(_themeDir, "start.mp3")));
            mp.Play();

            Dispatcher.Invoke(() => GameOverImage.Visibility = Visibility.Hidden);

            if (GameStart != null)
                GameStart(this, new EventArgs());
        }

        protected override void IncrementBallsScored()
        {
            if (_ballsScored < BallsToUse)
            {
                _ballsScored++;
                Dispatcher.Invoke(() => BallsScoredText.Text = _ballsScored.ToString());
            }

            if (_ballsScored == BallsToUse)
            {
                Dispatcher.Invoke(() => GameOverImage.Visibility = Visibility.Visible);
                var mp = new MediaPlayer();
                mp.Open(new Uri(Path.Combine(_themeDir, "stop.mp3")));
                mp.Play();
            }
        }

        protected override void IncrementBallsInPlay()
        {
            _ballsInPlay++;
            if (_ballsInPlay >= BallsToUse && AllBawlsInPlay != null)
                AllBawlsInPlay(this, new EventArgs());
        }

        protected override void Exit()
        {
            if (_ballsScored >= BallsToUse)
                Dispatcher.Invoke(this.Close);
        }
        #endregion
    }
}
