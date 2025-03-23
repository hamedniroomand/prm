namespace PMS.API;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Project
    {
        private const string Base = $"{ApiBase}/projects";

        public const string All = Base;
        public const string Get = $"{Base}/{{id}}";
        public const string Create = Base;
        public const string Update = Get;
        public const string Delete = Get;
    }

    public static class Auth
    {
        private const string Base = $"{ApiBase}/auth";

        public const string Login = $"{Base}/login";
    }
}