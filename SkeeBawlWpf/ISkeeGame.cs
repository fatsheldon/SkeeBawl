using System;
using LedWiz;

namespace SkeeBawlWpf
{
    //public delegate void GameEndHandler(Window sender);
    public interface ISkeeGame
    {
        //event GameEndHandler GameEnd;
        void LedWizInputChange(object sender, LedWizInputArgs e);
        void Show();
        void Close();
        event EventHandler Closed;
        event EventHandler GameStart;
        event EventHandler AllBawlsInPlay;
    }
}