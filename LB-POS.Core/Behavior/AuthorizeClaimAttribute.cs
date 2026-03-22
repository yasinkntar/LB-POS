namespace LB_POS.Core.Behavior
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizeClaimAttribute : Attribute
    {
        public string ClaimType { get; }
        public string ClaimValue { get; }

        public AuthorizeClaimAttribute(string claimType, string claimValue)
        {
            ClaimType = claimType;
            ClaimValue = claimValue;
        }
    }
}
