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
    public partial class Diagnostiques : Form
    {
        public Diagnostiques()
        {
            InitializeComponent();
            DisplayDiag();

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Artiste\Documents\TroubleDataDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayDiag()
        {
            Con.Open();
            string Query = "Select * from DiagnostiqueTable";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DiagnostiqueDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        int Key = 0;
        private void Clear()
        {
            DiagTb.Text = "";
            CTb.Text = "";
       
            Key = 0;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (DiagTb.Text == "" || CTb.Text == "" )
            {
                MessageBox.Show("Informations manquantes");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into DiagnostiqueTable(DiagNom, DiagCout)values (@DIN,@DIC)", Con);
                    cmd.Parameters.AddWithValue("@DIN", DiagTb.Text);
                    cmd.Parameters.AddWithValue("@DIC", CTb.Text);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ajouté avec succès");

                    Con.Close();
                    DisplayDiag();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DiagnostiqueDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DiagTb.Text = DiagnostiqueDGV.SelectedRows[0].Cells[1].Value.ToString();
            CTb.Text = DiagnostiqueDGV.SelectedRows[0].Cells[2].Value.ToString();
            
            if (DiagTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(DiagTb.Text = DiagnostiqueDGV.SelectedRows[0].Cells[0].Value.ToString());

            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (DiagTb.Text == "" || CTb.Text == "")
            {
                MessageBox.Show("Informations manquantes");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update DiagnostiqueTable set DiagNom=@DIN, DiagCout=@DIC where DiagNum=@DIKey", Con);
                    cmd.Parameters.AddWithValue("@DIN", DiagTb.Text);
                    cmd.Parameters.AddWithValue("@DIC", CTb.Text);
                    cmd.Parameters.AddWithValue("@DIKey",Key);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Modifié avec succès");

                    Con.Close();
                    DisplayDiag();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SuppBtn_Click(object sender, EventArgs e)
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
                    SqlCommand cmd = new SqlCommand("Delete from DiagnostiqueTable where DiagNum=@DIKey", Con);
                    cmd.Parameters.AddWithValue("@DIKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Supprimé avec succès");

                    Con.Close();
                    DisplayDiag();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void Accueil_Click(object sender, EventArgs e)
        {
            Accueil Obj = new Accueil();
            Obj.Show();
            this.Hide();
        }

        private void CTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
