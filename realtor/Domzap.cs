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
    public partial class Domzap : Form
    {
        DataBase dataBase = new DataBase();

        public Domzap()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataBase.openConnection();

            int addres_doma;
            var ulica = textBox3.Text;
            var lift = textBox4.Text;
            var domofon = textBox5.Text;
            var parkovka = textBox6.Text;
            var ded_plosh = textBox7.Text;
            int nomer_doma;
            

            if (int.TryParse(textBox2.Text, out addres_doma) == int.TryParse(textBox8.Text, out nomer_doma))
            {

                var addQuery = $"insert into dom_db(addres_doma, ulica, lift, domofon, parkovka, ded_plosh, nomer_doma ) values ('{addres_doma}','{ulica}', '{lift}','{domofon}', '{parkovka}', '{ded_plosh}', '{nomer_doma}')";

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

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    dataBase.openConnection();

        //    int addres_doma;
        //    var ulica = textBox3.Text;
        //    var lift = textBox4.Text;
        //    var domofon = textBox5.Text;
        //    var parkovka = textBox6.Text;
        //    var ded_plosh = textBox7.Text;
        //    int noer_doma;


        //    if (int.TryParse(textBox2.Text, out addres_doma) || int.TryParse(textBox8.Text, out nomer_doma))
        //    {

        //        var addQuery = $"insert into dom_db(addres_doma, ulica, lift, domofon, parkovka, ded_plosh, nomer_doma ) values ('{addres_doma}','{ulica}', '{lift}','{domofon}', '{parkovka}', '{ded_plosh}', '{nomer_doma}')";

        //        var command = new SqlCommand(addQuery, dataBase.getConnection());
        //        command.ExecuteNonQuery();

        //        MessageBox.Show("Запись создана!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    else
        //    {
        //        MessageBox.Show("Запись не создана!", "ошибка №1!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //    dataBase.closeConnection();
        //}
    }
    
}
