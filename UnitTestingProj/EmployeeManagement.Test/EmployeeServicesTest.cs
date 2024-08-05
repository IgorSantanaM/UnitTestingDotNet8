﻿using EmployeeManagement.Business;
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
    }
}
