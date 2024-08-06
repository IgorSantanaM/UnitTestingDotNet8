using EmployeeManagement.Business;
using EmployeeManagement.Business.EventArguments;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Services.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test
{
    public class EmployeeServicesTest
    {
        public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustNotBeNew()
        {
            // Arrange - set up
            var emmployeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(emmployeeManagementTestDataRepository, new EmployeeFactory());

            // Act - what the test must
            var internalEmployee = employeeService.CreateInternalEmployee("Igor", "Santana");

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
