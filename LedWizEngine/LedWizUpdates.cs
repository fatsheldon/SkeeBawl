using System.Collections.Generic;
using SharpDX.DirectInput;

namespace LedWiz
{
    /// <summary>
    /// this silly class is supposed to translate the raw joystick inputs to somethig the consumer projects can use without needing SharpDX references
    /// It basically just maps the inputs to values according to the LEDWIZ documentation
    /// </summary>
    public class LedWizUpdates : List<LedWizUpdate>
    {
        public LedWizUpdates(IEnumerable<JoystickUpdate> joystickUpdates)
        {
            foreach (var joystickUpdate in joystickUpdates)
            {
                switch (joystickUpdate.Offset)
                {
                    case JoystickOffset.Buttons0:
                        this.Add(
                            new LedWizUpdate
                            {
                                Sequence = joystickUpdate.Sequence,
                                Timestamp = joystickUpdate.Timestamp,
                                Value = joystickUpdate.Value,
                                LedWizInput = 17,
                                JoystickButton = JoystickButton.Button1
                            });
                        break;
                    case JoystickOffset.Buttons1:
                        this.Add(
                            new LedWizUpdate
                            {
                                Sequence = joystickUpdate.Sequence,
                                Timestamp = joystickUpdate.Timestamp,
                                Value = joystickUpdate.Value,
                                LedWizInput = 18,
                                JoystickButton = JoystickButton.Button2
                            });
                        break;
                    case JoystickOffset.Buttons2:
                        this.Add(
                            new LedWizUpdate
                            {
                                Sequence = joystickUpdate.Sequence,
                                Timestamp = joystickUpdate.Timestamp,
                                Value = joystickUpdate.Value,
                                LedWizInput = 19,
                                JoystickButton = JoystickButton.Button3
                            });
                        break;
                    case JoystickOffset.Buttons3:
                        this.Add(
                            new LedWizUpdate
                            {
                                Sequence = joystickUpdate.Sequence,
                                Timestamp = joystickUpdate.Timestamp,
                                Value = joystickUpdate.Value,
                                LedWizInput = 20,
                                JoystickButton = JoystickButton.Button4
                            });
                        break;
                    case JoystickOffset.Buttons4:
                        this.Add(
                            new LedWizUpdate
                            {
                                Sequence = joystickUpdate.Sequence,
                                Timestamp = joystickUpdate.Timestamp,
                                Value = joystickUpdate.Value,
                                LedWizInput = 21,
                                JoystickButton = JoystickButton.Button5
                            });
                        break;
                    case JoystickOffset.Buttons5:
                        this.Add(
                            new LedWizUpdate
                            {
                                Sequence = joystickUpdate.Sequence,
                                Timestamp = joystickUpdate.Timestamp,
                                Value = joystickUpdate.Value,
                                LedWizInput = 22,
                                JoystickButton = JoystickButton.Button6
                            });
                        break;
                    case JoystickOffset.Buttons6:
                        this.Add(
                            new LedWizUpdate
                            {
                                Sequence = joystickUpdate.Sequence,
                                Timestamp = joystickUpdate.Timestamp,
                                Value = joystickUpdate.Value,
                                LedWizInput = 23,
                                JoystickButton = JoystickButton.Button7
                            });
                        break;
                    case JoystickOffset.Buttons7:
                        this.Add(
                            new LedWizUpdate
                            {
                                Sequence = joystickUpdate.Sequence,
                                Timestamp = joystickUpdate.Timestamp,
                                Value = joystickUpdate.Value,
                                LedWizInput = 24,
                                JoystickButton = JoystickButton.Button8
                            });
                        break;
                    case JoystickOffset.Buttons8:
                        this.Add(
                            new LedWizUpdate
                            {
                                Sequence = joystickUpdate.Sequence,
                                Timestamp = joystickUpdate.Timestamp,
                                Value = joystickUpdate.Value,
                                LedWizInput = 25,
                                JoystickButton = JoystickButton.Button9
                            });
                        break;
                    case JoystickOffset.Buttons9:
                        this.Add(
                            new LedWizUpdate
                            {
                                Sequence = joystickUpdate.Sequence,
                                Timestamp = joystickUpdate.Timestamp,
                                Value = joystickUpdate.Value,
                                LedWizInput = 26,
                                JoystickButton = JoystickButton.Button10
                            });
                        break;
                    case JoystickOffset.Buttons10:
                        this.Add(
                            new LedWizUpdate
                            {
                                Sequence = joystickUpdate.Sequence,
                                Timestamp = joystickUpdate.Timestamp,
                                Value = joystickUpdate.Value,
                                LedWizInput = 27,
                                JoystickButton = JoystickButton.Button11
                            });
                        break;
                    case JoystickOffset.Buttons11:
                        this.Add(
                            new LedWizUpdate
                            {
                                Sequence = joystickUpdate.Sequence,
                                Timestamp = joystickUpdate.Timestamp,
                                Value = joystickUpdate.Value,
                                LedWizInput = 28,
                                JoystickButton = JoystickButton.Button12
                            });
                        break;

                        // I HAVE NOT TESTED ANYTHING BEYOND HERE AND DO NOT KNOW if JoystickOffset.Button12, 13, 14, 15 are even valid cases for the last 4 input slots
                    case JoystickOffset.Buttons12:
                        this.Add(
                            new LedWizUpdate
                            {
                                Sequence = joystickUpdate.Sequence,
                                Timestamp = joystickUpdate.Timestamp,
                                Value = joystickUpdate.Value,
                                LedWizInput = 29,
                                JoystickButton = JoystickButton.JoyUp
                            });
                        break;
                    case JoystickOffset.Buttons13:
                        this.Add(
                            new LedWizUpdate
                            {
                                Sequence = joystickUpdate.Sequence,
                                Timestamp = joystickUpdate.Timestamp,
                                Value = joystickUpdate.Value,
                                LedWizInput = 30,
                                JoystickButton = JoystickButton.JoyDown
                            });
                        break;
                    case JoystickOffset.Buttons14:
                        this.Add(
                            new LedWizUpdate
                            {
                                Sequence = joystickUpdate.Sequence,
                                Timestamp = joystickUpdate.Timestamp,
                                Value = joystickUpdate.Value,
                                LedWizInput = 31,
                                JoystickButton = JoystickButton.JoyLeft
                            });
                        break;
                    case JoystickOffset.Buttons15:
                        this.Add(
                            new LedWizUpdate
                            {
                                Sequence = joystickUpdate.Sequence,
                                Timestamp = joystickUpdate.Timestamp,
                                Value = joystickUpdate.Value,
                                LedWizInput = 32,
                                JoystickButton = JoystickButton.JoyRight
                            });
                        break;
                }

            }
        }
    }
}