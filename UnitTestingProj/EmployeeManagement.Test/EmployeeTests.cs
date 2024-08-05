using EmployeeManagement.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test
{
    public class EmployeeTests
    {
        [Fact]
        public void EmployeeFullNamePropertyGetter_InputFirstNameAndLastName_FullNameIsConcatenation()
        {

            // Arrange
            var employee = new InternalEmployee("Igor", "Santana", 0, 2500, false, 1);
            // Act
            employee.FirstName = "Lucia";
            employee.LastName = "Shelton";
            // Assert
            Assert.Equal("Lucia Shelton", employee.FullName);
        }
        [Fact]
        public void EmployeeFullNamePropertyGetter_InputFirstNameAndLastName_FirstNameEqualsLastName()
        {

            // Arrange
            var employee = new InternalEmployee("Igor", "Santana", 0, 2500, false, 1);
            // Act
            employee.FirstName = "Lucia";
            employee.LastName = "Shelton";
            // Assert
            Assert.StartsWith(employee.FirstName, employee.FullName);
        }
    }
}
