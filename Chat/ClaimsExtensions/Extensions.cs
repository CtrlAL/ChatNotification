using System.Security.Claims;

namespace ChatService.ClaimsExtensions
{
    public static class Extensions
    {
        public static void GetUserInfo(this ClaimsPrincipal? user, out string? username, out string? userId)
        {
            username = user?.FindFirst("preferred_username")?.Value
                        ?? user?.FindFirst("name")?.Value;

            userId = user?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static bool CheckTokenContinuity(this ClaimsPrincipal? user, string? username, string? userId)
        {
            if (user == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                return false;
            }

            return true;
        }
    }
}
