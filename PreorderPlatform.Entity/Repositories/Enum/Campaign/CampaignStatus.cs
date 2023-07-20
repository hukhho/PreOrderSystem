using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PreorderPlatform.Entity.Repositories.Enum.Status
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CampaignStatus
    {
        Draft,
        Active,
        Completed,
        Cancelled
    }
}
