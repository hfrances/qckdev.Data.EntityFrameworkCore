using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace qckdev.Data.EntityFrameworkCore.Test
{
    [TestClass]
    public class UnitTest
    {

        [TestMethod]
        public async Task TestMethodQueryTest()
        {
            const string connectionString = "Data Source=:memory:";
            using var conn = new SqliteConnection(connectionString);
            using var context = Extensions.CreateDbContext<TestDbContext>(
                builder => builder.UseSqlite(conn)
            );
            bool created;
            int rdo;

            await context.Database.OpenConnectionAsync();

            try
            {
                var entity = new Entities.Test { TestId = Guid.NewGuid(), Name = "Test 1" };

                // Create schema.
                created = await context.Database.EnsureCreatedAsync();
                Assert.IsTrue(created);

                // Initialize data.
                await context.Tests.AddAsync(entity);
                rdo = await context.SaveChangesAsync();
                Assert.AreEqual(1, rdo);

                // Check if data is created properly.
                using (var contextNew = Extensions.CreateDbContext<TestDbContext>(
                    builder => builder.UseSqlite(conn)))
                {
                    var fromContext = await contextNew.Tests.SingleOrDefaultAsync();
                    Assert.AreEqual(
                        JsonSerializer.Serialize(entity), 
                        JsonSerializer.Serialize(fromContext),
                        "Failed checking if data has been created properly"
                    );
                }
            }
            finally
            {
                await context.Database.CloseConnectionAsync();
            }
        }

        [TestMethod]
        public async Task TrimEndStringTypeHandlerTestAsync()
        {
            const string connectionString = "Data Source=:memory:";
            using var conn = new SqliteConnection(connectionString);
            using var context = Extensions.CreateDbContext<TestDbContext>(
                builder => builder.UseSqlite(conn)
            );
            bool created;
            int rdo;

            await context.Database.OpenConnectionAsync();

            try
            {
                var entity = new Entities.Test
                {
                    TestId = Guid.NewGuid(),
                    Name = "Test 1",
                    Spaced = "Value with spaces   ",
                    SpacedRaw = "Value with spaces   "
                };

                // Create schema.
                created = await context.Database.EnsureCreatedAsync();
                Assert.IsTrue(created);

                // Initialize data.
                await context.Tests.AddAsync(entity);
                rdo = await context.SaveChangesAsync();
                Assert.AreEqual(1, rdo);

                // Check TrimEnd functionality.
                using (var contextNew = Extensions.CreateDbContext<TestDbContext>(
                    builder => builder.UseSqlite(conn)))
                {
                    var fromContext = await contextNew.Tests.SingleOrDefaultAsync();

                    Assert.AreEqual(
                        entity.Spaced.TrimEnd(), fromContext.Spaced,
                        "Failed checking dapper functionality"
                    );
                    Assert.AreEqual(
                        entity.SpacedRaw, fromContext.SpacedRaw,
                        "Failed checking dapper functionality"
                    );
                }
            }
            finally
            {
                await context.Database.CloseConnectionAsync();
            }
        }
    }
}
