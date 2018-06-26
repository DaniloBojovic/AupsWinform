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
using MetroFramework.Forms;

namespace AupsWF
{
    public partial class Form1 : MetroForm
    {
        private DataGridView dataGridView1 = new DataGridView();
        private BindingSource bindingSource1 = new BindingSource();
        private SqlDataAdapter dataAdapter;
        private DataTable dt;
        public static string c1, c2, c3;
        public static DateTime c4;
        public static string cellStr;

        public Form1()
        {
            InitializeComponent();
            Load += new EventHandler(Form1_Load);
            pnlHome.Location = pnlWelcome.Location;
            pnlKontakt.Location = pnlWelcome.Location;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hidePanels();
            pnlWelcome.Visible = true;
            metroTabControl1.SelectedTab = metroTabPage2;
            btnDodaj.Visible = false;
        }

        private void btnKontakt_Click(object sender, EventArgs e)
        {
            hidePanels();
            pnlKontakt.Visible = true;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            hidePanels();
            pnlWelcome.Visible = true;
        }

        private void btnProces_Click_1(object sender, EventArgs e)
        {
            hidePanels();
            pnlHome.Visible = true;
            metroTabControl1.SelectedTab = metroTabPage1;
        }

        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (metroTabControl1.SelectedIndex)
            {
                case 0:
                    gridOperacijaRada.ColumnHeadersVisible = Visible;
                    gridOperacijaRada.Visible = true;
                    gridOperacijaRada.DataSource = bindingSource1;
                    getData("Select ID, Name, Klijent, Datum from Product");
                    btnDodaj.Visible = true;
                    //metroTileDodaj.Visible = true;
                    //metroTileSacuvaj.Visible = true;
                    //metroTileOsvezi.Visible = true;
                    //btnDodaj.Enabled = true;
                    //groupBoxOperacijaRada.Visible = true;
                    break;
                case 1:
                    break;
            }
            
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            FormOperacijaRada formOperacijaRada = new FormOperacijaRada(this);
            formOperacijaRada.ShowDialog();
        }

        private void gridOperacijaRada_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = gridOperacijaRada.CurrentRow;
            if (!row.IsNewRow)
            {
                try
                {
                    c1 = row.Cells["ID"].Value.ToString();
                    c2 = row.Cells["Name"].Value.ToString();
                    c3 = row.Cells["Klijent"].Value.ToString();
                    c4 = Convert.ToDateTime(row.Cells["Datum"].Value);
                    cellStr = ($"{c1}, {c2}");
                    //MessageBox.Show($"{c1}, {c2}, {c3}");
                    //MessageBox.Show(cellStr);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                FormOperacijaRadaShowCell fOperacijaRada = new FormOperacijaRadaShowCell(this);
                fOperacijaRada.ShowDialog();
            }
        }

        private void tileProizvodniPlan_Click(object sender, EventArgs e)
        {
            FormOperativniPlan fOP = new FormOperativniPlan();
            fOP.ShowDialog();
        }

        public void getData(string command)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
                //String connectionString = "Data Source=DANILO;Initial Catalog=Test;" +
                //                            "Integrated Security = True";

                dataAdapter = new SqlDataAdapter(command, connectionString);

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                dt = new DataTable();
                dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(dt);
                gridOperacijaRada.DataSource = dt;

                for (int i = 0; i < gridOperacijaRada.Columns.Count - 1; i++)
                {
                    gridOperacijaRada.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                gridOperacijaRada.Columns[gridOperacijaRada.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    int colw = gridOperacijaRada.Columns[i].Width;
                    gridOperacijaRada.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    gridOperacijaRada.Columns[i].Width = colw;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void hidePanels()
        {
            pnlWelcome.Visible = false;
            pnlHome.Visible = false;
            pnlKontakt.Visible = false;
        }
    }
}
