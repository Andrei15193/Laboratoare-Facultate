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

namespace GTBDDLab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _transaction = new Transaction("Server= ANDREI-DESKTOP; Database= AndreiLocal; Trusted_Connection= True; ");
            _marksTable = new DataTable();
            _marksTable.Columns.Add("Materie", typeof(string));
            _marksTable.Columns.Add("Student", typeof(string));
            _marksTable.Columns.Add("Nota", typeof(int));
            marksDataGridView.DataSource = _marksTable.DefaultView;
            marksDataGridView.ReadOnly = true;
        }

        private void afiseazaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDataReader reader = _transaction.ExecuteReader("select materie, student, nota from Note", "Note");
            _marksTable.Rows.Clear();
            while (reader.Read())
                _marksTable.Rows.Add(reader[0], reader[1], reader[2]);
            _marksTable.AcceptChanges();
            reader.Close();
        }

        private void transactionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (transactionTypeComboBox.SelectedIndex > -1)
                startTransactionButton.Enabled = true;
        }

        private void startTransactionButton_Click(object sender, EventArgs e)
        {
            SwitchForTransaction();
            if (transactionTypeComboBox.SelectedIndex == 0)
                _transaction.Start(TransactionType.Optimistic);
            else
                _transaction.Start(TransactionType.Pessimistic);
        }

        private void commitTransaction_Click(object sender, EventArgs e)
        {
            SwitchForTransaction();
            try
            {
                _transaction.Commit();
            }
            catch (SqlException exception)
            {
                MessageBox.Show(this, exception.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rollbackTransaction_Click(object sender, EventArgs e)
        {
            SwitchForTransaction();
            _transaction.Rollback();
        }

        private void SwitchForTransaction()
        {
            startTransactionButton.Enabled = !startTransactionButton.Enabled;
            commitTransactionButton.Enabled = !commitTransactionButton.Enabled;
            rollbackTransactionButton.Enabled = !rollbackTransactionButton.Enabled;
            noteToolStripMenuItem.Enabled = !noteToolStripMenuItem.Enabled;
            transactionTypeComboBox.Enabled = !transactionTypeComboBox.Enabled;
            marksDataGridView.ReadOnly = !marksDataGridView.ReadOnly;
        }

        private void adaugaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable changes = _marksTable.GetChanges(DataRowState.Added);
                if (changes != null)
                {
                    foreach (DataRow row in changes.Rows)
                        _transaction.ExecuteNonQuery(
                            string.Format("insert into Note(materie, student, nota) values ('{0}', '{1}', {2})",
                                row["Materie"].ToString(),
                                row["Student"].ToString(),
                                row["Nota"].ToString()
                            ), "Note");
                    _marksTable.AcceptChanges();
                }
            }
            catch (SqlException exception)
            {
                MessageBox.Show(this, exception.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void actualizeazaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable changes = _marksTable.GetChanges(DataRowState.Modified);
                if (changes != null)
                {
                    foreach (DataRow row in changes.Rows)
                        _transaction.ExecuteNonQuery(
                            string.Format("update Note set materie = '{0}', student = '{1}', nota = {2} where materie = '{3}' and student = '{4}' and nota = {5}",
                                row["Materie"],
                                row["Student"],
                                row["Nota"],
                                row["Materie", DataRowVersion.Original],
                                row["Student", DataRowVersion.Original],
                                row["Nota", DataRowVersion.Original]
                            ), "Note");
                    _marksTable.AcceptChanges();
                }
            }
            catch (SqlException exception)
            {
                MessageBox.Show(this, exception.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void stergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable changes = _marksTable.GetChanges(DataRowState.Deleted);
                if (changes != null)
                {
                    foreach (DataRow row in changes.Rows)
                        _transaction.ExecuteNonQuery(
                            string.Format("delete from Note where materie = '{0}' and student = '{1}' and nota = {2}",
                                row["Materie", DataRowVersion.Original].ToString(),
                                row["Student", DataRowVersion.Original].ToString(),
                                row["Nota", DataRowVersion.Original].ToString()
                            ), "Note");
                    _marksTable.AcceptChanges();
                }
            }
            catch (SqlException exception)
            {
                MessageBox.Show(this, exception.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Transaction _transaction;
        private readonly DataTable _marksTable;
    }
}
