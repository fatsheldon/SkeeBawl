using System;
using System.Collections.Generic;
using SharpDX.DirectInput;

namespace LedWiz
{
    public class LedWizInputArgs : EventArgs
    {
        public LedWizInputArgs(IEnumerable<JoystickUpdate> joystickUpdates)
        {
            LedWizUpdates = new LedWizUpdates(joystickUpdates);
        }
        public List<LedWizUpdate> LedWizUpdates { get; private set; }
    }
}