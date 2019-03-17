using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Courier.Models.DbModel;
namespace Courier.Models.ViewModels
{

    public class MultipleItems
    {
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<support> Supports { get; set; }

        public IEnumerable<DbModel.Track> Tracks { get; set; }

        public IEnumerable<Receipt> Receipts { get; set; }

        public IEnumerable<Quota> Quotas { get; set; }
    }
}