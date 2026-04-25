using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Core.Models.BaseDto;

namespace Tamkeen.Core.Models.MonthlyEvaluation.Request
{
    public class MonthlyEvaluationCreateDto: BaseDTOs
    {
        public Guid TrainingApplicationId { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }

        public int AttendanceGrade { get; set; }
        public int PerformanceGrade { get; set; }

        public string? Comments { get; set; }
    }
}
