using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TroublesData
{
    public partial class Accueil : Form
    {
        public Accueil()
        {
            InitializeComponent();
            if(Login.Role=="Recéptionnistes")
            {
                RecepLab.Enabled = false;
                DocLab.Enabled=false;
                DiagLab.Enabled=false;
            }
            CountPatients();
            CountDocteurs();
            CountDiag();



        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Artiste\Documents\TroubleDataDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void CountPatients()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from PatientTable",Con) ;
            DataTable dt = new DataTable();
            sda.Fill(dt);
            PatNLab.Text = dt.Rows[0][0].ToString();


            Con.Close();    
        }
        private void CountDocteurs()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from DocteurTable", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DocNLab.Text = dt.Rows[0][0].ToString();


            Con.Close();
        }
        private void CountDiag()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from DiagnostiqueTable", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DiagNLab.Text = dt.Rows[0][0].ToString();


            Con.Close();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void Accueil_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void PatLab_Click(object sender, EventArgs e)
        {
            Patients Obj = new Patients();
            Obj.Show();
            this.Hide();
        }

        private void DocLab_Click(object sender, EventArgs e)
        {
            Docteurs Obj = new Docteurs();
            Obj.Show();
            this.Hide();
        }

        private void DiagLab_Click(object sender, EventArgs e)
        {
            Diagnostiques Obj = new Diagnostiques();
            Obj.Show();
            this.Hide();
        }

        private void RecepLab_Click(object sender, EventArgs e)
        {
            Réceptionnistes Obj = new Réceptionnistes();
            Obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void PatNLab_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }
    }
}
