namespace PreOrderPlatform.Service.ViewModels.ApiResponse
{
    public class PaginationInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public PaginationInfo(int totalItems, int itemsPerPage, int currentPage, int totalPages)
        {
            TotalItems = totalItems;
            ItemsPerPage = itemsPerPage;
            CurrentPage = currentPage;
            TotalPages = totalPages;
        }
    }
}
    