using System;
using System.Collections.Generic;
using System.Text;

namespace Tamkeen.Core.Models.MonthlyEvaluation.Response
{
    public class MonthlyEvaluationResponse
    {
        public Guid Id { get; set; }
        public string TraineeName { get; set; } = string.Empty;
        public int Month { get; set; }
        public int Year { get; set; }
        public string ProgramTitle { get; set; } = string.Empty;
        public int AttendanceGrade { get; set; }
        public int PerformanceGrade { get; set; }

        public string? Comments { get; set; }
    }
}
