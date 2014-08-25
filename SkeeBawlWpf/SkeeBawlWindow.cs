using System.Windows;
using System.Windows.Input;

namespace SkeeBawlWpf
{
    public abstract class SkeeBawlWindow : Window
    {
        protected SkeeBawlWindow()
        {
            this.Cursor = Cursors.None;
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
        }
    }
}