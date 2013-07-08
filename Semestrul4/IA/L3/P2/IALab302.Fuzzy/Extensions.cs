using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace IALab302.Fuzzy
{
    public static class Extensions
    {
        public static IEnumerable<DataPoint> Intersect(this IEnumerable<DataPoint> dataPointCollection, double y)
        {
            IEnumerator<DataPoint> dataPoint = dataPointCollection.Where((point) => point.YValues.Count() > 0).GetEnumerator();
            if (dataPoint.MoveNext())
            {
                double previousX = dataPoint.Current.XValue, previousY = dataPoint.Current.YValues[0];
                yield return new DataPoint(dataPoint.Current.XValue, Math.Min(dataPoint.Current.YValues[0], y));
                for (; dataPoint.MoveNext(); previousX = dataPoint.Current.XValue, previousY = dataPoint.Current.YValues[0])
                {
                    double currentX = dataPoint.Current.XValue, currentY = dataPoint.Current.YValues[0];
                    double a = currentY - previousY, b = previousX - currentX, c = previousX * currentY - currentX * previousY, intermediarX = (c - y * b) / a;
                    if (previousY != currentY && previousX <= intermediarX && intermediarX <= currentX)
                        yield return new DataPoint(intermediarX, y);
                    if (dataPoint.Current.YValues[0] <= y)
                        yield return new DataPoint(dataPoint.Current.XValue, new double[] { dataPoint.Current.YValues[0], dataPoint.Current.YValues.Where((yValue) => yValue <= y).Last() }.Distinct().ToArray());
                    else
                    {
                        yield return new DataPoint(dataPoint.Current.XValue, y);
                    }

                }
            }
        }

        public static IEnumerable<DataPoint> Concat(this IEnumerable<DataPoint> points, IEnumerable<DataPoint> pointsToAdd)
        {
            return new LinkedList<DataPoint>(points.Union(pointsToAdd));
        }

        public static IEnumerable<DataPoint> Concat(this IEnumerable<DataPoint> points, DataPoint pointToAdd)
        {
            LinkedList<DataPoint> newPoints = new LinkedList<DataPoint>(points);
            newPoints.AddLast(pointToAdd);
            return newPoints;
        }

        public static DataPoint Intersect(DataPoint dataPoint1, DataPoint dataPoint2, DataPoint dataPoint3, DataPoint dataPoint4)
        {
            double a1 = dataPoint2.YValues[0] - dataPoint1.YValues[0], b1 = dataPoint1.XValue - dataPoint2.XValue, c1 = dataPoint1.YValues[0] * dataPoint2.XValue - dataPoint2.YValues[0] * dataPoint1.XValue;
            double a2 = dataPoint4.YValues[0] - dataPoint3.YValues[0], b2 = dataPoint3.XValue - dataPoint4.XValue, c2 = dataPoint3.YValues[0] * dataPoint4.XValue - dataPoint4.YValues[0] * dataPoint3.XValue;
            double x = (c1 * b2 + b1 * c2) / (a1 * b2 - a2 * b1);
            return new DataPoint(x, (-a1 * x - c1) / b1);
        }

        public static DataPoint IntersectY(DataPoint dataPoint1, DataPoint dataPoint2, double y)
        {
            double a = dataPoint2.YValues[0] - dataPoint1.YValues[0], b = dataPoint1.XValue - dataPoint2.XValue, c = dataPoint1.YValues[0] * dataPoint2.XValue - dataPoint2.YValues[0] * dataPoint1.XValue;
            return new DataPoint((y * b + c) / (-a), y);
        }

        public static bool Membership(DataPoint dataPoint1, DataPoint dataPoint2, DataPoint dataPoint3)
        {
            double a = dataPoint2.YValues[0] - dataPoint1.YValues[0], b = dataPoint1.XValue - dataPoint2.XValue, c = dataPoint1.YValues[0] * dataPoint2.XValue - dataPoint2.YValues[0] * dataPoint1.XValue;
            return (a * dataPoint3.XValue + b * dataPoint3.YValues[0] + c == 0);
        }
    }
}
