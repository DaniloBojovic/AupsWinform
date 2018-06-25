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
    public partial class FormOperacijaRadaShowCell : MetroForm
    {
        private Form1 form1;

        public FormOperacijaRadaShowCell(Form1 form2)
        {
            InitializeComponent();
            form1 = form2;
        }

        //Popunjava polja na klik jednog reda u glavnoj formi
        private void FormOperacijaRadaShowCell_Load(object sender, EventArgs e)
        {
            //lblOperativniPlan.Text = Form1.cellStr; 
            txtID.Text = Form1.c1;
            txtIme.Text = Form1.c2;
            txtKlijent.Text = Form1.c3;
            txtDatum.Value = Form1.c4;
            txtRok.Value = Form1.c4;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateData();
            form1.getData("Select ID, Name, Klijent, Datum from Product");
            this.Close();
        }

        private void UpdateData(/*string command*/)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("UPDATE Product SET Name = @Name, Klijent = @Klijent, Datum = @Datum " +
                            "WHERE ID = @ID", connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", txtID.Text);
                        cmd.Parameters.AddWithValue("@Name", txtIme.Text);
                        cmd.Parameters.AddWithValue("@Klijent", txtKlijent.Text);
                        cmd.Parameters.AddWithValue("@Datum", Convert.ToDateTime(txtDatum.Text));
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
