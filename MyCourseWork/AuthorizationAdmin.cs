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
    /// Class authorization admin
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class AuthorizationAdmin : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationAdmin"/> class.
        /// </summary>
        public AuthorizationAdmin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (loginTextBox.Text == "admin" && passwordTextBox.Text == "1111")
            {
                DatabaseEditing editing = new DatabaseEditing();
                editing.Show();
                this.Close();
            }
            else MessageBox.Show("Невірний логін або пароль");
        }

        /// <summary>
        /// Handles the KeyDown event of the AuthorizationAdmin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void AuthorizationAdmin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();// имитируем нажатие button1
            }
        }

    }
}
