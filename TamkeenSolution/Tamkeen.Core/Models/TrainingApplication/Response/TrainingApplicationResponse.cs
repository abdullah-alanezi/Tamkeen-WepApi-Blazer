using System;
using System.Collections.Generic;
using System.Text;

namespace Tamkeen.Core.Models.TrainingApplication.Response
{
    public class TrainingApplicationResponse
    {
        public Guid Id { get; set; }

        public Guid TrainingProgramId { get; set; }
        public string ProgramTitle { get; set; } = string.Empty;

        public Guid TraineeId { get; set; }
        public string TraineeName { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string? HRFeedback { get; set; }

        public DateTime AppliedAt { get; set; }
    }
}
