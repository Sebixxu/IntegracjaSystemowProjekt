using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ISP.WCF.Models;

namespace ISP.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILaptopService" in both code and config file together.
    [ServiceContract]
    public interface ILaptopService
    {
        [OperationContract]
        int GetLaptopCountByManufacturer(string manufacturerName);

        [OperationContract]
        int GetLaptopCountByScreenAspectRatio(string aspectRatio);

        [OperationContract]
        IEnumerable<string> GetListOfScreenAspectRatios();

        [OperationContract]
        IEnumerable<string> GetListOfScreenSurfaceTypes();

        [OperationContract]
        IEnumerable<string> GetListOfManufacturerNames();

        [OperationContract] 
        LaptopsResponse GetLaptopsByScreenType(string screenType);
    }
}
