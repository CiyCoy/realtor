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
    public partial class Sdelkazap : Form
    {
        DataBase dataBase = new DataBase();
        public Sdelkazap()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();

            int kod_sdelki;
            int id_kv;
            int id_sotr;
            int id_klient;
            var tip = textBox9.Text;
            int summa;
            var data = textBox7.Text;
            var status= textBox6.Text;

            


            if (int.TryParse(textBox2.Text, out kod_sdelki) == int.TryParse(textBox3.Text, out id_kv) == int.TryParse(textBox4.Text, out id_sotr) == int.TryParse(textBox5.Text, out id_klient) == int.TryParse(textBox8.Text, out summa) )
            {

                var addQuery = $"insert into sdelka_db (kod_sdelki, id_kv, id_sotr, id_klient, tip, summa, data, status ) values ('{kod_sdelki}','{id_kv}', '{id_sotr}','{id_klient}', '{tip}', '{summa}', '{data}', '{status}')";

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
