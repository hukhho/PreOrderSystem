using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace PreOrderPlatform.Service.Utility.Pagination
{
    public class PaginationParam<TKey> where TKey : System.Enum
    {
        private int _page = PaginationConstant.DefaultPage;

        public int Page
        {
            get => _page;
            set => _page = (value);
        }

        [FromQuery]
        [DefaultValue(PaginationConstant.DefaultPageSize)]
        public int PageSize { get; set; } = PaginationConstant.DefaultPageSize;

        [FromQuery]
        public TKey? SortKey { get; set; } = default(TKey?);

        [EnumDataType(typeof(PaginationConstant.OrderCriteria))]
        [JsonConverter(typeof(PaginationConstant.OrderCriteria))]
        [FromQuery]
        public PaginationConstant.OrderCriteria SortOrder { get; set; }
    }
}
