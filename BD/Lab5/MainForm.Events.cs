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
        private void AddStudentClick(object sender, EventArgs e)
        {
            using (StudentDetailsForm dialog = new StudentDetailsForm("Adauga"))
            {
                DataRow newDataRow;
                DataGridViewRow newViewRow;
                DataTable studentsDataTable;
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    newViewRow = new DataGridViewRow();
                    newViewRow.Cells.Add(new DataGridViewTextBoxCell());
                    newViewRow.Cells.Add(new DataGridViewTextBoxCell());
                    studentsDataTable = dataSet.Tables["Students"];

                    newDataRow = studentsDataTable.NewRow();
                    newViewRow.Cells[0].Value = newDataRow["nume"] = dialog.StudentName;
                    newViewRow.Cells[1].Value = newDataRow["grupa"] = dialog.SudentGroup;
                    newDataRow["datan"] = dialog.StudentDateOfBirth;
                    newDataRow["nrmatricol"] = dialog.StudentSerialNumber;
                    newDataRow["cods"] = sectionsDataGridView.SelectedRows[0].Cells["cods"].Value;
                    studentsDataTable.Rows.Add(newDataRow);
                    studentsDataGridView.Rows.Add(newViewRow);
                }
            }
        }

        private void UpdateStudentClick(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = studentsDataGridView.SelectedRows;
            DataRow selectedDataRow;
            if (selectedRows.Count != 0)
            {
                selectedDataRow = this.studentsFromSelectedSection[selectedRows[0].Index + numberOfDeletedRows];
                using (StudentDetailsForm dialog = new StudentDetailsForm("Modifica", selectedDataRow["nume"] as string, selectedDataRow["nrmatricol"] as string, selectedDataRow["grupa"] as string, selectedDataRow["datan"] as DateTime?))
                {
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        selectedRows[0].Cells[0].Value = selectedDataRow["nume"] = dialog.StudentName;
                        selectedRows[0].Cells[1].Value = selectedDataRow["grupa"] = dialog.SudentGroup;
                        selectedDataRow["datan"] = dialog.StudentDateOfBirth;
                    }
                }
            }
        }

        private void DeleteStudentClick(object sender, EventArgs e)
        {
            int index;
            DataGridViewSelectedRowCollection selectedRows = studentsDataGridView.SelectedRows;
            if (selectedRows.Count != 0)
            {
                index = selectedRows[0].Index;
                this.studentsFromSelectedSection[index + numberOfDeletedRows++].Delete();
                this.studentsDataGridView.Rows.RemoveAt(index);
            }
        }

        private void CellClicked(object sender, DataGridViewCellEventArgs e)
        {
            numberOfDeletedRows = 0;
            this.button1.Enabled = this.button2.Enabled = this.button3.Enabled = true;
            UpdateStudentTable();
            FillStudentsTable();
        }

        private void FormClosingEvent(object sender, FormClosingEventArgs e)
        {
            UpdateStudentTable();
        }
    }
}
