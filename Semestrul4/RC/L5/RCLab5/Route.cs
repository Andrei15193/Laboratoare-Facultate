using System.Xml.Serialization;

namespace RCLab5
{
    [XmlRoot("Route")]
    public class Route
    {
        public Route(string destination, string neighbour, int metric)
        {
            Destination = destination;
            Neighbour = neighbour;
            Metric = metric;
        }

        public Route()
            : this(string.Empty, string.Empty, 0)
        {
        }

        public string Destination { get; set; }

        public string Neighbour { get; set; }

        public int Metric { get; set; }
    }
}
