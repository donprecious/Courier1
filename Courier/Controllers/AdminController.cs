using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Courier.Models.ViewModels;
using Courier.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Courier.Models.DbModel;
using PagedList;
using System.Net;
using Track = Courier.Models.Track;

namespace Courier.Controllers
{
   
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            var da = new CourierEntities();
            var q = da.Orders.Select(a => a.OrderID).Count();
            var supportno = new SupportW().GetSupports(/*useremail*/); 
            ViewBag.Supports = supportno.Count(); 
            ViewBag.Orders = q;
            
            ViewBag.Receipts = (new Receipts().GetReceipts()).Count();
            ViewBag.Quotas = (new Quotas().GetQuota()).Count();
            //string userId = User.
            //var query = da.Tracks.Where(a => a.Order.userId == userId);
            ViewBag.Users = (new Users().GetUsers()).Count();
          //  var da = new CourierEntities();

            var orders = from s in da.Tracks
                         select s;
            orders = orders.Take(5).OrderBy(a => a.Order.DateSpam);
            return View(orders);
        }
        public ActionResult Order(string sortOrder, string currentFilter, string searchString, int? page)
        {
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
            var da = new CourierEntities();

            var orders = from s in da.Tracks
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(s => s.Order.Packagename.Contains(searchString)
                                      );
            }
            switch (sortOrder)
            {
                case "orderId":
                    orders = orders.OrderByDescending(s => s.Order.OrderID);
                    break;
                case "Date":
                    orders = orders.OrderBy(s => s.Order.DateSpam);
                    break;
                case "date_desc":
                    orders = orders.OrderByDescending(s => s.Order.DateSpam);
                    break;
                default:  // Name ascending 
                    orders = orders.OrderByDescending(s => s.Order.DateSpam);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var result = orders.ToPagedList(pageNumber, pageSize);
            return View(result);
        }

        public ActionResult Quotas(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Quota = String.IsNullOrEmpty(sortOrder) ? "Quota" : "";
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
            var da = new CourierEntities();

            var quota = from s in da.Quotas
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                quota = quota.Where(s => s.Destination.Contains(searchString) || s.Source.Contains(searchString) || s.email.Contains(searchString)
                                      );
            }
            switch (sortOrder)
            {
                case "orderId":
                    quota = quota.OrderByDescending(s => s.QuotaID);
                    break;
                case "Date":
                    quota = quota.OrderBy(s => s.DateSpam);
                    break;
                case "date_desc":
                    quota = quota.OrderByDescending(s => s.DateSpam);
                    break;
                default:  // Name ascending 
                    quota = quota.OrderByDescending(s => s.DateSpam);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(quota.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Supports(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Quota = String.IsNullOrEmpty(sortOrder) ? "Quota" : "";
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
            var da = new CourierEntities();

            var quota = from s in da.supports
                        select s;
           
            switch (sortOrder)
            {
                case "orderId":
                    quota = quota.OrderByDescending(s => s.TicketID);
                    break;
                case "Date":
                    quota = quota.OrderBy(s => s.dateSpam);
                    break;
                case "date_desc":
                    quota = quota.OrderByDescending(s => s.dateSpam);
                    break;
                default:  // Name ascending 
                    quota = quota.OrderByDescending(s => s.dateSpam);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(quota.ToPagedList(pageNumber, pageSize)); 

        }


        public ActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrder(OrderV m)

        {
            

            var Order = new Orders();;
            var CreateOrder = Order.createOrder(m.userId, m.Packagename, m.Description, m.weight, m.height);

            if (CreateOrder)
            {
              return RedirectToAction("Location", new{id = Orders.OrderID});

            }
            else
            {

                ViewBag.Error = Orders.returnMsg;
                TempData["show"] = 1;

                return View("Error");
            }
        }

        public ActionResult Users(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.Quota = String.IsNullOrEmpty(sortOrder) ? "Quota" : "";
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
            var da = new CourierEntities();

            var quota = from s in da.Users
                        select s;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    quota = quota.Where(s => s.Destination.Contains(searchString) || s.Source.Contains(searchString) || s.email.Contains(searchString)
            //                          );
            //}
            switch (sortOrder)
            {
                case "orderId":
                    quota = quota.OrderByDescending(s => s.FirstName);
                    break;
                case "Date":
                    quota = quota.OrderBy(s => s.DateStamp);
                    break;
                case "date_desc":
                    quota = quota.OrderByDescending(s => s.DateStamp);
                    break;
                default:  // Name ascending 
                    quota = quota.OrderByDescending(s => s.DateStamp);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(quota.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult DeleteUser(string id)
        {
            if (new Users().DeleteUser(id))
            {
                TempData["show"] = 1;
            }

            return RedirectToAction("Users");
        }

        public ActionResult Location(int id)
        {
            var locVm = new LocationV()
            {
                OrderID = id
            };
            return View(locVm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Location(LocationV m)
        {
            var des = new Models.Location().AddDestination(m.OrderID, m.Destination, m.DestinationLatitude, m.DestinationLogitude);
            var source = new Models.Location().AddSource(m.OrderID, m.Source, m.DestinationLogitude, m.SourceLogitude);
           // var current = new Models.Location().AddCurrentLocation(m.OrderID, m.Source, m.SourceLatitude, m.SourceLogitude);

            if(des && source)
            {
                //Update Location 

                //Display a Modal and return to client area
                var tr = new Track().GenerateTrack(m.OrderID);
                return RedirectToAction("Order");
            }

            else
            {
                TempData["show"] = 1;
                ViewBag.ErrorMessage = Models.Location.returnMsg;
            }
            return View();
        }


        
        public ActionResult DeleteOrder(int id)
        {

            if (new Orders().DeleteOrder(id)) {
                return RedirectToAction("Order");
            }
            else
            {
                ViewBag.ErrorMessage = Orders.returnMsg;

                return View("Error");
            }
        }

        public ActionResult EditOrder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var da = new CourierEntities();
            var order = da.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [ActionName("EditOrder")]
        [ValidateAntiForgeryToken,HttpPost]

        public ActionResult Edit(int? id, Order m)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var UpdateRecord = new Orders().EditOrder(Convert.ToInt32(id), m.Packagename, m.Description, m.weight, m.height);
            if (UpdateRecord)
            {
                ViewBag.resultMessage = "Recorded Updated";
                TempData["show"] = 1;
                return RedirectToAction("Order");
            }
            else
            {
                ViewBag.resultMessage = Orders.returnMsg;
                TempData["show"] = 1;
                return View();
            }
        }

        public ActionResult UpdateLocation(int id)
        {
            var checkID = Orders.CheckOrder(id);
            if (checkID)
            {
                var da = new CourierEntities();
                var q = da.Orders.Where(a => a.OrderID == id).SingleOrDefault();
                Session["PackageName"] = q.Packagename;

                ViewBag.TrackingNo = Convert.ToString(Session["TrackingNo"] = da.Tracks.Where(a => a.OrderID == id).Select(a => a.TrackID).SingleOrDefault());
                @ViewBag.PackageName = Convert.ToString(Session["PackageName"]);
                
          }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateLocation(int id, LocationV m)
        {
            var checkID = Orders.CheckOrder(id);
            if (checkID)
            {
                var da = new CourierEntities();
                var q = da.CurrentLocations.Where(a => a.OrderID == id).SingleOrDefault();
                    if(q == null)
                {
                    //add new Locaction
                    var curr = new Courier.Models.Location().AddCurrentLocation(id, m.CurrentLocation, m.CurrentLatitude, m.CurrentLogitude);
                }
                else
                {
                    var updateLoc = new Courier.Models.Location().UpdateCurrentLocation(id, m.CurrentLocation, m.CurrentLatitude, m.CurrentLogitude);
                }
                return RedirectToAction("Order");
                }
            return View(m);
        }
        
        

        public ActionResult GenerateTrackingNo(int id)
        {
            int orderID = id;
            var chkOrderID = Orders.CheckOrder(orderID);
            if (chkOrderID)
            {
                var r = new Models.Track().GenerateTrack(orderID);
                if(r)
                {
                    TempData["show"] = 1;
                    TempData["Message"] = "Track No Generated";

                }
            }
            
             return RedirectToAction("Order"); 
        }

        public ActionResult GenerateReceipt(int id)
        {
            ReceiptV m = new ReceiptV();
            if (new Orders().FindOrder(id))
            {
                var da = new CourierEntities();
                var packageName = da.Orders.Where(a => a.OrderID == id).Select(a => a.Packagename).SingleOrDefault();
                var userEmail = da.Orders.Where(a => a.OrderID == id).Select(a => a.User.Email).SingleOrDefault();
                var Orderno = da.Orders.Where(a => a.OrderID == id).Select(a => a.OrderID).SingleOrDefault();
                ViewBag.OrderNo = (Session["OrderNo"] = Orderno).ToString();
                ViewBag.PackageName =( Session["packageName"] = packageName).ToString();
                  ViewBag.UserEmail =(   Session["userEmail"] = userEmail).ToString();
                m.orderID = Orderno;
            }

            return View(m);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult GenerateReceipt(int id, ReceiptV m)
        {
            var x = new Receipts().createReceipt(id, m.amount, m.AdditionalInfo);

            if (x)
            {
                return RedirectToAction("Order");
            }
            return View(m);
        }

        public ActionResult RespondToQuota(int id)
        {
            var db = new CourierEntities();

            string email = ViewBag.Email = db.Quotas.Where(a => a.QuotaID == id).Select(a => a.email).SingleOrDefault();
            Session["email"] = email;

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult RespondToQuota(int id, QuotaReplyV m)
        {
            var db = new CourierEntities();

            string email = ViewBag.Email = db.Quotas.Where(a => a.QuotaID == id).Select(a => a.email).SingleOrDefault();
            Session["email"] = email;
            m.QuotaID = id;


            if (new Quotas().RespondQuota(id, m.QuotaMessage))
            {
                TempData["show"] = 1;
                return View();
            }


            return View(m);

        }

        public ActionResult RespondToSupport(int id)
        {
            var db = new CourierEntities();
            ViewBag.Id = id;
            string email = ViewBag.Email = db.supports.Where(a => a.TicketID == id).Select(a => a.email).SingleOrDefault();
            Session["email"] = email;
         var s =   db.supports.Where(a => a.TicketID == id).SingleOrDefault();
         var supportReply = new supportReply();
         supportReply.TicketID = s.TicketID;
         return View(supportReply);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult RespondToSupport(int id, supportReply m)
        {
            var db = new CourierEntities();

            string email = ViewBag.Email = db.supports.Where(a => a.TicketID == id).Select(a => a.email).SingleOrDefault();
            Session["email"] = email;
            m.TicketID= id;


            if (new SupportW().RespondSupport(id, m.Message))
            {
                TempData["show"] = 1;
                return View();
            }


            return View(m);

        }
        public ActionResult DeleteQuota( int id)
        {
            if(new Quotas().DeleteQuota(id))
            {
                TempData["show"] = 1;
            }
          
            return RedirectToAction("Quotas");
        }

        public ActionResult DeleteSupport(int id)
        {
            if (new SupportW().DeleteSupport(id))
            {
                TempData["show"] = 1;
            }

            return RedirectToAction("Supports");
        }

        public ActionResult LocationUpdate(int id)
        {
            var da = new CourierEntities();
            var r = new Courier.Models.ViewModels.LocationV();
            var query = da.Tracks.Where(a => a.OrderID == id).SingleOrDefault();
            if(query != null)
            {
                if(query.CurrentLocation != null)
                {
                    r.CurrentLatitude = Convert.ToDecimal(query.CurrentLocation.Latitude);
                    r.CurrentLogitude = Convert.ToDecimal(query.CurrentLocation.Logitude);
                    r.CurrentLocation = query.CurrentLocation.Address;
                }

                r.DestinationLatitude = Convert.ToDecimal(query.destination.Latitude);
                r.DestinationLogitude = Convert.ToDecimal(query.destination.Logitude);
                r.Destination= query.destination.Address;

                r.SourceLatitude = Convert.ToDecimal(query.Source.Latitude);
                r.SourceLogitude = Convert.ToDecimal(query.Source.Logitude);
                r.Source = query.Source.address;
            }
            return View(r);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult LocationUpdate(int id, LocationV m)
        {
            var da = new CourierEntities();
            if(new Orders().FindOrder(id))
            {
                var update = new Courier.Models.Location();
                var curr =  update.AddCurrentLocation(id, m.CurrentLocation,m.CurrentLatitude,m.CurrentLogitude);
                var des = update.UpdateDestination(id, m.Destination, m.DestinationLatitude, m.DestinationLogitude);
                var sou = update.UpdateSource(id, m.Source, m.SourceLatitude, m.SourceLogitude);

                if(curr && des && sou)
                {
                    return RedirectToAction("Order");
                }
            }
           
            return View();
        }

        public ActionResult UserCrediential()
        {
            var r = new Users().UserCrediential();
            return View(r);
        }

        public ActionResult Sms()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Sms(List<string> phones, string message, string from)
        {
            if (string.IsNullOrWhiteSpace(message)) return Json(new{message="message cant be empty", status=400} );
            if (string.IsNullOrWhiteSpace(from)) return Json(new{message="Sender Id cannot be empty", status=400} );
            if (!phones.Any()) return Json(new{message="Phone number cant be empty", status=400} ); 
            if (!from.Any()) return Json(new{message="Phone number cant be empty", status=400} ); 
        
            var send = new Services.SmsService().SendMessage(phones, from,message);
            if (send)
            {
                return Json(new{message="Message Sent", status=200} ); 
            }
            return Json(new{message="Something went Wrong", status=400} );  
        }
    }
}