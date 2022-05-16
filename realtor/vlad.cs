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
    
    enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }

    public partial class vlad : Form
    {

        DataBase dataBase = new DataBase();

        int selectedRow;

        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=LAPTOP-P0O5FV57;Initial Catalog=realtor;Integrated Security=True";
        string sql = "SELECT * FROM vladelec_db";


        public vlad()
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



        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void vlad_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "realtorDataSet.vladelec_db". При необходимости она может быть перемещена или удалена.
            this.vladelec_dbTableAdapter.Fill(this.realtorDataSet.vladelec_db);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "realtorDataSet.vladelec_db". При необходимости она может быть перемещена или удалена.
            //this.vladelec_dbTableAdapter.Fill(this.realtorDataSet.vladelec_db);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "realtorDataSet.vladelec_db". При необходимости она может быть перемещена или удалена.
            //this.vladelec_dbTableAdapter.Fill(this.realtorDataSet.vladelec_db);

        }
        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetInt32(2), record.GetString(3), record.GetInt32(4), record.GetDateTime(5));

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

        //private void pictureBox2_Click(object sender, EventArgs e)
        //{
        //    Refresh();
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            vladzap zap = new vladzap();
            zap.Show();
         
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
        }

        //private void textBox1_TextChanged(object sender, EventArgs e)
        //{
        //    textBox1_TextChanged(sender, e, values);
        //}

        private void Search(DataGridView dgv)
        {
            string searchString = $"select * from vladelec_db where concat (id_vlad, fio, tel, addres_prop, nomer_pas, data_pojd) like '%" + textBox1.Text + "%'";

            SqlCommand com = new SqlCommand(searchString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader read = com.ExecuteReader();

            while (read.Read())
            {
                ReadSingleRow(dgv, read);
            }


            read.Close();
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

        private void deleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = true;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[6].Value = RowState.Deleted;
                return;
            }

        }

        private void Updatee()
        {
            dataBase.openConnection();

            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var RowState = (RowState)dataGridView1.Rows[index].Cells[6].Value;

                if (RowState == RowState.Existed)
                    continue;

                if (RowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    string deleteQuery = $"delete from vladelec_db where id = {id}";

                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }
            }

            dataBase.closeConnection();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //deleteRow();
            // удаляем выделенные строки из dataGridView1
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
            //Updatee();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("vladelec_db", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@fio", SqlDbType.NVarChar, 100, "fio"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@tel", SqlDbType.Int, 0, "tel"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@addres_prop", SqlDbType.NVarChar, 100, "addres_prop"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@nomer_pas", SqlDbType.Int, 0, "nomer_pas"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@data_posj", SqlDbType.DateTime, 0, "data_posj"));
                

                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@id_vlad", SqlDbType.Int, 0, "id_vlad");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(ds);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
           Menuska mn = new Menuska();
            this.Hide();
            mn.ShowDialog();
            this.Show();
        }

        
    }
}
