namespace PRM.API;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Project
    {
        private const string Base = $"{ApiBase}/projects";

        public const string All = Base;
        public const string Single = $"{Base}/{{id}}";
    }

    public static class Auth
    {
        private const string Base = $"{ApiBase}/auth";

        public const string Login = $"{Base}/login";
    }

    public static class Admin
    {
        private const string Base = $"{ApiBase}/admin";

        public static class Project
        {
            private const string Base = $"{Admin.Base}/projects";
            private const string Single = $"{Base}/{{id:int}}";
            public const string All = Base;
            public const string Create = Base;
            public const string Get = Single;
            public const string Update = Single;
            public const string Delete = Single;

            public const string Assign = $"{Single}/assign";
        }

        public static class User
        {
            private const string Base = $"{Admin.Base}/users";
            private const string Single = $"{Base}/{{id:int}}";
            public const string All = Base;
            public const string Create = Base;
            public const string Get = Single;
        }
    }
}