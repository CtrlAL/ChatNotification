namespace RegistrationService.Domain
{
    public record RegisterRequest(string Email, string Username, string Password);
    public record ResetPasswordRequest(string Username, string NewPassword);
}
