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
using Microsoft.VisualBasic;

namespace Password_Keeper
{
    public partial class loginform : Form
    {
        //SqlCeConnection con = new SqlCeConnection();
        public loginform()
        {
            InitializeComponent();
        }
        private void loginform_Load(object sender, EventArgs e)
        {
            
            string str;
            SqlCeCommand com;
            SqlCeConnection con = new SqlCeConnection(@"Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Database1.sdf"));
            con.Open();
            str = "select * from admin";
            com = new SqlCeCommand(str, con);
            SqlCeDataReader reader = com.ExecuteReader();
            reader.Read();
            label5.Text = reader["Hint"].ToString();
            reader.Close();
            con.Close();
            con.Open();
            SqlCeDataReader reader1 = com.ExecuteReader();
            reader1.Read();
            label6.Text = reader1["Security_Question"].ToString();
            reader1.Close();
            con.Close();
        }
        string cs = "Data Source=" + System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Database1.sdf");

        private void login_Click(object sender, EventArgs e)
        {
            if (pass.Text == "" || textBox1.Text == "")
            {
                MessageBox.Show("Please provide Password and Security Answer", "Password Keeper");
                return;
            }

            try
            {
                //Create SqlConnection
                SqlCeConnection con = new SqlCeConnection(cs);
                SqlCeCommand cmd = new SqlCeCommand("Select * from admin where Password=@password and Security_Answer=@answer", con);
                cmd.Parameters.AddWithValue("@password", pass.Text);
                cmd.Parameters.AddWithValue("@answer", textBox1.Text);
                con.Open();
                SqlCeDataAdapter adapt = new SqlCeDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                con.Close();
                int count = ds.Tables[0].Rows.Count;
                if (count == 1)
                {
                    MessageBox.Show("Login Successful!", "Password Keeper");
                    this.Hide();
                    Main fm = new Main();
                    fm.Show();
                }
                else
                {
                    MessageBox.Show("Login Failed!", "Password Keeper");
                    ClearData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearData()
        {
            pass.Text = "";
            textBox1.Text = "";
            

        }
        private void cancel_Click(object sender, EventArgs e)
        {
            if (Interaction.MsgBox("Are you sure you want to exit?", MsgBoxStyle.YesNo, "Password Keeper") == MsgBoxResult.Yes)
            {
                Application.Exit();
            }
        }

 
        private void loginform_Close(object sender, EventArgs e)
        {
            
                Application.Exit();
            
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                pass.UseSystemPasswordChar = false;
                textBox1.UseSystemPasswordChar = false;
            }
            else 
            {
                pass.UseSystemPasswordChar = true;
                textBox1.UseSystemPasswordChar = true;
            }
        }

        
        
        }
    }

