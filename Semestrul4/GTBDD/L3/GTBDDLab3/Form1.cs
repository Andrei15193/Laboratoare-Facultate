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

namespace GTBDDLab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            isolationLevelComboBox.DataSource = niveleDeIzolare.ToArray();
            adapter.SelectCommand = new SqlCommand("select Rezervari.* from Rezervari", conexiune);
            adapter.InsertCommand = new SqlCommand("insert into Rezervari(cursa, nr_locuri, nr_locuri_libere) values(@cursa, @nr_locuri, @nr_locuri_libere)", conexiune);
            adapter.InsertCommand.Parameters.Add(
                new SqlParameter("@cursa", SqlDbType.VarChar, 200)
                {
                    SourceColumn = "cursa",
                    SourceVersion = DataRowVersion.Current
                }
            );
            adapter.InsertCommand.Parameters.Add(
                 new SqlParameter("@nr_locuri", SqlDbType.Int)
                 {
                     SourceColumn = "nr_locuri",
                     SourceVersion = DataRowVersion.Current
                 }
            );
            adapter.InsertCommand.Parameters.Add(
                 new SqlParameter("@nr_locuri_libere", SqlDbType.Int)
                 {
                     SourceColumn = "nr_locuri_libere",
                     SourceVersion = DataRowVersion.Current
                 }
            );
            adapter.DeleteCommand = new SqlCommand("delete from Rezervari where cursa = @cursa", conexiune);
            adapter.DeleteCommand.Parameters.Add(
                 new SqlParameter("@cursa", SqlDbType.VarChar, 200)
                 {
                     SourceColumn = "cursa",
                     SourceVersion = DataRowVersion.Original
                 }
            );
            adapter.UpdateCommand = new SqlCommand("update Rezervari set nr_locuri_libere = @nr_locuri_libere where cursa = @cursa", conexiune);
            adapter.UpdateCommand.Parameters.Add(
                 new SqlParameter("@nr_locuri_libere", SqlDbType.Int)
                 {
                     SourceColumn = "nr_locuri_libere",
                     SourceVersion = DataRowVersion.Current
                 }
            );
            adapter.UpdateCommand.Parameters.Add(
                 new SqlParameter("@cursa", SqlDbType.VarChar, 200)
                 {
                     SourceColumn = "cursa",
                     SourceVersion = DataRowVersion.Original
                 }
            );
        }

        private void updateRemoteDatabaseEvent(object sender, EventArgs e)
        {
            try
            {
                adapter.Update(dataSet.Tables["Rezervari"]);
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void afiseazaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataSet.Tables.Contains("Rezervari"))
                dataSet.Tables["Rezervari"].Clear();
            adapter.Fill(dataSet, "Rezervari");
            if (rezervariDataGridView.DataSource == null)
            {
                rezervariDataGridView.DataSource = dataSet.Tables["Rezervari"];
                rezervariDataGridView.Columns["cursa"].ReadOnly = true;
                rezervariDataGridView.Columns["nr_locuri"].ReadOnly = true;
            }
        }

        private void incepeTranzactieButton_Click(object sender, EventArgs e)
        {
            incepeTranzactieButton.Enabled = false;
            comiteTranzactieButton.Enabled = true;
            anulareTranzactieButton.Enabled = true;
            rezervariToolStripMenuItem.Enabled = true;
            isolationLevelComboBox.Enabled = false;
            rezervariDataGridView.ReadOnly = false;
            try
            {
                conexiune.Open();
                tranzactie = conexiune.BeginTransaction((IsolationLevel)Enum.Parse(typeof(IsolationLevel), isolationLevelComboBox.SelectedItem as string));
                adapter.SelectCommand.Transaction = tranzactie;
                adapter.DeleteCommand.Transaction = tranzactie;
                adapter.UpdateCommand.Transaction = tranzactie;
                adapter.InsertCommand.Transaction = tranzactie;
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comiteTranzactieButton_Click(object sender, EventArgs e)
        {
            try
            {
                tranzactie.Commit();
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                resetForm();
            }
        }

        private void resetForm()
        {
            if (conexiune.State == ConnectionState.Open)
                conexiune.Close();
            isolationLevelComboBox.Enabled = true;
            rezervariToolStripMenuItem.Enabled = false;
            anulareTranzactieButton.Enabled = false;
            comiteTranzactieButton.Enabled = false;
            incepeTranzactieButton.Enabled = true;
            rezervariDataGridView.ReadOnly = true;
        }

        private void anulareTranzactieButton_Click(object sender, EventArgs e)
        {
            try
            {
                tranzactie.Rollback();
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                resetForm();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (conexiune.State == ConnectionState.Open)
                    conexiune.Close();
            }
            catch (Exception)
            {
            }
        }

        private SqlTransaction tranzactie = null;
        private SqlConnection conexiune = new SqlConnection("Server= ANDREI-DESKTOP; Database= AndreiLocal; Trusted_Connection= true;");
        private readonly DataSet dataSet = new DataSet();
        private readonly SqlDataAdapter adapter = new SqlDataAdapter();
        private readonly IEnumerable<string> niveleDeIzolare = from isolationLevel in Enum.GetNames(typeof(IsolationLevel)) where (string.Compare(isolationLevel, "chaos", true) != 0 && string.Compare(isolationLevel, "snapshot", true) != 0) select isolationLevel;
    }
}
