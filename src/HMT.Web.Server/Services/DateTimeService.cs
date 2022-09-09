using HMT.Web.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMT.Web.Server.Services
{
    public class DateTimeService : IDateTimeService
    {
        // You can change it however you'd like in the future. For eg: You decide to use Datetime.Utc now in the future for 'Now'.
        // This way you only have to make that change here.
        public DateTime Now => DateTime.Now;
    }
}