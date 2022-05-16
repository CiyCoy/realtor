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
    public partial class Klient : Form
    {

        DataBase dataBase = new DataBase();

        int selectedRow;

        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=LAPTOP-P0O5FV57;Initial Catalog=realtor;Integrated Security=True";
        string sql = "SELECT * FROM klient_db";

        public Klient()
        {
            InitializeComponent();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);

                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                // делаем недоступным столбец id для изменения
                //dataGridView1.Columns["Id"].ReadOnly = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Klientzp zp = new Klientzp();
            this.Show();
        }

        private void Klient_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "realtorDataSet.klient_db". При необходимости она может быть перемещена или удалена.
            this.klient_dbTableAdapter.Fill(this.realtorDataSet.klient_db);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];

                textBox2.Text = row.Cells[0].Value.ToString();
                textBox3.Text = row.Cells[1].Value.ToString();
                textBox4.Text = row.Cells[2].Value.ToString();
                textBox5.Text = row.Cells[3].Value.ToString();
                textBox6.Text = row.Cells[4].Value.ToString();
                textBox7.Text = row.Cells[5].Value.ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }



            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("klient_db", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@fio", SqlDbType.NVarChar, 100, "fio"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@tel", SqlDbType.Int, 0, "tel"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@addres_prop", SqlDbType.NVarChar, 100, "addres_prop"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@nomer_pas", SqlDbType.Int, 0, "nomer_pas"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@data_posj", SqlDbType.DateTime, 0, "data_posj"));


                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@id_klient", SqlDbType.Int, 0, "id_vlad");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(ds);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Klientzp zp = new Klientzp();
            zp.Show();
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            Klientzp zp = new Klientzp();
            this.Show();
        }
    }
}
