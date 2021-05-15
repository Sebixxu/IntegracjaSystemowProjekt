using System.Runtime.Serialization;

namespace ISP.WCF.Models
{
    public enum ResponseState
    {
        [EnumMember]
        OK,
        [EnumMember]
        NotFound,
        [EnumMember]
        Error
    }
}