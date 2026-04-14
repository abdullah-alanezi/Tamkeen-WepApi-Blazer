using System;
using System.Collections.Generic;
using System.Text;

namespace Tamkeen.Core.Models.DTOs
{

    public class MonthlyEvaluationDto
    {
        public Guid Id { get; set; }
        public Guid TrainingApplicationId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int AttendanceGrade { get; set; }
        public int PerformanceGrade { get; set; }
        public int TotalGrade => (AttendanceGrade + PerformanceGrade) / 2; // مثال لحقل محسوب
        public string? Comments { get; set; }
    }
}
