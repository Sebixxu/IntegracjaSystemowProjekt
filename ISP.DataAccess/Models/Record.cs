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

            return ((Record)obj).ManufacturerName == this.ManufacturerName &&
                   ((Record)obj).ScreenDiagonal == this.ScreenDiagonal &&
                   ((Record)obj).Resolution == this.Resolution &&
                   ((Record)obj).ScreenSurfaceType == this.ScreenSurfaceType &&
                   ((Record)obj).IsTouchable == this.IsTouchable &&
                   ((Record)obj).ProcessorName == this.ProcessorName &&
                   ((Record)obj).NumberOfPhysicalCores == this.NumberOfPhysicalCores &&
                   ((Record)obj).Frequency == this.Frequency &&
                   ((Record)obj).Ram == this.Ram &&
                   ((Record)obj).DiskSize == this.DiskSize &&
                   ((Record)obj).DiskType == this.DiskType &&
                   ((Record)obj).Gpu == this.Gpu &&
                   ((Record)obj).Vram == this.Vram &&
                   ((Record)obj).Os == this.Os &&
                   ((Record)obj).Drive == this.Drive;
        }

        public override int GetHashCode()
        {
            return (this.ManufacturerName + 
                    this.ScreenDiagonal +
                    this.Resolution +
                    this.ScreenSurfaceType +
                    this.IsTouchable +
                    this.ProcessorName +
                    this.NumberOfPhysicalCores +
                    this.Frequency +
                    this.Ram +
                    this.DiskSize +
                    this.DiskType +
                    this.Gpu +
                    this.Vram +
                    this.Os +
                    this.Drive
                    )
                .GetHashCode();
        }
    }
}