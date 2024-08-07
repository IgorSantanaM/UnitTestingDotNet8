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

namespace EmployeeManagement.Test
{
    public class EmployeeServicesTest : IClassFixture<EmployeeServiceFixture>
    {

        // Class fi
        private readonly EmployeeServiceFixture _fixture;

        public EmployeeServicesTest(EmployeeServiceFixture fixture) 
            {
            _fixture = fixture;
            }
        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustNotBeNew()
        {
            // Arrange - set up

            // Act - what the test must
            var internalEmployee = _fixture.EmployeeService.CreateInternalEmployee("Igor", "Santana");

            //Assert - verify if the test is true
            Assert.All(internalEmployee.AttendedCourses, course => Assert.False(course.IsNew));

        }
        [Fact]
        public async Task GiveRaise_RaiseBelowMinimumGiven_EmployeeInvalidRaiseExceptionMustBeThrown()
        {
            // Arrange
            var emmployeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(emmployeeManagementTestDataRepository, new EmployeeFactory());
            var internalEmployee = new InternalEmployee("Igor", "Santana", 5, 300, false, 1);

            // Act & Assert
            await Assert.ThrowsAsync<EmployeeInvalidRaiseException>(
                async () => await employeeService.GiveRaiseAsync(internalEmployee, 50)
            );
            
        }
        [Fact]
        public void NotifyOfAbsent_EmployeeisAbsent_OnEmployeeIsAbsentMustTrigger()
        {
            // Arrange
            var emmployeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(emmployeeManagementTestDataRepository, new EmployeeFactory());
            var internalEmployee = new InternalEmployee("Igor", "Santana", 5, 300, false, 1);

            // Act & Assert
            Assert.Raises<EmployeeIsAbsentEventArgs>(
                handler => employeeService.EmployeeIsAbsent += handler,
                handler => employeeService.EmployeeIsAbsent -= handler,
                () => employeeService.NotifyOfAbsence(internalEmployee));
        }
    }
}
