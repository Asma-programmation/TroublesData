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

namespace TroublesData
{
    public partial class Réceptionnistes : Form
    {
        public Réceptionnistes()
        {
            InitializeComponent();
            DisplayRec();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Artiste\Documents\TroubleDataDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Supp_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Veuillez sélectionner un élément ");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from ReceptionnistesTable where RecepId=@RKey" ,Con) ;
                    cmd.Parameters.AddWithValue("@RKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Supprimé avec succès");

                    Con.Close();
                    DisplayRec();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void DisplayRec()
        {
            Con.Open();
            string Query = "Select * from ReceptionnistesTable"; 
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet(); 
            sda.Fill(ds);
            RecepDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Add_Click(object sender, EventArgs e)
        {
            if (RNameTb.Text == "" || RPassTb.Text == "" || RPhoneTb.Text == "" || RAdrTb.Text =="")
            {
                MessageBox.Show("Informations manquantes");
            }else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ReceptionnistesTable(RecepNom,RecepPhone,RecepAdr,RecepPass)values(@RN,@RP,@RA,@RPA)", Con);
                    cmd.Parameters.AddWithValue("@RN", RNameTb.Text);
                    cmd.Parameters.AddWithValue("@RP", RPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@RA", RAdrTb.Text);
                    cmd.Parameters.AddWithValue("@RPA",RPassTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ajouté avec succès");
       
                    Con.Close();
                    DisplayRec();
                    Clear();
                }catch (Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        int key =0;
        private void RecepDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RNameTb.Text = RecepDGV.SelectedRows[0].Cells[1].Value.ToString();
            RPhoneTb.Text = RecepDGV.SelectedRows[0].Cells[2].Value.ToString();
            RAdrTb.Text = RecepDGV.SelectedRows[0].Cells[3].Value.ToString();
            RPassTb.Text = RecepDGV.SelectedRows[0].Cells[4].Value.ToString();
            if(RNameTb.Text == "")
            {
                key = 0;
            }else
            {
                key = Convert.ToInt32(RNameTb.Text = RecepDGV.SelectedRows[0].Cells[0].Value.ToString());

            }

        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (RNameTb.Text == "" || RPassTb.Text == "" || RPhoneTb.Text == "" || RAdrTb.Text == "")
            {
                MessageBox.Show("Informations manquantes");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update ReceptionnistesTable set RecepNom=@RN, RecepPhone=@RP, RecepAdr=@RA, RecepPass=@RPA where RecepId=@RKey", Con);
                    cmd.Parameters.AddWithValue("@RN", RNameTb.Text);
                    cmd.Parameters.AddWithValue("@RP", RPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@RA", RAdrTb.Text);
                    cmd.Parameters.AddWithValue("@RPA", RPassTb.Text);
                    cmd.Parameters.AddWithValue("@RKey", key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Modifié avec succès");

                    Con.Close();
                    DisplayRec();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void Clear()
        {
            RNameTb.Text  = "";
            RPassTb.Text  = "";
            RPhoneTb.Text = "";
            RAdrTb.Text = "";
            key = 0;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Réceptionnistes_Load(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void RPhoneTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Accueil_Click(object sender, EventArgs e)
        {
            Accueil Obj = new Accueil();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Accueil Obj = new Accueil();
            Obj.Show();
            this.Hide();
        }
    }
}
