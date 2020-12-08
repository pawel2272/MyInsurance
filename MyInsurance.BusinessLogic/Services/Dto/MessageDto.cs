using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInsurance.BusinessLogic.Services.Dto
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int CaseId { get; set; }
        public string Text { get; set; }
    }
}
