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
    public partial class sign_in : Form
    {

        DataBase dataBase = new DataBase();
        public sign_in()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void sign_in_Load(object sender, EventArgs e)
        {
            textBox_pass.PasswordChar = '*';
            pictureBox3.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataBase dataBase = new DataBase();

            var login = textBox_log.Text;
            var pass = textBox_pass.Text;

            string querystring = $"insert into register(login_user, password_user) values ('{login}', '{pass}')";


            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

            dataBase.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успешно создан!", "Успех!");
                Form1 frm = new Form1();
                this.Hide();
                frm.ShowDialog();

            }
            else
            {
                MessageBox.Show("Аккаунт не создан!");
            }
            
            dataBase.closeConnection();
        }

        private Boolean checkuser()
        {
            var loginUser = textBox_log.Text;
            var passwordUser = textBox_pass.Text;

            SqlDataAdapter adapter = new SqlDataAdapter(); 
            DataTable table = new DataTable();
            string querystring = $"select id_user, login_user, password_user from register where = '{loginUser}' and '{passwordUser}'";

            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if(table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь уже существует!");
                return true;
            }
            else
            {
                return false;
            }

           
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBox_log.Text = "";
            textBox_pass.Text = "";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBox_pass.UseSystemPasswordChar = false;
            pictureBox1.Visible = false;
            pictureBox2.Visible = true;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox_pass.UseSystemPasswordChar = true;
            pictureBox1.Visible = true;
            pictureBox2.Visible = false;
        }

    }
}
