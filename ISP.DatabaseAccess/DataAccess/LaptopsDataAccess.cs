using System.Collections.Generic;
using System.Linq;

namespace ISP.DatabaseAccess.DataAccess
{
    public class LaptopsDataAccess
    {
        protected static LaptopDbContext Context = new LaptopDbContext();

        public IEnumerable<string> GetDistinctManufacturerNameFromLaptops()
        {
            var manufacturerNameDistinctList = Context.Laptops.Select(x => x.ManufacturerName).Distinct().ToList();

            return manufacturerNameDistinctList;
        }

        public IEnumerable<string> GetDistinctScreenSurfaceTypeFromLaptops()
        {
            var screenSurfaceTypeDistinctList = Context.Laptops.Select(x => x.ScreenSurfaceType).Distinct().ToList();

            return screenSurfaceTypeDistinctList;
        }

        public IEnumerable<string> GetDistinctResolutionFromLaptops()
        {
            var resolutionDistinctList = Context.Laptops.Select(x => x.Resolution).Distinct().ToList();

            return resolutionDistinctList;
        }

        public IEnumerable<LaptopsDto> GetLaptopsByScreenTypes(IEnumerable<string> screenTypes)
        {
            var laptopsDto = Context.Laptops.Where(x => screenTypes.Contains(x.ScreenSurfaceType)).ToList();

            return laptopsDto;
        }

        public IEnumerable<LaptopsDto> GetLaptopsByResolutions(IEnumerable<string> resolutions)
        {
            var laptopsDto = Context.Laptops.Where(x => resolutions.Contains(x.Resolution)).ToList();

            return laptopsDto;
        }

        public IEnumerable<LaptopsDto> GetLaptopsByManufacturers(IEnumerable<string> manufacturerName)
        {
            var laptopsDto = Context.Laptops.Where(x => manufacturerName.Contains(x.ManufacturerName)).ToList();

            return laptopsDto;
        }

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