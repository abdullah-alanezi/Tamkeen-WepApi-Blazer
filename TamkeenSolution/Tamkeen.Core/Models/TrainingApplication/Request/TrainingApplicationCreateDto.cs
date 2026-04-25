using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Core.Models.BaseDto;

namespace Tamkeen.Core.Models.TrainingApplication.Request
{
    public class TrainingApplicationCreateDto: BaseDTOs
    {
        public Guid TrainingProgramId { get; set; }
        public Guid TraineeId { get; set; }
    }
}
