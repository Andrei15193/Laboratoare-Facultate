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

namespace BDLab6
{
    public partial class Preparate : Form
    {
        public Preparate(Categorie categorie, Form1.SqlDataAdaptersAndDataSet sqlDataAdaptersAndDataSet)
        {
            this.categorie = categorie;
            this.adaptersAndDataSet = sqlDataAdaptersAndDataSet;
            this.reteteTable = this.adaptersAndDataSet.dataSet.Tables["Retete"];
            this.preparateTable = this.adaptersAndDataSet.dataSet.Tables["Preparate"];
            InitializeComponent();
            this.LoadData(categorie);
        }

        private void LoadData(Categorie categorie)
        {
            this.preparateListBox.Items.Clear();
            foreach (DataRow row in this.preparateTable.Rows)
                if (Convert.ToInt32(row["codC"]) == categorie.Cod)
                    this.preparateListBox.Items.Add(row["numeP"]);
        }

        private void PrepraratSelectat(object sender, EventArgs e)
        {
            int codP;
            LinkedList<int[]> retete = new LinkedList<int[]>();
            if (this.preparateListBox.SelectedIndex != -1){
                this.editeazaPreparatMenu.Enabled = true;
                this.afiseazaIngredienteMenuItem.Enabled = true;
                codP = Convert.ToInt32(this.preparateTable.Rows[this.preparateListBox.SelectedIndex]["codP"]);
                foreach (DataRow row in this.reteteTable.Rows)
                    if (codP == Convert.ToInt32(row["codP"]))
                        retete.AddLast(new int[]{Convert.ToInt32(row["codI"]), Convert.ToInt32(row["cantitate"])});
            }
            this.AfiseazaReteta(retete);
        }

        private void AfiseazaReteta(LinkedList<int[]> retete)
        {
            bool found;
            DataRow preparat;
            StringBuilder stringBuilder;
            if (this.preparateListBox.SelectedIndex != -1)
            {
                preparat = this.preparateTable.Rows[this.preparateListBox.SelectedIndex];
                stringBuilder = new StringBuilder();
                stringBuilder.Append("Pret: " + preparat["pret"] + ". Timp: " + preparat["timp_preparare"] + "\r\n");
                foreach (int[] entry in retete)
                {
                    found = false;
                    System.Collections.IEnumerator it = this.adaptersAndDataSet.dataSet.Tables["Ingrediente"].Rows.GetEnumerator();
                    while (!found && it.MoveNext())
                        if (Convert.ToInt32((it.Current as DataRow)["CodI"]) == entry[0])
                            found = true;
                    if (found)
                        stringBuilder.Append(entry[1] + " (" + (it.Current as DataRow)["unitate_masura"] + ") de " + (it.Current as DataRow)["numeI"].ToString().ToLower() + ", ");
                }
                
                this.retetaLabel.Text = stringBuilder.ToString().Substring(0, stringBuilder.Length - 2) + ".";
            }
        }

        private void AdaugaPreparat(object sender, EventArgs e)
        {
            using (Preparat preparat = new Preparat("Adauga"))
            {
                if (preparat.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    DataRow newRow = this.preparateTable.NewRow();
                    newRow["codC"] = this.categorie.Cod;
                    newRow["codP"] = GetMaxCodP() + 1;
                    newRow["numeP"] = preparat.NumePreparat;
                    newRow["pret"] = preparat.PretPreparat;
                    newRow["timp_preparare"] = preparat.DurataPreparare;
                    this.preparateTable.Rows.Add(newRow);
                    this.adaptersAndDataSet.preparateDataAdapter.Update(new DataRow[] {newRow});
                    this.preparateListBox.Items.Add(string.Copy(preparat.NumePreparat));
                }
            }
        }

        private int GetMaxCodP()
        {
            int value;
            int max = 0;
            foreach (DataRow row in this.preparateTable.Rows)
            {
                value = Convert.ToInt32(row["codP"]);
                if (value > max)
                    max = value;
            }
            return max;
        }

        private void ActualizeazaPreparat(object sender, EventArgs e)
        {
            int selectedIndex = this.preparateListBox.SelectedIndex;
            if (selectedIndex != -1){
                
                DataRow preparatRow = this.preparateTable.Rows[selectedIndex];
                using (Preparat preparat = new Preparat("Modifica", preparatRow["numeP"].ToString(), Convert.ToDecimal(preparatRow["pret"]), Convert.ToDecimal(preparatRow["timp_preparare"])))
                {
                    if (preparat.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        preparatRow["numeP"] = preparat.NumePreparat;
                        preparatRow["pret"] = preparat.PretPreparat;
                        preparatRow["timp_preparare"] = preparat.DurataPreparare;
                        this.adaptersAndDataSet.preparateDataAdapter.Update(new DataRow[] { preparatRow });
                        this.preparateListBox.Items[selectedIndex] = string.Copy(preparat.NumePreparat);
                    }
                }
            }
        }

        private void StergePreparat(object sender, EventArgs e)
        {
            int codP;
            int selectedIndex = this.preparateListBox.SelectedIndex;
            if (selectedIndex != -1)
            {
                codP = Convert.ToInt32(this.preparateTable.Rows[selectedIndex]["codP"]);
                foreach (DataRow row in this.reteteTable.Rows)
                    if (Convert.ToInt32(row["codP"]) == codP)
                        row.Delete();
                this.preparateTable.Rows[selectedIndex].Delete();
                this.adaptersAndDataSet.reteteDataAdapter.Update(this.reteteTable);
                this.adaptersAndDataSet.preparateDataAdapter.Update(this.preparateTable);
                this.preparateListBox.Items.RemoveAt(selectedIndex);
                this.editeazaPreparatMenu.Enabled = false;
                this.afiseazaIngredienteMenuItem.Enabled = false;
            }
        }

        private void AfiseazaIngrediente(object sender, EventArgs e)
        {
            int selectedIndex = this.preparateListBox.SelectedIndex;
            if (selectedIndex != -1)
                using (IngredienteView dialog = new IngredienteView(Convert.ToInt32(this.preparateTable.Rows[selectedIndex]["codP"]), this.adaptersAndDataSet))
                {
                    dialog.ShowDialog();
                    this.PrepraratSelectat(this, e);
                }
        }

        private Categorie categorie;
        private DataTable preparateTable;
        private DataTable reteteTable;
        private Form1.SqlDataAdaptersAndDataSet adaptersAndDataSet;
    }
}
