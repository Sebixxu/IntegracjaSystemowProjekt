using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ISP.DatabaseAccess;
using ISP.DatabaseAccess.DataAccess;
using ISP.WCF.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ISP.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LaptopService" in both code and config file together.
    public class LaptopService : ILaptopService
    {
        public static LaptopsDataAccess LaptopsDataAccess = new LaptopsDataAccess();

        public int GetLaptopCountByManufacturer(string manufacturerName)
        {
            var manufacturerLaptopsCount = LaptopsDataAccess.GetLaptopsByManufacturers(new List<string> { manufacturerName }).Count();

            return manufacturerLaptopsCount;
        }

        public int GetLaptopCountByScreenAspectRatio(string aspectRatio)
        {
            var supportedResolutionToFind = ConvertAspectRatioToResolutionList(aspectRatio);
            var aspectRatioLaptopsCount = LaptopsDataAccess.GetLaptopsByResolutions(supportedResolutionToFind).Count();

            return aspectRatioLaptopsCount;
        }

        public IEnumerable<string> GetListOfScreenAspectRatios()
        {
            var supportedAspectRatios = GetSupportedAspectRatios();
            var supportedAspectRatiosCodes = supportedAspectRatios.Select(x => x.AspectRatioCode);
            var changedSupportedAspectRatiosCodes = supportedAspectRatiosCodes.Select(x =>
            {
                if (string.IsNullOrEmpty(x))
                    x = "<brak wartości>";

                return x;
            });

            return changedSupportedAspectRatiosCodes;
        }

        public IEnumerable<string> GetListOfScreenSurfaceTypes()
        {
            var distinctScreenSurfaceTypeFromLaptops = LaptopsDataAccess.GetDistinctScreenSurfaceTypeFromLaptops().ToList();
            var changedDistinctScreenSurfaceTypeFromLaptops = distinctScreenSurfaceTypeFromLaptops.Select(x =>
            {
                if (string.IsNullOrEmpty(x))
                    x = "<brak wartości>";

                return x;
            });

            return changedDistinctScreenSurfaceTypeFromLaptops;
        }

        public IEnumerable<string> GetListOfManufacturerNames()
        {
            var distinctManufacturerNameFromLaptops = LaptopsDataAccess.GetDistinctManufacturerNameFromLaptops();
            var changedDistinctManufacturerNameFromLaptops = distinctManufacturerNameFromLaptops.Select(x =>
            {
                if (string.IsNullOrEmpty(x))
                    x = "<brak wartości>";

                return x;
            });

            return changedDistinctManufacturerNameFromLaptops;
        }

        public LaptopsResponse GetLaptopsByScreenType(string screenType)
        {
            var laptopsByScreenTypes = LaptopsDataAccess.GetLaptopsByScreenTypes(new List<string> { screenType }).ToList();
            var mappedLaptops = MapLaptops(laptopsByScreenTypes);

            var responseState = !laptopsByScreenTypes.Any() ? ResponseState.NotFound : ResponseState.OK;

            return new LaptopsResponse
            {
                State = responseState,
                Laptops = mappedLaptops
            };
        }

        private IList<Laptop> MapLaptops(IEnumerable<LaptopsDto> laptopsDtos)
        {
            var laptopsDtosList = laptopsDtos.ToList();
            IList<Laptop> laptops = new List<Laptop>(laptopsDtosList.Count);

            foreach (var laptopsDto in laptopsDtosList)
            {
                laptops.Add(new Laptop
                {
                    ScreenDiagonal = laptopsDto.ScreenDiagonal,
                    Resolution = laptopsDto.Resolution,
                    ManufacturerName = laptopsDto.ManufacturerName,
                    DiskType = laptopsDto.DiskType,
                    DiskSize = laptopsDto.DiskSize,
                    Vram = laptopsDto.Vram,
                    Ram = laptopsDto.Ram,
                    Drive = laptopsDto.Drive,
                    Gpu = laptopsDto.Gpu,
                    Os = laptopsDto.Os,
                    ProcessorName = laptopsDto.ProcessorName,
                    ScreenSurfaceType = laptopsDto.ScreenSurfaceType,
                    IsTouchable = laptopsDto.IsTouchable,
                    Frequency = laptopsDto.Frequency,
                    NumberOfPhysicalCores = laptopsDto.NumberOfPhysicalCores
                });
            }

            return laptops;
        }

        private IList<string> ConvertAspectRatioToResolutionList(string aspectRatio)
        {
            var foundAspectRatio = GetSupportedAspectRatios().FirstOrDefault(x => x.AspectRatioCode == aspectRatio);

            if (foundAspectRatio == null)
                return new List<string>();

            var supportedResolutionToFind = foundAspectRatio.SupportedResolutions;
            return supportedResolutionToFind;
        }

        private IEnumerable<AspectRatio> GetSupportedAspectRatios()
        {
            var aspectRatioToResolution = LoadJsonFile();
            var supportedAspectRatios = aspectRatioToResolution.AspectRatios.ToList();

            return supportedAspectRatios;
        }

        private AspectRatioToResolution LoadJsonFile()
        {
            JObject jObject = JObject.Parse(File.ReadAllText(@"AspectRatioToResolutionMap.json"));
            var aspectRatioToResolutions = jObject.ToObject<AspectRatioToResolution>();

            return aspectRatioToResolutions;
        }
    }
}
