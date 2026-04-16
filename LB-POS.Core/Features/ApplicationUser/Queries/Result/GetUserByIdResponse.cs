namespace LB_POS.Core.Features.ApplicationUser.Queries.Result
{
    public class GetUserByIdResponse
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        //public string? Country { get; set; }
    }
}
