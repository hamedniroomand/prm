namespace PMS.Application.Common;

public class ApplicationSettings(string jwtString)
{
    public string JwtString { get; } = jwtString;
}