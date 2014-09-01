namespace SkeeBawlWpf
{
    public class ThemeSkeeConfig
    {
        public ThemeSkeeImageConfig BackgroundImageConfig { get; set; }
        public ThemeSkeeImageConfig GameOverImageConfig { get; set; }
        public ThemeSkeeTextBlockConfig ScoreTextConfig { get; set; }
        public ThemeSkeeTextBlockConfig BallsScoredTextConfig { get; set; }

        public string StartSound { get; set; }
        public string GameOverSound { get; set; }
        public string Score10Sound { get; set; }
        public string Score20Sound { get; set; }
        public string Score30Sound { get; set; }
        public string Score40Sound { get; set; }
        public string Score50Sound { get; set; }
    }

    public class ThemeSkeeItemConfig
    {//i thought this would be more useful... hmpf.
        public int Top { get; set; }
    }
    public class ThemeSkeeImageConfig : ThemeSkeeItemConfig
    {
        public int Left { get; set; }
        public bool StretchFill { get; set; }
        public string Source { get; set; }
    }
    public class ThemeSkeeTextBlockConfig : ThemeSkeeItemConfig
    {
        public string FontName { get; set; }
        public string FontColor { get; set; }
        public int FontSize { get; set; }
        public int Right { get; set; }
        public bool CustomFont { get; set; }//if yes, then FontName like this:  "./#DS-Digital Bold"
        public int PadLeft { get; set; }
        public bool GameOverVisibility { get; set; }
        public int GameOverTop { get; set; }
        public int GameOverRight { get; set; }
        public int GameOverFontSize { get; set; }
    }
}