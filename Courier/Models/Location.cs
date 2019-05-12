using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Courier.Models.DbModel;
namespace Courier.Models
{
    public class Location
    {
        public static string returnMsg;

        public static int value;
        public static int OrderID;

        public bool AddDestination(int orderID, string address, decimal lat, decimal log )
        {
            try
            {
                using(var da = new CourierEntities())
                {
                    //check if orderid exit
                    if (Orders.CheckOrder(orderID))
                    {
                        //ensure no dublicate of an id is found
                        var CheckOrderID = da.destinations.Where(a => a.OrderID == orderID).SingleOrDefault();
                        if (CheckOrderID == null)
                        {
                            // add new record
                            da.destinations.Add(new Courier.Models.DbModel.destination
                            {
                                Address = address,
                                Latitude = lat,
                                Logitude = log,
                                OrderID = orderID,

                            });
                            da.SaveChanges();
                            return true;
                        }
                   
                    }
                    
                }
                return false;
            }
            catch (Exception ex)
            {
                returnMsg = "This Order has been created you can only update it. \n"+ ex.Message;
                value = 1;
                return false;

            }
        }

        public bool AddSource(int orderID, string address, decimal lat, decimal log)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    //check if orderid exit
                    if (Orders.CheckOrder(orderID))
                    {
                        //ensure no dublicate of an id is found
                        var CheckOrderID = da.Sources.Where(a => a.OrderID == orderID).SingleOrDefault();
                        if (CheckOrderID == null)
                        {
                            // add new record
                            da.Sources.Add(new DbModel.Source
                            {
                               address = address,
                                Latitude = lat,
                                Logitude = log,
                                OrderID = orderID,

                            });
                            da.SaveChanges();
                            return true;
                        }
                        
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                returnMsg = "This Order has been created you can only update it. " + ex.Message;
                value = 1;
                return false;

            }
        }

        public bool AddCurrentLocation(int orderID, string address, decimal lat, decimal log)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    //check if orderid exit
                    if (Orders.CheckOrder(orderID))
                    {
                        //ensure no dublicate of an id is found
                        var CheckOrderID = da.CurrentLocations.Where(a => a.OrderID == orderID).SingleOrDefault();
                        if (CheckOrderID == null)
                        {
                            // add new record

                            var curr = new Courier.Models.DbModel.CurrentLocation
                            {
                                Address = address,
                                Latitude = lat,
                                Logitude = log,
                                OrderID = orderID,

                            };
                            da.CurrentLocations.Add(curr);

                            //update track entity with order id
                            
                            da.SaveChanges();

                            //update track entity with order id
                            var tra = da.Tracks.Where(a => a.OrderID == orderID).SingleOrDefault();
                            tra.CurrentLocationID = curr.CurrentLocationID;
                            da.Entry(tra).State = System.Data.Entity.EntityState.Modified;

                            da.SaveChanges();
                            return true;
                        }
                        else
                        {
                            //Update Current Location
                            UpdateCurrentLocation(orderID, address, lat, log);
                            return true;
                        }
                    }

                }
                return false;
            }
            catch (Exception ex)
            {
                returnMsg = "This Order has been created you can only update it. ";
                value = 1;
                return false;

            }
        }

        public bool UpdateCurrentLocation(int orderID, string address, decimal lat, decimal log)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    //check if orderid exit
                    if (Orders.CheckOrder(orderID))
                    {
                        //ensure no dublicate of an id is found
                        var CheckOrderID = da.CurrentLocations.Where(a => a.OrderID == orderID).SingleOrDefault();
                        if (CheckOrderID != null)
                        {
                            // add new record
                            CheckOrderID.Address = address;
                            CheckOrderID.Latitude = lat;
                            CheckOrderID.Logitude = log;
                            CheckOrderID.OrderID = orderID;
                            da.Entry(CheckOrderID).State = System.Data.Entity.EntityState.Modified;

                            da.SaveChanges();

                            //update track entity with order id
                            var tra = da.Tracks.Where(a => a.OrderID == orderID).SingleOrDefault();
                            if(tra != null )
                            {
                                tra.CurrentLocationID = CheckOrderID.CurrentLocationID;
                                da.Entry(tra).State = System.Data.Entity.EntityState.Modified;
                                da.SaveChanges();
                            }
                           
                            return true;
                        }
                    }

                }
                return false;
            }
            catch (Exception ex)
            {
                returnMsg = "This Order has been created you can only update it. ";
                value = 1;
                return false;

            }
        }

        public bool UpdateDestination(int orderID, string address, decimal lat, decimal log)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    //check if orderid exit
                    if (Orders.CheckOrder(orderID))
                    {
                        //ensure no dublicate of an id is found
                        var CheckOrderID = da.destinations.Where(a => a.OrderID == orderID).SingleOrDefault();
                        if (CheckOrderID != null)
                        {

                            //update track entity with order id
                            CheckOrderID.Address = address;
                            CheckOrderID.Latitude = lat;
                            CheckOrderID.Logitude = log;

                            da.Entry(CheckOrderID).State = System.Data.Entity.EntityState.Modified;
                            da.SaveChanges();
                            return true;
                        }
                    }

                }
                return false;
            }
            catch (Exception ex)
            {
                returnMsg = "This Order has been created you can only update it. " + ex;
                value = 1;
                return false;

            }
        }

        public bool UpdateSource(int orderID, string address, decimal lat, decimal log)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    //check if orderid exit
                    if (Orders.CheckOrder(orderID))
                    {
                        //ensure no dublicate of an id is found
                        var CheckOrderID = da.Sources.Where(a => a.OrderID == orderID).SingleOrDefault();
                        if (CheckOrderID != null)
                        {

                            //update track entity with order id
                            CheckOrderID.address = address;
                            CheckOrderID.Latitude = lat;
                            CheckOrderID.Logitude = log;

                            da.Entry(CheckOrderID).State = System.Data.Entity.EntityState.Modified;
                            da.SaveChanges();
                            return true;
                        }
                    }

                }
                return false;
            }
            catch (Exception ex)
            {
                returnMsg = "This Order has been created you can only update it. " + ex;
                value = 1;
                return false;

            }
        }


    }
}