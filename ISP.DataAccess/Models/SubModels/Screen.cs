using System.Xml.Serialization;

namespace IntegracjaSystemowProjekt.Models
{
    public class Screen
    {
        [XmlAttribute("touch")]
        public string IsTouchable { get; set; } //tempo string

        [XmlElement("size")]
        public string Size { get; set; }

        [XmlElement("resolution")]
        public string Resolution { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }
    }
}