using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PreorderPlatform.Entity.Models;
using System;
using System.Security.Cryptography;

namespace PreorderPlatform.Entity.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ILogger _logger;
        private readonly PreOrderSystemContext context;

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
                    var random = new Random();
                    string password = "Hung@123";
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                    // Initialize Roles
                    if (!context.Roles.Any())
                    {
                        var roles = new List<Role>
                        {
                            new Role { Id = Guid.NewGuid(), Name = "ADMIN" },
                            new Role { Id = Guid.NewGuid(), Name = "BUSINESS_OWNER" },
                            new Role { Id = Guid.NewGuid(), Name = "BUSINESS_STAFF" },
                            new Role { Id = Guid.NewGuid(), Name = "CUSTOMER" }
                        };

                        context.Roles.AddRange(roles);
                        context.SaveChanges();

                        _logger.LogInformation("Roles have been initialized.");
                    }
                    // Initialize Categories
                    if (!context.Categories.Any())
                    {
                        var categories = new List<Category>
                        {
                            new Category
                            {
                                Id = Guid.NewGuid(),
                                Name = "Laptop",
                                Description = "Laptop devices",
                                Status = true
                            },
                            new Category
                            {
                                Id = Guid.NewGuid(),
                                Name = "Smartphone",
                                Description = "Smartphone devices",
                                Status = true
                            },
                            new Category
                            {
                                Id = Guid.NewGuid(),
                                Name = "TV",
                                Description = "Television devices",
                                Status = true
                            },
                            new Category
                            {
                                Id = Guid.NewGuid(),
                                Name = "Tablet",
                                Description = "Tablet devices",
                                Status = true
                            },
                            new Category
                            {
                                Id = Guid.NewGuid(),
                                Name = "Headphone",
                                Description = "Headphone devices",
                                Status = true
                            }
                        };

                        context.Categories.AddRange(categories);
                        context.SaveChanges();

                        _logger.LogInformation("Categories have been initialized.");
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

                            _logger.LogInformation(
                                $"Admin {admin.FirstName} {admin.LastName} has been created."
                            );
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

                            _logger.LogInformation(
                                $"Customer {customer.FirstName} {customer.LastName} has been created."
                            );
                        }
                    }

                    var businessOwnerRoleId = context.Roles
                        .FirstOrDefault(r => r.Name == "BUSINESS_OWNER")
                        .Id;
                    var businessStaffRoleId = context.Roles
                        .FirstOrDefault(r => r.Name == "BUSINESS_STAFF")
                        .Id;

                    // Initialize Businesses and Users
                    if (!context.Businesses.Any())
                    {
                        var businesses = new List<Business>();

                        for (int i = 1; i <= 5; i++)
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

                        for (int i = 1; i <= 5; i++)
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

                            _logger.LogInformation(
                                $"User {businessOwner.FirstName} {businessOwner.LastName} has been created."
                            );

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
                            business.Description =
                                $"Description for Business {i}, Owned by {businessOwner.FirstName} {businessOwner.LastName}";

                            context.Users.Add(businessStaff);

                            context.SaveChanges();

                            _logger.LogInformation(
                                $"User {businessStaff.FirstName} {businessStaff.LastName} has been created."
                            );
                        }
                    }

                    // Initialize PaymentCredentials if none exist
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
                                BankAccountNumber = $"123456789{business.Name}",
                                BankName = $"Bank of {business.Name}",
                                BankRecipientName = $"Recipient of {business.Name}",
                                MomoApiKey = $"MomoApiKey for {business.Name}",
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

                            _logger.LogInformation(
                                $"Payment Credential for business {business.Name} has been created."
                            );
                        }
                    }

                    // Initialize Products
                    if (!context.Products.Any())
                    {
                        var businesses = context.Businesses.ToList();
                        // Assign each category to a business

                        var laptopBusiness = businesses.FirstOrDefault();
                        var smartphoneBusiness = businesses.Skip(1).FirstOrDefault();
                        var tvBusiness = businesses.Skip(2).FirstOrDefault();
                        var tabletBusiness = businesses.Skip(3).FirstOrDefault();
                        var headphoneBusiness = businesses.Skip(4).FirstOrDefault();

                        var laptopCategory = context.Categories.FirstOrDefault(
                            c => c.Name == "Laptop"
                        );
                        var smartphoneCategory = context.Categories.FirstOrDefault(
                            c => c.Name == "Smartphone"
                        );
                        var tvCategory = context.Categories.FirstOrDefault(c => c.Name == "TV");
                        var tabletCategory = context.Categories.FirstOrDefault(
                            c => c.Name == "Tablet"
                        );
                        var headphoneCategory = context.Categories.FirstOrDefault(
                            c => c.Name == "Headphone"
                        );

                        var products = new List<Product>
                        {
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "MacBook Pro",
                                Description = "Apple laptop",
                                Price = 2000,
                                CategoryId = laptopCategory.Id,
                                BusinessId = laptopBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Dell XPS",
                                Description = "Dell laptop",
                                Price = 1500,
                                CategoryId = laptopCategory.Id,
                                BusinessId = laptopBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "HP Pavilion",
                                Description = "HP laptop",
                                Price = 1300,
                                CategoryId = laptopCategory.Id,
                                BusinessId = laptopBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Lenovo ThinkPad",
                                Description = "Lenovo laptop",
                                Price = 1200,
                                CategoryId = laptopCategory.Id,
                                BusinessId = laptopBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Surface Pro",
                                Description = "Microsoft laptop",
                                Price = 1700,
                                CategoryId = laptopCategory.Id,
                                BusinessId = laptopBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "iPhone 13",
                                Description = "Apple smartphone",
                                Price = 1100,
                                CategoryId = smartphoneCategory.Id,
                                BusinessId = smartphoneBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Samsung Galaxy S21",
                                Description = "Samsung smartphone",
                                Price = 1000,
                                CategoryId = smartphoneCategory.Id,
                                BusinessId = smartphoneBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Google Pixel 6",
                                Description = "Google smartphone",
                                Price = 900,
                                CategoryId = smartphoneCategory.Id,
                                BusinessId = smartphoneBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "OnePlus 9",
                                Description = "OnePlus smartphone",
                                Price = 800,
                                CategoryId = smartphoneCategory.Id,
                                BusinessId = smartphoneBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Huawei P50",
                                Description = "Huawei smartphone",
                                Price = 850,
                                CategoryId = smartphoneCategory.Id,
                                BusinessId = smartphoneBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Samsung QLED",
                                Description = "Samsung TV",
                                Price = 1500,
                                CategoryId = tvCategory.Id,
                                BusinessId = tvBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Sony Bravia",
                                Description = "Sony TV",
                                Price = 1400,
                                CategoryId = tvCategory.Id,
                                BusinessId = tvBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "LG OLED",
                                Description = "LG TV",
                                Price = 1600,
                                CategoryId = tvCategory.Id,
                                BusinessId = tvBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "TCL 6-Series",
                                Description = "TCL TV",
                                Price = 900,
                                CategoryId = tvCategory.Id,
                                BusinessId = tvBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Hisense H8G",
                                Description = "Hisense TV",
                                Price = 800,
                                CategoryId = tvCategory.Id,
                                BusinessId = tvBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "iPad Pro",
                                Description = "Apple tablet",
                                Price = 1100,
                                CategoryId = tabletCategory.Id,
                                BusinessId = tabletBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Samsung Galaxy Tab S7",
                                Description = "Samsung tablet",
                                Price = 900,
                                CategoryId = tabletCategory.Id,
                                BusinessId = tabletBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Microsoft Surface Pro 7",
                                Description = "Microsoft tablet",
                                Price = 1200,
                                CategoryId = tabletCategory.Id,
                                BusinessId = tabletBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Lenovo Tab P11",
                                Description = "Lenovo tablet",
                                Price = 600,
                                CategoryId = tabletCategory.Id,
                                BusinessId = tabletBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Amazon Fire HD 10",
                                Description = "Amazon tablet",
                                Price = 150,
                                CategoryId = tabletCategory.Id,
                                BusinessId = tabletBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Bose QuietComfort 35 II",
                                Description = "Bose headphone",
                                Price = 350,
                                CategoryId = headphoneCategory.Id,
                                BusinessId = headphoneBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Sony WH-1000XM4",
                                Description = "Sony headphone",
                                Price = 350,
                                CategoryId = headphoneCategory.Id,
                                BusinessId = headphoneBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Jabra Elite 85h",
                                Description = "Jabra headphone",
                                Price = 250,
                                CategoryId = headphoneCategory.Id,
                                BusinessId = headphoneBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Sennheiser HD 660 S",
                                Description = "Sennheiser headphone",
                                Price = 500,
                                CategoryId = headphoneCategory.Id,
                                BusinessId = headphoneBusiness.Id,
                                Status = true
                            },
                            new Product
                            {
                                Id = Guid.NewGuid(),
                                Name = "Audio-Technica ATH-M50x",
                                Description = "Audio-Technica headphone",
                                Price = 150,
                                CategoryId = headphoneCategory.Id,
                                BusinessId = headphoneBusiness.Id,
                                Status = true
                            },
                        };

                        context.Products.AddRange(products);
                        context.SaveChanges();

                        _logger.LogInformation("Products have been initialized.");
                    }

                    // Initialize Campaigns
                    if (!context.Campaigns.Any())
                    {
                        var campaigns = new List<Campaign>();
                        _logger.LogInformation("Fetching businesses and their products...");

                        var businesses = context.Businesses
                            .Include(b => b.Products)
                            .Include(b => b.Users)
                            .ToList();

                        foreach (var business in businesses)
                        {
                            _logger.LogInformation($"Processing business: {business.Name}");

                            for (int i = 1; i <= 2; i++)
                            {
                                var users = business.Users.ToList();
                                _logger.LogInformation(
                                    $"Fetched {users.Count} users for business: {business.Name}"
                                );

                                // Continue to next iteration if there are no users or products for this business
                                if (!users.Any() || !business.Products.Any())
                                {
                                    _logger.LogInformation(
                                        $"No users or products found for business: {business.Name}. Skipping..."
                                    );
                                    continue;
                                }

                                // Ensure that we only create campaigns for businesses that have products
                                if (business.Products.Any())
                                {
                                    var ownerId = users[random.Next(users.Count)].Id;
                                    _logger.LogInformation(
                                        $"Selected user with ID {ownerId} as owner for campaign of business: {business.Name}"
                                    );

                                    campaigns.Add(
                                        new Campaign
                                        {
                                            Id = Guid.NewGuid(),
                                            Name = $"Campaign{i} for {business.Name}",
                                            Description =
                                                $"Description for Campaign{i} of {business.Name} , create by {ownerId}",
                                            BusinessId = business.Id,
                                            OwnerId = ownerId,
                                            ProductId = business.Products.First().Id
                                        }
                                    );

                                    _logger.LogInformation(
                                        $"Created Campaign{i} for business: {business.Name}"
                                    );
                                }
                            }
                        }

                        _logger.LogInformation("Adding campaigns to database...");
                        context.Campaigns.AddRange(campaigns);
                        context.SaveChanges();

                        _logger.LogInformation("Campaigns have been initialized.");
                    }

                    // Initialize CampaignDetails
                    if (!context.CampaignDetails.Any())
                    {
                        var campaignDetails = new List<CampaignDetail>();
                        var campaigns = context.Campaigns.Include(c => c.Product).ToList();

                        foreach (var campaign in campaigns)
                        {
                            int randomAllowed = random.Next(1, 10) * 10;
                            for (int i = 1; i <= 3; i++)
                            {
                                campaignDetails.Add(
                                    new CampaignDetail
                                    {
                                        Id = Guid.NewGuid(),
                                        Phase = i,
                                        AllowedQuantity = randomAllowed + i * 50,
                                        Price = campaign.Product.Price + i * 10 * 3,
                                        CampaignId = campaign.Id,
                                    }
                                );
                                ;
                                ;
                                ;
                            }
                        }

                        context.CampaignDetails.AddRange(campaignDetails);
                        context.SaveChanges();

                        _logger.LogInformation("Campaign Details have been initialized.");
                    }

                    if (!context.Orders.Any())
                    {
                        _logger.LogInformation("Fetching customers...");
                        var users = context.Users
                            .Include(u => u.Role)
                            .Where(u => u.Role.Name == "CUSTOMER")
                            .ToList();


                        // List of possible shipping statuses
                        var shippingStatuses = new List<string> { "Not Shipped", "Shipped", "In Transit", "Delivered", "Delayed" };

                        foreach (var user in users)
                        {
                            _logger.LogInformation($"Processing user: {user.Id} name: {user.FirstName} lastname: {user.LastName}");

                            var order = new Order
                            {
                                Id = Guid.NewGuid(),
                                CreatedAt = DateTime.Now,
                                TotalQuantity = 0,
                                TotalPrice = 0,
                                IsDeposited = false,
                                RequiredDepositAmount = 0,
                                Status = "Created",
                                RevicerName = user.FirstName + " " + user.LastName,
                                RevicerPhone = user.Phone,
                                ShippingAddress = user.Address,
                                ShippingProvince = user.Province,
                                ShippingWard = user.Ward,
                                ShippingDistrict = user.District,
                                ShippingCode = "000000" + random.Next(10000, 99999).ToString(),
                                ShippingPrice = 0,
                                ShippingStatus = shippingStatuses[random.Next(shippingStatuses.Count)],
                                UserId = user.Id,
                                OrderItems = new List<OrderItem>(),
                                Payments = new List<Payment>(),
                            };

                            _logger.LogInformation("Order created.");

                            // Assume that we have some CampaignDetails in the database
                            var campaignDetails = context.CampaignDetails.ToList();

                            if (campaignDetails.Any())
                            {
                                var numberOfItems = random.Next(1, 5);

                                _logger.LogInformation($"Creating {numberOfItems} order items...");

                                for (var i = 0; i < numberOfItems; i++)
                                {
                                    var campaignDetail = campaignDetails[
                                        random.Next(campaignDetails.Count)
                                    ];
                                    var quantity = random.Next(1, 4);
                                    var unitPrice = campaignDetail.Price;

                                    var orderItem = new OrderItem
                                    {
                                        Id = Guid.NewGuid(),
                                        CampaignDetailId = campaignDetail.Id,
                                        Quantity = quantity,
                                        UnitPrice = unitPrice,
                                        OrderId = order.Id
                                    };

                                    order.OrderItems.Add(orderItem);

                                    _logger.LogInformation(
                                        $"Order item created with quantity {quantity} and unit price {unitPrice}."
                                    );

                                    order.TotalQuantity += quantity;
                                    order.TotalPrice += quantity * unitPrice;
                                    order.RequiredDepositAmount = quantity * unitPrice * 1 / 2;
                                }

                                order.ShippingPrice = order.TotalQuantity * 10; // Assume shipping cost is 10 per item
                                order.TotalPrice += order.ShippingPrice;
                            }

                            var payment = new Payment
                            {
                                Id = Guid.NewGuid(),
                                Method = "Credit Card",
                                Total = order.TotalPrice * 0.5m, // Assume we pay 50% upfront
                                PaymentCount = 1,
                                PayedAt = DateTime.Now,
                                Status = "Completed",
                                UserId = user.Id,
                                OrderId = order.Id
                            };

                            _logger.LogInformation("Payment created.");

                            order.Payments.Add(payment);

                            order.IsDeposited = true;

                            context.Orders.Add(order);

                            _logger.LogInformation("Order added to database.");
                        }

                        _logger.LogInformation("Saving changes to database...");
                        context.SaveChanges();

                        _logger.LogInformation(
                            "Orders, order items, and payments created successfully."
                        );
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
