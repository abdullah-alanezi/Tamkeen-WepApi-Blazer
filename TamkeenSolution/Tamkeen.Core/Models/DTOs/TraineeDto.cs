using System;
using System.Collections.Generic;
using System.Text;

namespace Tamkeen.Core.Models.DTOs
{
    public class TraineeDto
    {
        public Guid Id { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string University { get; set; } = string.Empty;
        public string Major { get; set; } = string.Empty;
    }
}
