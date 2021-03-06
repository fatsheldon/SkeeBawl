﻿using System;
using System.IO;
using System.Windows;

namespace SkeeBawlWpf
{
    public partial class ClassicGame : SkeeBawlGameWindow, ISkeeGame
    {
        public ClassicGame()
        {
            InitializeComponent();
        }

        #region ISkeeGame
        public event EventHandler GameStart;
        public event EventHandler AllBawlsInPlay;
        #endregion

        #region privates
        private readonly string _themeDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\classic");
        private const int BallsToUse = 9;
        private int _ballsInPlay;
        private int _ballsScoredSwitch;
        private int _ballsScored;
        private int _score;
        #endregion

        #region SkeeBawlGameWindow
        protected override void IncreaseScore(int howMuch)
        {
            if (_ballsScored < BallsToUse && _ballsScoredSwitch < BallsToUse)
            {
                _ballsScored++;
                switch (howMuch)
                {
                    case 10:
                        PlaySound(new Uri(Path.Combine(_themeDir, "Ding10.mp3")));
                        break;
                    case 20:
                        PlaySound(new Uri(Path.Combine(_themeDir, "Ding20.mp3")));
                        break;
                    case 30:
                        PlaySound(new Uri(Path.Combine(_themeDir, "Ding30.mp3")));
                        break;
                    case 40:
                        PlaySound(new Uri(Path.Combine(_themeDir, "Ding40.mp3")));
                        break;
                    case 50:
                        PlaySound(new Uri(Path.Combine(_themeDir, "Ding50.mp3")));
                        break;
                }
                _score += howMuch;
                Dispatcher.Invoke(() => ScoreText.Text = _score.ToString());
            }
        }

        protected override void CheckStartNewGame()
        {
            if (_ballsScoredSwitch >= BallsToUse)
            {
                _ballsScoredSwitch = 0;
                _ballsScored = 0;
                _ballsInPlay = 0;
                _score = 0;

                Dispatcher.Invoke(() => ScoreText.Text = _score.ToString());
                Dispatcher.Invoke(() => BallsScoredText.Text = _ballsScoredSwitch.ToString());
                StartNewGame();
            }
        }

        protected override void StartNewGame()
        {
            PlaySound(new Uri(Path.Combine(_themeDir, "start.mp3")));
            Dispatcher.Invoke(() => GameOverImage.Visibility = Visibility.Hidden);

            if (GameStart != null)
                GameStart(this, new EventArgs());
        }

        protected override void IncrementBallsScored()
        {
            if (_ballsScoredSwitch < BallsToUse)
            {
                _ballsScoredSwitch++;
                Dispatcher.Invoke(() => BallsScoredText.Text = _ballsScoredSwitch.ToString());
            }

            if (_ballsScoredSwitch == BallsToUse)
            {
                _ballsScoredSwitch++;
                Dispatcher.Invoke(() => GameOverImage.Visibility = Visibility.Visible);
                PlaySound(new Uri(Path.Combine(_themeDir, "stop.mp3")));
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
            if (_ballsScoredSwitch >= BallsToUse)
                Dispatcher.Invoke(this.Close);
        }
        #endregion
    }
}
