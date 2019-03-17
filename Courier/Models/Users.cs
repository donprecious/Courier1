using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Courier.Models.DbModel;

namespace Courier.Models
{
    public class Users
    {
        public static string returnMsg ;

        public static int value;
        public static int OrderID;
        
        public IEnumerable<User> GetUsers()
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    var q = (from p in da.Users  select p).ToList();
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

        public bool DeleteUser(string userId)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    var q = da.Users.Find(userId);
                    da.Users.Remove(q);
                   // q.
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

        public bool BlockUser(int quotaId)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    var q = da.Quotas.Find(quotaId);
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
