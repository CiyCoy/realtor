using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace realtor
{
    public partial class Sotrzap : Form
    {

        DataBase dataBase = new DataBase();
        public Sotrzap()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();

            int id;
            var Fio = textBox3.Text;
            int telefon;
            var dolj = textBox5.Text;
            


            if (int.TryParse(textBox2.Text, out id) == int.TryParse(textBox4.Text, out telefon))
            {

                var addQuery = $"insert into sotrudnik_db (id_sotr, fio, telefon, dolj) values ('{id}','{Fio}', '{telefon}','{dolj}')";

                var command = new SqlCommand(addQuery, dataBase.getConnection());
                command.ExecuteNonQuery();

                MessageBox.Show("Запись создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Запись не создана!", "ошибка №1!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            dataBase.closeConnection();

        }
    }
}
