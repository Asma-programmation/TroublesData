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
    public partial class Patients : Form
    {
        public Patients()
        {
            InitializeComponent();
            DisplayPat();
            Clear();


        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Artiste\Documents\TroubleDataDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayPat()
        {
            Con.Open();
            string Query = "Select * from PatientTable";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PatientsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        int Key = 0;
        private void Clear()
        {
            PNameTb.Text = "";
            PPhoneTb.Text = "";
            PAdr.Text = "";
            PGenCb.SelectedIndex = 0;
            PObs.Text = "";
            PTCb.SelectedIndex = 0;
            Key = 0;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void PAddBtn_Click(object sender, EventArgs e)
        {
            if (PNameTb.Text == "" || PGenCb.Text == "" || PPhoneTb.Text == "" || PAdr.Text == "" || PGenCb.SelectedIndex == -1 || PTCb.SelectedIndex == -1)
            {
                MessageBox.Show("Informations manquantes");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into PatientTable(PatNom, DN, Genre, Phone, Adresse, Trouble, Observation)values (@PN, @PDN, @PG, @PPh, @PA, @PT,@POB)", Con);
                    cmd.Parameters.AddWithValue("@PN", PNameTb.Text);
                    cmd.Parameters.AddWithValue("@PDN", PNBd.Value.Date);
                    cmd.Parameters.AddWithValue("@PG", PGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PT", PTCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PPh", PPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@PA", PAdr.Text);
                    cmd.Parameters.AddWithValue("@POB", PObs.Text);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ajouté avec succès");

                    Con.Close();
                    DisplayPat();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void PObs_TextChanged(object sender, EventArgs e)
        {

        }

        private void PAdr_TextChanged(object sender, EventArgs e)
        {

        }

        private void PNameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void PGenCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PEditBtn_Click(object sender, EventArgs e)
        {
            if (PNameTb.Text == "" || PGenCb.Text == "" || PPhoneTb.Text == "" || PAdr.Text == "" || PGenCb.SelectedIndex == -1 || PTCb.SelectedIndex == -1)
            {
                MessageBox.Show("Informations manquantes");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update PatientTable set PatNom=@PN, DN=@PDN, Genre=@PG, Phone=@PPh, Adresse=@PA, Trouble=@PT, Observation=@POB where PatId=@PKey", Con);
                    cmd.Parameters.AddWithValue("@PN", PNameTb.Text);
                    cmd.Parameters.AddWithValue("@PDN", PNBd.Value.Date);
                    cmd.Parameters.AddWithValue("@PG", PGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PT", PTCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PPh", PPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@PA", PAdr.Text);
                    cmd.Parameters.AddWithValue("@POB", PObs.Text);
                    cmd.Parameters.AddWithValue("@PKey", Key);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Modifié avec succès");

                    Con.Close();
                    DisplayPat();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void PatientsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PNameTb.Text = PatientsDGV.SelectedRows[0].Cells[1].Value.ToString();
            PGenCb.SelectedItem = PatientsDGV.SelectedRows[0].Cells[2].Value.ToString();
            PNBd.Text = PatientsDGV.SelectedRows[0].Cells[3].Value.ToString();
            PTCb.SelectedItem = PatientsDGV.SelectedRows[0].Cells[6].Value.ToString();
            PAdr.Text = PatientsDGV.SelectedRows[0].Cells[4].Value.ToString();
            PPhoneTb.Text = PatientsDGV.SelectedRows[0].Cells[5].Value.ToString();
            PObs.Text = PatientsDGV.SelectedRows[0].Cells[7].Value.ToString();


            if (PNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(PNameTb.Text = PatientsDGV.SelectedRows[0].Cells[0].Value.ToString());

            }
        }

        private void PSuppBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Veuillez sélectionner un élément ");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from PatientTable where PatId=@PKey", Con);
                    cmd.Parameters.AddWithValue("@PKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Supprimé avec succès");

                    Con.Close();
                    DisplayPat();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Accueil Obj = new Accueil();
            Obj.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void PTCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PNBd_onValueChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
 }
