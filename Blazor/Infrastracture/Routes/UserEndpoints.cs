namespace Blazor.Infrastracture.Routes
{
    public static class UserEndpoints
    {
        public static string GetCurrentUser = "api/identity/users/me";

        public static string GetAll = "api/identity/users";

        public static string Get(string userId)
        {
            return $"api/identity/user/{userId}";
        }

        public static string GetUserRoles(string userId)
        {
            return $"api/identity/user/roles/{userId}";
        }

        public static string Login = "api/auth/login";

        public static string Register = "api/auth/register";  

        public static string ForgotPassword = "api/identity/user/forgot-password";
    }
}
