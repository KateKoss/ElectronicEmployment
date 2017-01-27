using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyCourseWork
{
    /// <summary>
    /// Сlass user authentication
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class AuthorizationUzer : Form
    {
        OleDbConnection connection = new OleDbConnection();
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationUzer"/> class.
        /// </summary>
        public AuthorizationUzer()
        {
            InitializeComponent();
            string path = Application.StartupPath + @"\Employees.mdb";
            if (!File.Exists(path))
                File.WriteAllBytes(path, Properties.Resources.Employees);
            connection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + @"\Employees.mdb";
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand command = new OleDbCommand("SELECT EmpNumber,Login,Passwordd FROM Employees WHERE Login = '" + loginTextBox.Text.ToString() + "'", connection); //WHERE Login = " + loginTextBox.Text.ToString()
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                PersonalAccount account = new PersonalAccount(Convert.ToInt32(reader[0]));
                account.Show();
                loginTextBox.Clear();
                passwordTextBox.Clear();
            }
            catch
            {
                MessageBox.Show("Невірний логін або пароль");
            }            
            connection.Close();
        }

        /// <summary>
        /// Handles the LinkClicked event of the linkLabel1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registration r = new Registration();
            r.Show();
        }
    }
}
