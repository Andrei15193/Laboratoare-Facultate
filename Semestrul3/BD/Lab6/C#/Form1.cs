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
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.sqlDataAdaptersAndDataSet.preparateDataAdapter = new SqlDataAdapter();
            this.sqlDataAdaptersAndDataSet.reteteDataAdapter = new SqlDataAdapter();
            this.sqlDataAdaptersAndDataSet.ingredienteDataAdapter = new SqlDataAdapter();
            this.sqlDataAdaptersAndDataSet.dataSet = new DataSet();
            InitializeComponent();
            this.SetSqlDataAdaptersAndDataSet();
            this.LoadData();
        }

        private void LoadData()
        {
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            command.Connection = this.dbConnection;
            command.CommandText = "select * from Categorii";
            dbConnection.Open();
            reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
                this.categoriiListBox.Items.Add(new Categorie(reader["numeC"].ToString(), Convert.ToInt32(reader["codC"])));
            reader.Close();
            this.sqlDataAdaptersAndDataSet.reteteDataAdapter.Fill(this.sqlDataAdaptersAndDataSet.dataSet, "Retete");
            this.sqlDataAdaptersAndDataSet.preparateDataAdapter.Fill(this.sqlDataAdaptersAndDataSet.dataSet, "Preparate");
            this.sqlDataAdaptersAndDataSet.ingredienteDataAdapter.Fill(this.sqlDataAdaptersAndDataSet.dataSet, "Ingrediente");
        }

        private void SetSqlDataAdaptersAndDataSet()
        {
            this.SetPreparateSqlDataAdapter();
            this.SetReteteSqlDataAdapter();
            this.SetIngredienteSqlDataAdapter();
        }

        private void SetPreparateSqlDataAdapter()
        {
            SqlCommand command = new SqlCommand("select codP, codC, numeP, pret, timp_preparare from Preparate", this.dbConnection);
            SqlCommandBuilder commandBuilder;
            this.sqlDataAdaptersAndDataSet.preparateDataAdapter.SelectCommand = command;
            commandBuilder = new SqlCommandBuilder(this.sqlDataAdaptersAndDataSet.preparateDataAdapter);
            this.sqlDataAdaptersAndDataSet.preparateDataAdapter.InsertCommand = commandBuilder.GetInsertCommand(true);
            this.sqlDataAdaptersAndDataSet.preparateDataAdapter.DeleteCommand = commandBuilder.GetDeleteCommand(true);
            this.sqlDataAdaptersAndDataSet.preparateDataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand(true);
        }

        private void SetReteteSqlDataAdapter()
        {
            SqlCommand command = new SqlCommand("select codP, codI, cantitate from Retete", this.dbConnection);
            SqlCommandBuilder commandBuilder;
            this.sqlDataAdaptersAndDataSet.reteteDataAdapter.SelectCommand = command;
            commandBuilder = new SqlCommandBuilder(this.sqlDataAdaptersAndDataSet.reteteDataAdapter);
            this.sqlDataAdaptersAndDataSet.reteteDataAdapter.InsertCommand = commandBuilder.GetInsertCommand(true);
            this.sqlDataAdaptersAndDataSet.reteteDataAdapter.DeleteCommand = commandBuilder.GetDeleteCommand(true);
            this.sqlDataAdaptersAndDataSet.reteteDataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand(true);
        }

        private void SetIngredienteSqlDataAdapter()
        {
            SqlCommand command = new SqlCommand("select codI, numeI, unitate_masura from Ingrediente", this.dbConnection);
            SqlCommandBuilder commandBuilder;
            this.sqlDataAdaptersAndDataSet.ingredienteDataAdapter.SelectCommand = command;
            commandBuilder = new SqlCommandBuilder(this.sqlDataAdaptersAndDataSet.ingredienteDataAdapter);
            this.sqlDataAdaptersAndDataSet.ingredienteDataAdapter.InsertCommand = commandBuilder.GetInsertCommand(true);
            this.sqlDataAdaptersAndDataSet.ingredienteDataAdapter.DeleteCommand = commandBuilder.GetDeleteCommand(true);
            this.sqlDataAdaptersAndDataSet.ingredienteDataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand(true);
        }

        private void CategorieSelectata(object sender, EventArgs e)
        {
            if (this.categoriiListBox.SelectedIndex != -1)
                this.afiseazaPreparate.Enabled = true;
        }

        private void AfiseazaPreparate(object sender, EventArgs e)
        {
            using (Preparate preparateDialog = new Preparate(this.categoriiListBox.SelectedItem as Categorie, this.sqlDataAdaptersAndDataSet))
            {
                preparateDialog.ShowDialog();
            }
        }

        public struct SqlDataAdaptersAndDataSet
        {
            public SqlDataAdapter preparateDataAdapter;
            public SqlDataAdapter reteteDataAdapter;
            public SqlDataAdapter ingredienteDataAdapter;
            public DataSet dataSet;
        }

        private SqlDataAdaptersAndDataSet sqlDataAdaptersAndDataSet;
        private SqlConnection dbConnection = new SqlConnection("Server=ANDREIWINDOWS8; Database=LocalDb; Trusted_Connection=yes;");

        private void DeschideEditorIngrediente(object sender, EventArgs e)
        {
            using (EditorIngrediente editor = new EditorIngrediente(this.sqlDataAdaptersAndDataSet))
            {
                editor.ShowDialog();
            }
        }
    }
}
