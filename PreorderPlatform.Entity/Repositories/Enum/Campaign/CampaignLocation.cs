using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PreorderPlatform.Entity.Repositories.Enum.Campaign
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CampaignLocation
    {
        HoChiMinh,
        HaNoi,
        DaNang
    }
}
