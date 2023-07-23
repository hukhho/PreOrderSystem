using System.Text.Json.Serialization;

namespace PreOrderPlatform.Entity.Enum.Campaign
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CampaignType
    {
        StandardPreorder,  // Regular preorder campaign for upcoming products
        EarlyBird,         // Early bird campaigns offering special incentives for early orders
        LimitedEdition,    // Preorder campaign for limited edition or special variant of a product
        Crowdfunding,      // Crowdfunding-style preorder where production depends on reaching a certain number of orders
        BetaTesting,       // Preorder campaign for early adopters or beta testers
        BundlePreorder     // Preorder campaign for product bundles or sets
    }
}
