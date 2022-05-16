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
    public partial class vladzap : Form
    {
        DataBase dataBase = new DataBase();
        public vladzap()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            dataBase.openConnection();

            int id;
            var Fio = textBox3.Text;
            int tel;
            var adres = textBox5.Text;
            int nomer;
            var data = textBox7.Text;


            if (int.TryParse(textBox2.Text, out id) == int.TryParse(textBox4.Text, out tel) == int.TryParse(textBox6.Text, out nomer))
            {              
                var addQuery = $"insert into vladelec_db (id_vlad, fio, tel, addres_prop, nomer_pas, data_pojd) values ('{id}','{Fio}', '{tel}','{adres}', '{nomer}', '{data}')";

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
