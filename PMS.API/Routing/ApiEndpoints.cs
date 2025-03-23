namespace PMS.API;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Project
    {
        private const string Base = $"{ApiBase}/projects";

        public const string All = Base;
        public const string Single = $"{Base}/{{id}}";
        public const string Get = Single;
        public const string Create = Single;
        public const string Update = Single;
        public const string Delete = Single;
    }

    public static class Auth
    {
        private const string Base = $"{ApiBase}/auth";

        public const string Login = $"{Base}/login";
    }
}