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

                repo.Save(new Assignment
                {
                    Name = "Name"
                });

                var i = await repo.AllAsync();

                Assert.Equal("Name", i[0].Name);
                Assert.Equal(1, i.Count);
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
                    Name = "Name"
                };

                var start = new DateTime(2017, 01, 01);
                var end = new DateTime(2017, 02, 02);

                var period = new Period { Description = "Desc", Start = start, End = end };

                assignment.AddPeriod(period);


                repo.Save(assignment);
                period.AssignmentId = assignment.Id;
                db.Save(period);

                var all = db.FetchOneToMany<Assignment>(x => x.Periods,
                    "select a.*, p.* from assignments a inner join periods p where a.id = p.assignmentid");

                var i = await repo.AllAsync();

                Assert.Equal("Name", i[0].Name);
                Assert.Equal(1, i[0].Periods.Count);
            }

            TearDown();
        }
    }
}
