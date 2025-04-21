namespace CarJenWebApi.Dtos.UserDtos
{
    public class UserSummaryDto
    {
        public int UserId { get; set; }
        public string RoleTitle { get; set; }
        public string NationalNumber { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public int EvaluationScore { get; set; }
        public decimal Bonus { get; set; }
    }

}
