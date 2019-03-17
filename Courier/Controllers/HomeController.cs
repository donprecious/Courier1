using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Courier.Models.ViewModels;
using Courier.Models;
namespace Courier.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
          

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(SupportV m)
        {

            if (ModelState.IsValid)
            {
                if (new SupportW().sendSupportMessage(m.email, m.Message))
            {
                TempData["show"] = 1;
                return View();
            }
            else
            {
                ViewBag.Error = SupportW.returnMsg;
                return View("Error");
            }
            }
           
            
            return View(m);
        }
        public ActionResult Track()
        {
            ViewBag.Message = "Track your Order";

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Track(TrackV m)
        {
            var da = new Courier.Models.DbModel.CourierEntities();

            var Q = da.Tracks.Where(a => a.TrackID == m.TrackID).SingleOrDefault();

            if(Q != null)
            {
                ViewBag.msg = "";
                m.packageName = Q.Order.Packagename;
                
                if (Q.CurrentLocation != null) {
   m.CurrentLatitude =  Convert.ToDecimal(Q.CurrentLocation.Latitude);
                m.CurrentLogitude = Convert.ToDecimal(Q.CurrentLocation.Logitude);
                m.CurrentLocation = Q.CurrentLocation.Address;
                }
                else
                {
                    ViewBag.msg = "Sorry this Package is not ready yet.";
                }
             

                m.DestinationLatitude = Convert.ToDecimal(Q.destination.Latitude);
                m.DestinationLogitude = Convert.ToDecimal(Q.destination.Logitude);
                m.Destination = Q.destination.Address;

                m.SourceLatitude = Convert.ToDecimal(Q.Source.Latitude);
                m.SourceLogitude = Convert.ToDecimal(Q.Source.Logitude);
                m.Source = Q.Source.address;
                
            }
           
            return View(m);
        }

        [HttpPost]
        public ActionResult sendQuota(join m)
        {
            if (ModelState.IsValid)
            {
                if(new Quotas().createQuota(m.quota.Source, m.quota.Destination, m.quota.packageInfo, m.quota.Email))
                {
                    TempData["view"] = 1;
                    ViewBag.Error = "Your quota has been sent we would get back to you shortly!";
                }
               
            }
            else
            {
                TempData["view"] = 1;
                ViewBag.Error = "Opps something went wrong pls try again";
                return View("Index",m);
            }
            return View("index");
        }

    }
}