using System.Runtime.Serialization;

namespace ISP.WCF.Models
{
    [DataContract]
    public class Laptop
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string ManufacturerName { get; set; }
        [DataMember]
        public string ScreenDiagonal { get; set; }
        [DataMember]
        public string Resolution { get; set; }
        [DataMember]
        public string ScreenSurfaceType { get; set; }
        [DataMember]
        public bool IsTouchable { get; set; }
        [DataMember]
        public string ProcessorName { get; set; }
        [DataMember]
        public int? NumberOfPhysicalCores { get; set; }
        [DataMember]
        public int? Frequency { get; set; }
        [DataMember]
        public string Ram { get; set; }
        [DataMember]
        public string DiskSize { get; set; }
        [DataMember]
        public string DiskType { get; set; }
        [DataMember]
        public string Gpu { get; set; }
        [DataMember]
        public string Vram { get; set; }
        [DataMember]
        public string Os { get; set; }
        [DataMember]
        public string Drive { get; set; }
    }
}