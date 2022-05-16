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

    
    public partial class Dom : Form
    {
        DataBase dataBase = new DataBase();

        int selectedRow;

        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=LAPTOP-P0O5FV57;Initial Catalog=realtor;Integrated Security=True";
        string sql = "SELECT * FROM dom_db";
        public Dom()
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
                //dataGridView1.Columns["addres_doma"].ReadOnly = true;
            }
        }

        private void Dom_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "realtorDataSet.dom_db". При необходимости она может быть перемещена или удалена.
            this.dom_dbTableAdapter.Fill(this.realtorDataSet.dom_db);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "realtorDataSet.dom_db". При необходимости она может быть перемещена или удалена.
            //this.dom_dbTableAdapter.Fill(this.realtorDataSet.dom_db);



        }

        private void ReadSinglRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetInt32(2), record.GetInt32(0), record.GetInt32(1));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Domzap dzp = new Domzap();
            dzp.Show();
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

            }
        }

        private void RefreshDataGrid(DataGridView dgv)
        {
            //dgv.Rows.Clear();

            string queryString = $"select * from dom_db";

            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                dataGridView1.Created.ToString();
                
            }    
            reader.Close();


        }

        private void button7_Click(object sender, EventArgs e)
        {
            Refresh();
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
            // удаляем выделенные строки из dataGridView1
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
                adapter.InsertCommand = new SqlCommand("dom_db", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@ulica", SqlDbType.NVarChar, 50, "ulica"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@lift", SqlDbType.Float, 50, "lift"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@domofon", SqlDbType.Float, 50, "domofon"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@parkovka", SqlDbType.Float, 50, "parkovka"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@ded_plosh", SqlDbType.Float, 50, "ded_plosh"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@nomer_doma", SqlDbType.Int, 0, "nomer_doma"));

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@addres_doma", SqlDbType.Int, 0, "addres_doma");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(ds);
            }
        }
    }
}
