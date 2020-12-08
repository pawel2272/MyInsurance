using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInsurance.BusinessLogic.Services.Dto
{
    public class PolicyDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public int Amount { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public System.DateTime DateOfEnding { get; set; }
    }
}
