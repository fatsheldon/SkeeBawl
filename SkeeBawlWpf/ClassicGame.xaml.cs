using System;
using System.IO;
using System.Media;
using System.Windows;

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
                switch (howMuch)
                {
                    case 10:
                        using (var sp = new SoundPlayer(Path.Combine(_themeDir, "Ding10.wav")))
                            sp.Play();
                        break;
                    case 20:
                        using (var sp = new SoundPlayer(Path.Combine(_themeDir, "Ding20a.wav")))
                            sp.Play();
                        break;
                    case 30:
                        using (var sp = new SoundPlayer(Path.Combine(_themeDir, "Ding30a.wav")))
                            sp.Play();
                        break;
                    case 40:
                        using (var sp = new SoundPlayer(Path.Combine(_themeDir, "Ding40a.wav")))
                            sp.Play();
                        break;
                    case 50:
                        using (var sp = new SoundPlayer(Path.Combine(_themeDir, "Ding50a.wav")))
                            sp.Play();
                        break;
                }
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
            using (var sp = new SoundPlayer(Path.Combine(_themeDir, "start.wav")))
                sp.Play();
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
                Dispatcher.Invoke(() => GameOverImage.Visibility = Visibility.Visible);
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
