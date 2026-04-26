using System;
using System.Collections.Generic;
using System.Text;
namespace Tamkeen.WebInfrastructure.Constants.Routes
{
    public static class ApiEndpoints
    {
        private const string Base = "api";

        // =============================
        // 🔐 Identity Endpoints
        // =============================
        public static class Identity
        {
            private const string Controller = "Auth";
            private const string BasePath = $"{Base}/{Controller}";

            public const string Login = $"{BasePath}/login";
            public const string RefreshToken = $"{BasePath}/refresh";
            public const string Logout = $"{BasePath}/logout";
        }

        // =============================
        // 👤 Trainees Endpoints
        // =============================
        public static class Trainees
        {
            private const string Controller = "Trainees";
            private const string BasePath = $"{Base}/{Controller}";

            public const string GetAll = $"{BasePath}/GetAll";
            public const string GetById = $"{BasePath}/GetById/{{id}}"; // 🔥 تحسين
            public const string Create = $"{BasePath}/AddTrainee";
            public const string Update = $"{BasePath}/Update";
            public const string Delete = $"{BasePath}/Delete/{{id}}";
        }

        // =============================
        // 📚 Training Programs
        // =============================
        public static class Programs
        {
            private const string Controller = "TrainingProgram";
            private const string BasePath = $"{Base}/{Controller}";

            public const string GetAll = $"{BasePath}/GetAllPrograms";
            public const string GetById = $"{BasePath}/GetById/{{id}}"; // 🔥 أضفناها
            public const string Create = $"{BasePath}/Create";
            public const string Update = $"{BasePath}/Update";
            public const string Delete = $"{BasePath}/Delete/{{id}}";
        }

        // =============================
        // 📝 Applications (مهم لمشروعك)
        // =============================
        public static class Applications
        {
            private const string Controller = "TrainingApplication";
            private const string BasePath = $"{Base}/{Controller}";

            public const string Apply = $"{BasePath}/Apply";
            public const string GetAll = $"{BasePath}/GetAll";
            public const string Accept = $"{BasePath}/Accept/{{id}}";
            public const string Reject = $"{BasePath}/Reject/{{id}}";
        }
    }
}
