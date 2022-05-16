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
    public partial class Sdelka : Form
    {
        DataBase dataBase = new DataBase();

            int selectedRow;

            DataSet ds;
            SqlDataAdapter adapter;
            SqlCommandBuilder commandBuilder;
            string connectionString = @"Data Source=LAPTOP-P0O5FV57;Initial Catalog=realtor;Integrated Security=True";
            string sql = "SELECT * FROM sdelka_db";
        public Sdelka()
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
                //dataGridView1.Columns["kod_sdelki"].ReadOnly = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sdelkazap sdzap = new Sdelkazap();
            sdzap.Show();
        }

        private void Sdelka_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "realtorDataSet.sdelka_db". При необходимости она может быть перемещена или удалена.
            this.sdelka_dbTableAdapter.Fill(this.realtorDataSet.sdelka_db);

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
                textBox9.Text = row.Cells[4].Value.ToString();
                textBox8.Text = row.Cells[5].Value.ToString();
                textBox7.Text = row.Cells[6].Value.ToString();
                textBox6.Text = row.Cells[7].Value.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("sdelka_db", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@id_kv", SqlDbType.Int, 0, "id_kv"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@id_sotr", SqlDbType.Int, 0, "id_sotr"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@id_klient", SqlDbType.Int, 0, "id_klient"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@tip", SqlDbType.NVarChar, 100, "tip"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@summa", SqlDbType.Int, 0, "summa"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@data", SqlDbType.DateTime, 0, "data"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@status", SqlDbType.NVarChar, 100, "status"));


                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@kod_sdelki", SqlDbType.Int, 0, "id_vlad");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(ds);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }
    }
}
