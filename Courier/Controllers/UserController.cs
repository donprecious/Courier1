using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Courier.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Courier.Models;
//using Courier.Models.DbModel;
using PagedList;
using System.Net;
using System.Dynamic;

namespace Courier.Controllers
{
    [Authorize]
    public class UserController : Controller
    {

        // GET: User
        public ActionResult Client(int? page)
        {
            var da = new Courier.Models.DbModel.CourierEntities();

            string useremail  = User.Identity.GetUserName();
            #region Errors
            //// var  myModel = new MultipleItems();
            //    ViewBag.Supports = (new SupportW().GetSupports(useremail)).Count();
            //  //  //myModel.Supports = new SupportW().GetSupports(useremail);

            //  ViewBag.Orders = (new Orders().GetOrders(useremail)).Count();
            //  // // myModel.Orders = new Orders().GetOrders(useremail);

            //ViewBag.Receipts = (new Receipts().GetReceipts(useremail)).Count();
            //  // // myModel.Receipts = new Receipts().GetReceipts(useremail);

            //  ViewBag.Receipts = (new Receipts().GetReceipts(useremail)).Count();

            //    ViewBag.Quotas = (new Quotas().GetQuota(useremail)).Count();
            //  // myModel.Quotas = new Quotas().GetQuota(useremail);
            //  var da = new Courier.Models.DbModel.CourierEntities();
            //  var orders = (from s in da.Orders
            //               select s).OrderByDescending(a => a.DateSpam);

            //  var tracks = (from s in da.Tracks
            //                select s).OrderByDescending(a => a.Order.DateSpam);
            //  return View(tracks);

            #endregion
            var q = da.Orders.Where(a => a.User.UserName== useremail).Select(a => a.OrderID).Count();
            var supportno = new SupportW().GetSupports(useremail);
            ViewBag.Supports = supportno.Count(); ;

            ViewBag.Orders = q;
            
            ViewBag.Receipts = (new Receipts().GetReceipts(useremail)).Count();
           

            ViewBag.Receipts = (new Receipts().GetReceipts(useremail)).Count();

            ViewBag.Quotas = (new Quotas().GetQuota(useremail)).Count();
            string userId = User.Identity.GetUserId();

          
            
            var query = da.Tracks.Where(a => a.Order.userId == userId);

           
            query = query.OrderByDescending(s => s.Order.DateSpam);
            int pageSize = 10;
            int pageNumber = (page ?? 1);


            return View(query.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Order()
        {
            return View();
        }

        [ActionName("Orders")]
        public ActionResult OrderList(string sortOrder, string currentFilter, string searchString, int? page)
        {
            string userId = User.Identity.GetUserId();
          
            ViewBag.CurrentSort = sortOrder;
            ViewBag.OrderID = String.IsNullOrEmpty(sortOrder) ? "orderId" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var da = new Courier.Models.DbModel.CourierEntities();
            var query = da.Tracks.Where(a => a.Order.userId == userId);

            switch (sortOrder)
            {
                case "orderId":
                    query = query.OrderByDescending(s => s.Order.OrderID);
                    break;
                case "Date":
                    query = query.OrderBy(s => s.Order.DateSpam);
                    break;
                case "date_desc":
                    query = query.OrderByDescending(s => s.Order.DateSpam);
                    break;
                default:  // Name ascending 
                    query = query.OrderByDescending(s => s.Order.DateSpam);
                    break;
            }
           
            int pageSize = 10;
            int pageNumber = (page ?? 1);


            return View(query.ToPagedList(pageNumber, pageSize));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Order(OrderV m)
        {

            string userId = User.Identity.GetUserId();
            string PackageName ;
            int OrderID;
            var CreateOrder = new Orders().createOrder(userId, m.Packagename, m.Description, m.weight, m.height);
            
            if (CreateOrder) {
                OrderID = Orders.OrderID;
                ViewBag.OrderID = OrderID;
                Session["OrderID"] = OrderID;

                var da = new Courier.Models.DbModel.CourierEntities();

                PackageName = da.Orders.Where(a => a.OrderID == OrderID).Select(a => a.Packagename).SingleOrDefault();
               
                Session["PackageName"] = PackageName;
                ViewBag.PackageName = PackageName;
                return RedirectToAction("Location");
               }
            else
            {
                ViewBag.Error = Orders.returnMsg;
                return View("Error");
            }
            // return View();
        }
        public ActionResult Location()
        {
            ViewBag.OrderID = Convert.ToInt32(Session["OrderID"]);
            ViewBag.PackageName = Convert.ToString(Session["PackageName"]);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Location(LocationV m)
        {
          
            int orderID = Convert.ToInt32(Session["OrderID"]);
              if (orderID != 0)
            {
                var Des = new Location().AddDestination(orderID, m.Destination, m.DestinationLatitude, m.DestinationLogitude);
                var Sou = new Location().AddSource(orderID, m.Source, m.SourceLatitude, m.SourceLogitude);
               
                if (Des && Sou)
                {
                    //Display a Modal and return to client area
                    var tr = new Track().GenerateTrack(orderID);

                    return RedirectToAction("Client");
                }
                else
                {
                    ViewBag.Error = Orders.returnMsg;

                    return View("Error");
                }
            }
            else
            {
                ViewBag.Error = "Sorry something went wrong, It Seems that your session has expired. <br /> You can update the package Location and try again Please Try again";

                return View("Error");
            }



        }

        public ActionResult Quota(int? page)
        {
            string userEmail = User.Identity.GetUserName();

            var da = new Courier.Models.DbModel.CourierEntities();
            var query = (from a in da.Quotas where a.email == userEmail select a ).OrderByDescending(a => a.DateSpam);

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Support(int? page)
        {
            string userEmail = User.Identity.GetUserName();
            var da = new Courier.Models.DbModel.CourierEntities();
            var query = (from a in da.supports where a.email== userEmail select a).OrderByDescending(a =>a.dateSpam);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(query.ToPagedList(pageNumber, pageSize));
            
        }

       [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Support(FormCollection m)
        {
            string email = m["email"];
            string message = m["message"];
            if( new SupportW().sendSupportMessage(email, message))
            {
                return RedirectToAction("Support");
            }
            else
            {
                ViewBag.Error = SupportW.returnMsg;
                return View("Error");
            }
        }

        public ActionResult Reciept(int id)
        {
            var da = new Courier.Models.DbModel.CourierEntities();
            var orderid = da.Orders.Where(a => a.OrderID == id).Select(a => a.OrderID).SingleOrDefault();
            var packageName = da.Orders.Where(a => a.OrderID == id).Select(a => a.Packagename).SingleOrDefault();
            ViewBag.OrderID = orderid;
            ViewBag.PackageName = packageName;
            var rec = da.Receipts.Where(a => a.OrderID == id).Select(a => a.ReceiptID);
            if (rec != null)
            {
               
                ViewBag.RecID = rec.SingleOrDefault();
                ViewBag.RecNO = rec;
            }
            else
            {
                ViewBag.RecID = "UNPAID";
                ViewBag.RecNO = "NOT FOUND";
            }
            ViewBag.Amount = da.Receipts.Where(a => a.OrderID == id).Select(a => a.Amount).SingleOrDefault();

            ViewBag.des = da.destinations.Where(a => a.OrderID == id).Select(a => a.Address).SingleOrDefault();
            ViewBag.track = da.Tracks.Where(a => a.OrderID == id).Select(a => a.TrackID).SingleOrDefault();
            ViewBag.sou = da.Sources.Where(a => a.OrderID == id).Select(a => a.address).SingleOrDefault();
            return View();
        }
       

        public ActionResult Reciepts(string sortOrder, string currentFilter, string searchString, int? page)
        {
            string userId = User.Identity.GetUserId();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.OrderID = String.IsNullOrEmpty(sortOrder) ? "orderId" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var da = new Courier.Models.DbModel.CourierEntities();
            var query = da.Tracks.Where(a => a.Order.userId == userId);

            switch (sortOrder)
            {
                case "orderId":
                    query = query.OrderByDescending(s => s.Order.OrderID);
                    break;
                case "Date":
                    query = query.OrderBy(s => s.Order.DateSpam);
                    break;
                case "date_desc":
                    query = query.OrderByDescending(s => s.Order.DateSpam);
                    break;
                default:  // Name ascending 
                    query = query.OrderByDescending(s => s.Order.DateSpam);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);


            return View(query.ToPagedList(pageNumber, pageSize));

        }
    }

    
    }
