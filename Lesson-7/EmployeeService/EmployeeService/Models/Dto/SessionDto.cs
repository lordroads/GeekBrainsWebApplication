using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Models.Dto
{
    public class SessionDto
    {
        public int SessionId { get; set; }
        public string SessionToken { get; set; }
        public AccountDto Account { get; set; }
    }
}
