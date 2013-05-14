namespace ISSApp
{
    partial class ConsumerView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.booksDataGridView = new System.Windows.Forms.DataGridView();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Author = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ISBN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.booksLabel = new System.Windows.Forms.Label();
            this.retrieveBookButton = new System.Windows.Forms.Button();
            this.bookCountLabel = new System.Windows.Forms.Label();
            this.bookCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.booksDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bookCountNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // booksDataGridView
            // 
            this.booksDataGridView.AllowUserToAddRows = false;
            this.booksDataGridView.AllowUserToDeleteRows = false;
            this.booksDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.booksDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.booksDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title,
            this.Author,
            this.Count,
            this.ISBN});
            this.booksDataGridView.Location = new System.Drawing.Point(15, 25);
            this.booksDataGridView.Name = "booksDataGridView";
            this.booksDataGridView.ReadOnly = true;
            this.booksDataGridView.RowHeadersVisible = false;
            this.booksDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.booksDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.booksDataGridView.Size = new System.Drawing.Size(527, 215);
            this.booksDataGridView.TabIndex = 0;
            this.booksDataGridView.SelectionChanged += new System.EventHandler(this.BookTableSelectionChanged);
            // 
            // Title
            // 
            this.Title.HeaderText = "Title";
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            this.Title.Width = 200;
            // 
            // Author
            // 
            this.Author.HeaderText = "Author";
            this.Author.Name = "Author";
            this.Author.ReadOnly = true;
            this.Author.Width = 120;
            // 
            // Count
            // 
            this.Count.HeaderText = "Available copies";
            this.Count.Name = "Count";
            this.Count.ReadOnly = true;
            // 
            // ISBN
            // 
            this.ISBN.HeaderText = "ISBN";
            this.ISBN.Name = "ISBN";
            this.ISBN.ReadOnly = true;
            // 
            // booksLabel
            // 
            this.booksLabel.AutoSize = true;
            this.booksLabel.Location = new System.Drawing.Point(12, 9);
            this.booksLabel.Name = "booksLabel";
            this.booksLabel.Size = new System.Drawing.Size(40, 13);
            this.booksLabel.TabIndex = 1;
            this.booksLabel.Text = "Books:";
            // 
            // borrowBookButton
            // 
            this.retrieveBookButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.retrieveBookButton.Enabled = false;
            this.retrieveBookButton.Location = new System.Drawing.Point(12, 246);
            this.retrieveBookButton.Name = "borrowBookButton";
            this.retrieveBookButton.Size = new System.Drawing.Size(89, 23);
            this.retrieveBookButton.TabIndex = 2;
            this.retrieveBookButton.Text = "Borrow book";
            this.retrieveBookButton.UseVisualStyleBackColor = true;
            this.retrieveBookButton.Click += new System.EventHandler(this.OnBorrowBooks);
            // 
            // bookCountLabel
            // 
            this.bookCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bookCountLabel.AutoSize = true;
            this.bookCountLabel.Location = new System.Drawing.Point(107, 251);
            this.bookCountLabel.Name = "bookCountLabel";
            this.bookCountLabel.Size = new System.Drawing.Size(93, 13);
            this.bookCountLabel.TabIndex = 3;
            this.bookCountLabel.Text = "Number of copies:";
            // 
            // bookCountNumericUpDown
            // 
            this.bookCountNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bookCountNumericUpDown.Location = new System.Drawing.Point(206, 249);
            this.bookCountNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.bookCountNumericUpDown.Name = "bookCountNumericUpDown";
            this.bookCountNumericUpDown.Size = new System.Drawing.Size(130, 20);
            this.bookCountNumericUpDown.TabIndex = 4;
            this.bookCountNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 281);
            this.Controls.Add(this.bookCountNumericUpDown);
            this.Controls.Add(this.bookCountLabel);
            this.Controls.Add(this.retrieveBookButton);
            this.Controls.Add(this.booksLabel);
            this.Controls.Add(this.booksDataGridView);
            this.MinimumSize = new System.Drawing.Size(570, 320);
            this.Name = "MainView";
            this.Text = "Consumer App";
            ((System.ComponentModel.ISupportInitialize)(this.booksDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bookCountNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView booksDataGridView;
        private System.Windows.Forms.Label booksLabel;
        private System.Windows.Forms.Button retrieveBookButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Author;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.DataGridViewTextBoxColumn ISBN;
        private System.Windows.Forms.Label bookCountLabel;
        private System.Windows.Forms.NumericUpDown bookCountNumericUpDown;
    }
}

