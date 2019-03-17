using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Courier.Models.DbModel;

namespace Courier.Models
{
    public class Quotas
    {
        public static string returnMsg;

        public static int value;

        //try
        //  {

        //  }
        //  catch (Exception ex)
        //  {
        //      returnMsg = ex.Message;
        //      return false;
        //  }

        public bool createQuota(string source, string destination, string packageinfo, string email)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    da.Quotas.Add(new Quota()
                    {
                        Destination = destination,
                        Source = source,
                        PackageInfo = packageinfo,
                        email =email
                    });
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
        public bool RespondQuota(int quotaId, string message)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    var chk = da.Responses.Where(a => a.QuotaID == quotaId).SingleOrDefault();
                    if(chk == null)
                    {
                        da.Responses.Add(new Response()
                        {
                           QuotaID = quotaId,
                           Message = message
                        });
                    }
                    else
                    {
                        //update quota
                        chk.Message = message;
                        da.Entry(chk).State = System.Data.Entity.EntityState.Modified;
                    }
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

        public bool DeleteQuota(int quotaId)
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
        public bool UpdateQuotaReply(int quotaId, string message)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    var q = da.Responses.Where(a => a.QuotaID == quotaId).SingleOrDefault();
                    q.Message = message;
                    da.Entry(q).State = System.Data.Entity.EntityState.Modified;

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
        public bool ReplyResponse(int responseID, string messsage)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    da.ResponseReplies.Add(new ResponseReply()
                    {
                      Message = messsage,
                      ResponseID = responseID
                    });
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


        public IEnumerable<Quota> GetQuota()
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    var q = (from p in da.Quotas select p).ToList();
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

        public IEnumerable<Quota> GetQuota(string email)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                   var q = (from p in da.Quotas where p.email == email select p).ToList();
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
        //public List<Quota> QuotaList(string userID, ViewType)
        //{
        //    return null;
        //}
    }

   
}