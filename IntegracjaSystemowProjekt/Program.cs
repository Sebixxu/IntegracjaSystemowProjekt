using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegracjaSystemowProjekt.BusinessLogic;
using IntegracjaSystemowProjekt.Models;

namespace IntegracjaSystemowProjekt
{
    class Program
    {
        static void Main(string[] args)
        {
            var records = FileAccess.ReadFile();

            var groupsCount = records.GroupBy(x => x.ManufacturerName).Select(group => new
            {
                GroupKey = group.Key,
                Count = group.Count()
            });


            Table tbl = new Table("Lp.", ColumnNames.ManufacturerNameColumnName, ColumnNames.ScreenDiagonalColumnName, ColumnNames.ResolutionColumnName,
                ColumnNames.ScreenSurfaceTypeColumnName, ColumnNames.IsTouchableColumnName, ColumnNames.ProcessorNameColumnName, ColumnNames.NumberOfPhysicalCoresColumnName,
                ColumnNames.FrequencyColumnName, ColumnNames.RamColumnName, ColumnNames.DiskSizeColumnName, ColumnNames.DiskTypeColumnName, ColumnNames.GpuColumnName,
                ColumnNames.VramColumnName, ColumnNames.OsColumnName, ColumnNames.DriveColumnName);

            foreach (var record in records.Select((value, i) => new { i, value }))
            {
                tbl.AddRow(record.i + 1, record.value.ManufacturerName, record.value.ScreenDiagonal, record.value.Resolution, record.value.ScreenSurfaceType,
                    record.value.IsTouchable, record.value.ProcessorName, record.value.NumberOfPhysicalCores, record.value.Frequency, record.value.Ram, record.value.DiskSize,
                    record.value.DiskType, record.value.Gpu, record.value.Vram, record.value.Os, record.value.Drive);
            }

            tbl.Print();

            Table tbl2 = new Table("Producent", "Liczba laptopów");

            foreach (var group in groupsCount)
                tbl2.AddRow(group.GroupKey, group.Count);


            tbl2.Print();

            Console.ReadKey();
        }
    }
}
