using System;
using System.Threading.Tasks;
using R3Use.Core;
using R3Use.Core.Entities;
using R3Use.Core.Repository;
using R3Use.Infrastructure;
using Xunit;

namespace NPoco.Integration.Tests
{
    public class RepositoryItegrationTests : SqliteIntegration
    {
        [Fact]
        public async Task Should_Store_And_Load_One_Entity()
        {
            Setup();

            using (var db = CreateDatabase())
            {
                var repo = new AssignmentRepository(db);

                await repo.AddAsync(new Assignment
                {
                    Id = 200,
                    Name = "Name"
                });

                var i = await repo.GetAsync(200);

                Assert.Equal(200, i.Id);
                Assert.Equal("Name", i.Name);
            }

            TearDown();
        }

        [Fact]
        public async Task Should_Store_And_Load_One_Entity_With_Sub_Entities()
        {
            Setup();

            using (var db = CreateDatabase())
            {
                var repo = new AssignmentRepository(db);

                var assignment = new Assignment
                {
                    Id = 200,
                    Name = "Name"
                };
                var start = new DateTime(2017, 01, 01);
                var end = new DateTime(2017, 02, 02);

                assignment.AddPeriod(new Period { Description = "Desc", Start = start, End = end });

                await repo.AddAsync(assignment);

                var i = await repo.GetAsync(200);

                Assert.Equal(200, i.Id);
                Assert.Equal("Name", i.Name);
            }

            TearDown();
        }
    }
}
