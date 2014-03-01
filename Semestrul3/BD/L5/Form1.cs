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

namespace BDLab5
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.deletedRowIndexes = new LinkedList<int>();
            dataSet = new DataSet();
            dbConnection = new SqlConnection("Server=ANDREIWINDOWS8; Database=LocalDb; Trusted_Connection=yes; ");
            SetDataAdapters();
            sectionsDataAdapter.Fill(dataSet, "Sections");
            studentsDataAdapter.Fill(dataSet, "Students");
            dataSet.Relations.Add(new DataRelation("studentsFromSection", dataSet.Tables["Sections"].Columns["cods"], dataSet.Tables["Students"].Columns["cods"]));
            sectionsDataGridView.DataSource = dataSet.Tables["Sections"];
            sectionsDataGridView.Columns[0].Width = sectionsDataGridView.Width;
            sectionsDataGridView.Columns[1].Visible = false;
            sectionsDataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            studentsDataGridView.Columns[0].Width = studentsDataGridView.Width - studentsDataGridView.Columns[1].Width;
            FillStudentsTable();
        }

        private void SetDataAdapters()
        {
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(studentsDataAdapter = new SqlDataAdapter("select nume, grupa, nrmatricol, datan, cods from studenti", dbConnection));
            sectionsDataAdapter = new SqlDataAdapter("select denumires as Denumire, cods from sectii", dbConnection);
            studentsDataAdapter.InsertCommand = commandBuilder.GetInsertCommand(true);
            studentsDataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand(true);
            studentsDataAdapter.DeleteCommand = commandBuilder.GetDeleteCommand(true);
        }

        private void FillStudentsTable()
        {
            if (sectionsDataGridView.SelectedRows.Count != 0)
            {
                DataGridViewRowCollection destination = studentsDataGridView.Rows;
                destination.Clear();
                this.studentsFromSelectedSection = dataSet.Tables["Sections"].Rows[sectionsDataGridView.SelectedRows[0].Index].GetChildRows(dataSet.Relations[0]);
                foreach (DataRow row in studentsFromSelectedSection)
                {
                    DataGridViewRow currentViewRow = new DataGridViewRow();
                    currentViewRow.Cells.Add(new DataGridViewTextBoxCell());
                    currentViewRow.Cells.Add(new DataGridViewTextBoxCell());
                    currentViewRow.Cells[0].Value = row[0];
                    currentViewRow.Cells[1].Value = row[1];
                    destination.Add(currentViewRow);
                }
            }
        }

        private void UpdateStudentTable()
        {
            studentsDataAdapter.Update(this.dataSet.Tables["Students"]);
        }

        private LinkedList<int> deletedRowIndexes;
        private DataSet dataSet;
        private SqlConnection dbConnection;
        private SqlDataAdapter sectionsDataAdapter;
        private SqlDataAdapter studentsDataAdapter;
        private DataRow[] studentsFromSelectedSection;
    }
}
