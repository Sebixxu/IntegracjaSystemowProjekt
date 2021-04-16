using System.Collections.Generic;
using IntegracjaSystemowProjekt.Models;
using IntegracjaSystemowProjekt.WPF.Extensions;
using IntegracjaSystemowProjekt.WPF.Models;

namespace IntegracjaSystemowProjekt.WPF.Helpers
{
    public static class ValueHelper
    {
        public static List<RecordModel> MapValues(Laptops laptops)
        {
            var recordModels = new List<RecordModel>();

            foreach (var laptop in laptops.LaptopsCollection)
            {
                var recordModel = MapValue(laptop);
                recordModels.Add(recordModel);
            }

            return recordModels;
        }

        public static List<RecordModel> MapValues(IEnumerable<Record> records)
        {
            var recordModels = new List<RecordModel>();

            foreach (var record in records)
            {
                var recordModel = MapValue(record);
                recordModels.Add(recordModel);
            }

            return recordModels;
        }

        private static RecordModel MapValue(Record record)
        {
            var recordModel = new RecordModel
            {
                ScreenDiagonal = record.ScreenDiagonal,
                Resolution = record.Resolution,
                ManufacturerName = record.ManufacturerName,
                DiskType = record.DiskType,
                DiskSize = record.DiskSize,
                Vram = record.Vram,
                Ram = record.Ram,
                Drive = record.Drive,
                Gpu = record.Gpu,
                Os = record.Os,
                ProcessorName = record.ProcessorName,
                ScreenSurfaceType = record.ScreenSurfaceType,
                IsTouchable = record.IsTouchable.ParseBoolValue(),
                Frequency = record.Frequency.ParseIntValue(),
                NumberOfPhysicalCores = record.NumberOfPhysicalCores.ParseIntValue()
            };

            return recordModel;
        }

        private static RecordModel MapValue(Laptop laptop)
        {
            var recordModel = new RecordModel
            {
                ScreenDiagonal = laptop.Screen.Size,
                Resolution = laptop.Screen.Resolution,
                ManufacturerName = laptop.Manufacturer,
                DiskType = laptop.Disc.Type,
                DiskSize = laptop.Disc.Storage,
                Vram = laptop.GraphicCard.Memory,
                Ram = laptop.Ram,
                Drive = laptop.DiscReader,
                Gpu = laptop.GraphicCard.Name,
                Os = laptop.Os,
                ProcessorName = laptop.Processor.Name,
                ScreenSurfaceType = laptop.Screen.Type,
                IsTouchable = laptop.Screen.IsTouchable.ParseBoolValue(),
                Frequency = laptop.Processor.ClockSpeedAsText.ParseIntValue(),
                NumberOfPhysicalCores = laptop.Processor.PhysicalCores.ToString().ParseIntValue() //?
            };

            return recordModel;
        }
    }
}