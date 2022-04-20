using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Atestat_Admitere
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;

        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\Admitere.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
            con.Open();
            afisare("SELECT * FROM Admitere");
        }

        void afisare(String s)
        {
            cmd = new SqlCommand(s, con);
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = "UPDATE Admitere SET media = (proba1+proba2-0.01)/2, rezultat = 'Respins'";
            cmd = new SqlCommand(s, con);
            cmd.ExecuteNonQuery();
            s = "UPDATE Admitere SET rezultat = 'Admis' WHERE id IN (SELECT TOP(20) id FROM admitere WHERE proba1>=5 AND proba2>=5 ORDER BY media DESC)";
            cmd = new SqlCommand(s, con);
            cmd.ExecuteNonQuery();
            afisare("SELECT * FROM admitere ORDER BY media DESC");
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = "SELECT nume,prenume,rezultat,media FROM admitere WHERE sex='M' ORDER BY media DESC";
            afisare(s);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string s = "SELECT nume,prenume,rezultat,media FROM admitere WHERE sex='F' ORDER BY media DESC";
            afisare(s);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string s = "SELECT TOP(5) nume,prenume,media,datan,oras FROM admitere WHERE rezultat='Admis' ORDER BY media DESC";
            afisare(s);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string s = "SELECT TOP(5) nume,prenume,media,datan,oras FROM admitere WHERE rezultat='Admis' ORDER BY media ASC";
            afisare(s);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string s = "SELECT nume,prenume,oras,datan,media FROM admitere WHERE GETDATE()>=DATEADD(year,18,datan) AND GETDATE()<DATEADD(year,21,datan) ORDER BY datan,nume,prenume";
            afisare(s);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string s = "SELECT nume,prenume,proba1,rezultat FROM admitere ORDER BY proba1 DESC";
            afisare(s);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string s = "SELECT nume,prenume,proba2,rezultat FROM admitere ORDER BY proba2 DESC";
            afisare(s);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string s = "SELECT nume,prenume,media,rezultat FROM admitere ORDER BY nume,prenume";
            afisare(s);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string s = "SELECT COUNT(*) FROM admitere";
            cmd = new SqlCommand(s, con);
            int nrt = Convert.ToInt32(cmd.ExecuteScalar());
            s = "SELECT COUNT(*) FROM admitere WHERE media BETWEEN 1 AND 5";
            cmd = new SqlCommand(s, con);
            int nrc = Convert.ToInt32(cmd.ExecuteScalar());
            label1.Text ="1.00-5:  " + nrc * 100 / nrt + "%";
            s = "SELECT COUNT(*) FROM admitere WHERE media BETWEEN 5.01 AND 7";
            cmd = new SqlCommand(s, con);
            nrc = Convert.ToInt32(cmd.ExecuteScalar());
            label2.Text = "5.01-7:  " + nrc * 100 / nrt + "%";
            s = "SELECT COUNT(*) FROM admitere WHERE media BETWEEN 7.01 AND 9";
            cmd = new SqlCommand(s, con);
            nrc = Convert.ToInt32(cmd.ExecuteScalar());
            label3.Text = "7.01-9:  " + nrc * 100 / nrt + "%";
            s = "SELECT COUNT(*) FROM admitere WHERE media >9";
            cmd = new SqlCommand(s, con);
            nrc = Convert.ToInt32(cmd.ExecuteScalar());
            label4.Text = "9.01-10: " + nrc * 100 / nrt + "%";
        }
    }
}
