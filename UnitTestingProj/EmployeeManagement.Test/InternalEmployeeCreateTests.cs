using AutoMapper;
using EmployeeManagement.Business;
using EmployeeManagement.Controllers;
using EmployeeManagement.Test.Fixtures;
using EmployeeManagement.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test
{
    public class InternalEmployeeCreateTests
    {
        [Fact]
        public async Task AddInternalEmployee_InvalidInput_MustReturnBadRequest()
        {
            // Arrange
            var employeeServiceMock = new Mock<IEmployeeService>();
            var mapperMock = new Mock<IMapper>();
            var internalEmployeeController = new InternalEmployeeController(employeeServiceMock.Object, mapperMock.Object);

            var createInternalEmployeeViewModel =
                new CreateInternalEmployeeViewModel();

            internalEmployeeController.ModelState.AddModelError("First Name", "Required");

            // Act

            var result = await internalEmployeeController.AddInternalEmployee(createInternalEmployeeViewModel);

            // Assert 
            var badRequestResult = Assert.IsType<BackRequestObjectResult>(result);
        }
    }
}
