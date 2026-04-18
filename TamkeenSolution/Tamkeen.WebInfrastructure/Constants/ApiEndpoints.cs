namespace Tamkeen.WebInfrastructure.Constants
{
    public static class ApiEndpoints
    {
        private const string Base = "api";

        // Trainees Endpoints
        public static class Trainees
        {
            private const string Controller = "Trainees";
            private const string BasePath = $"{Base}/{Controller}";

            public const string GetAll = $"{BasePath}/GetAll";
            public const string GetById = $"{BasePath}/GetById/{{0}}";
            public const string Create = $"{BasePath}/Create";
            public const string Update = $"{BasePath}/Update";
            public const string Delete = $"{BasePath}/Delete/{{0}}";
        }

        // Training Programs Endpoints
        public static class Programs
        {
            private const string Controller = "TrainingProgram";
            private const string BasePath = $"{Base}/{Controller}";

            public const string GetAll = $"{BasePath}/GetAll";
            public const string Create = $"{BasePath}/Create";
        }
    }
}