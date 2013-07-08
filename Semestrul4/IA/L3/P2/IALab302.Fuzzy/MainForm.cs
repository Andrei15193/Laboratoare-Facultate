using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace IALab302.Fuzzy
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            _resultSeries = new Series();
            _centroidSeries = new Series();
            InitializeComponent();
            InitializeFuzzySystem();
            SetUpTemepratureChart();
            SetUpCapacityChart();
            SetUpPowerChart();
            foreach (ChartArea chartArea in _chart.ChartAreas)
            {
                chartArea.AxisY.Maximum = 1;
                chartArea.AxisY.Minimum = 0;
            }
            _chart.ChartAreas["temperatureChartArea"].AxisX.Minimum = 20;
            _chart.ChartAreas["temperatureChartArea"].AxisX.Maximum = 120;
            _chart.ChartAreas["capacityChartArea"].AxisX.Minimum = 0;
            _chart.ChartAreas["capacityChartArea"].AxisX.Maximum = 10;
            _chart.ChartAreas["powerChartArea"].AxisX.Minimum = 0;
            _chart.ChartAreas["powerChartArea"].AxisX.Maximum = 20;
        }

        private void SetUpTemepratureChart()
        {
            int i = 0;
            foreach (FuzzyLabel label in _fuzzySystem.Variables.Where((variable) => variable.Name == "Temperatura").First().Labels)
            {
                Series series = new Series();
                series.ChartType = SeriesChartType.Line;
                series.BorderWidth = 2;
                series.Color = _pallete[i++];
                series.ChartArea = "temperatureChartArea";
                foreach (DataPoint point in label.Series.Points)
                    series.Points.Add(point);
                series.Legend = "temperatureLegend";
                series.LegendText = label.Name;
                _chart.Series.Add(series);
            }
        }

        private void SetUpCapacityChart()
        {
            int i = 0;
            foreach (FuzzyLabel label in _fuzzySystem.Variables.Where((variable) => variable.Name == "Capacitate").First().Labels)
            {
                Series series = new Series();
                series.ChartType = SeriesChartType.Line;
                series.BorderWidth = 2;
                series.Color = _pallete[i++];
                series.ChartArea = "capacityChartArea";
                foreach (DataPoint point in label.Series.Points)
                    series.Points.Add(point);
                series.Legend = "capacityLegend";
                series.LegendText = label.Name;
                _chart.Series.Add(series);
            }
        }

        private void SetUpPowerChart()
        {
            int i = 0;
            foreach (FuzzyLabel label in _fuzzySystem.Variables.Where((variable) => variable.Name == "Putere furnal").First().Labels)
            {
                Series series = new Series();
                series.ChartType = SeriesChartType.Line;
                series.BorderWidth = 2;
                series.Color = _pallete[i++];
                series.ChartArea = "powerChartArea";
                foreach (DataPoint point in label.Series.Points)
                    series.Points.Add(point);
                series.Legend = "powerLegend";
                series.LegendText = label.Name;
                _chart.Series.Add(series);
            }
            _resultSeries.IsVisibleInLegend = false;
            _resultSeries.LegendText = "Rezultat Fuzzy";
            _resultSeries.Legend = "powerLegend";
            _resultSeries.ChartType = SeriesChartType.Area;
            _resultSeries.ChartArea = "powerChartArea";
            _resultSeries.Color = Color.FromArgb(255, 202, 72);
            _resultSeries.BackSecondaryColor = Color.FromArgb(254, 132, 2);
            _resultSeries.BackGradientStyle = GradientStyle.TopBottom;
            _centroidSeries.IsVisibleInLegend = false;
            _centroidSeries.LegendText = "Centroid Fuzzy";
            _centroidSeries.Legend = "powerLegend";
            _centroidSeries.Color = Color.Red;
            _centroidSeries.ChartType = SeriesChartType.Point;
            _centroidSeries.ChartArea = "powerChartArea";
            _chart.Series.Add(_resultSeries);
            _chart.Series.Add(_centroidSeries);
        }

        private void InitializeFuzzySystem()
        {
            FuzzyVariable fuzzyVariable = new FuzzyVariable("Temperatura");
            fuzzyVariable.Labels.Add(new FuzzyLabel("Rece",
                new DataPoint(20, 0),
                new DataPoint(20, 1),
                new DataPoint(30, 1),
                new DataPoint(50, 0)));
            fuzzyVariable.Labels.Add(new FuzzyLabel("Racoare",
                new DataPoint(30, 0),
                new DataPoint(50, 1),
                new DataPoint(70, 0)));
            fuzzyVariable.Labels.Add(new FuzzyLabel("Moderat",
                new DataPoint(60, 0),
                new DataPoint(70, 1),
                new DataPoint(80, 0)));
            fuzzyVariable.Labels.Add(new FuzzyLabel("Cald",
                new DataPoint(70, 0),
                new DataPoint(90, 1),
                new DataPoint(110, 0)));
            fuzzyVariable.Labels.Add(new FuzzyLabel("Fierbinte",
                new DataPoint(90, 0),
                new DataPoint(110, 1),
                new DataPoint(120, 1),
                new DataPoint(120, 0)));
            _fuzzySystem.Add(fuzzyVariable);
            fuzzyVariable = new FuzzyVariable("Capacitate");
            fuzzyVariable.Labels.Add(new FuzzyLabel("Mica",
                new DataPoint(0, 0),
                new DataPoint(0, 1),
                new DataPoint(5, 0)));
            fuzzyVariable.Labels.Add(new FuzzyLabel("Medie",
                new DataPoint(3, 0),
                new DataPoint(5, 1),
                new DataPoint(7, 0)));
            fuzzyVariable.Labels.Add(new FuzzyLabel("Mare",
                new DataPoint(5, 0),
                new DataPoint(10, 1),
                new DataPoint(10, 0)));
            _fuzzySystem.Add(fuzzyVariable);
            fuzzyVariable = new FuzzyVariable("Putere furnal");
            fuzzyVariable.Labels.Add(new FuzzyLabel("Mica",
                new DataPoint(0, 0),
                new DataPoint(0, 1),
                new DataPoint(10, 0)));
            fuzzyVariable.Labels.Add(new FuzzyLabel("Medie",
                new DataPoint(5, 0),
                new DataPoint(10, 1),
                new DataPoint(15, 0)));
            fuzzyVariable.Labels.Add(new FuzzyLabel("Mare",
                new DataPoint(10, 0),
                new DataPoint(20, 1),
                new DataPoint(20, 0)));
            _fuzzySystem.Add(fuzzyVariable);

            _fuzzySystem.Add(
                new KeyValuePair<string, string>("Temperatura", "Rece"),
                new KeyValuePair<string, string>("Capacitate", "Mica"),
                new KeyValuePair<string, string>("Putere furnal", "Mica")
            );
            _fuzzySystem.Add(
                new KeyValuePair<string, string>("Temperatura", "Rece"),
                new KeyValuePair<string, string>("Capacitate", "Medie"),
                new KeyValuePair<string, string>("Putere furnal", "Medie")
            );
            _fuzzySystem.Add(
                new KeyValuePair<string, string>("Temperatura", "Rece"),
                new KeyValuePair<string, string>("Capacitate", "Mare"),
                new KeyValuePair<string, string>("Putere furnal", "Mare")
            );
            _fuzzySystem.Add(
                new KeyValuePair<string, string>("Temperatura", "Racoare"),
                new KeyValuePair<string, string>("Capacitate", "Mica"),
                new KeyValuePair<string, string>("Putere furnal", "Mica")
            );
            _fuzzySystem.Add(
                new KeyValuePair<string, string>("Temperatura", "Racoare"),
                new KeyValuePair<string, string>("Capacitate", "Medie"),
                new KeyValuePair<string, string>("Putere furnal", "Medie")
            );
            _fuzzySystem.Add(
                new KeyValuePair<string, string>("Temperatura", "Racoare"),
                new KeyValuePair<string, string>("Capacitate", "Mare"),
                new KeyValuePair<string, string>("Putere furnal", "Mare")
            );
            _fuzzySystem.Add(
                new KeyValuePair<string, string>("Temperatura", "Moderat"),
                new KeyValuePair<string, string>("Capacitate", "Mica"),
                new KeyValuePair<string, string>("Putere furnal", "Mica")
            );
            _fuzzySystem.Add(
                new KeyValuePair<string, string>("Temperatura", "Moderat"),
                new KeyValuePair<string, string>("Capacitate", "Medie"),
                new KeyValuePair<string, string>("Putere furnal", "Mica")
            );
            _fuzzySystem.Add(
                new KeyValuePair<string, string>("Temperatura", "Moderat"),
                new KeyValuePair<string, string>("Capacitate", "Mare"),
                new KeyValuePair<string, string>("Putere furnal", "Mica")
            );
            _fuzzySystem.Add(
                new KeyValuePair<string, string>("Temperatura", "Cald"),
                new KeyValuePair<string, string>("Capacitate", "Mica"),
                new KeyValuePair<string, string>("Putere furnal", "Mica")
            );
            _fuzzySystem.Add(
                new KeyValuePair<string, string>("Temperatura", "Cald"),
                new KeyValuePair<string, string>("Capacitate", "Medie"),
                new KeyValuePair<string, string>("Putere furnal", "Mica")
            );
            _fuzzySystem.Add(
                new KeyValuePair<string, string>("Temperatura", "Cald"),
                new KeyValuePair<string, string>("Capacitate", "Mare"),
                new KeyValuePair<string, string>("Putere furnal", "Mica")
            );
            _fuzzySystem.Add(
                new KeyValuePair<string, string>("Temperatura", "Fierbinte"),
                new KeyValuePair<string, string>("Capacitate", "Mica"),
                new KeyValuePair<string, string>("Putere furnal", "Mica")
            );
            _fuzzySystem.Add(
                new KeyValuePair<string, string>("Temperatura", "Fierbinte"),
                new KeyValuePair<string, string>("Capacitate", "Medie"),
                new KeyValuePair<string, string>("Putere furnal", "Mica")
            );
            _fuzzySystem.Add(
                new KeyValuePair<string, string>("Temperatura", "Fierbinte"),
                new KeyValuePair<string, string>("Capacitate", "Mare"),
                new KeyValuePair<string, string>("Putere furnal", "Mica")
            );
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            _centroidSeries.IsVisibleInLegend = false;
            _resultSeries.IsVisibleInLegend = false;
            _centroidSeries.Points.Clear();
            _resultSeries.Points.Clear();
            IEnumerable<DataPoint> result = _fuzzySystem.Infer("Putere furnal",
                new KeyValuePair<string, double>("Temperatura", (double)_temperatureNumericUpDown.Value),
                new KeyValuePair<string, double>("Capacitate", (double)_capacityNumericUpDown.Value));
            Debug.WriteLine("Plot:");
            foreach (DataPoint point in result)
            {
                Debug.WriteLine("({0}, {1})", point.XValue, point.YValues[0]);
                _resultSeries.Points.Add(point);
            }
            double centroidX = result.Select((point) => point.XValue).Average(), centroidY = result.Select((point) => point.YValues[0]).Average();
            _centroidSeries.Points.AddXY(centroidX, centroidY);
            _centroidSeries.ToolTip = string.Format("({0}, {1})", centroidX, centroidY);
            _resultSeries.IsVisibleInLegend = true;
            _centroidSeries.IsVisibleInLegend = true;
        }

        private void _rulesButton_Click(object sender, EventArgs e)
        {
            new RulesForm(_fuzzySystem.Rules).ShowDialog();
        }
        
        private FuzzySystem _fuzzySystem = new FuzzySystem();
        private readonly Series _resultSeries;
        private readonly Series _centroidSeries;
        private static readonly Color[] _pallete = new Color[]
        {
            Color.FromArgb(65, 146, 75),
            Color.FromArgb(107, 202, 226),
            Color.FromArgb(175, 234, 170),
            Color.FromArgb(81, 165, 186),
            Color.FromArgb(135, 226, 147)
        };
    }
}
