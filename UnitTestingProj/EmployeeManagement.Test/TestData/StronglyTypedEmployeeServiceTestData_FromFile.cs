using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test.TestData
{
    public class StronglyTypedEmployeeServiceTestData_FromFile : TheoryData<int, bool>
    {
        public StronglyTypedEmployeeServiceTestData_FromFile()
        {
            string[] testDataLines = File.ReadAllLines("TestData/EmployeeServiceTestData.csv");
            
            foreach(string line in testDataLines)
            {
                //split the string
                string[] splitString = line.Split(",");
                // try parsing
                if (int.TryParse(splitString[0], out int raise)
                    && bool.TryParse(splitString[1], out bool minimumRaiseGiven))
                {
                    // add test data
                    Add(raise, minimumRaiseGiven);
                }
            }
        }
    }
}
