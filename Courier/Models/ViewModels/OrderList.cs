using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Courier.Models.DbModel;
namespace Courier.Models.ViewModels
{
    public class OrderList
    {
        public IEnumerable<Order> orders { get; set; }
    }
}