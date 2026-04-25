using System;
using System.Collections.Generic;
using System.Text;

namespace Tamkeen.Core.Models.Interview.Response
{
    public class InterviewResponse
    {
        public Guid Id { get; set; }

        public DateTime ScheduledOn { get; set; }
        public string TraineeName { get; set; } = string.Empty;
        public string? MeetingLink { get; set; }
        public string? Location { get; set; }

        public string? InterviewerName { get; set; }

        public string ProgramTitle { get; set; } = string.Empty;
        public string? Feedback { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
