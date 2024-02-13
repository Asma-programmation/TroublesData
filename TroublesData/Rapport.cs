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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TroublesData
{
    public partial class Rapport : Form
    {
        public Rapport()
        {
            InitializeComponent();
            DisplayRap();
            GetDocId();
            GetPatId();
            GetDiagNum();
            Clear();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Artiste\Documents\TroubleDataDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayRap()
        {
            Con.Open();
            string Query = "Select * from RapportTable";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            RapportDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void Clear()
        {
            IDCb.SelectedIndex = 0;
            IPCb.SelectedIndex = 0;
            IDiCb.SelectedIndex = 0;
            TTb.Text = "";
            NomDTb.Text = "";
            NomPTb.Text = "";
            DiagTb.Text = "";
            

           // Key = 0;
        }
        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void GetDocName()
        {
            Con.Open();
            string Query = "Select * from DocteurTable where DocId=" + IDCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                NomDTb.Text = dr["Nom"].ToString();
            }
            Con.Close();
        }
        private void GetPatName()
        {
            Con.Open();
            string Query = "Select * from PatientTable where PatId=" + IPCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                NomPTb.Text = dr["PatNom"].ToString();
            }
            Con.Close();
        }
        private void GetDiag()
        {
            Con.Open();
            string Query = "Select * from DiagnostiqueTable where DiagNum=" + IDiCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                DiagTb.Text = dr["DiagNom"].ToString();
            }
            Con.Close();
        }
        private void GetDocId()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select DocId from DocteurTable", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("DocId", typeof(int));
            dt.Load(rdr);
            IDCb.ValueMember = "DocId";
            IDCb.DataSource = dt;
            Con.Close() ;

        }
        private void GetPatId()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select PatId from PatientTable", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("PatId", typeof(int));
            dt.Load(rdr);
            IPCb.ValueMember = "PatId";
            IPCb.DataSource = dt;
            Con.Close();

        }
        private void GetDiagNum()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select DiagNum from DiagnostiqueTable", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("DiagNum", typeof(int));
            dt.Load(rdr);
            IDiCb.ValueMember = "DiagNum";
            IDiCb.DataSource = dt;
            Con.Close();

        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void PrintBtn_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void IDCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetDocName();
        }

        private void IPCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetPatName();
        }

        private void IDiCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetDiag();
        }
        //int Key = 0;
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (NomPTb.Text == "" || NomDTb.Text == "" || DiagTb.Text == "")
            {
                MessageBox.Show("Informations manquantes");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into RapportTable(DocId, DocNom, PatId, PatNom, DiagNum, DiagNom, Traitements, Cout)values (@DI, @DN, @PI, @PN, @DINU,@DIN, @DIT, @DIC)", Con);
                    cmd.Parameters.AddWithValue("@DI", IDCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@DN", NomDTb.Text);
                    cmd.Parameters.AddWithValue("@PI", IPCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@PN", NomPTb.Text);
                    cmd.Parameters.AddWithValue("@DINU", IDiCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@DIN", DiagTb.Text);
                    cmd.Parameters.AddWithValue("@DIT", TTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ajouté avec succès");

                    Con.Close();
                    DisplayRap();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void RapportDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RapportTxt.Text = "";
            RapportTxt.Text = "                         Trouble Data 1.0 \n\n" + "                                  Rapport                " + "\n******************************************************" + "\n" + DateTime.Today.Date+"\n\n\n\n Docteur:    " + RapportDGV.SelectedRows[0].Cells[2].Value.ToString() + "               Patient:  " + RapportDGV.SelectedRows[0].Cells[4].Value.ToString() + "\n\n\n          Test:" + RapportDGV.SelectedRows[0].Cells[6].Value.ToString() + "               " + " \n   Traitements:   \n" + RapportDGV.SelectedRows[0].Cells[7].Value.ToString() + "\n\n\n\n\n\n\n\n\n\n\n\n                      salutation confraternelle amicale ";
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(RapportTxt.Text + "\n", new Font("Cambira", 18, FontStyle.Italic), Brushes.Black, new Point(95, 80));
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void CTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
