using System;
using System.Collections.Generic;
using System.Text;

namespace Tamkeen.Core.Models.DTOs
{
    public class InterviewDto
    {
        public Guid Id { get; set; }
        public Guid TrainingApplicationId { get; set; }
        public DateTime ScheduledOn { get; set; }
        public string? MeetingLink { get; set; }
        public string? Location { get; set; }
        public string? InterviewerName { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
