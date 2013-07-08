using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace IALab302.Fuzzy
{
    public class FuzzyLabel
    {
        public FuzzyLabel(string name, IEnumerable<DataPoint> serie)
        {
            series = new Series(name);
            foreach (DataPoint dataPoint in serie)
                series.Points.Add(new DataPoint(dataPoint.XValue, dataPoint.YValues));
        }

        public FuzzyLabel(string name, params DataPoint[] serie)
            : this(name, serie as IEnumerable<DataPoint>)
        {
        }

        public double Membership(double x)
        {
            int i = 1;
            while (i < series.Points.Count && (series.Points[i].XValue < x || series.Points[i - 1].XValue == series.Points[i].XValue))
                i++;
            if (i >= series.Points.Count)
                return 0;
            else
            {
                double previousX = series.Points[i - 1].XValue, previousY = series.Points[i - 1].YValues[0];
                double currentX = series.Points[i].XValue, currentY = series.Points[i].YValues[0];
                double a = currentY - previousY, b = previousX - currentX, c = previousY * currentX - currentY * previousX;
                return (-a * x - c) / b;
            }
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj is FuzzyLabel && (obj as FuzzyLabel).Name == Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

        public string Name
        {
            get
            {
                return series.Name;
            }

            set
            {
                series.Name = value;
            }
        }

        public Series Series
        {
            get
            {
                return series;
            }
        }

        private Series series;
    }
}
