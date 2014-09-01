using System;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using Newtonsoft.Json;
using FontFamily = System.Windows.Media.FontFamily;
using Path = System.IO.Path;

namespace SkeeBawlWpf
{
    /// <summary>
    /// Interaction logic for ThemeSkee.xaml
    /// </summary>
    public partial class ThemeSkee : SkeeBawlGameWindow, ISkeeGame
    {
        #region privates
        private const int BallsToUse = 9;
        private int _ballsInPlay = 0;
        private int _ballsScored = 0;
        private int _score = 0;
        private readonly string _themeSkeeDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets\\ThemeSkee");
        private readonly string _chosenThemeDir;
        private readonly ThemeSkeeConfig _themeSkeeConfig;
        #endregion

        public ThemeSkee()
        {
            InitializeComponent();
            var dirs = Directory.GetDirectories(_themeSkeeDir).ToArray();
            _chosenThemeDir = dirs[new Random().Next(0, dirs.Length)];
            

            _themeSkeeConfig = JsonConvert.DeserializeObject<ThemeSkeeConfig>(File.ReadAllText(Path.Combine(_chosenThemeDir, "theme.config")));

            BackgroundImage.Stretch = _themeSkeeConfig.BackgroundImageConfig.StretchFill ? Stretch.Fill : Stretch.None;
            BackgroundImage.Source = new BitmapImage(new Uri(Path.Combine(_chosenThemeDir, _themeSkeeConfig.BackgroundImageConfig.Source)));
            GameOverImage.Source = new BitmapImage(new Uri(Path.Combine(_chosenThemeDir, _themeSkeeConfig.GameOverImageConfig.Source)));
        }

        public event EventHandler GameStart;
        public event EventHandler AllBawlsInPlay;

        protected override void IncrementBallsInPlay()
        {
            _ballsInPlay++;
            if (_ballsInPlay >= BallsToUse && AllBawlsInPlay != null)
                AllBawlsInPlay(this, new EventArgs());
        }

        protected override void IncrementBallsScored()
        {
            if (_ballsScored < BallsToUse)
            {
                _ballsScored++;
                var ballsScoredString = _themeSkeeConfig.BallsScoredTextConfig.PadLeft > 0
                    ? _ballsScored.ToString().PadLeft(_themeSkeeConfig.BallsScoredTextConfig.PadLeft, '0')
                    : _ballsScored.ToString();
                Dispatcher.Invoke(() => BallsScoredText.Text = ballsScoredString);
            }

            if (_ballsScored == BallsToUse)
            {
                _ballsScored++;
                Dispatcher.Invoke(() => BackgroundImage.Visibility = Visibility.Hidden);
                Dispatcher.Invoke(() => GameOverImage.Visibility = Visibility.Visible);

                Dispatcher.Invoke(() => ScoreText.Visibility = _themeSkeeConfig.ScoreTextConfig.GameOverVisibility ? Visibility.Visible : Visibility.Hidden);
                Dispatcher.Invoke(() => Canvas.SetRight(ScoreText, _themeSkeeConfig.ScoreTextConfig.GameOverRight));
                Dispatcher.Invoke(() => Canvas.SetTop(ScoreText, _themeSkeeConfig.ScoreTextConfig.GameOverTop));
                if (_themeSkeeConfig.ScoreTextConfig.GameOverFontSize > 0)
                    Dispatcher.Invoke(() => ScoreText.FontSize = _themeSkeeConfig.ScoreTextConfig.GameOverFontSize);

                Dispatcher.Invoke(() => BallsScoredText.Visibility = _themeSkeeConfig.BallsScoredTextConfig.GameOverVisibility ? Visibility.Visible : Visibility.Hidden);
                Dispatcher.Invoke(() => Canvas.SetRight(BallsScoredText, _themeSkeeConfig.BallsScoredTextConfig.GameOverRight));
                Dispatcher.Invoke(() => Canvas.SetTop(BallsScoredText, _themeSkeeConfig.BallsScoredTextConfig.GameOverTop));
                if (_themeSkeeConfig.BallsScoredTextConfig.GameOverFontSize > 0)
                    Dispatcher.Invoke(() => BallsScoredText.FontSize = _themeSkeeConfig.BallsScoredTextConfig.GameOverFontSize);

                PlaySound(new Uri(Path.Combine(_chosenThemeDir, _themeSkeeConfig.GameOverSound)));
            }
        }

