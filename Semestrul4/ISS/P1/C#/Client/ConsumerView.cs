using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISSApp
{
    public partial class ConsumerView : Form
    {
        public ConsumerView(Library library)
        {
            InitializeComponent();
            this.library = library;
            booksDataGridView.DataSource = library.BooksTableView;
        }

        private void BookTableSelectionChanged(object sender, EventArgs e)
        {
            switch (this.booksDataGridView.SelectedRows.Count)
            {
                case 0:
                    this.retrieveBookButton.Text = borrowBookText;
                    this.retrieveBookButton.Enabled = false;
                    break;
                case 1:
                    this.retrieveBookButton.Enabled = true;
                    this.retrieveBookButton.Text = borrowBookText;
                    break;
                default:
                    this.retrieveBookButton.Enabled = true;
                    this.retrieveBookButton.Text = borrowBooksText;
                    break;
            }
        }

        private void OnBorrowBooks(object sender, EventArgs e)
        {
            int i = 0, j;
            while (i < this.booksDataGridView.SelectedRows.Count && this.bookCountNumericUpDown.Value <= Convert.ToDecimal(this.booksDataGridView.SelectedRows[i].Cells["Count"].Value))
                i++;
            if (i == this.booksDataGridView.SelectedRows.Count)
                for (j = 0; j < this.booksDataGridView.SelectedRows.Count; j++)
                    this.booksDataGridView.SelectedRows[j].Cells["Count"].Value = Convert.ToDecimal(this.booksDataGridView.SelectedRows[j].Cells["Count"].Value) - this.bookCountNumericUpDown.Value;
        }

        private static string borrowBookText = "Borrow book";
        private static string borrowBooksText = "Borrow books";
        private Library library;
    }
}
