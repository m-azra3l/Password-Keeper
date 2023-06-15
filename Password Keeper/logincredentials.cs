using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.IO;

namespace Password_Keeper
{
    public partial class logincredentials : Form
    {
        public logincredentials()
        {
            InitializeComponent();
        }
        private void ClearData()
        {
            pass.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                pass.UseSystemPasswordChar = false;
                textBox1.UseSystemPasswordChar = false;
                textBox4.UseSystemPasswordChar = false;
            }
            else
            {
                pass.UseSystemPasswordChar = true;
                textBox1.UseSystemPasswordChar = true;
                textBox4.UseSystemPasswordChar = true;
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void login_Click(object sender, EventArgs e)
        {
            SqlCeConnection con = new SqlCeConnection();
            con.ConnectionString = "Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Database1.sdf");
            con.Open();
            string password = textBox1.Text;
            SqlCeCommand cmd = new SqlCeCommand("select password from admin where password='" + pass.Text + "'", con);
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0 & textBox2.Text != "")
            {
                try
                {
                    SqlCeConnection con1 = new SqlCeConnection(@"Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Database1.sdf"));
                    con1.Open();


                    String sql = "Update admin set Password='" + this.textBox1.Text + "' ,Hint='" + this.textBox2.Text + "' ,Security_Question='" + this.textBox3.Text + "' ,Security_Answer='" + this.textBox4.Text + "'";

                    SqlCeCommand cmd1 = new SqlCeCommand(sql, con1);
                    cmd1.ExecuteNonQuery();

                    con1.Close();
                    MessageBox.Show("Your information has been updated successfully..","Password Keeper");


                    this.Close();
                }
                catch (SqlCeException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Invalid or incomplete information! Please enter the correct and complete information", "Password Keeper");
                ClearData();
            }
            con.Close();
        }

      
    }
}