        protected override void IncreaseScore(int howMuch)
        {
            if (_ballsScored < BallsToUse)
            {
                switch (howMuch)
                {
                    case 10:
                        PlaySound(new Uri(Path.Combine(_chosenThemeDir, _themeSkeeConfig.Score10Sound)));
                        break;
                    case 20:
                        PlaySound(new Uri(Path.Combine(_chosenThemeDir, _themeSkeeConfig.Score20Sound)));
                        break;
                    case 30:
                        PlaySound(new Uri(Path.Combine(_chosenThemeDir, _themeSkeeConfig.Score30Sound)));
                        break;
                    case 40:
                        PlaySound(new Uri(Path.Combine(_chosenThemeDir, _themeSkeeConfig.Score40Sound)));
                        break;
                    case 50:
                        PlaySound(new Uri(Path.Combine(_chosenThemeDir, _themeSkeeConfig.Score50Sound)));
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
            PlaySound(new Uri(Path.Combine(_chosenThemeDir, _themeSkeeConfig.StartSound)));

            Dispatcher.Invoke(() => Canvas.SetRight(ScoreText, _themeSkeeConfig.ScoreTextConfig.Right));
            Dispatcher.Invoke(() => Canvas.SetTop(ScoreText, _themeSkeeConfig.ScoreTextConfig.Top));
            Dispatcher.Invoke(() => ScoreText.FontSize = _themeSkeeConfig.ScoreTextConfig.FontSize);
            Dispatcher.Invoke(() => ScoreText.FontFamily = _themeSkeeConfig.ScoreTextConfig.CustomFont ? new FontFamily(_chosenThemeDir + _themeSkeeConfig.ScoreTextConfig.FontName) : new FontFamily(_themeSkeeConfig.ScoreTextConfig.FontName));
            if (!string.IsNullOrEmpty(_themeSkeeConfig.ScoreTextConfig.FontColor))
                Dispatcher.Invoke(() => ScoreText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_themeSkeeConfig.ScoreTextConfig.FontColor)));

            Dispatcher.Invoke(() => Canvas.SetRight(BallsScoredText, _themeSkeeConfig.BallsScoredTextConfig.Right));
            Dispatcher.Invoke(() => Canvas.SetTop(BallsScoredText, _themeSkeeConfig.BallsScoredTextConfig.Top));
            Dispatcher.Invoke(() => BallsScoredText.Visibility = Visibility.Visible);
            Dispatcher.Invoke(() => BallsScoredText.FontSize = _themeSkeeConfig.BallsScoredTextConfig.FontSize);
            Dispatcher.Invoke(() => BallsScoredText.FontFamily = _themeSkeeConfig.BallsScoredTextConfig.CustomFont ? new FontFamily(_chosenThemeDir + _themeSkeeConfig.BallsScoredTextConfig.FontName) : new FontFamily(_themeSkeeConfig.BallsScoredTextConfig.FontName));
            if (!string.IsNullOrEmpty(_themeSkeeConfig.BallsScoredTextConfig.FontColor))
                Dispatcher.Invoke(() => BallsScoredText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(_themeSkeeConfig.BallsScoredTextConfig.FontColor)));

            Dispatcher.Invoke(() => BackgroundImage.Visibility = Visibility.Visible);
            Dispatcher.Invoke(() => GameOverImage.Visibility = Visibility.Hidden);

            if (GameStart != null)
                GameStart(this, new EventArgs());
        }

        protected override void Exit()
        {
            if (_ballsScored >= BallsToUse)
                Dispatcher.Invoke(this.Close);
        }


    }
}
