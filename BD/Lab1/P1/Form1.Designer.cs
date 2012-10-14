namespace CSLabBD
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.MainMenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuFileSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenuFileClear = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.MainToolBar = new System.Windows.Forms.ToolStrip();
            this.MainToolBarOpenFile = new System.Windows.Forms.ToolStripButton();
            this.MainToolBarSaveFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MainToolBarClear = new System.Windows.Forms.ToolStripButton();
            this.MainToolBarClose = new System.Windows.Forms.ToolStripButton();
            this.LabelMovies = new System.Windows.Forms.Label();
            this.ListBoxMovies = new System.Windows.Forms.ListBox();
            this.LabelTitle = new System.Windows.Forms.Label();
            this.LabelReleaseDate = new System.Windows.Forms.Label();
            this.LabelVotes = new System.Windows.Forms.Label();
            this.TextBoxTitle = new System.Windows.Forms.TextBox();
            this.DatePickerReleaseDate = new System.Windows.Forms.DateTimePicker();
            this.NumericVotes = new System.Windows.Forms.NumericUpDown();
            this.LabelActors = new System.Windows.Forms.Label();
            this.ListBoxActors = new System.Windows.Forms.ListBox();
            this.ContextMenuActors = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextMenuActorsAddActor = new System.Windows.Forms.ToolStripMenuItem();
            this.ButtonAddMovie = new System.Windows.Forms.Button();
            this.ButtonUpdateMovie = new System.Windows.Forms.Button();
            this.ButtonDeleteMovie = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.ContextMenuActorsRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuActorsSelectedActor = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuActorsAllActors = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu.SuspendLayout();
            this.MainToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericVotes)).BeginInit();
            this.ContextMenuActors.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuFile,
            this.MainMenuClose});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(390, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "menuStrip1";
            // 
            // MainMenuFile
            // 
            this.MainMenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuFileOpen,
            this.MainMenuFileSave,
            this.MainMenuFileSeparator,
            this.MainMenuFileClear});
            this.MainMenuFile.Name = "MainMenuFile";
            this.MainMenuFile.Size = new System.Drawing.Size(37, 20);
            this.MainMenuFile.Text = "File";
            // 
            // MainMenuFileOpen
            // 
            this.MainMenuFileOpen.Name = "MainMenuFileOpen";
            this.MainMenuFileOpen.Size = new System.Drawing.Size(103, 22);
            this.MainMenuFileOpen.Text = "Open";
            this.MainMenuFileOpen.Click += new System.EventHandler(this.EventOpenFile);
            // 
            // MainMenuFileSave
            // 
            this.MainMenuFileSave.Name = "MainMenuFileSave";
            this.MainMenuFileSave.Size = new System.Drawing.Size(103, 22);
            this.MainMenuFileSave.Text = "Save";
            this.MainMenuFileSave.Click += new System.EventHandler(this.EventSaveFile);
            // 
            // MainMenuFileSeparator
            // 
            this.MainMenuFileSeparator.Name = "MainMenuFileSeparator";
            this.MainMenuFileSeparator.Size = new System.Drawing.Size(100, 6);
            // 
            // MainMenuFileClear
            // 
            this.MainMenuFileClear.Name = "MainMenuFileClear";
            this.MainMenuFileClear.Size = new System.Drawing.Size(103, 22);
            this.MainMenuFileClear.Text = "Clear";
            this.MainMenuFileClear.Click += new System.EventHandler(this.EventClearAll);
            // 
            // MainMenuClose
            // 
            this.MainMenuClose.Name = "MainMenuClose";
            this.MainMenuClose.Size = new System.Drawing.Size(48, 20);
            this.MainMenuClose.Text = "Close";
            this.MainMenuClose.Click += new System.EventHandler(this.EventCloseApplication);
            // 
            // MainToolBar
            // 
            this.MainToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainToolBarOpenFile,
            this.MainToolBarSaveFile,
            this.toolStripSeparator2,
            this.MainToolBarClear,
            this.MainToolBarClose});
            this.MainToolBar.Location = new System.Drawing.Point(0, 24);
            this.MainToolBar.Name = "MainToolBar";
            this.MainToolBar.Size = new System.Drawing.Size(390, 25);
            this.MainToolBar.TabIndex = 1;
            this.MainToolBar.Text = "toolStrip1";
            // 
            // MainToolBarOpenFile
            // 
            this.MainToolBarOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MainToolBarOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("MainToolBarOpenFile.Image")));
            this.MainToolBarOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MainToolBarOpenFile.Name = "MainToolBarOpenFile";
            this.MainToolBarOpenFile.Size = new System.Drawing.Size(23, 22);
            this.MainToolBarOpenFile.Text = "toolStripButton1";
            this.MainToolBarOpenFile.Click += new System.EventHandler(this.EventOpenFile);
            // 
            // MainToolBarSaveFile
            // 
            this.MainToolBarSaveFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MainToolBarSaveFile.Image = ((System.Drawing.Image)(resources.GetObject("MainToolBarSaveFile.Image")));
            this.MainToolBarSaveFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MainToolBarSaveFile.Name = "MainToolBarSaveFile";
            this.MainToolBarSaveFile.Size = new System.Drawing.Size(23, 22);
            this.MainToolBarSaveFile.Text = "toolStripButton2";
            this.MainToolBarSaveFile.Click += new System.EventHandler(this.EventSaveFile);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // MainToolBarClear
            // 
            this.MainToolBarClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MainToolBarClear.Image = ((System.Drawing.Image)(resources.GetObject("MainToolBarClear.Image")));
            this.MainToolBarClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MainToolBarClear.Name = "MainToolBarClear";
            this.MainToolBarClear.Size = new System.Drawing.Size(23, 22);
            this.MainToolBarClear.Text = "toolStripButton1";
            this.MainToolBarClear.Click += new System.EventHandler(this.EventClearAll);
            // 
            // MainToolBarClose
            // 
            this.MainToolBarClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MainToolBarClose.Image = ((System.Drawing.Image)(resources.GetObject("MainToolBarClose.Image")));
            this.MainToolBarClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MainToolBarClose.Name = "MainToolBarClose";
            this.MainToolBarClose.Size = new System.Drawing.Size(23, 22);
            this.MainToolBarClose.Text = "toolStripButton3";
            this.MainToolBarClose.Click += new System.EventHandler(this.EventCloseApplication);
            // 
            // LabelMovies
            // 
            this.LabelMovies.AutoSize = true;
            this.LabelMovies.Location = new System.Drawing.Point(12, 49);
            this.LabelMovies.Name = "LabelMovies";
            this.LabelMovies.Size = new System.Drawing.Size(44, 13);
            this.LabelMovies.TabIndex = 2;
            this.LabelMovies.Text = "Movies:";
            // 
            // ListBoxMovies
            // 
            this.ListBoxMovies.FormattingEnabled = true;
            this.ListBoxMovies.Location = new System.Drawing.Point(13, 66);
            this.ListBoxMovies.Name = "ListBoxMovies";
            this.ListBoxMovies.Size = new System.Drawing.Size(168, 212);
            this.ListBoxMovies.TabIndex = 3;
            this.ListBoxMovies.MouseClick += new System.Windows.Forms.MouseEventHandler(this.EventMovieSelect);
            // 
            // LabelTitle
            // 
            this.LabelTitle.AutoSize = true;
            this.LabelTitle.Location = new System.Drawing.Point(187, 66);
            this.LabelTitle.Name = "LabelTitle";
            this.LabelTitle.Size = new System.Drawing.Size(30, 13);
            this.LabelTitle.TabIndex = 4;
            this.LabelTitle.Text = "Title:";
            // 
            // LabelReleaseDate
            // 
            this.LabelReleaseDate.AutoSize = true;
            this.LabelReleaseDate.Location = new System.Drawing.Point(187, 93);
            this.LabelReleaseDate.Name = "LabelReleaseDate";
            this.LabelReleaseDate.Size = new System.Drawing.Size(73, 13);
            this.LabelReleaseDate.TabIndex = 5;
            this.LabelReleaseDate.Text = "Release date:";
            // 
            // LabelVotes
            // 
            this.LabelVotes.AutoSize = true;
            this.LabelVotes.Location = new System.Drawing.Point(187, 119);
            this.LabelVotes.Name = "LabelVotes";
            this.LabelVotes.Size = new System.Drawing.Size(37, 13);
            this.LabelVotes.TabIndex = 6;
            this.LabelVotes.Text = "Votes:";
            // 
            // TextBoxTitle
            // 
            this.TextBoxTitle.Location = new System.Drawing.Point(267, 63);
            this.TextBoxTitle.Name = "TextBoxTitle";
            this.TextBoxTitle.Size = new System.Drawing.Size(111, 20);
            this.TextBoxTitle.TabIndex = 7;
            // 
            // DatePickerReleaseDate
            // 
            this.DatePickerReleaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DatePickerReleaseDate.Location = new System.Drawing.Point(267, 90);
            this.DatePickerReleaseDate.Name = "DatePickerReleaseDate";
            this.DatePickerReleaseDate.Size = new System.Drawing.Size(111, 20);
            this.DatePickerReleaseDate.TabIndex = 8;
            // 
            // NumericVotes
            // 
            this.NumericVotes.Location = new System.Drawing.Point(267, 117);
            this.NumericVotes.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.NumericVotes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericVotes.Name = "NumericVotes";
            this.NumericVotes.Size = new System.Drawing.Size(111, 20);
            this.NumericVotes.TabIndex = 9;
            this.NumericVotes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // LabelActors
            // 
            this.LabelActors.AutoSize = true;
            this.LabelActors.Location = new System.Drawing.Point(187, 140);
            this.LabelActors.Name = "LabelActors";
            this.LabelActors.Size = new System.Drawing.Size(40, 13);
            this.LabelActors.TabIndex = 10;
            this.LabelActors.Text = "Actors:";
            // 
            // ListBoxActors
            // 
            this.ListBoxActors.ContextMenuStrip = this.ContextMenuActors;
            this.ListBoxActors.FormattingEnabled = true;
            this.ListBoxActors.Location = new System.Drawing.Point(187, 157);
            this.ListBoxActors.Name = "ListBoxActors";
            this.ListBoxActors.Size = new System.Drawing.Size(191, 121);
            this.ListBoxActors.TabIndex = 11;
            // 
            // ContextMenuActors
            // 
            this.ContextMenuActors.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuActorsAddActor,
            this.ContextMenuActorsRemove});
            this.ContextMenuActors.Name = "ContextMenuActors";
            this.ContextMenuActors.Size = new System.Drawing.Size(127, 48);
            // 
            // ContextMenuActorsAddActor
            // 
            this.ContextMenuActorsAddActor.Name = "ContextMenuActorsAddActor";
            this.ContextMenuActorsAddActor.Size = new System.Drawing.Size(152, 22);
            this.ContextMenuActorsAddActor.Text = "Add actor";
            this.ContextMenuActorsAddActor.Click += new System.EventHandler(this.EventAddActor);
            // 
            // ButtonAddMovie
            // 
            this.ButtonAddMovie.Location = new System.Drawing.Point(102, 284);
            this.ButtonAddMovie.Name = "ButtonAddMovie";
            this.ButtonAddMovie.Size = new System.Drawing.Size(89, 23);
            this.ButtonAddMovie.TabIndex = 12;
            this.ButtonAddMovie.Text = "Add movie";
            this.ButtonAddMovie.UseVisualStyleBackColor = true;
            this.ButtonAddMovie.Click += new System.EventHandler(this.EventAddMovie);
            // 
            // ButtonUpdateMovie
            // 
            this.ButtonUpdateMovie.Location = new System.Drawing.Point(197, 284);
            this.ButtonUpdateMovie.Name = "ButtonUpdateMovie";
            this.ButtonUpdateMovie.Size = new System.Drawing.Size(88, 23);
            this.ButtonUpdateMovie.TabIndex = 13;
            this.ButtonUpdateMovie.Text = "Update movie";
            this.ButtonUpdateMovie.UseVisualStyleBackColor = true;
            this.ButtonUpdateMovie.Click += new System.EventHandler(this.EventUpdateMovie);
            // 
            // ButtonDeleteMovie
            // 
            this.ButtonDeleteMovie.Location = new System.Drawing.Point(291, 284);
            this.ButtonDeleteMovie.Name = "ButtonDeleteMovie";
            this.ButtonDeleteMovie.Size = new System.Drawing.Size(87, 23);
            this.ButtonDeleteMovie.TabIndex = 14;
            this.ButtonDeleteMovie.Text = "Delete movie";
            this.ButtonDeleteMovie.UseVisualStyleBackColor = true;
            this.ButtonDeleteMovie.Click += new System.EventHandler(this.EventDeleteMovie);
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.EventSaveFile);
            // 
            // ContextMenuActorsRemove
            // 
            this.ContextMenuActorsRemove.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextMenuActorsSelectedActor,
            this.ContextMenuActorsAllActors});
            this.ContextMenuActorsRemove.Name = "ContextMenuActorsRemove";
            this.ContextMenuActorsRemove.Size = new System.Drawing.Size(152, 22);
            this.ContextMenuActorsRemove.Text = "Remove";
            // 
            // ContextMenuActorsSelectedActor
            // 
            this.ContextMenuActorsSelectedActor.Name = "ContextMenuActorsSelectedActor";
            this.ContextMenuActorsSelectedActor.Size = new System.Drawing.Size(152, 22);
            this.ContextMenuActorsSelectedActor.Text = "Selected actor";
            this.ContextMenuActorsSelectedActor.Click += new System.EventHandler(this.EventRemoveSelectedActor);
            // 
            // ContextMenuActorsAllActors
            // 
            this.ContextMenuActorsAllActors.Name = "ContextMenuActorsAllActors";
            this.ContextMenuActorsAllActors.Size = new System.Drawing.Size(152, 22);
            this.ContextMenuActorsAllActors.Text = "All actors";
            this.ContextMenuActorsAllActors.Click += new System.EventHandler(this.EventRemoveAllActors);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 316);
            this.Controls.Add(this.ButtonDeleteMovie);
            this.Controls.Add(this.ButtonUpdateMovie);
            this.Controls.Add(this.ButtonAddMovie);
            this.Controls.Add(this.ListBoxActors);
            this.Controls.Add(this.LabelActors);
            this.Controls.Add(this.NumericVotes);
            this.Controls.Add(this.DatePickerReleaseDate);
            this.Controls.Add(this.TextBoxTitle);
            this.Controls.Add(this.LabelVotes);
            this.Controls.Add(this.LabelReleaseDate);
            this.Controls.Add(this.LabelTitle);
            this.Controls.Add(this.ListBoxMovies);
            this.Controls.Add(this.LabelMovies);
            this.Controls.Add(this.MainToolBar);
            this.Controls.Add(this.MainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "BD Laborator 1";
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.MainToolBar.ResumeLayout(false);
            this.MainToolBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericVotes)).EndInit();
            this.ContextMenuActors.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MainMenuFile;
        private System.Windows.Forms.ToolStripMenuItem MainMenuFileOpen;
        private System.Windows.Forms.ToolStripMenuItem MainMenuFileSave;
        private System.Windows.Forms.ToolStripSeparator MainMenuFileSeparator;
        private System.Windows.Forms.ToolStripMenuItem MainMenuFileClear;
        private System.Windows.Forms.ToolStripMenuItem MainMenuClose;
        private System.Windows.Forms.ToolStrip MainToolBar;
        private System.Windows.Forms.ToolStripButton MainToolBarOpenFile;
        private System.Windows.Forms.ToolStripButton MainToolBarSaveFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton MainToolBarClose;
        private System.Windows.Forms.Label LabelMovies;
        private System.Windows.Forms.ListBox ListBoxMovies;
        private System.Windows.Forms.Label LabelTitle;
        private System.Windows.Forms.Label LabelReleaseDate;
        private System.Windows.Forms.Label LabelVotes;
        private System.Windows.Forms.TextBox TextBoxTitle;
        private System.Windows.Forms.DateTimePicker DatePickerReleaseDate;
        private System.Windows.Forms.NumericUpDown NumericVotes;
        private System.Windows.Forms.Label LabelActors;
        private System.Windows.Forms.ListBox ListBoxActors;
        private System.Windows.Forms.Button ButtonAddMovie;
        private System.Windows.Forms.Button ButtonUpdateMovie;
        private System.Windows.Forms.Button ButtonDeleteMovie;
        private System.Windows.Forms.ToolStripButton MainToolBarClear;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.ContextMenuStrip ContextMenuActors;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuActorsAddActor;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuActorsRemove;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuActorsSelectedActor;
        private System.Windows.Forms.ToolStripMenuItem ContextMenuActorsAllActors;
    }
}

