using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.Campaign.Request
{
    public class CampaignSearchRequest
    {
        public string? Name { get; set; }

        [DataType(DataType.Date)]
        [Description("Enter the date in the format 'YYYY-MM-DD'")]
        [FromQuery]
        public DateTime? DateInRange { get; set; }
        public bool? Status { get; set; }
        public Guid? OwnerId { get; set; }
        public Guid? BusinessId { get; set; }

    }
}
