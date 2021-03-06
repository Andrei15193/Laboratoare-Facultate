﻿using System;
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
    public partial class LibrarianView : Form
    {
        public LibrarianView(Library library)
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
                    this.retrieveBookButton.Text = retrieveBookText;
                    this.retrieveBookButton.Enabled = false;
                    break;
                case 1:
                    this.retrieveBookButton.Enabled = true;
                    this.retrieveBookButton.Text = retrieveBookText;
                    break;
                default:
                    this.retrieveBookButton.Enabled = true;
                    this.retrieveBookButton.Text = retrieveBooksText;
                    break;
            }
        }

        private void OnRetrieveBooks(object sender, EventArgs e)
        {
            for (int j = 0; j < this.booksDataGridView.SelectedRows.Count; j++)
                this.booksDataGridView.SelectedRows[j].Cells["Count"].Value = Convert.ToDecimal(this.booksDataGridView.SelectedRows[j].Cells["Count"].Value) + this.bookCountNumericUpDown.Value;
        }

        private void OnAddBook(object sender, EventArgs e)
        {
            using (BookEditView bookEditView = new BookEditView())
            {
                if (bookEditView.ShowDialog() == DialogResult.OK)
                    this.booksDataGridView.Rows.Add(bookEditView.Title, bookEditView.Author, 0, bookEditView.ISBN);
            }
        }

        private void OnUpdateBook(object sender, EventArgs e)
        {
            if (this.booksDataGridView.SelectedRows.Count == 1)
            {
                DataGridViewRow viewRow = this.booksDataGridView.SelectedRows[0];
                using (BookEditView bookEditView = new BookEditView(viewRow.Cells["Title"].Value as string, viewRow.Cells["Author"].Value as string, viewRow.Cells["ISBN"].Value as string))
                {
                    if (bookEditView.ShowDialog() == DialogResult.OK)
                    {
                        viewRow.Cells["Title"].Value = bookEditView.Title;
                        viewRow.Cells["Author"].Value = bookEditView.Author;
                        viewRow.Cells["ISBN"].Value = bookEditView.ISBN;
                    }
                }
            }
            else
                MessageBox.Show(this, "In order to edit a book you must select only one!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void OnRemoveBook(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.booksDataGridView.SelectedRows)
                this.booksDataGridView.Rows.Remove(row);
        }

        private bool canRun = true;
        private static string retrieveBookText = "Retrieve book";
        private static string retrieveBooksText = "Retrieve books";
        private Library library;
    }
}
