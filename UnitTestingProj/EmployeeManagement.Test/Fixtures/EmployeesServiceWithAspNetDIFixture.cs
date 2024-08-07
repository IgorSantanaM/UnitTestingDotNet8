using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Services;
using EmployeeManagement.Services.Test;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test.Fixtures
{
    public class EmployeesServiceWithAspNetDIFixture : IDisposable
    {
        private readonly ServiceProvider _serviceProvider;

        public IEmployeeManagementRepository EmployeeManagementTestDataRepository
        { 
            get
            {
#pragma warning disable CS8603 // Possível retorno de referência nula.
                return _serviceProvider.GetService<IEmployeeManagementRepository>();
#pragma warning restore CS8603 // Possível retorno de referência nula.
            } 
        }
        public IEmployeeService EmployeeService 
        {
            get
            {
#pragma warning disable CS8603 // Possível retorno de referência nula.
                return _serviceProvider.GetService<IEmployeeService>();
#pragma warning restore CS8603 // Possível retorno de referência nula.
            }
        }
        public EmployeesServiceWithAspNetDIFixture()
        {
            var services = new ServiceCollection();
            services.AddScoped<IEmployeeFactory>();
            services.AddScoped<IEmployeeManagementRepository, EmployeeManagementTestDataRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            // build provider 
            _serviceProvider = services.BuildServiceProvider();
            

        }
        public void Dispose()
        {
            // clean up and set upp the code if required.
        }
    }
}
