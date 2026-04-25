using System;
using System.Collections.Generic;
using System.Text;

namespace Tamkeen.Core.Models.TrainingProgram.Response
{
    public class TrainingProgramResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty;

        public int ApplicationsCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int Capacity { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
