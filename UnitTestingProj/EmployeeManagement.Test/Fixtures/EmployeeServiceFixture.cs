using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Services;
using EmployeeManagement.Services.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test.Fixtures
{
    public class EmployeeServiceFixture : IDisposable
    {
        // readonly
        public IEmployeeManagementRepository EmployeeManagementRepository { get;}

        // readonly
        public EmployeeService EmployeeService { get;}
        public EmployeeServiceFixture() 
        {
            var emmployeeManagementTestDataRepository = new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(emmployeeManagementTestDataRepository, new EmployeeFactory());
        }

        public void Dispose()
        {
            // cleanup the setup code, if required
        }
    }
}
