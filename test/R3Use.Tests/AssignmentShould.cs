using R3Use.Core.Entities;
using Xunit;

namespace NPoco.Tests
{
    public class AssignmentShould
    {
        [Fact]
        public void Add_Period()
        {
            // Arrange 
            var p = new Assignment();

            // Act
            p.AddPeriod(new Period());

            // Assert
            //Assert.Equal(1, p.Periods.Count);
        }

        
    }
}