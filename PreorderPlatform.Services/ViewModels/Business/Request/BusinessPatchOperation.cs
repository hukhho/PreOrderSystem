namespace PreorderPlatform.Services.ViewModels.Business.Request
{
    public class BusinessPatchOperation
    {
        public string? Op { get; set; }
        public string? Path { get; set; }
        public object? Value { get; set; }
    }
}