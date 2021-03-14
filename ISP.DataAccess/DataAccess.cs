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
        public static IEnumerable<Record> GetDefaultFileData()
        {
            var records = new FileAccess().ReadDefaultFile();

            return records;
        }

        public static IEnumerable<Record> GetFileDataByPath(string path)
        {
            var records = new FileAccess().ReadFileByPath(path);

            return records;
        }
    }
}
