using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace IALab1
{
    public partial class MainView : Form
    {
        public MainView()
        {
            InitializeComponent();
            toolTip.SetToolTip(coefficientsLabel, "Enter all coin values separated by semicolons. The GCD of all numbers must be 1! Any number must be greater than 1!");
            toolTip.SetToolTip(maximumCoefficientValueLabel, "The maximum value the coin value coefficients can have (exclusive). The minimum value is 1!");
            this.Value = 0;
        }

        private uint Value { get; set; }

        private void NewSearchMethodHasBeenSelected(object sender, EventArgs e)
        {
            switch (this.searchTypeComboBox.SelectedIndex)
            {
                case 0:
                    if (this.bfs == null)
                        this.bfs = new BFS();
                    this.searchMethod = this.bfs;
                    EnableForStart();
                    break;
                case 1:
                    if (this.bfs == null)
                        this.gbfs = new GBFS();
                    this.searchMethod = this.gbfs;
                    EnableForStart();
                    break;
                default:
                    DisableForStart();
                    break;
            }
        }

        private void DisableForStart()
        {
            this.StartButton.Enabled = false;
        }

        private void EnableForStart()
        {
            this.StartButton.Enabled = true;
        }

        private void SearchEnded(ThreadedTaskResult result)
        {
            if (result == ThreadedTaskResult.Finished)
                this.label4.Invoke((MethodInvoker)delegate { this.searchStateLabel.Text = this.task.FrobeniusNumber.ToString(); });
            else if (result == ThreadedTaskResult.Aborted || result == ThreadedTaskResult.Interrupted)
                this.label4.Invoke((MethodInvoker)delegate { this.searchStateLabel.Text = "Aborted"; });
            else
                this.label4.Invoke((MethodInvoker)delegate { this.searchStateLabel.Text = "With errors"; });
            this.task = null;
            this.DisableForStop();
        }

        private void StartSearch(object sender, EventArgs e)
        {
            uint[] numbers;
            if (this.searchMethod != null)
            {
                numbers = ParseNumbers();
                if (numbers == null)
                    MessageBox.Show(this, "Invalid coinv values!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    EnableForStop();
                    task = new SearchThreadedTask(this.searchMethod);
                    task.ThreadEndCallback += this.SearchEnded;
                    task.StartTask(numbers);
                }
            }
            else
                MessageBox.Show(this, "No search method selected!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private uint[] ParseNumbers()
        {
            uint i = 0;
            string[] values = this.coinValues.Text.Split(',');
            uint[] numbers = new uint[values.Length];
            try
            {
                while (i < values.Length && (numbers[i] = Convert.ToUInt32(values[i])) > 1)
                    i++;
            }
            catch (Exception)
            {
                numbers = null;
            }
            if (i == values.Length && Gcd(numbers) == 1)
                return numbers;
            else
                return null;
        }

        private uint Gcd(uint[] numbers)
        {
            uint gcd = numbers[0];
            for (uint i = 1; i < numbers.Length; i++)
                gcd = Gcd(gcd, numbers[i]);
            return gcd;
        }

        private uint Gcd(uint a, uint b)
        {
            uint c;
            while (b != 0)
            {
                c = a % b;
                a = b;
                b = c;
            }
            return a;
        }

        private void CancelSearch(object sender, System.EventArgs e)
        {
            this.task.AbortTask();
        }

        private void EnableForStop()
        {
            this.StartButton.Enabled = false;
            this.stopButton.Enabled = true;
        }

        private void DisableForStop()
        {
            this.StartButton.Invoke((MethodInvoker)delegate { this.StartButton.Enabled = true; });
            this.stopButton.Invoke((MethodInvoker)delegate { this.stopButton.Enabled = false; });
        }

        private SearchThreadedTask task = null;
        private SearchMethod searchMethod = null;
        private SearchMethod bfs = null;
        private SearchMethod gbfs = null;
    }
}
