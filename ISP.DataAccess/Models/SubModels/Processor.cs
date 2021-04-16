using System.Xml.Serialization;

namespace IntegracjaSystemowProjekt.Models
{
    public class Processor
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlIgnore]
        public int? PhysicalCores { get; set; }

        [XmlElement("physical_cores")]
        public string PhysicalCoresAsText
        {
            get { return (PhysicalCores.HasValue) ? PhysicalCores.ToString() : null; }
            set { PhysicalCores = !string.IsNullOrEmpty(value) ? int.Parse(value) : default(int?); }
        }

        [XmlIgnore]
        public int? ClockSpeed { get; set; }

        [XmlElement("clock_speed")]
        public string ClockSpeedAsText
        {
            get { return (ClockSpeed.HasValue) ? ClockSpeed.ToString() : null; }
            set { ClockSpeed = !string.IsNullOrEmpty(value) ? int.Parse(value) : default(int?); }
        }
    }
}