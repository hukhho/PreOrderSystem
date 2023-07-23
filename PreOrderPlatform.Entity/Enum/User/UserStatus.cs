using System.Text.Json.Serialization;

namespace PreOrderPlatform.Entity.Enum.User
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserStatus
    {
        Active,
        Inactive,
        Suspended
    }
}
