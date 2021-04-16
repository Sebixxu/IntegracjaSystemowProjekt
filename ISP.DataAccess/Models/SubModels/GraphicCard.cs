using System.Xml.Serialization;

namespace IntegracjaSystemowProjekt.Models
{
    public class GraphicCard
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("memory")]
        public string Memory { get; set; }
    }
}