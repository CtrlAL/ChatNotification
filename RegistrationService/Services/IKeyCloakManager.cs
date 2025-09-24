namespace RegistrationService.Services
{
    public interface IKeyCloakManager
    {
        public Task RegisterUserAsync(string email, string username, string password);
        public Task ResetPasswordAsync(string username, string newPassword);
    }
}