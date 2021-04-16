using System.Xml.Serialization;

namespace IntegracjaSystemowProjekt.Models
{
    public class Laptop
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlElement("manufacturer")]
        public string Manufacturer { get; set; }

        [XmlElement("screen")]
        public Screen Screen { get; set; }

        [XmlElement("processor")]
        public Processor Processor { get; set; }

        [XmlElement("ram")]
        public string Ram { get; set; }

        [XmlElement("disc")]
        public Disc Disc { get; set; }

        [XmlElement("graphic_card")]
        public GraphicCard GraphicCard { get; set; }

        [XmlElement("os")]
        public string Os { get; set; }

        [XmlElement("disc_reader")]
        public string DiscReader { get; set; }
    }
}