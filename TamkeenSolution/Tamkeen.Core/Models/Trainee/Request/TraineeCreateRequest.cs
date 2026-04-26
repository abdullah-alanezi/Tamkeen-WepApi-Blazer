using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Core.Models.BaseDto;

namespace Tamkeen.Core.Models.Trainee.Request
{
    public class TraineeCreateRequest: BaseDTOs
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public string University { get; set; } = string.Empty;
        public string Major { get; set; } = string.Empty;

        public string? ResumeUrl { get; set; }
        public string UserSSN { get; set; } = string.Empty;
    }
}
