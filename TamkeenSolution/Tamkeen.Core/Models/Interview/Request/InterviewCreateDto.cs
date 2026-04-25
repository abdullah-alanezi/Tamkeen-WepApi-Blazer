using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Core.Models.BaseDto;

namespace Tamkeen.Core.Models.Interview.Request
{
    public class InterviewCreateDto: BaseDTOs
    {
        public Guid TrainingApplicationId { get; set; }

        public DateTime ScheduledOn { get; set; }

        public string? MeetingLink { get; set; }
        public string? Location { get; set; }

        public string? Status { get; set; }
        public string? InterviewerName { get; set; }
    }
}
