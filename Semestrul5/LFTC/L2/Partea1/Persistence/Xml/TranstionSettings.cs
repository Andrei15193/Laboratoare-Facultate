using System.Xml.Serialization;

namespace Partea1.Persistence.Xml
{
    public struct TranstionSettings
    {
        [XmlAttribute]
        public string SourceState
        {
            get;
            set;
        }

        [XmlAttribute]
        public string DestinationState
        {
            get;
            set;
        }

        [XmlAttribute]
        public char TranstitingCharacter
        {
            get;
            set;
        }
    }
}
