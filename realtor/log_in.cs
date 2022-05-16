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
    public partial class Form1 : Form
    {

        DataBase database = new DataBase();


        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox_pass.PasswordChar = '*';
            textBox_log.MaxLength = 50;
            textBox_pass.MaxLength = 50;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var loginUser = textBox_log.Text;
            var passUser = textBox_pass.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select id_user, login_user, password_user from register where login_user = '{loginUser}' and password_user = '{passUser}'";

            SqlCommand command = new SqlCommand(querystring, database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if(table.Rows.Count == 1)
            {
                MessageBox.Show("Вы успешно вошли!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Menuska ms = new Menuska();
                this.Hide();
                ms.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Такого аккаунта не существует!", "аккаунта не существует!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sign_in sign_In = new sign_in();
            sign_In.Show();
            this.Hide();
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

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    textBox_log.Text = "";
        //    textBox_pass.Text = ""; 
        //}
    }
}
