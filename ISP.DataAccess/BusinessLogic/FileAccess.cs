using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using IntegracjaSystemowProjekt.Models;

namespace IntegracjaSystemowProjekt.BusinessLogic
{
    public class FileAccess
    {
        private const string txtFilePath = "katalog.txt";
        private const string xmlFilePath = "katalog.xml";

        public void SaveXmlFile(Laptops laptops, string path)
        {
            string xml;
            XmlSerializer xsSubmit = new XmlSerializer(typeof(Laptops));

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, laptops);
                    xml = sww.ToString();
                }
            }

            File.WriteAllText(path, xml);
        }

        public Laptops ReadDefaultXmlFile()
        {
            Laptops laptops;

            var file = File.ReadAllText(xmlFilePath);

            XmlSerializer serializer = new XmlSerializer(typeof(Laptops));
            using (TextReader reader = new StringReader(file))
            {
                laptops = (Laptops)serializer.Deserialize(reader);
            }

            return laptops;
        }

        public Laptops ReadXmlFileByPath(string path)
        {
            Laptops laptops;

            var file = File.ReadAllText(path);

            XmlSerializer serializer = new XmlSerializer(typeof(Laptops));
            using (TextReader reader = new StringReader(file))
            {
                laptops = (Laptops)serializer.Deserialize(reader);
            }

            return laptops;
        }

        public IList<Record> ReadTxtFileByPath(string path)
        {
            IList<Record> records = new List<Record>();

            var file = File.ReadLines(path);

            foreach (var fileLine in file)
                records.Add(MapValues(fileLine.Split(';')));

            return records;
        }

        public IList<Record> ReadDefaultTxtFile()
        {
            IEnumerable<string> file;
            IList<Record> records = new List<Record>();

            try
            {
                file = File.ReadLines(txtFilePath);
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