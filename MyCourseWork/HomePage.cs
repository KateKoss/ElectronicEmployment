using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyCourseWork
{
    /// <summary>
    /// Start page of programm
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class HomePage : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomePage"/> class.
        /// </summary>
        public HomePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the aboutProgrammToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void aboutProgrammToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutProgramm info = new AboutProgramm();
            info.Show();
        }

        /// <summary>
        /// Handles the Click event of the userButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void userButton_Click(object sender, EventArgs e)
        {
            AuthorizationUzer newUser = new AuthorizationUzer();
            newUser.Show();
        }

        /// <summary>
        /// Handles the Click event of the adminButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void adminButton_Click(object sender, EventArgs e)
        {
            AuthorizationAdmin newAdmin = new AuthorizationAdmin();
            newAdmin.Show();
        }

        private void вийтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Handles the FormClosing event of the HomePage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void HomePage_FormClosing(object sender, FormClosingEventArgs e)
        {
            const string message = "Are you sure you want to close this window?";//Are you sure that you would like to close the form?
            const string caption = "Close the window?";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.No)
            {
                // cancel the closure of the form.
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the exitToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
