using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTBDDLab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            sqlCommand = new SqlCommand() { Connection = sqlConnection, CommandType = CommandType.StoredProcedure };
            dataAdapter.SelectCommand = sqlCommand;
        }

        private void adaugaOfertaButton_Click(object sender, EventArgs e)
        {
            SqlParameter outputParameter = new SqlParameter("@out", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            sqlCommand.CommandText = "Problema2";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.Add(new SqlParameter("@codP", SqlDbType.Int) { Value = codProdusNumericUpDown.Value });
            sqlCommand.Parameters.Add(new SqlParameter("@codM", SqlDbType.Int) { Value = codMagazinNumericUpDown.Value });
            sqlCommand.Parameters.Add(new SqlParameter("@dela", SqlDbType.DateTime) { Value = deLaDateTimePicker.Value });
            sqlCommand.Parameters.Add(new SqlParameter("@panala", SqlDbType.DateTime) { Value = panaLaDateTimePicker.Value });
            sqlCommand.Parameters.Add(new SqlParameter("@pret", SqlDbType.Int) { Value = pretNumericUpDown.Value });
            sqlCommand.Parameters.Add(outputParameter);
            try
            {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                string value = outputParameter.Value.ToString();
                adaugaOfertaRezultatLabel.Text = adaugaOfertaMesajRezultat + (Convert.ToBoolean(outputParameter.Value.ToString()) ? "Da" : "Nu");
            }
            catch (SqlException exception)
            {
                MessageBox.Show(this, exception.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
        }

        private void ofertaZileiButton_Click(object sender, EventArgs e)
        {
            sqlCommand.CommandText = "Problema3";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.Add(new SqlParameter("@d", SqlDbType.Date) { Value = DateTime.Now });
            try
            {
                resultDataSet.Tables.Clear();
                dataAdapter.Fill(resultDataSet, "rezultat");
                ofertaZileiDataGridView.DataSource = resultDataSet.Tables["rezultat"].DefaultView;
            }
            catch (SqlException exception)
            {
                MessageBox.Show(this, exception.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
        }
        
        private readonly string adaugaOfertaMesajRezultat = "Noua oferta are cel mai bun pret? ";
        private readonly SqlConnection sqlConnection = new SqlConnection("Server= Andrei-Desktop; Database= AndreiLocal; Trusted_Connection= true;");
        private readonly SqlCommand sqlCommand;
        private readonly DataSet resultDataSet = new DataSet();
        private readonly SqlDataAdapter dataAdapter = new SqlDataAdapter();
    }
}
