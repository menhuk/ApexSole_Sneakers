using System.Security.Claims;

namespace ApexSole_Sneakers
{
    public static class Extensions
    {
        public  static string GetUserId(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
