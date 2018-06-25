using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AupsWF
{
    public partial class FormOperacijaRada : MetroForm
    {
        private Form1 form1;

        public FormOperacijaRada(Form1 form2)
        {
            InitializeComponent();
            form1 = form2;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Insert();
            form1.getData("Select ID, Name, Klijent, Datum from Product");
            this.Close();
        }

        private void Insert(/*string command*/)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("INSERT INTO Product(ID,Name,Klijent,Datum) VALUES(@ID, @Name, @Klijent, @Datum)", connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", txtID.Text);
                        cmd.Parameters.AddWithValue("@Name", txtIme.Text);
                        cmd.Parameters.AddWithValue("@Klijent", txtKlijent.Text);;
                        cmd.Parameters.AddWithValue("@Datum", txtDatum1.Text);
                        MessageBox.Show(txtDatum1.Text);
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(txtDatum1.Text);
            }
        }
    }
}
