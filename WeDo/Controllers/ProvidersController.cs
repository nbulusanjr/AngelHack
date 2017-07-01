using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeDo.DAL;
using WeDo.Models;

namespace WeDo.Controllers
{
    public class ProvidersController : Controller
    {
        // GET: Providers
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetPendingNotifications()
        {

            try
            {
                var auth = AuthorizationGateway.GetAuthorizedInfo();

                RequestModel request = new RequestModel();

                var result = request.GetNotifications(auth);

                return Json(new { Result = "OK", Records = result }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult GetMyPendingBids()
        {
            try
            {
                var auth = AuthorizationGateway.GetAuthorizedInfo();
                RequestModel request = new RequestModel();

                var result = request.GetBidsByMe(auth);

                return Json(new { Result = "OK", Records = result }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult AcceptRequest(RequestNotificationsModel notification)
        {
            try
            {
                var auth = AuthorizationGateway.GetAuthorizedInfo();
                RequestModel request = new RequestModel();

                var result = request.AcceptRequest(notification,auth);
                

                return Json(new { Result = "OK", Record = result }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult PlaceMyBid(RequestBidsModel bid)
        {
            try
            {
                var auth = AuthorizationGateway.GetAuthorizedInfo();
                RequestModel request = new RequestModel();

                var result = request.PlaceBid(bid, auth);


                var context = GlobalHost.ConnectionManager.GetHubContext<PushNotificationHub>();

                context.Clients.All.broadcastBid(bid.RequestID,bid);


                return Json(new { Result = "OK", Record = result }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}