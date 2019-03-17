using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Courier.Models
{
    public enum QuotaViewType
    {
       READ, UNREAD, ALL
    }

    public enum OrderStatus
    {
        NO_TRACKING_NO, TRACK_NO_GENERATED
    }
}