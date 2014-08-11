using System;
using System.ComponentModel;
using System.Linq;
using SharpDX.DirectInput;

namespace LedWiz
{
    public class LedWizEngine : IDisposable
    {
        private readonly BackgroundWorker _hardwarePoller = new BackgroundWorker();
        private readonly Joystick _joystick;
        private LEDWiz _ledWiz; // the dll wrapper for sending output

        public delegate void LedWizInputHandler(object sender, LedWizInputArgs e);
        public event LedWizInputHandler InputChange;
        public event EventHandler EngineStateChange;

        public LedWizEngine()
        {
            var directInput = new SharpDX.DirectInput.DirectInput();
            var stuff = directInput.GetDevices();
            var joystickDeviceInstance = directInput.GetDevices().FirstOrDefault(x => x.Type == SharpDX.DirectInput.DeviceType.Joystick);//FIX THIS to be more specific! currently it'll take the first joystick it finds... this is dumb.
            if (joystickDeviceInstance == null)
                throw new LedWizDeviceNotFoundException();
            _joystick = new SharpDX.DirectInput.Joystick(directInput, joystickDeviceInstance.InstanceGuid);
            _joystick.Properties.BufferSize = 128;

            _hardwarePoller.WorkerSupportsCancellation = true;
            _hardwarePoller.WorkerReportsProgress = false;
            _hardwarePoller.DoWork += _hardwarePoller_DoWork;
            _hardwarePoller.RunWorkerCompleted += _hardwarePoller_RunWorkerCompleted;

            _ledWiz = new LEDWiz(IntPtr.Zero);
            _ledWiz.StartupLighting();
        }

        public void StartPolling()
        {
            _hardwarePoller.RunWorkerAsync();
        }

        public void StopPolling()
        {
            _hardwarePoller.CancelAsync();
        }

        private void _hardwarePoller_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (EngineStateChange != null)
                EngineStateChange(this, e as EventArgs);
        }

        private void _hardwarePoller_DoWork(object sender, DoWorkEventArgs e)
        {
            _joystick.Acquire();
            while (true)
            {
                if (((BackgroundWorker)sender).CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                _joystick.Poll();
                var datas = _joystick.GetBufferedData();
                if (datas.Any())
                    if (InputChange != null)
                        InputChange(this, new LedWizInputArgs(datas));
            }
        }

        public void SetSolenoid(bool isOn)
        {
            _ledWiz.SBA(0, isOn ? 1 : 0, 0, 0, 0, 2);
        }

        public void Dispose()
        {
            _ledWiz.Dispose();
        }
    }
}