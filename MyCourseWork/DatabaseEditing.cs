using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCourseWork
{
    /// <summary>
    /// Editing of database
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class DatabaseEditing : Form
    {
        DataView dataView1;
        DataView dataView2;
        OleDbConnection connection = new OleDbConnection();

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseEditing"/> class.
        /// </summary>
        public DatabaseEditing()
        {
            InitializeComponent();
            string path = Application.StartupPath + @"\Employees.mdb";  //вказуємо відносний шлях
            if (!File.Exists(path))
                File.WriteAllBytes(path, Properties.Resources.Employees);
            employeesTableAdapter.Connection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + @"\Employees.mdb";
            vacancyTableAdapter.Connection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + @"\Employees.mdb";
        }

        /// <summary>
        /// Handles the Load event of the Form1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "employeesDataSet1.Vacancy". При необходимости она может быть перемещена или удалена.
            this.vacancyTableAdapter.Fill(this.employeesDataSet1.Vacancy);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "employeesDataSet.Employees". При необходимости она может быть перемещена или удалена.
            this.employeesTableAdapter.Fill(this.employeesDataSet.Employees);
            //переводим имена колонок
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Прізвище";
            dataGridView1.Columns[2].HeaderText = "Ім'я";
            dataGridView1.Columns[3].HeaderText = "По-батькові";
            dataGridView1.Columns[4].HeaderText = "Професія";
            dataGridView1.Columns[5].HeaderText = "Дата народження";
            dataGridView1.Columns[6].HeaderText = "Зарплатня";
            dataGridView1.Columns[7].HeaderText = "Поререднє місце роботи";
            dataGridView1.Columns[8].HeaderText = "Нараховані виплати по безробіттю";
            dataGridView1.Columns[9].HeaderText = "Перекваліфікація";
            dataGridView1.Columns[10].HeaderText = "Направлення на працевлаштування";

            vacancyDataGridView.Columns[0].HeaderText = "Порядковий номер";
            vacancyDataGridView.Columns[1].HeaderText = "Рубрика";
            vacancyDataGridView.Columns[2].HeaderText = "Посада";
            vacancyDataGridView.Columns[3].HeaderText = "Компанія";
            vacancyDataGridView.Columns[4].HeaderText = "Вимоги компанії";
            vacancyDataGridView.Columns[5].HeaderText = "Зарплата";
            vacancyDataGridView.Columns[6].HeaderText = "Розташування";
            // Налаштування об'єкту DataView
            dataView1 = new DataView(employeesDataSet.Employees);
            dataView2 = new DataView(employeesDataSet1.Vacancy);
            // // Налаштування dataGridView для відображення даних
            dataGridView1.DataSource = dataView1;
            vacancyDataGridView.DataSource = dataView2;
            comboBox1.Items.AddRange(new string[] { "ID", "Прізвище", "Ім'я", "По-батькові", "Професія",
                "Дата народження","Зарплатня","Поререднє місце роботи","Нараховані виплати по безробіттю","Перекваліфікація","Направлення на працевлаштування" });
            comboBox2.Items.AddRange(new string[] { "Порядковий номер", "Рубрика", "Посада", "Компанія", "Вимоги компанії", "Зарплата", "Розташування" });
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.employeesTableAdapter.Update(this.employeesDataSet.Employees);
            MessageBox.Show("Зміни збережено");
        }

        /// <summary>
        /// Handles the Click event of the button4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem == null || textBox2.Text == "")
                {
                    MessageBox.Show("Заповніть поля");
                }
                else
                {
                    // Привласнення початкового порядку сортування
                    dataView1.Sort = findRightCollumn(comboBox1.SelectedItem.ToString());
                }
            }
            catch
            {
                MessageBox.Show("Ви не можете змінювати значення стовбців таблиць в Employees.mdb\nЯкщо ви змінили іх, поверніть попереднє значення.");
            }
        }

        /// <summary>
        /// Handles the Click event of the button2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || textBox2.Text == "")
            {
                MessageBox.Show("Заповніть поля");
            }
            else
                dataView1.RowFilter = findRightCollumn(comboBox1.SelectedItem.ToString()) + " = '" + textBox2.Text + "'";
        }

        /// <summary>
        /// Handles the Click event of the button3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || textBox2.Text == "")
            {
                MessageBox.Show("Заповніть поля");
            }
            else
            {
                dataView1.Sort = findRightCollumn(comboBox1.SelectedItem.ToString());
                dataView1.RowFilter = findRightCollumn(comboBox1.SelectedItem.ToString()) + " = '" + textBox2.Text + "'";
            }
        }

        /// <summary>
        /// Finds the right collumn.
        /// </summary>
        /// <param name="selectetValue">The selectet value.</param>
        /// <returns></returns>
        private string findRightCollumn(string selectetValue)
        {
            switch (selectetValue)
            {
                case "ID":
                    return "EmpNumber";
                case "Прізвище":
                    return "LastName";
                case "Ім'я":
                    return "FirstName";
                case "По-батькові":
                    return "Patronymic";
                case "Професія":
                    return "Profession";
                case "Дата народження":
                    return "DateOfBirth";
                case "Зарплатня":
                    return "Salary";
                case "Поререднє місце роботи":
                    return "PreviousEmployment";
                case "Нараховані виплати по безробіттю":
                    return "AccruedUnemploymentBenefits";
                case "Перекваліфікація":
                    return "Retraining";
                case "Направлення на працевлаштування":
                    return "ReferralToJob";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Finds the right collumn vacancy.
        /// </summary>
        /// <param name="selectetValue">The selectet value.</param>
        /// <returns></returns>
        private string findRightCollumnVacancy(string selectetValue)
        {
            switch (selectetValue)
            {
                case "Порядковий номер":
                    return "ID";
                case "Рубрика":
                    return "Sector";
                case "Посада":
                    return "Place";
                case "Компанія":
                    return "Company";
                case "Вимоги компанії":
                    return "Requirements";
                case "Зарплата":
                    return "Salary";
                case "Розташування":
                    return "Location";
                default:
                    return "";
            }
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Handles the Click event of the button6 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button6_Click(object sender, EventArgs e)
        {
            this.vacancyTableAdapter.Update(this.employeesDataSet1.Vacancy);
            MessageBox.Show("Зміни збережено");
        }

        /// <summary>
        /// Handles the Click event of the button7 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.SelectedItem == null || textBox1.Text == "")
                {
                    MessageBox.Show("Заповніть поля");
                }
                else
                {
                    // Привласнення початкового порядку сортування
                    dataView2.Sort = findRightCollumnVacancy(comboBox2.SelectedItem.ToString());
                }
            }
            catch
            {
                MessageBox.Show("Ви не можете змінювати значення стовбців таблиць в Employees.mdb\nЯкщо ви змінили іх, поверніть попереднє значення.");
            }
        }

        /// <summary>
        /// Handles the Click event of the button8 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button8_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null || textBox1.Text == "")
            {
                MessageBox.Show("Заповніть поля");
            }
            else
                dataView2.RowFilter = findRightCollumnVacancy(comboBox2.SelectedItem.ToString()) + " = '" + textBox1.Text + "'";
        }

        /// <summary>
        /// Handles the Click event of the button9 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button9_Click(object sender, EventArgs e)
        {
            dataView1.RowFilter = "";
        }

        /// <summary>
        /// Handles the Click event of the button10 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button10_Click(object sender, EventArgs e)
        {
            dataView2.RowFilter = "";
        }

        /// <summary>
        /// Handles the Click event of the exitToolStripMenuItem1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the aboutProgToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void aboutProgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutProgramm newWinndow = new AboutProgramm();
            newWinndow.Show();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
        }

        private void bindingNavigatorDeleteItem1_Click(object sender, EventArgs e)
        {
            vacancyDataGridView.Rows.RemoveAt(vacancyDataGridView.CurrentRow.Index);
        }
    }
}
