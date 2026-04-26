using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Core.Models.BaseDto;

namespace Tamkeen.Core.Models.TrainingProgram.Request
{
    public class TrainingProgramRequest: BaseDTOs
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int Capacity { get; set; }
    }
}
