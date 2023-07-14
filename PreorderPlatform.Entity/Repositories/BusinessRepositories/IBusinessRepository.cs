﻿using PreorderPlatform.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Entity.Repositories.BusinessRepositories
{
    public interface IBusinessRepository : IRepositoryBase<Business>
    {
        Task<Business> GetBusinessByIdAsync(int id);
        Task<Business> GetByOwnerIdAsync(int userId);
        Task<bool> IsUserOwnerOfBusiness(int userId, int businessId);
    }
}
