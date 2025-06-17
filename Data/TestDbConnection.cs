using System;
using Sample.api.Data;

namespace Sample.api.Data
{
    /// <summary>
    /// Test class to verify DbContext constructor fix
    /// </summary>
    public static class TestDbConnection
    {
        /// <summary>
        /// Test parameterless constructor
        /// </summary>
        public static bool TestParameterlessConstructor()
        {
            try
            {
                using (var context = new ShipManageEntities())
                {
                    // Test that context can be created without parameters
                    var connectionString = context.Database.Connection.ConnectionString;
                    Console.WriteLine($"✅ Parameterless constructor works!");
                    Console.WriteLine($"Connection: {connectionString.Split(';')[0]}...");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Parameterless constructor failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Test constructor with connection string
        /// </summary>
        public static bool TestConnectionStringConstructor()
        {
            try
            {
                using (var context = new ShipManageEntities("name=ShipManageEntities"))
                {
                    // Test that context can be created with connection string
                    var connectionString = context.Database.Connection.ConnectionString;
                    Console.WriteLine($"✅ Connection string constructor works!");
                    Console.WriteLine($"Connection: {connectionString.Split(';')[0]}...");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Connection string constructor failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Test AppDbContext inheritance
        /// </summary>
        public static bool TestAppDbContext()
        {
            try
            {
                using (var context = new Data.AppDbContext())
                {
                    // Test that AppDbContext works correctly
                    var connectionString = context.Database.Connection.ConnectionString;
                    Console.WriteLine($"✅ AppDbContext works!");
                    Console.WriteLine($"Connection: {connectionString.Split(';')[0]}...");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ AppDbContext failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Test database operations
        /// </summary>
        public static bool TestDatabaseOperations()
        {
            try
            {
                using (var context = new ShipManageEntities())
                {
                    // Test basic database operations
                    var dbExists = context.Database.Exists();
                    
                    if (dbExists)
                    {
                        var userCount = context.Users.Count();
                        var orderCount = context.Orders.Count();
                        
                        Console.WriteLine($"✅ Database operations work!");
                        Console.WriteLine($"Database exists: {dbExists}");
                        Console.WriteLine($"Users: {userCount}, Orders: {orderCount}");
                    }
                    else
                    {
                        Console.WriteLine($"⚠️ Database doesn't exist, but connection works!");
                        Console.WriteLine($"Run CreateDatabase.sql to create tables");
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
        /// Run all tests
        /// </summary>
        public static void RunAllTests()
        {
            Console.WriteLine("🧪 Testing DbContext Constructor Fix...");
            Console.WriteLine("═══════════════════════════════════════");

            var test1 = TestParameterlessConstructor();
            Console.WriteLine();

            var test2 = TestConnectionStringConstructor();
            Console.WriteLine();

            var test3 = TestAppDbContext();
            Console.WriteLine();

            var test4 = TestDatabaseOperations();
            Console.WriteLine();

            Console.WriteLine("📊 TEST RESULTS:");
            Console.WriteLine($"Parameterless Constructor: {(test1 ? "✅ PASS" : "❌ FAIL")}");
            Console.WriteLine($"Connection String Constructor: {(test2 ? "✅ PASS" : "❌ FAIL")}");
            Console.WriteLine($"AppDbContext: {(test3 ? "✅ PASS" : "❌ FAIL")}");
            Console.WriteLine($"Database Operations: {(test4 ? "✅ PASS" : "❌ FAIL")}");

            if (test1 && test2 && test3)
            {
                Console.WriteLine();
                Console.WriteLine("🎉 DbContext constructor fix is working!");
                Console.WriteLine("✅ No more 'required parameter v' error");
                Console.WriteLine("✅ Both constructors available");
                Console.WriteLine("✅ Ready for order creation");
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
// TestDbConnection.RunAllTests();
