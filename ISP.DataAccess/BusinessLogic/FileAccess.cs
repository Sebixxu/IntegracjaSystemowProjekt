using System;
using System.Collections.Generic;
using System.IO;
using IntegracjaSystemowProjekt.Models;

namespace IntegracjaSystemowProjekt.BusinessLogic
{
    public class FileAccess
    {
        private const string filePath = "katalog.txt";

        public IList<Record> ReadFileByPath(string path)
        {
            IList<Record> records = new List<Record>();

            var file = File.ReadLines(path);

            foreach (var fileLine in file)
                records.Add(MapValues(fileLine.Split(';')));

            return records;
        }

        public IList<Record> ReadDefaultFile()
        {
            IEnumerable<string> file;
            IList<Record> records = new List<Record>();

            try
            {
                file = File.ReadLines(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<Record>();
            }

            foreach (var fileLine in file)
                records.Add(MapValues(fileLine.Split(';')));

            return records;
        }

        private Record MapValues(string[] line)
        {
            try
            {
                var record = new Record
                {
                    ManufacturerName = line[0],
                    ScreenDiagonal = line[1],
                    Resolution = line[2],
                    ScreenSurfaceType = line[3],
                    IsTouchable = line[4],
                    ProcessorName = line[5],
                    NumberOfPhysicalCores = line[6],
                    Frequency = line[7],
                    Ram = line[8],
                    DiskSize = line[9],
                    DiskType = line[10],
                    Gpu = line[11],
                    Vram = line[12],
                    Os = line[13],
                    Drive = line[14]
                };

                return record;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new Record();
            }
        }
    }
}