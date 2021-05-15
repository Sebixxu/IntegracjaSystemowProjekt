using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISP.DatabaseAccess
{
    [Table("Laptops")]
    public class LaptopsDto
    {
        [Key]
        public int Id { get; set; }

        public string ManufacturerName { get; set; }
        public string ScreenDiagonal { get; set; }
        public string Resolution { get; set; }
        public string ScreenSurfaceType { get; set; }
        public bool IsTouchable { get; set; }
        public string ProcessorName { get; set; }
        public int? NumberOfPhysicalCores { get; set; }
        public int? Frequency { get; set; }
        public string Ram { get; set; }
        public string DiskSize { get; set; }
        public string DiskType { get; set; }
        public string Gpu { get; set; }
        public string Vram { get; set; }
        public string Os { get; set; }
        public string Drive { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is LaptopsDto)) return false;

            return ((LaptopsDto)obj).ManufacturerName == this.ManufacturerName &&
                   ((LaptopsDto)obj).ScreenDiagonal == this.ScreenDiagonal &&
                   ((LaptopsDto)obj).Resolution == this.Resolution &&
                   ((LaptopsDto)obj).ScreenSurfaceType == this.ScreenSurfaceType &&
                   ((LaptopsDto)obj).IsTouchable == this.IsTouchable &&
                   ((LaptopsDto)obj).ProcessorName == this.ProcessorName &&
                   ((LaptopsDto)obj).NumberOfPhysicalCores == this.NumberOfPhysicalCores &&
                   ((LaptopsDto)obj).Frequency == this.Frequency &&
                   ((LaptopsDto)obj).Ram == this.Ram &&
                   ((LaptopsDto)obj).DiskSize == this.DiskSize &&
                   ((LaptopsDto)obj).DiskType == this.DiskType &&
                   ((LaptopsDto)obj).Gpu == this.Gpu &&
                   ((LaptopsDto)obj).Vram == this.Vram &&
                   ((LaptopsDto)obj).Os == this.Os &&
                   ((LaptopsDto)obj).Drive == this.Drive;
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
