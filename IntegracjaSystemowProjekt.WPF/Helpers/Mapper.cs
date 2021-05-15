using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using IntegracjaSystemowProjekt.Models;
using IntegracjaSystemowProjekt.WPF.Extensions;
using IntegracjaSystemowProjekt.WPF.Models;
using ISP.DatabaseAccess;

namespace IntegracjaSystemowProjekt.WPF.Helpers
{
    public static class Mapper
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

        public static RecordModel MapValue(Record record)
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

        public static RecordModel MapValue(Laptop laptop)
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

        public static RecordModel MapLaptopDtoToRecord(LaptopsDto laptopsDto)
        {
            var recordModel = new RecordModel
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
            };

            return recordModel;
        }

        public static IEnumerable<RecordModel> MapLaptopsDtoToRecords(IEnumerable<LaptopsDto> laptopsDtos)
        {
            var records = new List<RecordModel>();

            foreach (var laptopsDto in laptopsDtos)
            {
                var recordModel = MapLaptopDtoToRecord(laptopsDto);
                records.Add(recordModel);
            }

            return records;
        }

        public static IEnumerable<LaptopsDto> MapRecordsToLaptopsDto(IEnumerable<RecordModel> recordModels)
        {
            var laptopsDtos = new List<LaptopsDto>();

            foreach (var recordModel in recordModels)
            {
                laptopsDtos.Add(new LaptopsDto
                {
                    ScreenDiagonal = recordModel.ScreenDiagonal,
                    Resolution = recordModel.Resolution,
                    ManufacturerName = recordModel.ManufacturerName,
                    DiskType = recordModel.DiskType,
                    DiskSize = recordModel.DiskSize,
                    Vram = recordModel.Vram,
                    Ram = recordModel.Ram,
                    Drive = recordModel.Drive,
                    Gpu = recordModel.Gpu,
                    Os = recordModel.Os,
                    ProcessorName = recordModel.ProcessorName,
                    ScreenSurfaceType = recordModel.ScreenSurfaceType,
                    IsTouchable = recordModel.IsTouchable,
                    Frequency = recordModel.Frequency,
                    NumberOfPhysicalCores = recordModel.NumberOfPhysicalCores
                });
            }

            return laptopsDtos;
        }

        public static Laptops MapRecordToLaptops(BindableCollection<RecordModel> recordModels)
        {
            var laptops = new Laptops { ModDate = DateTime.Now.ToString(), LaptopsCollection = new List<Laptop>() };

            foreach (var recordModel in recordModels.Select((value, i) => new { i, value }))
            {
                laptops.LaptopsCollection.Add(new Laptop
                {
                    Disc = new Disc
                    {
                        Storage = recordModel.value.DiskSize,
                        Type = recordModel.value.DiskType
                    },
                    DiscReader = recordModel.value.Drive,
                    GraphicCard = new GraphicCard
                    {
                        Memory = recordModel.value.Vram,
                        Name = recordModel.value.Gpu
                    },
                    Id = recordModel.i + 1,
                    Manufacturer = recordModel.value.ManufacturerName,
                    Os = recordModel.value.Os,
                    Processor = new Processor
                    {
                        PhysicalCores = recordModel.value.NumberOfPhysicalCores,
                        PhysicalCoresAsText = recordModel.value.NumberOfPhysicalCores?.ToString(),
                        ClockSpeed = recordModel.value.Frequency,
                        ClockSpeedAsText = recordModel.value.Frequency?.ToString(),
                        Name = recordModel.value.ProcessorName
                    },
                    Ram = recordModel.value.Ram,
                    Screen = new IntegracjaSystemowProjekt.Models.Screen
                    {
                        IsTouchable = ParseBoolToXmlString(recordModel.value.IsTouchable),
                        Resolution = recordModel.value.Resolution,
                        Size = recordModel.value.ScreenDiagonal,
                        Type = recordModel.value.ScreenSurfaceType
                    }
                });
            }

            return laptops;
        }

        private static string ParseBoolToXmlString(bool value)
        {
            return value ? "yes" : "false";
        }
    }
}