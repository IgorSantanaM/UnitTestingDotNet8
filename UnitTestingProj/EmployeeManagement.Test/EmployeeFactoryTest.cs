using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace EmployeeManagement.Test
{
    public class EmployeeFactoryTest : IDisposable
    {
        private EmployeeFactory _employeeFactory;
        
        // Contructor and Dispose share test context method
        public EmployeeFactoryTest()
        {
            _employeeFactory = new EmployeeFactory();
        }

        public void Dispose()
        {
           // Clean up the setup code, if required
        }
        [Fact]
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500() 
        {
            
            //Act
            var employee = (InternalEmployee)_employeeFactory.CreateEmployee("Kevin", "Dockx");

            //Assert
            Assert.Equal(2500, employee.Salary);

            // A unit test should contain only one assert or two or moreasserts of the same behavior example:
            //  Assert.True(employee.Salary >= 2500 );
            // Assert.True(employee.Salar   y >= 3500);
            // 

        }
        [Fact]
        public void CreateEmployee_IsExternalistrue_ReturnTypeMustBeExternal()
        { 
            //Act
            var employee = _employeeFactory.CreateEmployee("Kevin", "Dockx", "Marvin", true);
             
            //Assert
            Assert.IsType<ExternalEmployee>(employee);
            //Assert.IsAssignableFrom<Employee>(employee);

            // Not recommended to test private methods, but if wanted its a good practice to test the methods that implements a smal behavior of the private method so you can test it

        }


    }
}