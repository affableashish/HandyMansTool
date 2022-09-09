using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMT.Web.Server.Interfaces
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
    }
}
