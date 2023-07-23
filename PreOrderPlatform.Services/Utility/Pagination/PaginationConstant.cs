namespace PreOrderPlatform.Service.Utility.Pagination
{
    public class PaginationConstant
    {
        public const int MaxPageSize = 100;
        public const int DefaultPage = 1;
        public const int DefaultPageSize = 50;
        public const string DefaultSort = "id_asc";
        public const int MinPage = 0;
        public const int MinPageSize = 3;

        public enum OrderCriteria
        {
            DESC,

            ASC,
        }
    }
}
