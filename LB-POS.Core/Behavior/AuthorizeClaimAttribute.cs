using Microsoft.AspNetCore.Mvc;

namespace LB_POS.Core.Behavior
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class HasPermissionAttribute : TypeFilterAttribute
    {
        public string Permission { get; }
        public HasPermissionAttribute(string permission) : base(typeof(PermissionFilter))
        {
            Permission = permission;
            Arguments = new object[] { permission };
        }
    }
}
