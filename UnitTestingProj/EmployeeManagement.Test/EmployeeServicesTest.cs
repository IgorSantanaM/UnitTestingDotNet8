using EmployeeManagement.Business;
using EmployeeManagement.Business.EventArguments;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Services.Test;
using EmployeeManagement.Test.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace EmployeeManagement.Test
{
    [Collection("EmployeeServiceCollection")]
    public class EmployeeServicesTest // Class fixture share test context method : IClassFixture<EmployeeServiceFixture>
    {

        // Class fixture share test context method 
        private readonly EmployeeServiceFixture _fixture;
        private readonly ITestOutputHelper _testOutputHelper; // Output the wished output its good beacuse test runs in parallel and cannot make a Console.WriteLine

        public EmployeeServicesTest(EmployeeServiceFixture fixture, ITestOutputHelper testOutputHelper) 
            {
            _fixture = fixture;
            _testOutputHelper =  testOutputHelper;
            }
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustNotBeNew()
        {
            // Arrange - set up

            // Act - what the test must
            var internalEmployee = _fixture.EmployeeService.CreateInternalEmployee("Igor", "Santana");
            _testOutputHelper.WriteLine($"Employee after act: {internalEmployee.FirstName}");
            //Assert - verify if the test is true
            Assert.All(internalEmployee.AttendedCourses, course => Assert.False(course.IsNew));

        }
        [Fact]
        public async Task GiveRaise_RaiseBelowMinimumGiven_EmployeeInvalidRaiseExceptionMustBeThrown()
        {
            // Arrange
           var internalEmployee = new InternalEmployee("Igor", "Santana", 5, 300, false, 1);

            // Act & Assert
            await Assert.ThrowsAsync<EmployeeInvalidRaiseException>(
                async () => await _fixture.EmployeeService.GiveRaiseAsync(internalEmployee, 50)
            );
            
        }
        [Fact]
        public void NotifyOfAbsent_EmployeeisAbsent_OnEmployeeIsAbsentMustTrigger()
        {
            // Arrange
            var internalEmployee = new InternalEmployee("Igor", "Santana", 5, 300, false, 1);

            // Act & Assert
            Assert.Raises<EmployeeIsAbsentEventArgs>(
                handler => _fixture.EmployeeService.EmployeeIsAbsent += handler,
                handler => _fixture.EmployeeService.EmployeeIsAbsent -= handler,
                () => _fixture.EmployeeService.NotifyOfAbsence(internalEmployee));
        }
    }
}
