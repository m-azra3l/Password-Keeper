using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using Microsoft.VisualBasic;
using System.IO;

namespace Password_Keeper
{
    public partial class Main : Form
    {
        SqlCeConnection con = new SqlCeConnection(@"Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Database1.sdf"));
        SqlCeCommand cmd;
        SqlCeDataAdapter adapt;
        public Main()
        {
            InitializeComponent();
            DisplayData();
        }
        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlCeDataAdapter("select * from Services", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void ClearData()
        {
            comboBox1.Text = "Select Service";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Today.ToLongDateString();
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {

            Application.Exit();

        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Interaction.MsgBox("Are you sure you want to logout?", MsgBoxStyle.YesNo, "Password Keeper") == MsgBoxResult.Yes)
            {
                this.Hide();
                loginform fl = new loginform();
                fl.Show();
            }
        }

        private void viewMenu_Click(object sender, EventArgs e)
        {
            using (help hp = new help())
            {
                hp.ShowDialog();
            }
        }

        private void editLoginCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (logincredentials hp = new logincredentials())
            {
                hp.ShowDialog();
            }
        }

        private void helpMenu_Click(object sender, EventArgs e)
        {
            using (about hp = new about())
            {
                hp.ShowDialog();
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Services' table. You can move, or remove it, as needed.
            this.servicesTableAdapter.Fill(this.database1DataSet.Services);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                try
                {
                    SqlCeConnection con = new SqlCeConnection(@"Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Database1.sdf"));
                    con.Open();

                    String sql = "INSERT INTO Services(Service,Description,Username,Password,Security_Question,Security_Answer,Recovery_Email,Recovery_Number) VALUES('" + comboBox1.Text + "','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "')";
                    SqlCeCommand cmd = new SqlCeCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Service information has been saved successfully... ", "Password Keeper");
                    DisplayData();
                    ClearData();

                }
                catch (SqlCeException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Please provide details!", "Password Keeper");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           if (textBox1.Text != "")
            {
                cmd = new SqlCeCommand("update Services set Service=@service,Description=@description,Username=@username,Password=@password,Security_Question=@question,Security_Answer=@answer,Recovery_Email=@email,Recovery_Number=@number where Description=@description", con);
                con.Open();
                cmd.Parameters.AddWithValue("@service", comboBox1.Text);
                cmd.Parameters.AddWithValue("@description", textBox1.Text);
                cmd.Parameters.AddWithValue("@username", textBox2.Text);
                cmd.Parameters.AddWithValue("@password", textBox3.Text);
                cmd.Parameters.AddWithValue("@question", textBox4.Text);
                cmd.Parameters.AddWithValue("@answer", textBox5.Text);
                cmd.Parameters.AddWithValue("@email", textBox6.Text);
                cmd.Parameters.AddWithValue("@number", textBox7.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Infromation updated successfully...", "Password Keeper");
                con.Close();
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please select information from the list to fetch details for update or view..", "Password Keeper");
            }
        }

       

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                cmd = new SqlCeCommand("delete Services where Description=@description", con);
                con.Open();
                cmd.Parameters.AddWithValue("@description", textBox1.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Information has been deleted successfully...", "Password Keeper");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please select information from list to delete...", "Password Keeper");
            }
        }
    }
}
