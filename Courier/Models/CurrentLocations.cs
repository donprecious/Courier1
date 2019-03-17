using Courier.Models.DbModel;

namespace Courier.Models
{
    internal class CurrentLocations : CurrentLocation
    {
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public decimal Logitude { get; set; }
        public int OrderID { get; set; }
    }
}