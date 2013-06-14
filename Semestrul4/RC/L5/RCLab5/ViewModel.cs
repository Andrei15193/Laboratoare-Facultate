using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace RCLab5
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {
            NodeAddress = string.Empty;
            NetworkClass = string.Empty;
            routingTable = new DataTable();
            routingTable.Columns.AddRange(
                new DataColumn[]{
                    new DataColumn("Destination", typeof(string)),
                    new DataColumn("Neighbour", typeof(string)),
                    new DataColumn("Metric", typeof(int))
                }
            );
            routingTable.PrimaryKey = new DataColumn[] { routingTable.Columns["Destination"], routingTable.Columns["Neighbour"] };
        }

        public void Start(MainForm iHaveNoIdeaWhyIHaveToDoThis)
        {
            this.iHaveNoIdeaWhyIHaveToDoThis = iHaveNoIdeaWhyIHaveToDoThis;
            namedPipeServerStream = new NamedPipeServerStream(@"RCLab5\" + NodeAddress, PipeDirection.In, 100, PipeTransmissionMode.Byte, PipeOptions.WriteThrough);
            routingTable.Rows.Add(NetworkClass, NodeAddress, 0);
            Task task = Task.Factory.StartNew(PipeListen, iHaveNoIdeaWhyIHaveToDoThis);
        }

        private void PipeListen(object obj)
        {
            try
            {
                if (obj is MainForm)
                {
                    MainForm iHaveNoIdeaWhyIHaveToDoThis = obj as MainForm;
                    bool hasChanges = true;
                    XmlSerializer serializer = new XmlSerializer(typeof(RoutingTable));
                    Monitor.Enter(runsLock);
                    while (runs)
                    {
                        Monitor.Exit(runsLock);
                        Debug.WriteLine("waiting for con");
                        namedPipeServerStream.WaitForConnection();
                        Debug.WriteLine("conned");
                        RoutingTable neighbourRoutingTable = serializer.Deserialize(namedPipeServerStream) as RoutingTable;
                        namedPipeServerStream.Disconnect();
                        Debug.WriteLine("d-conned");
                        if (UpdateRoutingTable(neighbourRoutingTable, iHaveNoIdeaWhyIHaveToDoThis))
                            SendRoutingTable(serializer, iHaveNoIdeaWhyIHaveToDoThis);
                        Monitor.Enter(runsLock);
                    }
                    Monitor.Exit(runsLock);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine("");
                Debug.WriteLine("");
                Debug.WriteLine(exception);
                Debug.WriteLine("");
                Debug.WriteLine("");
            }
        }

        private bool UpdateRoutingTable(RoutingTable neighbourRoutingTable, MainForm iHaveNoIdeaWhyIHaveToDoThis)
        {
            lock (routingLock)
            {
                bool hasChanges = false;
                IEnumerable<Route> newRoutes = neighbourRoutingTable.Routes.Where((route) => !routingTable.Rows.Contains(new object[] { route.Destination, route.Neighbour })).ToArray();
                if (newRoutes.Count() > 0)
                {
                    hasChanges = true;
                    foreach (Route newRoute in newRoutes)
                    {
                        DataRow existingRow = null;
                        try
                        {
                            existingRow = routingTable.Rows.Find(new object[] { newRoute.Destination, newRoute.Neighbour });
                        }
                        catch (IndexOutOfRangeException)
                        {
                        }
                        if (iHaveNoIdeaWhyIHaveToDoThis.InvokeRequired)
                            iHaveNoIdeaWhyIHaveToDoThis.Invoke((Action)(() =>
                                {
                                    if (existingRow != null)
                                    {
                                        if (newRoute.Metric < Convert.ToInt32(existingRow["Metric"]))
                                            existingRow["Metric"] = newRoute.Metric + 1;
                                    }
                                    else
                                        routingTable.Rows.Add(newRoute.Destination, newRoute.Neighbour, newRoute.Metric + 1);
                                })
                            );
                        else
                            if (existingRow != null)
                            {
                                if (newRoute.Metric < Convert.ToInt32(existingRow["Metric"]))
                                    existingRow["Metric"] = newRoute.Metric + 1;
                            }
                            else
                                routingTable.Rows.Add(newRoute.Destination, newRoute.Neighbour, newRoute.Metric + 1);
                    }
                }
                IEnumerable<string> neighboursInRoutingTable = (from row in routingTable.Select()
                                                                select row["Neighbour"].ToString()).Distinct().ToArray();
                foreach (string neighbourToAdd in neighboursInRoutingTable.Except(this.neighboursInRoutingTable).ToArray())
                    if (neighbourToAdd != NodeAddress)
                        if (iHaveNoIdeaWhyIHaveToDoThis.InvokeRequired)
                            iHaveNoIdeaWhyIHaveToDoThis.Invoke((Action)(() =>
                                {
                                    this.neighboursInRoutingTable.Add(neighbourToAdd);
                                })
                            );
                        else
                            this.neighboursInRoutingTable.Add(neighbourToAdd);
                return hasChanges;
            }
        }

        private void SendRoutingTable(XmlSerializer serializer, MainForm iHaveNoIdeaWhyIHaveToDoThis)
        {
            RoutingTable routingTable;
            IEnumerable<string> neighboursToSendTo;
            lock (routingLock)
            {
                routingTable = new RoutingTable(NodeAddress, (from row in this.routingTable.Select()
                                                              select new Route(row["Destination"].ToString(), NodeAddress, Convert.ToInt32(row["Metric"]))).ToArray());
                neighboursToSendTo = neighboursInRoutingTable.ToArray();
            }
            //Parallel.ForEach(neighboursInRoutingTable, (neighbour) =>
            foreach (var neighbour in neighboursToSendTo)
                {
                    using (NamedPipeClientStream client = new NamedPipeClientStream(".", @"RCLab5\" + neighbour, PipeDirection.Out))
                    {
                        try
                        {
                            Debug.WriteLine("Conning to " + neighbour);
                            client.Connect(10000);
                            Debug.WriteLine("Conned to " + neighbour);
                            serializer.Serialize(client, routingTable);
                            client.Flush();
                        }
                        catch (Exception)
                        {
                            neighboursInRoutingTable.Remove(neighbour);
                        }
                    }
                    Debug.WriteLine("D-Conned from " + neighbour);
                }
           // );
        }

        public bool ValidateNodeAddress()
        {
            return (NodeAddress != null && Regex.IsMatch(NodeAddress, addressValidationRegEx, RegexOptions.Compiled));
        }

        public bool ValidateNetworkClass()
        {
            return (NetworkClass != null && Regex.IsMatch(NetworkClass, networkClassValidationRegEx, RegexOptions.Compiled) && CheckNetowrkClassValidity());
        }

        public void AddNeighbour(string neighbour)
        {
            lock (routingLock)
                if (neighbour != NodeAddress && !neighboursInRoutingTable.Contains(neighbour))
                {
                    neighboursInRoutingTable.Add(neighbour);
                    SendRoutingTable(new XmlSerializer(typeof(RoutingTable)), iHaveNoIdeaWhyIHaveToDoThis);
                }
        }

        private bool CheckNetowrkClassValidity()
        {
            string[] networkClassParts = networkClass.Split('/');
            byte[] netmask = new byte[4];
            byte[] assumedNetworkAddress = (from networkAddressByte in networkClassParts[0].Split('.')
                                            select byte.Parse(networkAddressByte)).ToArray();
            for (byte i = 0, networkClassSize = byte.Parse(networkClassParts[1]); i < 4; i++, networkClassSize -= 8)
                netmask[i] = (byte)(256 - Math.Pow(2, networkClassSize > 8 ? 0 : 8 - networkClassSize));
            byte j = 0;
            while (j < 4 && (byte)(netmask[j] & assumedNetworkAddress[j]) == assumedNetworkAddress[j])
                j++;
            return j == 4;
        }

        public string NodeAddress
        {
            get
            {
                return nodeAddress;
            }
            set
            {
                nodeAddress = value;
                OnPropertyChanged("NodeAddress");
            }
        }

        public string NetworkClass
        {
            get
            {
                return networkClass;
            }
            set
            {
                networkClass = value;
                OnPropertyChanged("NetworkClass");
            }
        }

        public DataView RoutingTableView
        {
            get
            {
                return routingTable.DefaultView;
            }
        }

        public IEnumerable<string> Neighbours
        {
            get
            {
                return neighboursInRoutingTable;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool runs = true;
        private NamedPipeServerStream namedPipeServerStream;
        private string nodeAddress;
        private string networkClass;
        private readonly Object runsLock = new Object();
        private readonly Object routingLock = new Object();
        private readonly DataTable routingTable;
        private readonly string addressValidationRegEx = @"^(([01]?[0-9]{1,2}|2([0-4][0-9]|5[0-5]))\.){3}([01]?[0-9]{1,2}|2([0-4][0-9]|5[0-5]))$";
        private readonly string networkClassValidationRegEx = @"^(([01]?[0-9]{1,2}|2([0-4][0-9]|5[0-5]))\.){3}([01]?[0-9]{1,2}|2([0-4][0-9]|5[0-5]))/([0-2]?[0-9]|30)$";
        private readonly BindingList<string> neighboursInRoutingTable = new BindingList<string>();
        private readonly RCLab5.EqualityComparer<Route> routeComparer = new EqualityComparer<Route>((x, y) => (x.Destination == y.Destination && x.Neighbour == y.Neighbour),
                                                                                                    (x) => new { x.Destination, x.Neighbour }.GetHashCode());
        private MainForm iHaveNoIdeaWhyIHaveToDoThis;
    }
}
