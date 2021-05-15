using System.Collections.Generic;
using Newtonsoft.Json;

namespace ISP.WCF.Models
{
    
    public class AspectRatioToResolution
    {
        [JsonProperty("aspectRatios")] 
        public List<AspectRatio> AspectRatios { get; set; }
    }
}