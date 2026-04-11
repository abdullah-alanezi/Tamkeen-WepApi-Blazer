using System;
using System.Collections.Generic;
using System.Text;

namespace Tamkeen.Core.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
    }
}
