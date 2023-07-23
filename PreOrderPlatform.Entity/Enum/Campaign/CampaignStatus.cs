using System.Text.Json.Serialization;

namespace PreOrderPlatform.Entity.Enum.Campaign
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CampaignStatus
    {
        Draft,
        Scheduled,
        Running,
        Paused,
        Completed,
        Cancelled,
    }
}
