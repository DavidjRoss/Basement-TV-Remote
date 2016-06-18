using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace TV_Remote
{
    class KeyboardController
    {
        private TextBox currentBox;
        globalKeyboardHook volumeUp;
        globalKeyboardHook volumeDown;
        globalKeyboardHook power;
        globalKeyboardHook input;

        SerialController serial;

        /// <summary>
        /// Create a keyboard controller which will hook the insert, volume up, volume down, and mute keys.
        /// These keys will then be used to control power, volume up/down, and input respectively on the tv.
        /// </summary>
        /// <param name="text">
        /// A textbox which will need to be updated to display what function was last used.
        /// </param>
        public KeyboardController(TextBox text)
        {
            serial = new SerialController();

            this.currentBox = text;
            this.volumeUp = new globalKeyboardHook();
            this.volumeDown = new globalKeyboardHook();
            this.power = new globalKeyboardHook();
            this.input = new globalKeyboardHook();

            addHooks();
            addEventHandlers();
        }

        /// <summary>
        /// Add the key hooks to each button that will be remapped
        /// </summary>
        private void addHooks()
        {
            volumeUp.HookedKeys.Add(Keys.VolumeUp);
            volumeDown.HookedKeys.Add(Keys.VolumeDown);
            power.HookedKeys.Add(Keys.Insert);
            input.HookedKeys.Add(Keys.VolumeMute);
        }

        /// <summary>
        /// Add the functions which will handle keypresses for each button to be remapped
        /// </summary>
        private void addEventHandlers()
        {
            volumeUp.KeyDown += new KeyEventHandler(volumeUpKeyDown);
            volumeDown.KeyDown += new KeyEventHandler(volumeDownKeyDown);
            power.KeyDown += new KeyEventHandler(powerKeyDown);
            input.KeyDown += new KeyEventHandler(inputKeyDown);
        }

        /// <summary>
        /// Update the textbox, and send the string '1' over the currently open serial port
        /// This corresponds to decreasing the volume through the arduino which will be receiving the signal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void volumeUpKeyDown(object sender, KeyEventArgs e)
        {
            updateBox("Volume Up");
            serial.sendMessage("1");
            e.Handled = true;
        }

        /// <summary>
        /// Update hte textbox, and send the string '2' over the currently open serial port
        /// This corresponds to increasing the volume through the arduino which will be receiving the signal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void volumeDownKeyDown(object sender, KeyEventArgs e)
        {
            updateBox("Volume Down");
            serial.sendMessage("2");
            e.Handled = true;
        }

        /// <summary>
        /// Update the textbox, and send the string '4' over the currently open serial port.
        /// This corresponds to changing the 'on' state through the arduino which will be receiving the signal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void powerKeyDown(object sender, KeyEventArgs e)
        {
            updateBox("Power");
            serial.sendMessage("3");
            e.Handled = true;
        }

        /// <summary>
        /// Update the textbox, and send the string '4' over the currently open serial port.
        /// This corresponds to changing the input through the arduino which will be receiving the signal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inputKeyDown(object sender, KeyEventArgs e)
        {
            updateBox("Input");
            serial.sendMessage("4");
            e.Handled = true;
        }

        /// <summary>
        ///  Pause remote functions by unhooking the keys and closing the serial port
        /// </summary>
        public void pauseRemote()
        {
            volumeDown.unhook();
            volumeUp.unhook();
            power.unhook();
            input.unhook();

            serial.closeSerial();
        }

        /// <summary>
        /// Continue remote functions by rehooking keys and reopening the serial port
        /// </summary>
        public DialogResult resumeRemote()
        {
            DialogResult errorStatus = serial.openSerial();
            if(errorStatus == DialogResult.None)
            {
                volumeDown.hook();
                volumeUp.hook();
                power.hook();
                input.hook();
            }
            return errorStatus;
        }
 
        /// <summary>
        /// Send new text to the textbox
        /// </summary>
        /// <param name="text">
        /// A string to be placed in the text box
        /// </param>
        public void updateBox(String text)
        {
            currentBox.Text = text;
        }
    }
}
