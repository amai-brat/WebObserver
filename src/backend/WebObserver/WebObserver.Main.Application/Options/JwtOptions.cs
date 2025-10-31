namespace WebObserver.Main.Application.Options;

public class JwtOptions
{
    public required string Key { get; set; }
    public required int AccessTokenLifetimeInMinutes { get; set; }
    public required List<string> AdminEmails { get; set; }
}