using System;

namespace LedWiz
{
    public class LedWizDeviceNotFoundException : Exception
    {
        public LedWizDeviceNotFoundException()
            : base("LED-Wiz controller not found.")
        { }
    }
}