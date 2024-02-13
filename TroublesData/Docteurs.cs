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
    public partial class Docteurs : Form
    {
        public Docteurs()
        {
            InitializeComponent();
            DisplayDoc();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Artiste\Documents\TroubleDataDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayDoc()
        {
            Con.Open();
            string Query = "Select * from DocteurTable";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DocteursDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void Clear()
        {
            DNameTb.Text = "";
            DocPhoneTb.Text = "";
            DocAdrTb.Text = "";
            DocExpTb.Text = "";
            DocGenCb.SelectedIndex = 0;
            DocSpecCb.SelectedIndex = 0;
            Key = 0;
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (DNameTb.Text == "" || DocPassTb.Text == "" || DocPhoneTb.Text == "" || DocAdrTb.Text == "" || DocGenCb.SelectedIndex == -1 || DocSpecCb.SelectedIndex == -1)
            {
                MessageBox.Show("Informations manquantes");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into DocteurTable(Nom, DN, Genre, Spec, Exp, Phone, Adr, Pass)values (@DN, @DDN, @DG, @DS, @DE, @DPh, @DA, @DPass)", Con);
                    cmd.Parameters.AddWithValue("@DN", DNameTb.Text);
                    cmd.Parameters.AddWithValue("@DDN", DNBd.Value.Date);
                    cmd.Parameters.AddWithValue("@DG", DocGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DS", DocSpecCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DPh", DocPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@DA", DocAdrTb.Text);
                    cmd.Parameters.AddWithValue("@DE", DocExpTb.Text);
                    cmd.Parameters.AddWithValue("@DPass", DocPassTb.Text);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ajouté avec succès");

                    Con.Close();
                    DisplayDoc();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        int Key = 0;
        private void DocteursDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DNameTb.Text = DocteursDGV.SelectedRows[0].Cells[1].Value.ToString();
            DNBd.Text = DocteursDGV.SelectedRows[0].Cells[2].Value.ToString();
            DocGenCb.SelectedItem = DocteursDGV.SelectedRows[0].Cells[3].Value.ToString();
            DocSpecCb.SelectedItem = DocteursDGV.SelectedRows[0].Cells[4].Value.ToString();
            DocExpTb.Text = DocteursDGV.SelectedRows[0].Cells[5].Value.ToString();
            DocPhoneTb.Text = DocteursDGV.SelectedRows[0].Cells[6].Value.ToString();
            DocAdrTb.Text = DocteursDGV.SelectedRows[0].Cells[7].Value.ToString();
            DocPassTb.Text = DocteursDGV.SelectedRows[0].Cells[8].Value.ToString();


            if (DNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(DNameTb.Text = DocteursDGV.SelectedRows[0].Cells[0].Value.ToString());

            }
        }

        private void Docteurs_Load(object sender, EventArgs e)
        {

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (DNameTb.Text == "" || DocPassTb.Text == "" || DocPhoneTb.Text == "" || DocAdrTb.Text == "" || DocGenCb.SelectedIndex == -1 || DocSpecCb.SelectedIndex == -1)
            {
                MessageBox.Show("Informations manquantes");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update DocteurTable set Nom=@DN, DN=@DDN, Genre=@DG, Spec=@DS, Exp=@DE, Phone=@DPh, Adr=@DA, Pass=@DPass where DocId=@DKey", Con);
                    cmd.Parameters.AddWithValue("@DN", DNameTb.Text);
                    cmd.Parameters.AddWithValue("@DDN", DNBd.Value.Date);
                    cmd.Parameters.AddWithValue("@DG", DocGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DS", DocSpecCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DPh", DocPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@DA", DocAdrTb.Text);
                    cmd.Parameters.AddWithValue("@DE", DocExpTb.Text);
                    cmd.Parameters.AddWithValue("@DPass", DocPassTb.Text);
                    cmd.Parameters.AddWithValue("@DKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Modifié avec succès");

                    Con.Close();
                    DisplayDoc();
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
                    SqlCommand cmd = new SqlCommand("Delete from DocteurTable where DocId=@DKey", Con);
                    cmd.Parameters.AddWithValue("@DKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Supprimé avec succès");

                    Con.Close();
                    DisplayDoc();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            Diagnostiques  Obj = new Diagnostiques();
            Obj.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Réceptionnistes Obj = new Réceptionnistes();
            Obj.Show();
            this.Hide();
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
