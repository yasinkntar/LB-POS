namespace LB_POS.Core.Features.ApplicationUser.Queries.Result
{
    public class GetUserByIdResponse
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        //public string? Country { get; set; }
    }
}
