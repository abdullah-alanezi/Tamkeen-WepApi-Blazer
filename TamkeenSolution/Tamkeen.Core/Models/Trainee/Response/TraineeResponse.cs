using System;
using System.Collections.Generic;
using System.Text;

namespace Tamkeen.Core.Models.Trainee.Response
{
    public class TraineeResponse
    {
        public Guid Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public string University { get; set; } = string.Empty;
        public string Major { get; set; } = string.Empty;

        public string? ResumeUrl { get; set; }
    }
}
