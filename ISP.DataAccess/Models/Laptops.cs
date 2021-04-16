using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace IntegracjaSystemowProjekt.Models
{
    [XmlRoot("laptops")]
    public class Laptops
    {
        [XmlAttribute("moddate")]
        public string ModDate { get; set; } //temp string

        [XmlElement("laptop")]
        public List<Laptop> LaptopsCollection { get; set; }
    }
}