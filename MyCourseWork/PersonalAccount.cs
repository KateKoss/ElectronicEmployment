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
    /// Form of personal account 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class PersonalAccount : Form
    {
        OleDbConnection connection = new OleDbConnection();
        
        int personalId;
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalAccount"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public PersonalAccount(int id)
        {
            InitializeComponent();
            personalId = id;
            string path = Application.StartupPath + @"\Employees.mdb";
            if (!File.Exists(path))
                File.WriteAllBytes(path, Properties.Resources.Employees);
            connection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + @"\Employees.mdb";
            vacancyTableAdapter.Connection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + @"\Employees.mdb";
        }

        /// <summary>
        /// Handles the Load event of the PersonalAccount control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PersonalAccount_Load(object sender, EventArgs e)
        {
            //try {
                // TODO: данная строка кода позволяет загрузить данные в таблицу "employeesDataSet1.Vacancy". При необходимости она может быть перемещена или удалена.
                this.vacancyTableAdapter.Fill(this.employeesDataSet1.Vacancy);
                vacancyDataGridView.Columns[0].HeaderText = "Порядковий номер";
                vacancyDataGridView.Columns[1].HeaderText = "Рубрика";
                vacancyDataGridView.Columns[2].HeaderText = "Посада";
                vacancyDataGridView.Columns[3].HeaderText = "Компанія";
                vacancyDataGridView.Columns[4].HeaderText = "Вимоги";
                vacancyDataGridView.Columns[5].HeaderText = "Зарплатня";
                vacancyDataGridView.Columns[6].HeaderText = "Розташування";
                OleDbCommand command = new OleDbCommand("SELECT EmpNumber,LastName,FirstName,Patronymic,Login,Passwordd FROM Employees WHERE EmpNumber = " + personalId.ToString(), connection);
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();

                label4.Text = reader[4].ToString();//вивести логін та ПІБ
                label5.Text = reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString();
                connection.Close();

                OleDbCommand command1 = new OleDbCommand("SELECT ID,Sector,Place,Company FROM Vacancy", connection);//,Position,Company,Requirements,Salary,Location
                connection.Open();
                OleDbDataReader reader1 = command1.ExecuteReader();
                

                for (int i = 0; reader1.Read(); i++)
                {
                    if (!sectorComboBox.Items.Contains(reader1[1].ToString()))       //якщо у списку немає рубрики 
                    {
                        sectorComboBox.Items.AddRange(new string[] { reader1[1].ToString() });   //dodaemo rubriky
                    }
                }
                connection.Close();
            //}
            //catch
            //{
            //    MessageBox.Show("Ви не можете змінювати значення стовбців таблиць в Employees.mdb\nЯкщо ви змінили іх, поверніть попереднє значення.");
            //}
        }

        /// <summary>
        /// Handles the Click event of the vacancyBindingNavigatorSaveItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void vacancyBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.vacancyBindingSource.EndEdit();
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of the sectorComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void sectorComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try//оброботать собітие изменения значения комбобокс
            {
                positionComboBox.Items.Clear();
                OleDbCommand command = new OleDbCommand("SELECT ID,Sector,Place,Company FROM Vacancy WHERE Sector = '" + sectorComboBox.Text.ToString() + "'", connection);// WHERE Sector = '" + sectorComboBox.ValueMember + "'
                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();

                for (int i = 0; reader.Read(); i++)
                {
                    if (!positionComboBox.Items.Contains(reader[2].ToString()))
                    {
                        positionComboBox.Items.AddRange(new string[] { reader[2].ToString() });
                    }
                }
                connection.Close();
            }
            catch
            {
                MessageBox.Show("Ви не можете змінювати значення стовбців таблиць в Employees.mdb\nЯкщо ви змінили іх, поверніть попереднє значення.");
            }
        }

        /// <summary>
        /// Handles the Click event of the searchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                DataView dataView1;
                // Налаштування об'єкту DataView
                dataView1 = new DataView(employeesDataSet1.Vacancy);
                // // Налаштування dataGridView для відображення даних
                vacancyDataGridView.DataSource = dataView1;
                if (sectorComboBox.SelectedItem != null && positionComboBox.SelectedItem != null)
                {
                    dataView1.RowFilter = "Sector = '" + sectorComboBox.Text + "'";
                    dataView1.RowFilter = "Place = '" + positionComboBox.Text + "'";
                }
                else
                    MessageBox.Show("Заповніть обидва поля запиту.");

            }
            catch
            {
                MessageBox.Show("Ви не можете змінювати значення стовбців таблиць в Employees.mdb\nЯкщо ви змінили іх, поверніть попереднє значення.");
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

        /// <summary>
        /// Handles the Click event of the colorToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = colorDialog1.Color;
            }
        }

        /// <summary>
        /// Handles the Click event of the fontToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                label1.Font = fontDialog1.Font;
                label2.Font = fontDialog1.Font;
                label3.Font = fontDialog1.Font;
                label4.Font = fontDialog1.Font;
                label5.Font = fontDialog1.Font;
                label6.Font = fontDialog1.Font;
                label7.Font = fontDialog1.Font;
                groupBox1.Font = fontDialog1.Font;
                groupBox2.Font = fontDialog1.Font;
                sectorComboBox.Font = fontDialog1.Font;
                positionComboBox.Font = fontDialog1.Font;
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the PersonalAccount control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void PersonalAccount_FormClosing(object sender, FormClosingEventArgs e)
        {
            const string message = "Ви впевненені, що ви хочете закрити це вікно?";//Are you sure that you would like to close the form?
            const string caption = "Закрити вікно?";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.No)
            {
                // cancel the closure of the form.
                e.Cancel = true;
            }
        }
    }
}
