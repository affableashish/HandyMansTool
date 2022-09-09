using Microsoft.EntityFrameworkCore;
using HMT.Web.Server.Data;
using HMT.Web.Server.Features.ViewRepairs;
using HMT.Web.Server.Models.Entities;
using Moq;
using Shouldly;

namespace HMT.UnitTests.RepairOrders
{
    public class RepairOrderTests
    {
        [Fact]
        public void Should_Calculate_Correct_Cost()
        {
            // ARRANGE
            var repairOrder = new RepairOrder() { Reason = "Crash", SomeUniqueThingInDb = "Something" };

            // ACT
            var cost = repairOrder.GetCostOfRepairOrder();

            // ASSERT
            Assert.Equal(2000, cost);
        }

        [Fact]
        public async Task Should_Return_All_Repair_Orders_UsingInMemoryDatabase()
        {
            // ARRANGE
            var options = new DbContextOptionsBuilder<HMTDbContext>()
                                .UseInMemoryDatabase(databaseName: "MMTDatabaseInMemory")
                                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new HMTDbContext(options))
            {
                context.RepairOrders.Add(new RepairOrder { OrderId = 1, SomeUniqueThingInDb = "something1", Reason = "Reason 1" });
                context.RepairOrders.Add(new RepairOrder { OrderId = 2, SomeUniqueThingInDb = "something2", Reason = "Reason 2" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new HMTDbContext(options))
            {
                // Setup the handler
                var viewRepairsHandler = new ViewRepairsPage.Handler(context);

                // ACT
                var repairOrderQuery = new ViewRepairsPage.Query();
                var result = await viewRepairsHandler.Handle(repairOrderQuery, new CancellationToken());

                // ASSERT
                result.ShouldNotBeNull();
                result.RepairOrders.Count().ShouldBe(2);
                // Other Asserts
            }
        }
    }
}