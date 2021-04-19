using System.Collections.Generic;
using System.Linq;

namespace ISP.DatabaseAccess.DataAccess
{
    public class LaptopsDataAccess
    {
        protected static LaptopDbContext Context = new LaptopDbContext();

        public IEnumerable<LaptopsDto> GetLaptops()
        {
            var laptopsDto = Context.Laptops;

            return laptopsDto;
        }

        public bool IsAlreadyExisting(LaptopsDto laptopsDto)
        {
            var laptop = Context.Laptops.FirstOrDefault(x =>
                x.IsTouchable == laptopsDto.IsTouchable &&
                x.ScreenDiagonal == laptopsDto.ScreenDiagonal &&
                x.DiskSize == laptopsDto.DiskSize &&
                x.DiskType == laptopsDto.DiskType &&
                x.Drive == laptopsDto.Drive &&
                x.Frequency == laptopsDto.Frequency &&
                x.Gpu == laptopsDto.Gpu &&
                x.ManufacturerName == laptopsDto.ManufacturerName &&
                x.NumberOfPhysicalCores == laptopsDto.NumberOfPhysicalCores &&
                x.Os == laptopsDto.Os &&
                x.ProcessorName == laptopsDto.ProcessorName &&
                x.Ram == laptopsDto.Ram &&
                x.Resolution == laptopsDto.Resolution &&
                x.ScreenSurfaceType == laptopsDto.ScreenSurfaceType &&
                x.Vram == laptopsDto.Vram);

            return laptop != null;
        }

        public void AddLaptop(LaptopsDto laptopDto)
        {
            Context.Laptops.Add(laptopDto);

            Context.SaveChanges();
        }

        public void AddLaptops(IEnumerable<LaptopsDto> laptopsDto)
        {
            Context.Laptops.AddRange(laptopsDto);

            Context.SaveChanges();
        }
    }
}