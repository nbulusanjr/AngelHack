using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
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
        public ContentResult GetPendingNotifications()
        {

            try
            {
                var auth = AuthorizationGateway.GetAuthorizedInfo();

                RequestModel request = new RequestModel();

                var result = request.GetNotifications(auth);

                //  return Json(new { Result = "OK", Records = result }, JsonRequestBehavior.AllowGet);
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject(new { Result = "OK", Records = result }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd hh:mm:ss tt" }),
                    ContentType = "application/json"
                };

            }
            catch (Exception ex)
            {
                // return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message }),
                    ContentType = "application/json"
                };
            }

        }

        [HttpPost]
        public ContentResult GetMyPendingDeliveries()
        {
            try
            {
                var auth = AuthorizationGateway.GetAuthorizedInfo();
                RequestModel request = new RequestModel();

                var result = request.GetBidsByMe(auth);

                //  return Json(new { Result = "OK", Records = result }, JsonRequestBehavior.AllowGet);
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject(new { Result = "OK", Records = result }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd hh:mm:ss tt" }),
                    ContentType = "application/json"
                };

            }
            catch (Exception ex)
            {
                //return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message }),
                    ContentType = "application/json"
                };
            }
        }

        [HttpPost]
        public JsonResult AcceptRequest(RequestModel notification)
        {
            try
            {
                var auth = AuthorizationGateway.GetAuthorizedInfo();
                RequestModel request = new RequestModel();

           request.AcceptRequest(notification,auth);
                

                return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeclineRequest(RequestModel notification)
        {
            try
            {
                var auth = AuthorizationGateway.GetAuthorizedInfo();
                RequestModel request = new RequestModel();

                request.DeclineRequest(notification, auth);


                return Json(new { Result = "OK"}, JsonRequestBehavior.AllowGet);

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

                //context.Clients.All.sendRequestToProviders(bid.RequestID,bid);

                context.Clients.All.broadcastBid(bid.RequestID, result);
                return Json(new { Result = "OK", Record = result }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SendToMyNotifications(RequestModel notification)
        {
            try
            {
                var auth = AuthorizationGateway.GetAuthorizedInfo();
                RequestModel request = new RequestModel();

                request.SendToNotifications(notification, auth);


                return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ConfirmPayment(RequestBidsModel bid)
        {

            try {

                RequestModel reqRepo = new RequestModel();
                var auth = AuthorizationGateway.GetAuthorizedInfo();
                reqRepo.ConfirmPayment(bid, auth);
                return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);

            }
            catch(Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);

            }

        }

        public ContentResult GetPendingBids()
        {
            try
            {
                var auth = AuthorizationGateway.GetAuthorizedInfo();
                RequestModel request = new RequestModel();
                var result = request.PendingForMyBid( auth);

                // return Json(new { Result = "OK", Records = result }, JsonRequestBehavior.AllowGet);

                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject(new { Result = "OK", Records = result }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd hh:mm:ss tt" }),
                    ContentType = "application/json"
                };

            }
            catch (Exception ex)
            {
                //return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject(new { Result = "ERROR", Message = ex.Message }),
                    ContentType = "application/json"
                };
            }
        }

    }
}