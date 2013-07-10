using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IALab406
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Stream stream = null;
                try
                {
                    stream = File.Open(openFileDialog1.FileName, FileMode.Open);
                    _geneticAlgorithm = new BinaryFormatter().Deserialize(stream) as GeneticAlgorithm;
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Could not load GP data file!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (stream != null)
                        stream.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (_geneticAlgorithm == null)
                    _geneticAlgorithm = new GeneticAlgorithm();
                if (_geneticAlgorithm.PopulationDensity != numericUpDown1.Value)
                    _geneticAlgorithm.GeneratePopulation((int)numericUpDown1.Value);
                StreamReader reader = null;
                try
                {
                    reader = File.OpenText(openFileDialog1.FileName);
                    Tuple<RobotChromosome, double> winner = _geneticAlgorithm.Learn((int)numericUpDown2.Value, reader);
                    richTextBox1.Text = string.Format("The best chromosome for this learning process is {0} with Fitness {1}", winner.Item1.ToString(), winner.Item2.ToString());
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        Stream stream = null;
                        try
                        {
                            stream = File.OpenWrite(openFileDialog1.FileName);
                            new BinaryFormatter().Serialize(stream, _geneticAlgorithm);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(this, "Could not load GP data file!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            if (stream != null)
                                stream.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (reader != null)
                        reader.Close();
                }
            }
            else
                MessageBox.Show(this, "Could not load input data file!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private GeneticAlgorithm _geneticAlgorithm;
    }
}
