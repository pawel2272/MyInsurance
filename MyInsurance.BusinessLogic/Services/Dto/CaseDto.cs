namespace MyInsurance.BusinessLogic.Services.Dto
{
    public class CaseDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Description { get; set; }
        public string Decision { get; set; }
        public bool IsEnded { get; set; }
        public int CustomerId { get; set; }
    }
}
