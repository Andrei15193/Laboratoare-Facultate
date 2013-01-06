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
    public partial class IngredienteView : Form
    {
        public IngredienteView(int codP, Form1.SqlDataAdaptersAndDataSet sqlDataAdaptersAndDataSet)
        {
            this.codP = codP;
            this.adaptersAndDataSet = sqlDataAdaptersAndDataSet;
            this.reteteTable = this.adaptersAndDataSet.dataSet.Tables["Retete"];
            this.ingredienteTable = this.adaptersAndDataSet.dataSet.Tables["Ingrediente"];
            InitializeComponent();
            this.LoadData();
        }

        private void LoadData()
        {
            int codI;
            Ingredient ingredient;
            LinkedList<int> codIs = new LinkedList<int>();
            foreach (DataRow row in this.reteteTable.Rows)
                if (Convert.ToInt32(row["codP"]) == this.codP)
                    codIs.AddLast(Convert.ToInt32(row["codI"]));
            foreach (DataRow row in this.ingredienteTable.Rows)
            {
                codI = Convert.ToInt32(row["CodI"]);
                ingredient = new Ingredient(codI, row["numeI"].ToString(), row["unitate_masura"].ToString());
                if (codIs.Contains(codI))
                    this.ingredienteAdaugateListBox.Items.Add(ingredient);
                else
                    this.ingredienteNeadaugateListBox.Items.Add(ingredient);
            }
        }

        private void AdaugaIngredient(object sender, EventArgs e)
        {
            DataRow newDataRow;
            Ingredient aux = this.ingredienteNeadaugateListBox.SelectedItem as Ingredient;
            int selectedIndex = this.ingredienteNeadaugateListBox.SelectedIndex;
            if (selectedIndex != -1 && aux != null)
            {
                this.ingredienteNeadaugateListBox.Items.RemoveAt(selectedIndex);
                this.ingredienteAdaugateListBox.Items.Add(aux);
                newDataRow = this.reteteTable.NewRow();
                newDataRow["codP"] = this.codP;
                newDataRow["codI"] = aux.Cod;
                newDataRow["cantitate"] = this.cantitateNumericUpDown.Value;
                this.reteteTable.Rows.Add(newDataRow);
                this.adaptersAndDataSet.reteteDataAdapter.Update(new DataRow[] { newDataRow });
            }
        }

        private void StergeIngredient(object sender, EventArgs e)
        {
            object aux;
            int selectedIndex = this.ingredienteAdaugateListBox.SelectedIndex;
            if (selectedIndex != -1)
            {
                aux = this.ingredienteAdaugateListBox.Items[selectedIndex];
                this.ingredienteAdaugateListBox.Items.RemoveAt(selectedIndex);
                this.ingredienteNeadaugateListBox.Items.Add(aux);
                this.reteteTable.Rows[getActualIndex(this.codP, selectedIndex)].Delete();
                this.adaptersAndDataSet.reteteDataAdapter.Update(this.reteteTable);
            }
        }

        private void IngredientSelected(object sender, EventArgs e)
        {
            int selectedIndex = this.ingredienteAdaugateListBox.SelectedIndex;
            if (selectedIndex != -1)
                this.cantitateNumericUpDown.Value = Convert.ToDecimal(this.reteteTable.Rows[getActualIndex(this.codP, selectedIndex)]["cantitate"]);
        }

        private void QuantityChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.ingredienteAdaugateListBox.SelectedIndex;
            if (selectedIndex != -1)
            {
                this.reteteTable.Rows[getActualIndex(this.codP, selectedIndex)]["cantitate"] = this.cantitateNumericUpDown.Value;
                this.adaptersAndDataSet.reteteDataAdapter.Update(new DataRow[] { this.reteteTable.Rows[selectedIndex] });
            }
        }

        private int getActualIndex(int codP, int selectedIndex)
        {
            int index = -1;
            System.Collections.IEnumerator it = this.reteteTable.Rows.GetEnumerator();
            while (it.MoveNext() && selectedIndex >= 0)
            {
                if (Convert.ToInt32((it.Current as DataRow)["codP"]) == codP)
                    selectedIndex--;
                index++;
            }
            return index;
        }

        private int codP;
        private DataTable reteteTable;
        private DataTable ingredienteTable;
        private Form1.SqlDataAdaptersAndDataSet adaptersAndDataSet;
    }
}
