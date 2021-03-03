using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegracjaSystemowProjekt.BusinessLogic;
using IntegracjaSystemowProjekt.Models;
using ISP.DataAccess;

namespace IntegracjaSystemowProjekt
{
    class Program
    {
        static void Main(string[] args)
        {
            var records = DataAccess.GetFileData().ToList();

            var groupsCount = records.GroupBy(x => x.ManufacturerName).Select(group => new
            {
                GroupKey = group.Key,
                Count = group.Count()
            });

            Table tbl = new Table("Lp.", Resources.Resource.ManufacturerNameColumnName, Resources.Resource.ScreenDiagonalColumnName, 
                Resources.Resource.ResolutionColumnName, Resources.Resource.ScreenSurfaceTypeColumnName, Resources.Resource.IsTouchableColumnName, 
                Resources.Resource.ProcessorNameColumnName, Resources.Resource.NumberOfPhysicalCoresColumnName, Resources.Resource.FrequencyColumnName, 
                Resources.Resource.RamColumnName, Resources.Resource.DiskSizeColumnName, Resources.Resource.DiskTypeColumnName, Resources.Resource.GpuColumnName, 
                Resources.Resource.VramColumnName, Resources.Resource.OsColumnName, Resources.Resource.DriveColumnName);
            Table tbl2 = new Table(Resources.Resource.ManufacturerNameColumnName, Resources.Resource.NumberOfDevices);

            foreach (var record in records.Select((value, i) => new { i, value }))
            {
                tbl.AddRow(record.i + 1, record.value.ManufacturerName, record.value.ScreenDiagonal, record.value.Resolution, record.value.ScreenSurfaceType,
                    record.value.IsTouchable, record.value.ProcessorName, record.value.NumberOfPhysicalCores, record.value.Frequency, record.value.Ram, record.value.DiskSize,
                    record.value.DiskType, record.value.Gpu, record.value.Vram, record.value.Os, record.value.Drive);
            }

            foreach (var group in groupsCount)
                tbl2.AddRow(group.GroupKey, group.Count);

            tbl.Print();

            tbl2.Print();

            Console.ReadKey();
        }
    }
}
