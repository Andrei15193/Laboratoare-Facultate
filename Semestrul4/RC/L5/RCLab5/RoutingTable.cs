using System.Collections.Generic;
using System.Xml.Serialization;

namespace RCLab5
{
    [XmlRoot("RoutingTable")]
    public class RoutingTable
    {
        public RoutingTable(string source, IEnumerable<Route> routes)
        {
            Source = source;
            this.routes = new LinkedList<Route>(routes);
        }

        public RoutingTable(string source)
            : this(source, new Route[0])
        {
        }

        public RoutingTable()
            : this(string.Empty, new Route[0])
        {
        }

        public string Source { get; set; }

        [XmlArray]
        public Route[] Routes
        {
            get
            {
                Route[] routes = new Route[this.routes.Count];
                this.routes.CopyTo(routes, 0);
                return routes;
            }
            set
            {
                routes.Clear();
                foreach (Route route in value)
                    routes.AddLast(route);
            }
        }

        [XmlIgnore]
        public LinkedList<Route> RoutingList
        {
            get
            {
                return routes;
            }
        }

        private readonly LinkedList<Route> routes;
    }
}
