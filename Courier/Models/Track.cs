using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Courier.Models.DbModel;
using Courier.Models;
using reuseables;
using System.Data.Entity;
namespace Courier.Models
{
    public class Track
    {
        

            public static string returnMsg;

            public static int value;
             
             
          public bool GenerateTrack(int orderID)
           {
            try
                {
                 using (var da = new CourierEntities())
                    {
                    var checkOrderId = da.Tracks.Where(a => a.OrderID == orderID).Select(a=> a.OrderID).SingleOrDefault();
                    if(checkOrderId == null)
                    {
                        string trackID = Convert.ToString(Generators.RandomNumber(5));
                        int destinationID = Convert.ToInt16(da.destinations.Where(a => a.OrderID == orderID).Select(a => a.DestinationID).SingleOrDefault());
                        int sourceID = Convert.ToInt16(da.Sources.Where(a => a.OrderID == orderID).Select(a => a.SourceID).SingleOrDefault());
                        int currentID = Convert.ToInt16(da.CurrentLocations.Where(a => a.OrderID == orderID).Select(a => a.CurrentLocationID).SingleOrDefault());
                        if (currentID != 0)
                        {
                            da.Tracks.Add(new DbModel.Track
                            {

                                TrackID = Generators.Numbergen(6).ToString(),
                                DestinationID = destinationID,
                                SourceID = sourceID,
                                CurrentLocationID = currentID,
                                OrderID = orderID,
                                
                                
                            });
                        }
                        else
                        {

                            da.Tracks.Add(new DbModel.Track
                            {
                                TrackID = Generators.Numbergen(6).ToString(),
                                DestinationID = destinationID,
                                SourceID = sourceID,
                                OrderID = orderID,
                            });
                        }

                        da.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                returnMsg = ex.Message;
                return false;
            }

        }
      
        public IEnumerable<DbModel.Track> GetTracks(string email)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    da.Tracks.Include(a => a.Order);
                    da.Tracks.Include(a => a.Order.User);
                    da.Tracks.Include(a => a.destination);
                    da.Tracks.Include(a => a.Source);
                    da.Tracks.Include(a => a.CurrentLocation);
                    var q = (from p in da.Tracks where p.Order.User.Email == email select  p).ToList();
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
    }
    }