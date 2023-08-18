namespace Blazor.Infrastracture.Routes
{
    public static class UserEndpoints
    {
        public static string GetAll = "api/identity/user";

        public static string Get(string userId)
        {
            return $"api/identity/user/{userId}";
        }

        public static string GetUserRoles(string userId)
        {
            return $"api/identity/user/roles/{userId}";
        }

        public static string Login = "auth/login";

        public static string Register = "auth/register";  

        public static string ForgotPassword = "api/identity/user/forgot-password";
    }
}
