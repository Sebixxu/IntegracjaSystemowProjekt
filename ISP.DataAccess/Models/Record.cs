namespace IntegracjaSystemowProjekt.Models
{
    public class Record
    {
        public string ManufacturerName { get; set; }
        public string ScreenDiagonal { get; set; }
        public string Resolution { get; set; }
        public string ScreenSurfaceType { get; set; }
        public string IsTouchable { get; set; }
        public string ProcessorName { get; set; }
        public string NumberOfPhysicalCores { get; set; }
        public string Frequency { get; set; }
        public string Ram { get; set; }
        public string DiskSize { get; set; }
        public string DiskType { get; set; }
        public string Gpu { get; set; }
        public string Vram { get; set; }
        public string Os { get; set; }
        public string Drive { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Record)) return false;

            return ((Record)obj).ManufacturerName == this.ManufacturerName && ((Record)obj).ScreenDiagonal == this.ScreenDiagonal;
        }

        public override int GetHashCode()
        {
            return (this.ManufacturerName + this.ScreenDiagonal).GetHashCode();
        }
    }
}