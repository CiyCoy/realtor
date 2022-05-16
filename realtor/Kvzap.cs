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
    public partial class Kvzap : Form
    {
        DataBase dataBase = new DataBase();
        public Kvzap()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            dataBase.openConnection();

            int id_kv;
            int addres_doma;
            int id_vlad;
            int etaj;
            int nomer_kv;
            var balkon = textBox7.Text;
            var telefon = textBox8.Text;
            var remont = textBox11.Text;
            int plosjad;
            int stoimost;




            if (int.TryParse(textBox2.Text, out id_kv) == int.TryParse(textBox3.Text, out addres_doma) == int.TryParse(textBox4.Text, out id_vlad) == int.TryParse(textBox5.Text, out etaj) == int.TryParse(textBox6.Text, out nomer_kv) == int.TryParse(textBox10.Text, out plosjad) == int.TryParse(textBox9.Text, out stoimost))
            {

                var addQuery = $"insert into kvartira_db (id_kv, addres_doma, id_vlad, etaj, nomer_kv, balkon,telefon,remont, plosjad, stoimost ) values ('{id_kv}','{addres_doma}', '{id_vlad}','{etaj}', '{nomer_kv}', '{balkon}' , '{telefon}' , '{remont}' , '{plosjad}', '{stoimost}')";

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
