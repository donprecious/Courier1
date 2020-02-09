using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Courier.Models.DbModel;
namespace Courier.Models
{
    public class SupportW
    {


        public static string returnMsg;

        public static int value;


        public bool sendSupportMessage(string email, string message)
        {
            try
            {
                using(var da = new CourierEntities())
                {
                    da.supports.Add(
                        new support()
                        {
                            email = email,
                            message = message,
                            dateSpam = DateTime.UtcNow
                        });
                    da.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {

                returnMsg = ex.Message;
                return false;
            }
        }

        public IEnumerable<support> GetSupports()
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    var q = (from p in da.supports select p).ToList();
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

        public IEnumerable<support> GetSupports(string email)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    var q = (from p in da.supports where p.email == email select p).ToList();
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

        public bool RespondSupport(int TicketID, string message)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    var chk = da.Responses.Where(a => a.TicketID==TicketID).SingleOrDefault();
                    if (chk == null)
                    {
                        da.Responses.Add(new Response()
                        {
                            TicketID = TicketID,
                            Message = message
                        });
                    }
                    //else
                    //{
                    //    //update quota
                    //    chk.Message = message;
                    //    da.Entry(chk).State = System.Data.Entity.EntityState.Modified;
                    //}
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

        public bool DeleteSupport(int ticketID)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    var q = da.Quotas.Find(ticketID);
                    da.Quotas.Remove(q);
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
    }
}