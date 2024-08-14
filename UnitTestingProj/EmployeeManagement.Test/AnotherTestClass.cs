using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test
{
    public class AnotherTestClass
    {
        public void WaitUntillFinish_ParallelTest_ReturnTrue()
        {
            Thread.Sleep(5000);
            Assert.True(true);
        }
    }
}
