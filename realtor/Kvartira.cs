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

    enum RowState2
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }

    public partial class Kvartira : Form
    {
        
        DataBase dataBase = new DataBase();

            int selectedRow;

            DataSet ds;
            SqlDataAdapter adapter;
            SqlCommandBuilder commandBuilder;
            string connectionString = @"Data Source=LAPTOP-P0O5FV57;Initial Catalog=realtor;Integrated Security=True";
            string sql = "SELECT * FROM kvartira_db";

        public Kvartira()
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
            Kvzap kzap = new Kvzap();
            kzap.Show();
        }

        private void Kvartira_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "realtorDataSet.kvartira_db". При необходимости она может быть перемещена или удалена.
            this.kvartira_dbTableAdapter.Fill(this.realtorDataSet.kvartira_db);

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
                textBox8.Text = row.Cells[6].Value.ToString();
                textBox11.Text = row.Cells[7].Value.ToString();
                textBox10.Text = row.Cells[8].Value.ToString();
                textBox9.Text = row.Cells[9].Value.ToString();

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

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("kvartira_db", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@addres_doma", SqlDbType.Int, 0, "addres_doma"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@id_vlad", SqlDbType.Int, 0, "id_vlad"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@etaj", SqlDbType.Int, 100, "etaj"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@nomer_kv", SqlDbType.Int, 0, "nomer_kv"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@balkon", SqlDbType.Float, 50, "balkon"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@telefon", SqlDbType.Float, 50, "telefon"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@remont", SqlDbType.Float, 50, "remont"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@plosjad", SqlDbType.Int, 0, "plosjad"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@stoimost", SqlDbType.Int, 0, "stoimost"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@id_kv", SqlDbType.Int, 0, "id_vlad");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(ds);
            }
        }
    }
}
