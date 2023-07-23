using System.Text.Json.Serialization;

namespace PreOrderPlatform.Entity.Enum.Campaign
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CampaignLocation
    {
        HoChiMinh,
        HaNoi,
        DaNang
    }
}
