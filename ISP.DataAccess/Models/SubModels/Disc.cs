using System.Xml.Serialization;

namespace IntegracjaSystemowProjekt.Models
{
    public class Disc
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlElement("storage")]
        public string Storage { get; set; }
    }
}