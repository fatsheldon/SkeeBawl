using System;
using System.Windows;
using LedWiz;

namespace SkeeBawlWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly LedWizEngine _ledWizEngine;
        private readonly MenuWindow _menu = new MenuWindow();
        private readonly bool _ledWizExists = false;
        public App()
        {
            try
            {
                _ledWizEngine = new LedWizEngine();
                _ledWizExists = true;
            }
            catch (LedWizDeviceNotFoundException)
            {
                _ledWizExists = false;
            }
            _menu.GameStart += menu_GameStart;
        }

        void menu_GameStart(object sender, GameStartEventArgs e)
        {
            if (_ledWizExists)
            {
                _ledWizEngine.InputChange -= _menu.ledWiz_InputChange;
                _ledWizEngine.InputChange += e.Game.LedWizInputChange;
                e.Game.GameStart += Game_GameStart;
                e.Game.AllBawlsInPlay += Game_AllBawlsInPlay;
            }
            e.Game.Closed += Game_Closed;
            e.Game.Show();
            _menu.Hide();
        }

        void Game_AllBawlsInPlay(object sender, EventArgs e)
        {
            _ledWizEngine.SetSolenoid(false);
        }

        void Game_GameStart(object sender, EventArgs e)
        {
            _ledWizEngine.SetSolenoid(true);
        }

        void Game_Closed(object sender, EventArgs e)
        {
            ((ISkeeGame) sender).GameStart -= Game_GameStart;
            ((ISkeeGame) sender).AllBawlsInPlay -= Game_AllBawlsInPlay;
            ((ISkeeGame) sender).Closed -= Game_Closed;
            if (_ledWizExists)
            {
                _ledWizEngine.InputChange -= ((ISkeeGame) sender).LedWizInputChange;
                _ledWizEngine.InputChange += _menu.ledWiz_InputChange;
                _ledWizEngine.SetSolenoid(false); //make sure balls are collecting
            }
            _menu.Show();
        }

        void ledWizEngine_EngineStateChange(object sender, EventArgs e)
        {
            if (_ledWizEngine != null)
                _ledWizEngine.Dispose();
            Current.Shutdown();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            if (_ledWizExists)
            {
                _ledWizEngine.StartPolling();
                _ledWizEngine.EngineStateChange += ledWizEngine_EngineStateChange;
                _ledWizEngine.InputChange += _menu.ledWiz_InputChange;
            }
            _menu.Closed += menu_Closed;
            _menu.Show();
        }

        void menu_Closed(object sender, EventArgs e)
        {
            if (_ledWizExists)
                _ledWizEngine.StopPolling();
            else
            {
                if (_ledWizEngine != null)
                    _ledWizEngine.Dispose();
                Current.Shutdown();
            }
        }
    }
}
