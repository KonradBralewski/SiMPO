namespace Shared.Contracts.Requests.Authentication
{
    public sealed record RegisterRequest(string Nickname,
                                         string Email,
                                         string Password,
                                         string Description,
                                         string DiscordId);
}
