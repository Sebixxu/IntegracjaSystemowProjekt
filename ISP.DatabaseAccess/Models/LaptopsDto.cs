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
    }
}
