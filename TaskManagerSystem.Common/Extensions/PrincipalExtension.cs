using System.Security.Claims;
using System.Security.Principal;

namespace TaskManagerSystem.Common.Extensions
{
    public static class PrincipalExtension
    {
        public static Guid GetUserId(this IPrincipal principal) => Guid.Parse(principal.GetClaimByName(ClaimTypes.NameIdentifier)?.Value);

        public static Claim GetClaimByName(this IPrincipal principal, string name) =>
           GetClaimsByName(principal, name).SingleOrDefault();

        public static Claim[] GetClaimsByName(this IPrincipal principal, string name)
        {
            var identity = principal.Identity;
            var claims = GetClaims(identity);
            var neededClaim = claims.Where(x => x.Type == name);
            return neededClaim.ToArray();
        }

        private static List<Claim> GetClaims(IIdentity identity)
        {
            try
            {
                if (identity == null)
                {
                    throw new ArgumentNullException("identity");
                }
                var claimsIdentity = identity as ClaimsIdentity;
                var claims = claimsIdentity?.Claims.ToList();
                return claims;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
