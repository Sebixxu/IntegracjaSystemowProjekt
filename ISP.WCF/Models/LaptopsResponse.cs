using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ISP.WCF.Models
{
    [DataContract]
    public class LaptopsResponse
    {
        [DataMember]
        public IEnumerable<Laptop> Laptops { get; set; }

        [DataMember]
        public ResponseState State { get; set; }
    }
}