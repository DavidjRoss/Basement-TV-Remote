using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;

namespace TV_Remote
{
    class SerialController
    {
        private static SerialPort serial;
        private string port = "COM3";

        /// <summary>
        /// Create a serial controller which will send signals over COM3 to an arduino which will control a tv
        /// </summary>
        public  SerialController()
        {
            serial = new SerialPort();
            setupSerialCommunication();
        }

        /// <summary>
        /// Set the defaults for the serial port, that is 9600 baud and COM3
        /// </summary>
        private void setupSerialCommunication()
        {
            serial.BaudRate = 9600;
            serial.PortName = "COM3";
        }

        /// <summary>
        /// Open the serial port if it is not open already. If it is open send an error message
        /// </summary>
        public DialogResult openSerial()
        {
            DialogResult errorCode = DialogResult.None;
            try
            {
                serial.Open();
            }
            catch(UnauthorizedAccessException)
            {
              errorCode =  MessageBox.Show("Serial port 'COM3' is already open.", "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return errorCode;
        }

        /// <summary>
        /// Close the serial port
        /// </summary>
        public void closeSerial()
        {
            serial.Close();
        }

        /// <summary>
        /// Send a message over the serial port
        /// </summary>
        /// <param name="message">
        /// A string holding a message to be sent
        /// </param>
        public void sendMessage(string message)
        {
            serial.Write(message);
        }

    }
}
