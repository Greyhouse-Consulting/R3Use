using System.Threading.Tasks;
using R3Use.Core;
using R3Use.Core.Repository;
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
                var repo = new ProspectRepository(db);

                await repo.Add(new Prospect
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
    }   
}
