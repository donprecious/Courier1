using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Courier.Models.DbModel;

namespace Courier.Models
{
    public class Orders
    {
        public static string returnMsg ;

        public static int value;
        public static int OrderID;

        public bool createOrder(string userID, string packagename, string description, string weight, string height)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    var order = new Order()
                    {

                        userId = userID,
                        Packagename = packagename,
                        Description = description,
                        weight = weight,
                        height = height,
                        DateSpam = DateTime.UtcNow,
                        
                    };
                    da.Orders.Add(order);
                   
                    da.SaveChanges();
                    OrderID = order.OrderID;
                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
                return false;
            }
        }
        public bool EditOrder(int OrderID, string packageName, string des, string weight, string height)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    var query = da.Orders.Find(OrderID);
                        if (query != null )
                    {
                        query.Packagename = packageName;
                        query.Description = des;
                        query.height = height;
                        query.weight = weight;                    
                    }
                  
                    da.Entry(query).State = System.Data.Entity.EntityState.Modified;
                    da.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
                return false;
            }
        }

        public bool DeleteOrder(int OrderID)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    var query = da.Orders.Find(OrderID);
                    if(query != null) { 

                    da.Orders.Remove(query);
                    da.SaveChanges();
                        return true;
                    }
                    else
                    {
                        returnMsg = "No Record found to delete";
                        return false;
                    }
                  
                }
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
                return false;
            }
        }

        public bool UpdateLocation(int LocationID, string currentLocation)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    var query = da.Locations.Find(LocationID);
                    if(query != null)
                    {
                        query.CurrentLocation = currentLocation;
                        da.Entry(query).State = System.Data.Entity.EntityState.Modified;
                        da.SaveChanges();

                        return true;
                    }
                    else
                    {
                        returnMsg = "Unable to update record, Record not Found";
                        return false;

                    }
                }
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
                return false;
            }
        }

        public static bool CheckOrder(int OrderID)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    var query = da.Orders.Find(OrderID);
                    if (query != null)
                    {
                        return true;
                    }
                    else
                    {
                        returnMsg = "No Record Order found";
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
                return false;
            }
        }

        public bool addLocation(int orderId, string destination, string source, string currentLocation)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    var CheckOrderID = da.Locations.Where(a => a.OrderID == orderId).SingleOrDefault();

                    if (CheckOrderID == null)
                    {
                        da.Locations.Add(
                        new DbModel.Location()
                        {
                            OrderID = orderId,
                            Destination = destination,
                            Source = source,
                            CurrentLocation = currentLocation,
                            DateSpam = DateTime.UtcNow
                        }
                        );
                    }
                    else
                    {
                        //return Order Location has already been add
                        returnMsg = "This Order has been created you can only update it. ";
                        value = 1;
                        return false;
                    }

                    da.SaveChanges();
                }
                
                return true;
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
                return false;
            }
        }

        public bool addLocation(int orderId, string destination, string source)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    var CheckOrderID = da.Locations.Where(a => a.OrderID == orderId).SingleOrDefault();

                    if(CheckOrderID == null)
                    {
                        //proceed to add
                        da.Locations.Add(
                       new DbModel.Location
                       {
                           OrderID = orderId,
                           Destination = destination,
                           Source = source,

                           DateSpam = DateTime.UtcNow
                       }
                        );
                    }
                    else
                    {
                        //return Order Location has already been add
                        returnMsg = "This Order has been created you can only update it. ";
                        value = 1;
                        return false;
                    }
                
                }

                return true;
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
                return false;
            }
        }

        public IEnumerable<Order> GetOrders()
        {
            try
            {
                using (var da = new CourierEntities()) {
                    var q = (from p in da.Orders select p).ToList();
                    if(q != null)
                    {
                        return q;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
                return null;

            }
        }

        public IEnumerable<Order> GetOrders(string email)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    var q = (from p in da.Orders where p.User.Email == email select p).ToList();
                    if (q != null)
                    {
                        return q;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
                return null;

            }
        }

        public bool FindOrder( int id)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    var q = (from p in da.Orders where p.OrderID == id select p);
                    if (q != null)
                    {
                        return true;
                    }
                }
               
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
                return false;

            }
            return false;
        }
    }
}
