using System.Collections.Generic;
using Newtonsoft.Json;

namespace ISP.WCF.Models
{
    public class AspectRatio
    {
        [JsonProperty("aspectRatioCode")]
        public string AspectRatioCode { get; set; }

        [JsonProperty("supportedResolutions")]
        public List<string> SupportedResolutions { get; set; }
    }
}