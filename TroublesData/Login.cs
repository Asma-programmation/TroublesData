using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TroublesData
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            RoleCb.SelectedIndex = 0;
            UserTb.Text = "";
            PassTb.Text = "";
        }
     SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Artiste\Documents\TroubleDataDb.mdf;Integrated Security=True;Connect Timeout=30");
        public static string Role;

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if(RoleCb.SelectedIndex==-1)
            {
                MessageBox.Show("sélectionnez votre position");

            }else if (RoleCb.SelectedIndex== 0) 
            {
                if (UserTb.Text == "" || PassTb.Text == "") 
                {
                    MessageBox.Show("Entrer Non d'Admin et Password");

                }else if(UserTb.Text == "Admin" && PassTb.Text == "Password")
                {
                    Role = "Admin"; 
                    Patients Obj = new Patients();
                    Obj.Show();
                    this.Hide();
                }else
                {
                    MessageBox.Show("Données incorrect");
                }
            }
            else if (RoleCb.SelectedIndex== 1) 
            {
                if (UserTb.Text == "" || PassTb.Text == "")
                {
                    MessageBox.Show("Entrer Nom de Docteur et Password");

                }
                else 
                {
                    Con.Open();
                    SqlDataAdapter sda =new SqlDataAdapter("Select Count(*) from DocteurTable where Nom='" + UserTb.Text+"' and Pass='"+ PassTb.Text+"'",Con);
                    DataTable dt =new DataTable();  
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString()=="1")
                    {
                        Role = "Docteur";
                        Rapport Obj =new Rapport(); 
                        Obj.Show();
                        this.Hide();
                    }else
                    {
                        MessageBox.Show("Docteur introuvable");
                    }
                    Con.Close();
                }
               
            }
            else 
            {
                if (UserTb.Text == "" || PassTb.Text == "")
                {
                    MessageBox.Show("Entrer le nom de Receptionniste et Password");

                }
                else
                {
                    Con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from ReceptionnistesTable where RecepNom ='" + UserTb.Text + "' and RecepPass='" + PassTb.Text + "'", Con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        Role = "Receptionniste";
                        Accueil Obj = new Accueil();
                        Obj.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Receptionniste introuvable");
                    }
                    Con.Close();
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
