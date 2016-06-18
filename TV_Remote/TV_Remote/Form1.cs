using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TV_Remote
{
    public partial class Form1 : Form
    {
        KeyboardController controller;

        /// <summary>
        /// On startup the remote will be paused to allow serial communications
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            this.controller = new KeyboardController(textBox1);
            pauseButton.Enabled = false;
            controller.pauseRemote();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Pause the remote so keypresses are no longer captured
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pauseButton_Click(object sender, EventArgs e)
        {
            controller.pauseRemote();
            switchButtonStatus(false, true);
        }

        /// <summary>
        /// Resume key captures and serial communication
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resumeButton_Click(object sender, EventArgs e)
        {
            DialogResult errorStatus = controller.resumeRemote();
            if (errorStatus == DialogResult.None)
            {
                switchButtonStatus(true, false);
            }
           
        }

        /// <summary>
        /// Used to switch the enabled status of both buttons
        /// </summary>
        /// <param name="pauseEnabled">
        /// New status of the pause button
        /// </param>
        /// <param name="resumeEnabled">
        /// New status of the resume button
        /// </param>
        private void switchButtonStatus(bool pauseEnabled, bool resumeEnabled)
        {
            pauseButton.Enabled = pauseEnabled;
            resumeButton.Enabled = resumeEnabled;
        }

        /// <summary>
        /// Change the text in the box shown
        /// </summary>
        /// <param name="newText"> 
        /// Text to be placed within the textbox
        /// </param>
        public void setTextBox(string newText)
        {
            textBox1.Text = newText;
        }
    }
}
