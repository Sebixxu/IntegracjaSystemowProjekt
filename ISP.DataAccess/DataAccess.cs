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
        public static IEnumerable<Record> GetFileData()
        {
            var records = new FileAccess().ReadFile();

            return records;
        }
    }
}
