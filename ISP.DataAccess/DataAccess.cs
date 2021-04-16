using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegracjaSystemowProjekt.BusinessLogic;
using IntegracjaSystemowProjekt.Models;

namespace ISP.DataAccess
{
    public static class DataAccess
    {
        public static IEnumerable<Record> GetDefaultTxtFileData()
        {
            var records = new FileAccess().ReadDefaultTxtFile();

            return records;
        }

        public static IEnumerable<Record> GetTxtFileDataByPath(string path)
        {
            var records = new FileAccess().ReadTxtFileByPath(path);

            return records;
        }

        public static Laptops GetDefaultXmlFileData()
        {
            var laptops = new FileAccess().ReadDefaultXmlFile();

            return laptops;
        }

        public static Laptops GetXmlFileDataByPath(string path)
        {
            var laptops = new FileAccess().ReadXmlFileByPath(path);

            return laptops;
        }

        public static void SaveXmlFile(Laptops laptops, string path)
        {
            new FileAccess().SaveXmlFile(laptops, path);
        }
    }
}
