namespace L3
{
	partial class Form1
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
			this._addSetButton = new System.Windows.Forms.Button();
			this._addElementToSetButton = new System.Windows.Forms.Button();
			this._setOperationComboBox = new System.Windows.Forms.ComboBox();
			this._setsListBox1 = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this._currentSetTextBox = new System.Windows.Forms.TextBox();
			this._setOperationExecuteButton = new System.Windows.Forms.Button();
			this._setElementNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this._setOperationsGroupBox = new System.Windows.Forms.GroupBox();
			this._setOperationResultTextBox = new System.Windows.Forms.TextBox();
			this._setListManagementGroup = new System.Windows.Forms.GroupBox();
			this._operationsSplitContainer = new System.Windows.Forms.SplitContainer();
			this._setsListBox2 = new System.Windows.Forms.ListBox();
			((System.ComponentModel.ISupportInitialize)(this._setElementNumericUpDown)).BeginInit();
			this._setOperationsGroupBox.SuspendLayout();
			this._setListManagementGroup.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._operationsSplitContainer)).BeginInit();
			this._operationsSplitContainer.Panel1.SuspendLayout();
			this._operationsSplitContainer.Panel2.SuspendLayout();
			this._operationsSplitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// _addSetButton
			// 
			this._addSetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._addSetButton.Location = new System.Drawing.Point(201, 18);
			this._addSetButton.Name = "_addSetButton";
			this._addSetButton.Size = new System.Drawing.Size(98, 23);
			this._addSetButton.TabIndex = 0;
			this._addSetButton.Text = "Adauga multime";
			this._addSetButton.UseVisualStyleBackColor = true;
			this._addSetButton.Click += new System.EventHandler(this._AddSetButtonClick);
			// 
			// _addElementToSetButton
			// 
			this._addElementToSetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._addElementToSetButton.Location = new System.Drawing.Point(201, 44);
			this._addElementToSetButton.Name = "_addElementToSetButton";
			this._addElementToSetButton.Size = new System.Drawing.Size(98, 23);
			this._addElementToSetButton.TabIndex = 1;
			this._addElementToSetButton.Text = "Adauga element";
			this._addElementToSetButton.UseVisualStyleBackColor = true;
			this._addElementToSetButton.Click += new System.EventHandler(this._AddElementToSetButtonClick);
			// 
			// _setOperationComboBox
			// 
			this._setOperationComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._setOperationComboBox.Enabled = false;
			this._setOperationComboBox.FormattingEnabled = true;
			this._setOperationComboBox.Location = new System.Drawing.Point(6, 19);
			this._setOperationComboBox.Name = "_setOperationComboBox";
			this._setOperationComboBox.Size = new System.Drawing.Size(213, 21);
			this._setOperationComboBox.TabIndex = 2;
			// 
			// _setsListBox1
			// 
			this._setsListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._setsListBox1.FormattingEnabled = true;
			this._setsListBox1.HorizontalScrollbar = true;
			this._setsListBox1.IntegralHeight = false;
			this._setsListBox1.Location = new System.Drawing.Point(3, 3);
			this._setsListBox1.Name = "_setsListBox1";
			this._setsListBox1.Size = new System.Drawing.Size(306, 118);
			this._setsListBox1.TabIndex = 3;
			this._setsListBox1.SelectedIndexChanged += new System.EventHandler(this._SetsListBoxSelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Multimi:";
			// 
			// _currentSetTextBox
			// 
			this._currentSetTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._currentSetTextBox.Location = new System.Drawing.Point(6, 19);
			this._currentSetTextBox.Name = "_currentSetTextBox";
			this._currentSetTextBox.ReadOnly = true;
			this._currentSetTextBox.Size = new System.Drawing.Size(189, 20);
			this._currentSetTextBox.TabIndex = 5;
			// 
			// _setOperationExecuteButton
			// 
			this._setOperationExecuteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._setOperationExecuteButton.Enabled = false;
			this._setOperationExecuteButton.Location = new System.Drawing.Point(225, 18);
			this._setOperationExecuteButton.Name = "_setOperationExecuteButton";
			this._setOperationExecuteButton.Size = new System.Drawing.Size(75, 23);
			this._setOperationExecuteButton.TabIndex = 6;
			this._setOperationExecuteButton.Text = "Executa";
			this._setOperationExecuteButton.UseVisualStyleBackColor = true;
			this._setOperationExecuteButton.Click += new System.EventHandler(this._SetOperationExecuteButtonClick);
			// 
			// _setElementNumericUpDown
			// 
			this._setElementNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._setElementNumericUpDown.Location = new System.Drawing.Point(6, 45);
			this._setElementNumericUpDown.Name = "_setElementNumericUpDown";
			this._setElementNumericUpDown.Size = new System.Drawing.Size(189, 20);
			this._setElementNumericUpDown.TabIndex = 7;
			// 
			// _setOperationsGroupBox
			// 
			this._setOperationsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._setOperationsGroupBox.Controls.Add(this._setOperationResultTextBox);
			this._setOperationsGroupBox.Controls.Add(this._setOperationExecuteButton);
			this._setOperationsGroupBox.Controls.Add(this._setOperationComboBox);
			this._setOperationsGroupBox.Location = new System.Drawing.Point(3, 127);
			this._setOperationsGroupBox.Name = "_setOperationsGroupBox";
			this._setOperationsGroupBox.Size = new System.Drawing.Size(306, 76);
			this._setOperationsGroupBox.TabIndex = 8;
			this._setOperationsGroupBox.TabStop = false;
			this._setOperationsGroupBox.Text = "Operatii";
			// 
			// _setOperationResultTextBox
			// 
			this._setOperationResultTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._setOperationResultTextBox.Location = new System.Drawing.Point(6, 46);
			this._setOperationResultTextBox.Name = "_setOperationResultTextBox";
			this._setOperationResultTextBox.ReadOnly = true;
			this._setOperationResultTextBox.Size = new System.Drawing.Size(294, 20);
			this._setOperationResultTextBox.TabIndex = 7;
			// 
			// _setListManagementGroup
			// 
			this._setListManagementGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._setListManagementGroup.Controls.Add(this._addElementToSetButton);
			this._setListManagementGroup.Controls.Add(this._currentSetTextBox);
			this._setListManagementGroup.Controls.Add(this._setElementNumericUpDown);
			this._setListManagementGroup.Controls.Add(this._addSetButton);
			this._setListManagementGroup.Location = new System.Drawing.Point(3, 128);
			this._setListManagementGroup.Name = "_setListManagementGroup";
			this._setListManagementGroup.Size = new System.Drawing.Size(305, 75);
			this._setListManagementGroup.TabIndex = 9;
			this._setListManagementGroup.TabStop = false;
			this._setListManagementGroup.Text = "Adaugare multimi";
			// 
			// _operationsSplitContainer
			// 
			this._operationsSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._operationsSplitContainer.Location = new System.Drawing.Point(12, 25);
			this._operationsSplitContainer.Name = "_operationsSplitContainer";
			// 
			// _operationsSplitContainer.Panel1
			// 
			this._operationsSplitContainer.Panel1.Controls.Add(this._setOperationsGroupBox);
			this._operationsSplitContainer.Panel1.Controls.Add(this._setsListBox1);
			this._operationsSplitContainer.Panel1MinSize = 150;
			// 
			// _operationsSplitContainer.Panel2
			// 
			this._operationsSplitContainer.Panel2.Controls.Add(this._setsListBox2);
			this._operationsSplitContainer.Panel2.Controls.Add(this._setListManagementGroup);
			this._operationsSplitContainer.Panel2MinSize = 150;
			this._operationsSplitContainer.Size = new System.Drawing.Size(629, 207);
			this._operationsSplitContainer.SplitterDistance = 312;
			this._operationsSplitContainer.SplitterWidth = 5;
			this._operationsSplitContainer.TabIndex = 10;
			// 
			// _setsListBox2
			// 
			this._setsListBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._setsListBox2.FormattingEnabled = true;
			this._setsListBox2.IntegralHeight = false;
			this._setsListBox2.Location = new System.Drawing.Point(3, 3);
			this._setsListBox2.Name = "_setsListBox2";
			this._setsListBox2.Size = new System.Drawing.Size(305, 118);
			this._setsListBox2.TabIndex = 10;
			this._setsListBox2.SelectedIndexChanged += new System.EventHandler(this._SetsListBoxSelectedIndexChanged);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(653, 244);
			this.Controls.Add(this._operationsSplitContainer);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "MPP Lab 3.7";
			((System.ComponentModel.ISupportInitialize)(this._setElementNumericUpDown)).EndInit();
			this._setOperationsGroupBox.ResumeLayout(false);
			this._setOperationsGroupBox.PerformLayout();
			this._setListManagementGroup.ResumeLayout(false);
			this._setListManagementGroup.PerformLayout();
			this._operationsSplitContainer.Panel1.ResumeLayout(false);
			this._operationsSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this._operationsSplitContainer)).EndInit();
			this._operationsSplitContainer.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button _addSetButton;
		private System.Windows.Forms.Button _addElementToSetButton;
		private System.Windows.Forms.ComboBox _setOperationComboBox;
		private System.Windows.Forms.ListBox _setsListBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox _currentSetTextBox;
		private System.Windows.Forms.Button _setOperationExecuteButton;
		private System.Windows.Forms.NumericUpDown _setElementNumericUpDown;
		private System.Windows.Forms.GroupBox _setOperationsGroupBox;
		private System.Windows.Forms.TextBox _setOperationResultTextBox;
		private System.Windows.Forms.GroupBox _setListManagementGroup;
		private System.Windows.Forms.SplitContainer _operationsSplitContainer;
		private System.Windows.Forms.ListBox _setsListBox2;

	}
}

