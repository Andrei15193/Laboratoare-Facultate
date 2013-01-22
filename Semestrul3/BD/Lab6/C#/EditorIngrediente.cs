using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BDLab6
{
    public partial class EditorIngrediente : Form
    {
        public EditorIngrediente(Form1.SqlDataAdaptersAndDataSet sqlDataAdaptersAndDataSet)
        {
            this.adaptersAndDataSet = sqlDataAdaptersAndDataSet;
            InitializeComponent();
            this.LoadData();
        }

        private void LoadData()
        {
            this.ingredienteListBox.Items.Clear();
            foreach (DataRow row in this.adaptersAndDataSet.dataSet.Tables["Ingrediente"].Rows)
                this.ingredienteListBox.Items.Add(row["numeI"]);
        }

        private Form1.SqlDataAdaptersAndDataSet adaptersAndDataSet;

        private void IngredientSelectat(object sender, EventArgs e)
        {
            bool found = false;
            int selectedIndex = this.ingredienteListBox.SelectedIndex, codI;
            System.Collections.IEnumerator it;
            if (selectedIndex != -1){
                this.modificaButton.Enabled = true;
                this.numeTextBox.Text = this.ingredienteListBox.SelectedItem.ToString();
                this.unitateDeMasuraTextBox.Text = this.adaptersAndDataSet.dataSet.Tables["Ingrediente"].Rows[selectedIndex]["unitate_masura"].ToString();

                codI = Convert.ToInt32(this.adaptersAndDataSet.dataSet.Tables["Ingrediente"].Rows[selectedIndex]["codI"]);
                it = this.adaptersAndDataSet.dataSet.Tables["Retete"].Rows.GetEnumerator();
                while (!found && it.MoveNext())
                    if (Convert.ToInt32((it.Current as DataRow)["codI"]) == codI)
                        found = true;
                if (!found)
                    this.stergeButton.Enabled = true;
                else
                    this.stergeButton.Enabled = false;
            }
        }

        private void AdaugaIngredient(object sender, EventArgs e)
        {
            if (this.numeTextBox.Text.Length == 0 || this.unitateDeMasuraTextBox.Text.Length == 0)
                MessageBox.Show(this, "Numele sau unitatea de masura este inexistent(a)!", "Eroare!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                DataRow newDataRow = this.adaptersAndDataSet.dataSet.Tables["Ingrediente"].NewRow();
                newDataRow["codI"] = getMaxCodI() + 1;
                newDataRow["numeI"] = this.numeTextBox.Text;
                newDataRow["unitate_masura"] = this.unitateDeMasuraTextBox.Text;
                this.ingredienteListBox.Items.Add(this.numeTextBox.Text);
                this.numeTextBox.Text = "";
                this.unitateDeMasuraTextBox.Text = "";
                this.adaptersAndDataSet.dataSet.Tables["Ingrediente"].Rows.Add(newDataRow);
                this.adaptersAndDataSet.ingredienteDataAdapter.Update(new DataRow[]{newDataRow});
                this.modificaButton.Enabled = false;
                this.stergeButton.Enabled = false;
            }
        }

        private void ModificaIngredient(object sender, EventArgs e)
        {
            if (this.numeTextBox.Text.Length == 0 || this.unitateDeMasuraTextBox.Text.Length == 0)
                MessageBox.Show(this, "Numele sau unitatea de masura este inexistent(a)!", "Eroare!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                DataRow dataRow = this.adaptersAndDataSet.dataSet.Tables["Ingrediente"].Rows[this.ingredienteListBox.SelectedIndex];
                dataRow["numeI"] = this.numeTextBox.Text;
                dataRow["unitate_masura"] = this.unitateDeMasuraTextBox.Text;
                this.ingredienteListBox.Items[this.ingredienteListBox.SelectedIndex] = this.numeTextBox.Text;
                this.adaptersAndDataSet.ingredienteDataAdapter.Update(new DataRow[] { dataRow });
            }
        }

        private void StergeIngredient(object sender, EventArgs e)
        {
            this.modificaButton.Enabled = false;
            this.stergeButton.Enabled = false;
            this.adaptersAndDataSet.dataSet.Tables["Ingrediente"].Rows[this.ingredienteListBox.SelectedIndex].Delete();
            this.adaptersAndDataSet.ingredienteDataAdapter.Update(new DataRow[] { this.adaptersAndDataSet.dataSet.Tables["Ingrediente"].Rows[this.ingredienteListBox.SelectedIndex] });
            this.ingredienteListBox.Items.RemoveAt(this.ingredienteListBox.SelectedIndex);
        }

        private int getMaxCodI()
        {
            int max = 0, value;
            foreach (DataRow row in this.adaptersAndDataSet.dataSet.Tables["Ingrediente"].Rows)
            {
                value = Convert.ToInt32(row["codI"]);
                if (value > max)
                    max = value;
            }
            return max;
        }
    }
}
