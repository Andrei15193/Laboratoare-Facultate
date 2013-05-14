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
    public partial class BookEditView : Form
    {
        public BookEditView()
        {
            InitializeComponent();
        }

        public BookEditView(string title, string author, string isbn)
            : this()
        {
            this.titleTextBox.Text = title;
            this.authorTextBox.Text = author;
            this.isbnTextBox.Enabled = false;
            this.isbnTextBox.Text = isbn;
            this.submitButton.Text = "Update";
        }

        public string Title
        {
            get
            {
                return this.titleTextBox.Text;
            }
        }

        public string Author
        {
            get
            {
                return this.authorTextBox.Text;
            }
        }

        public string ISBN
        {
            get
            {
                return this.isbnTextBox.Text;
            }
        }

        private void OnCancel(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void OnSubmit(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
