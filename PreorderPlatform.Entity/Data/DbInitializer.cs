using Microsoft.Extensions.Logging;
using PreorderPlatform.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace PreorderPlatform.Entity.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly PreOrderSystemContext context;
        private readonly ILogger _logger;

        public DbInitializer(ILogger<DbInitializer> logger, PreOrderSystemContext context)
        {
            this.context = context;
            this._logger = logger; // this line was missing
        }

        public void Initialize()
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    string password = "Hung@123";
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                    // Initialize Roles
                    if (!context.Roles.Any())
                    {
                        var roles = new List<Role> {
                    new Role { Id = Guid.NewGuid(), Name = "ADMIN" },
                    new Role { Id = Guid.NewGuid(), Name = "BUSINESS_OWNER" },
                    new Role { Id = Guid.NewGuid(), Name = "BUSINESS_STAFF" },
                    new Role { Id = Guid.NewGuid(), Name = "CUSTOMER" }
                };

                        context.Roles.AddRange(roles);
                        context.SaveChanges();

                        _logger.LogInformation("Roles have been initialized.");
                    }

                    // Initialize Roles
                    var adminRole = context.Roles.FirstOrDefault(r => r.Name == "ADMIN");
                    var customerRole = context.Roles.FirstOrDefault(r => r.Name == "CUSTOMER");


                    // Initialize Admins if none exist
                    if (!context.Users.Any(u => u.RoleId == adminRole.Id))
                    {
                        for (int i = 1; i <= 5; i++)
                        {
                            var admin = new User
                            {
                                FirstName = $"Admin{i}",
                                LastName = $"User{i}",
                                Phone = $"23456789{i}",
                                Email = $"admin{i}@gmail.com",
                                Password = $"{hashedPassword}",
                                Address = $"Admin Street {i}",
                                Ward = $"Ward {i}",
                                District = $"District {i}",
                                Province = $"Province {i}",
                                Status = true,
                                RoleId = adminRole.Id
                            };

                            context.Users.Add(admin);
                            context.SaveChanges();

                            _logger.LogInformation($"Admin {admin.FirstName} {admin.LastName} has been created.");
                        }
                    }
                    // Initialize Customers if none exist
                    if (!context.Users.Any(u => u.RoleId == customerRole.Id))
                    {
                        for (int i = 1; i <= 5; i++)
                        {
                            var customer = new User
                            {
                                FirstName = $"Customer{i}",
                                LastName = $"User{i}",
                                Phone = $"34567890{i}",
                                Email = $"customer{i}@gmail.com",
                                Password = $"{hashedPassword}",
                                Address = $"Customer Street {i}",
                                Ward = $"Ward {i}",
                                District = $"District {i}",
                                Province = $"Province {i}",
                                Status = true,
                                RoleId = customerRole.Id
                            };

                            context.Users.Add(customer);
                            context.SaveChanges();

                            _logger.LogInformation($"Customer {customer.FirstName} {customer.LastName} has been created.");
                        }
                    }





                    var businessOwnerRoleId = context.Roles.FirstOrDefault(r => r.Name == "BUSINESS_OWNER").Id;
                    var businessStaffRoleId = context.Roles.FirstOrDefault(r => r.Name == "BUSINESS_STAFF").Id;


                    // Initialize Businesses and Users
                    if (!context.Businesses.Any())
                    {
                        var businesses = new List<Business>();

                        for (int i = 1; i <= 3; i++)
                        {
                            var business = new Business
                            {
                                Name = $"Business {i}",
                                Phone = $"12345678{i}",
                                Email = $"business{i}@gmail.com",
                                Status = true
                            };

                            businesses.Add(business);
                        }

                        context.Businesses.AddRange(businesses);
                        context.SaveChanges();

                        _logger.LogInformation("Businesses have been initialized.");

                        for (int i = 1; i <= 3; i++)
                        {
                            var business = businesses[i - 1];

                            var businessOwner = new User
                            {
                                FirstName = $"BusinessOwner{i}",
                                LastName = $"User{i}",
                                Phone = $"23456789{i}",
                                Email = $"businessowner{i}@gmail.com",
                                Password = $"{hashedPassword}",
                                Address = $"Business Street {i}",
                                Ward = $"Ward {i}",
                                District = $"District {i}",
                                Province = $"Province {i}",
                                Status = true,
                                RoleId = businessOwnerRoleId,
                                BusinessId = business.Id
                            };

                            context.Users.Add(businessOwner);
                            context.SaveChanges();

                            _logger.LogInformation($"User {businessOwner.FirstName} {businessOwner.LastName} has been created.");

                            business.OwnerId = businessOwner.Id;

                            var businessStaff = new User
                            {
                                FirstName = $"BusinessStaff{i}",
                                LastName = $"User{i}",
                                Phone = $"34567890{i}",
                                Email = $"businessstaff{i}@gmail.com",
                                Password = $"{hashedPassword}",
                                Address = $"Staff Street {i}",
                                Ward = $"Ward {i}",
                                District = $"District {i}",
                                Province = $"Province {i}",
                                Status = true,
                                RoleId = businessStaffRoleId,
                                BusinessId = business.Id
                            };
                            business.Description = $"Description for Business {i}, Owned by {businessOwner.FirstName} {businessOwner.LastName}";

                            context.Users.Add(businessStaff);

                            context.SaveChanges();

                            _logger.LogInformation($"User {businessStaff.FirstName} {businessStaff.LastName} has been created.");

                        }
                    }

                    // Initialize PaymentCredentials if none exist
                    if (!context.BusinessPaymentCredentials.Any())
                    {
                        // Get the businesses to associate with the payment credentials
                        var businesses = context.Businesses.ToList();

                        foreach (var business in businesses)
                        {
                            var paymentCredential = new BusinessPaymentCredential
                            {
                                Id = Guid.NewGuid(),
                                BusinessId = business.Id,
                                BankAccountNumber = $"123456789{i}",
                                BankName = $"Bank {i}",
                                BankRecipientName = $"Recipient {i}",
                                MomoApiKey = $"MomoApiKey {i}",
                                MomoPartnerCode = "MOMOMWNB20210129",
                                MomoAccessToken = "nkDyGIefYvOL9Nyg",
                                MomoSecretToken = "YaCm3DJAJuAV9jGmwauQ0mwT6FpqYiOI",
                                IsMomoActive = true,
                                IsMain = true,
                                CreateAt = DateTime.Now,
                                Status = true
                            };

                            context.BusinessPaymentCredentials.Add(paymentCredential);
                            context.SaveChanges();

                            _logger.LogInformation($"Payment Credential for business {business.Name} has been created.");
                        }
                    }



                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Log exception
                    _logger.LogError(ex, "An error occurred while initializing the database.");
                    transaction.Rollback();
                }
            }
        }
    }
}
