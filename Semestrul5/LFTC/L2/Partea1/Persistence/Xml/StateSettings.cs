using System.Xml.Serialization;

namespace Partea1.Persistence.Xml
{
    public struct StateSettings
    {
        [XmlAttribute]
        public string Name
        {
            get;
            set;
        }
    }
}
