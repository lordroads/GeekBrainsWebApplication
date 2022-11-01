using EmployeeService.Models.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServiceTests.Data
{
    public class EmployeeTypeDtoDataTest : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return  new object[] { new EmployeeTypeDto { Id = 1, Description = "test1"} };
            yield return new object[] { new EmployeeTypeDto { Id = 2, Description = "test2" } };
            yield return new object[] { new EmployeeTypeDto { Id = 3, Description = "test3" } };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
