using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PreorderPlatform.Service.Utility.Pagination
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
