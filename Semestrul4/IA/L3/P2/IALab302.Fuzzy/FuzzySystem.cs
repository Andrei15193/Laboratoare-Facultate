using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace IALab302.Fuzzy
{
    public class FuzzySystem
    {
        public void Add(params KeyValuePair<string, string>[] variableLabelPairs)
        {
            DataRow newRule = _rules.NewRow();
            foreach (KeyValuePair<string, string> variableLabelPair in variableLabelPairs)
                newRule[variableLabelPair.Key] = _variables.Where((variable) => string.Compare(variable.Name, variableLabelPair.Key, true) == 0).First()
                    .Labels.Where((label) => label.Name == variableLabelPair.Value).First();
            _rules.Rows.Add(newRule);
        }

        public void Add(FuzzyVariable variable)
        {
            _rules.Columns.Add(variable.Name, typeof(FuzzyLabel));
            _variables.Add(variable);
        }

        public IEnumerable<DataPoint> Infer(string output, params KeyValuePair<string, double>[] inputs)
        {
            KeyValuePair<FuzzyLabel, double>[] results = new KeyValuePair<FuzzyLabel, double>[_rules.Rows.Count];
            for (int i = 0; i < _rules.Rows.Count; i++)
                results[i] = new KeyValuePair<FuzzyLabel, double>(_rules.Rows[i][output] as FuzzyLabel, ApplyRules(_rules.Rows[i], inputs).Min());
            return GetPoints(results);
        }

        public IEnumerable<FuzzyVariable> Variables
        {
            get
            {
                return _variables;
            }
        }

        public DataTable Rules
        {
            get
            {
                return _rules;
            }
        }

        private IEnumerable<double> ApplyRules(DataRow dataRow, KeyValuePair<string, double>[] inputs)
        {
            foreach (KeyValuePair<string, double> input in inputs)
                yield return (dataRow[input.Key] as FuzzyLabel).Membership(input.Value);
        }

        private IEnumerable<DataPoint> GetPoints(KeyValuePair<FuzzyLabel, double>[] results)
        {
            var plots = from result in results
                        where result.Value > 0
                        group result.Value by result.Key into grouping
                        let plotsWithMaximumMembership = new
                                                         {
                                                             Plot = grouping.Key,
                                                             Membership = grouping.Max()
                                                         }
                        orderby plotsWithMaximumMembership.Plot.Series.Points[0].XValue
                        select new
                        {
                            Points = plotsWithMaximumMembership.Plot.Series.Points.Intersect(plotsWithMaximumMembership.Membership),
                            plotsWithMaximumMembership.Membership
                        };
            switch (plots.Count())
            {
                case 0:
                    return new DataPoint[0];
                case 1:
                    return Normalize(plots.First().Points);
                default:
                    var previous = plots.GetEnumerator();
                    previous.MoveNext();
                    var current = plots.GetEnumerator();
                    current.MoveNext();
                    IEnumerable<DataPoint> points = previous.Current.Points;
                    for (; current.MoveNext(); previous.MoveNext())
                    {
                        var previousLastTwo = previous.Current.Points.Skip(previous.Current.Points.Count() - 2);
                        var currentFirstTwo = current.Current.Points.Take(2);
                        DataPoint intersect = Extensions.Intersect(previousLastTwo.First(), previousLastTwo.Last(), currentFirstTwo.First(), currentFirstTwo.Last());
                        if (intersect.YValues[0] <= previousLastTwo.First().YValues[0] && intersect.YValues[0] <= currentFirstTwo.Last().YValues[0])
                            points = points.Take(points.Count() - 1).Concat(intersect).Concat(current.Current.Points.Skip(1));
                        else
                            if (previousLastTwo.First().YValues[0] < currentFirstTwo.Last().YValues[0])
                            {
                                intersect = Extensions.IntersectY(currentFirstTwo.First(), currentFirstTwo.Last(), previous.Current.Membership);
                                points = points.Take(points.Count() - 2).Concat(intersect).Concat(current.Current.Points.Skip(1));
                            }
                            else
                            {
                                intersect = Extensions.IntersectY(previousLastTwo.First(), previousLastTwo.Last(), current.Current.Membership);
                                points = points.Take(points.Count() - 1).Concat(intersect).Concat(current.Current.Points.Skip(2));
                            }
                    }
                    return Normalize(points);
            }
        }

        private IEnumerable<DataPoint> Normalize(IEnumerable<DataPoint> points)
        {
            LinkedList<DataPoint> pointsToKeep = new LinkedList<DataPoint>();
            var previous = points.GetEnumerator();
            previous.MoveNext();
            var current = points.GetEnumerator();
            current.MoveNext();
            current.MoveNext();
            while (current.MoveNext())
            {
                pointsToKeep.AddLast(previous.Current);
                DataPoint previousDataPoint = previous.Current;
                previous.MoveNext();
                while (Extensions.Membership(previousDataPoint, current.Current, previous.Current) && current.MoveNext())
                    previous.MoveNext();
            }
            if (!CheckEquality(previous.Current, pointsToKeep.Last()))
                pointsToKeep.AddLast(previous.Current);
            previous.MoveNext();
            pointsToKeep.AddLast(previous.Current);
            return pointsToKeep;
        }

        private static bool CheckEquality(DataPoint dataPoint1, DataPoint dataPoint2)
        {
            return (dataPoint1.XValue == dataPoint2.XValue && dataPoint1.YValues[0] == dataPoint2.YValues[0]);
        }

        private DataTable _rules = new DataTable();
        private ICollection<FuzzyVariable> _variables = new LinkedList<FuzzyVariable>();
    }

}
