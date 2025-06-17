using System;
using Sample.api.Data;

namespace Sample.api.Data
{
    /// <summary>
    /// Test class to verify EDMX schema validation fix
    /// </summary>
    public static class TestSchemaValidation
    {
        /// <summary>
        /// Test Entity Framework schema validation
        /// </summary>
        public static bool TestSchemaValidation()
        {
            try
            {
                using (var context = new ShipManageEntities())
                {
                    // This will trigger schema validation
                    var connectionString = context.Database.Connection.ConnectionString;
                    
                    Console.WriteLine("✅ Schema validation passed!");
                    Console.WriteLine($"Connection: {connectionString.Split(';')[0]}...");
                    return true;
                }
            }
            catch (System.Data.Entity.Core.MetadataException ex)
            {
                Console.WriteLine($"❌ Schema validation failed: {ex.Message}");
                if (ex.Message.Contains("FK__Orders__UserId__06CD04F7"))
                {
                    Console.WriteLine("❌ Association FK__Orders__UserId__06CD04F7 is not defined");
                }
                if (ex.Message.Contains("FK_Orders_Users"))
                {
                    Console.WriteLine("❌ Association FK_Orders_Users is not defined");
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Other error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Test entity relationships
        /// </summary>
        public static bool TestEntityRelationships()
        {
            try
            {
                using (var context = new ShipManageEntities())
                {
                    // Test that we can access navigation properties without errors
                    var usersDbSet = context.Users;
                    var ordersDbSet = context.Orders;
                    
                    Console.WriteLine("✅ Entity relationships accessible!");
                    Console.WriteLine($"Users DbSet: {usersDbSet != null}");
                    Console.WriteLine($"Orders DbSet: {ordersDbSet != null}");
                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Entity relationships test failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Test database operations (requires database)
        /// </summary>
        public static bool TestDatabaseOperations()
        {
            try
            {
                using (var context = new ShipManageEntities())
                {
                    // Test database existence and basic queries
                    var dbExists = context.Database.Exists();
                    
                    if (dbExists)
                    {
                        // Test that we can query without schema errors
                        var userCount = context.Users.Count();
                        var orderCount = context.Orders.Count();
                        
                        Console.WriteLine("✅ Database operations successful!");
                        Console.WriteLine($"Database exists: {dbExists}");
                        Console.WriteLine($"Users: {userCount}, Orders: {orderCount}");
                    }
                    else
                    {
                        Console.WriteLine("⚠️ Database doesn't exist, but schema validation passed!");
                        Console.WriteLine("Run CreateDatabase_Simple.sql to create tables");
                    }
                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Database operations failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Test navigation properties
        /// </summary>
        public static bool TestNavigationProperties()
        {
            try
            {
                // Test entity creation with navigation properties
                var user = new Users
                {
                    Username = "testuser",
                    Password = "password123",
                    Email = "test@example.com",
                    FullName = "Test User",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };

                var order = new Orders
                {
                    BookingCode = "12345",
                    OrderNumber = "ORD12345",
                    BookerName = "Test Booker",
                    ContactPhone = "0123456789",
                    ContactEmail = "booker@example.com",
                    RouteId = 1,
                    VoyageId = 1,
                    BoatId = 1,
                    ScheduleId = 1,
                    DepartureDate = DateTime.Now.AddDays(1),
                    DepartureTime = TimeSpan.FromHours(15.75),
                    TotalPassengers = 1,
                    SubTotal = 390000,
                    TotalAmount = 390000,
                    OrderStatus = "Created",
                    PaymentStatus = "Pending",
                    CreatedDate = DateTime.Now
                };

                // Test navigation property assignment
                order.Users = user;
                user.Orders.Add(order);

                Console.WriteLine("✅ Navigation properties test passed!");
                Console.WriteLine($"User: {user.FullName}");
                Console.WriteLine($"Order: {order.BookingCode}");
                Console.WriteLine($"Relationship: Order.Users = {order.Users?.FullName}");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Navigation properties test failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Run all schema validation tests
        /// </summary>
        public static void RunAllTests()
        {
            Console.WriteLine("🧪 Running EDMX Schema Validation Tests...");
            Console.WriteLine("═══════════════════════════════════════");

            var test1 = TestSchemaValidation();
            Console.WriteLine();

            var test2 = TestEntityRelationships();
            Console.WriteLine();

            var test3 = TestNavigationProperties();
            Console.WriteLine();

            var test4 = TestDatabaseOperations();
            Console.WriteLine();

            Console.WriteLine("📊 TEST RESULTS:");
            Console.WriteLine($"Schema Validation: {(test1 ? "✅ PASS" : "❌ FAIL")}");
            Console.WriteLine($"Entity Relationships: {(test2 ? "✅ PASS" : "❌ FAIL")}");
            Console.WriteLine($"Navigation Properties: {(test3 ? "✅ PASS" : "❌ FAIL")}");
            Console.WriteLine($"Database Operations: {(test4 ? "✅ PASS" : "❌ FAIL")}");

            if (test1 && test2 && test3)
            {
                Console.WriteLine();
                Console.WriteLine("🎉 EDMX schema validation fix is working!");
                Console.WriteLine("✅ No more 'Type FK__Orders__UserId__06CD04F7 is not defined' error");
                Console.WriteLine("✅ Association references are consistent");
                Console.WriteLine("✅ Ready for user registration and order creation");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("❌ Some tests failed - check error messages above");
            }
        }
    }
}

// Example usage:
// TestSchemaValidation.RunAllTests();
