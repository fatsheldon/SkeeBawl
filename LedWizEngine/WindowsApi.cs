using System;
using System.Runtime.InteropServices;

namespace LedWiz
{
    internal static class WindowsApi
    {
        [DllImport("kernel32.dll")]
        public static extern ExecutionState SetThreadExecutionState(ExecutionState esFlags);

        [Flags]
        public enum ExecutionState : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }
    }
}