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

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Laptop)) return false;

            return ((Laptop) obj).Manufacturer == this.Manufacturer &&
                   ((Laptop) obj).Screen.Resolution == this.Screen.Resolution &&
                   ((Laptop) obj).Screen.IsTouchable == this.Screen.IsTouchable &&
                   ((Laptop) obj).Screen.Size == this.Screen.Size &&
                   ((Laptop) obj).Screen.Type == this.Screen.Type &&
                   ((Laptop) obj).Processor.ClockSpeedAsText == this.Processor.ClockSpeedAsText &&
                   ((Laptop) obj).Processor.PhysicalCoresAsText == this.Processor.PhysicalCoresAsText &&
                   ((Laptop) obj).Processor.Name == this.Processor.Name &&
                   ((Laptop) obj).Ram == this.Ram &&
                   ((Laptop) obj).Disc.Type == this.Disc.Type &&
                   ((Laptop) obj).Disc.Storage == this.Disc.Storage &&
                   ((Laptop) obj).GraphicCard.Memory == this.GraphicCard.Memory &&
                   ((Laptop) obj).GraphicCard.Name == this.GraphicCard.Name &&
                   ((Laptop) obj).Os == this.Os &&
                   ((Laptop) obj).DiscReader == this.DiscReader;
        }

        public override int GetHashCode()
        {
            return (this.Manufacturer +
                    this.Screen.Resolution +
                    this.Screen.IsTouchable +
                    this.Screen.Size +
                    this.Screen.Type +
                    this.Processor.ClockSpeedAsText +
                    this.Processor.PhysicalCoresAsText +
                    this.Processor.Name +
                    this.Ram +
                    this.Disc.Type +
                    this.Disc.Storage +
                    this.GraphicCard.Memory +
                    this.GraphicCard.Name +
                    this.Os +
                    this.DiscReader
                )
                .GetHashCode();
        }
    }
}