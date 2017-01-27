using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyCourseWork
{
    /// <summary>
    /// Registration of new user
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Registration : Form
    {
        OleDbConnection connection1 = new OleDbConnection();
        OleDbConnection firstConnection = new OleDbConnection();
        public Registration()
        {
            InitializeComponent();
            string path = Application.StartupPath + @"\Employees.mdb";
            if (!File.Exists(path))
                File.WriteAllBytes(path, Properties.Resources.Employees);
            connection1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + @"\Employees.mdb";
            firstConnection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + @"\Employees.mdb";
        }

        /// <summary>
        /// Determines whether [is login duplicated].
        /// </summary>
        /// <returns></returns>
        private bool isLoginDuplicated()
        {
            OleDbCommand firstCommand = new OleDbCommand("SELECT EmpNumber,Login FROM Employees WHERE Login = '" + textBox1.Text.ToString() + "'", firstConnection);
            try
            {
                firstConnection.Open();
                OleDbDataReader firstReader = firstCommand.ExecuteReader();
                firstReader.Read();
                if (firstReader[1].ToString() == textBox1.Text.ToString())
                {
                    firstConnection.Close();
                    return true;//логины повторяются
                }
                else
                {
                    firstConnection.Close();
                    return false;//логины не повторяются
                }
            }
            catch //(Exception r)
            {
                //MessageBox.Show(r.ToString());
                firstConnection.Close();
                return false;   //логины не повторяются
            }
                        
        }

        /// <summary>
        /// Handles the Click event of the finishButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void finishButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Заповніть обов'язкові поля, що позначені знаком *");
            }
            else
            {
                if (isLoginDuplicated())
                {
                    MessageBox.Show("Такий логін вже існує!");
                }
                else
                {
                    try
                    {
                        connection1.Open();
                        OleDbCommand command = new OleDbCommand("INSERT INTO [Employees](LastName,FirstName,Patronymic,Profession,DateOfBirth,Salary,PreviousEmployment,Login,Passwordd)"
                            + "Values(@lname,@fname,@patr,@prof,@dateBir,@salary,@prevEmploy,@log,@passw)", connection1);

                        command.Parameters.AddWithValue("@lname", textBox3.Text);
                        command.Parameters.AddWithValue("@fname", textBox4.Text);
                        command.Parameters.AddWithValue("@patr", textBox5.Text);
                        command.Parameters.AddWithValue("@prof", textBox6.Text);
                        command.Parameters.AddWithValue("@dateBir", dateTimePicker1.Value.Date.ToString());
                        command.Parameters.AddWithValue("@salary", trackBar1.Value);
                        command.Parameters.AddWithValue("@prevEmploy", textBox7.Text);
                        command.Parameters.AddWithValue("@log", textBox1.Text);
                        command.Parameters.AddWithValue("@passw", textBox2.Text);
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            Debug.WriteLine("Ok");
                        }
                        else MessageBox.Show("Ви не можете змінювати значення стовбців таблиць в Employees.mdb\nЯкщо ви змінили іх, поверніть попереднє значення.");

                        connection1.Close();

                        command = new OleDbCommand("SELECT EmpNumber,Login FROM Employees WHERE Login = '" + textBox1.Text.ToString() + "'", connection1);//
                        connection1.Open();
                        OleDbDataReader reader = command.ExecuteReader();
                        reader.Read();
                        PersonalAccount acc = new PersonalAccount(Convert.ToInt32(reader[0]));
                        connection1.Close();
                        acc.Show();
                        this.Close();
                    }
                    catch (Exception r)
                    {
                        MessageBox.Show(r.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Scroll event of the trackBar1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label13.Text = "" + trackBar1.Value;
        }

        /// <summary>
        /// Handles the Click event of the вийтиToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void вийтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the KeyPress event of the textBox3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Поле 'Прізвище' не може містити цифри та знаки пунктуації");
                errorProvider1.SetError(textBox3, "Мають бути тільки букви");
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the textBox4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Поле 'Ім'я' не може містити цифри та знаки пунктуації");
                errorProvider1.SetError(textBox4, "Мають бути тільки букви");
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the textBox5 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Поле 'По батькові' не може містити цифри та знаки пунктуації");
                errorProvider1.SetError(textBox5, "Мають бути тільки букви");
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the textBox6 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Поле не може містити цифри");
                errorProvider1.SetError(textBox6, "Мають бути тільки букви");
            }
        }
    }
}
