namespace PreOrderPlatform.Service.ViewModels.ApiResponse
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }
        public bool? Success { get; set; }
        public PaginationInfo? Pagination { get; set; }

        public ApiResponse(T? data, string message, bool success, PaginationInfo? pagination = null)
        {
            Data = data;
            Message = message;
            Success = success;
            Pagination = pagination;
        }
    }
}
