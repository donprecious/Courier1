using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Courier.Models.DbModel;
namespace Courier.Models
{
    public class Receipts
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


        public bool createReceipt(int orderID, decimal Amount,  string more)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    var chKRec = da.Receipts.Where(a => a.OrderID == orderID).SingleOrDefault();
                    if(chKRec == null)
                    {
                        // add new record
                        da.Receipts.Add(new Receipt()
                        {
                            Amount = Amount,
                            OrderID = orderID,
                            DateCreated = DateTime.UtcNow,
                            AdditionalInfo = more
                        });
                     
                    }
                    else
                    {
                        chKRec.Amount = Amount;
                        chKRec.AdditionalInfo = more;
                        da.Entry(chKRec).State = System.Data.Entity.EntityState.Modified;
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

        public bool UpdateReceipt(int ReceiptID, decimal amount)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    var query = (from p in da.Receipts where p.ReceiptID == ReceiptID select p).SingleOrDefault();
                    if(query != null)
                    {
                        query.Amount = amount;
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

        public IEnumerable<Receipt> GetReceipts()
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    var q = (from p in da.Receipts select p).ToList();
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

        public IEnumerable<Receipt> GetReceipts(string email)
        {
            try
            {
                using (var da = new CourierEntities())
                {
                    var q = (from p in da.Receipts where p.Order.User.Email == email select  p).ToList();
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