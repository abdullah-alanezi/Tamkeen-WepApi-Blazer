using System;
using System.Collections.Generic;
using System.Text;

namespace Tamkeen.Core.Models.DTOs
{
    public class TrainingApplicationDto
    {
        public Guid Id { get; set; }
        public Guid TraineeId { get; set; }
        public Guid TrainingProgramId { get; set; }

        public string TraineeName { get; set; } = string.Empty;
        public string ProgramTitle { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    }
}
