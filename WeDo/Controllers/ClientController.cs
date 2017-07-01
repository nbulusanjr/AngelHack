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
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            //try
            //{
            //    var auth = AuthorizationGateway.GetAuthorizedInfo();
            //    RequestModel model = new RequestModel();
            //    var result = model.GetBidsForMyRequest()

            //    return Json(new { Result = "OK", Record = result }, JsonRequestBehavior.AllowGet);

            //}
            //catch (Exception ex)
            //{
            //    return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            //}

            return View();
        }

        // GET: Client/Details/5
        public ActionResult Details(int id)
        {
            //try
            //{
            //    var auth = AuthorizationGateway.GetAuthorizedInfo();
            //    RequestModel model = new RequestModel();
            //    var result = model.GetBidsForMyRequest

            //    return Json(new { Result = "OK", Record = result }, JsonRequestBehavior.AllowGet);

            //}
            //catch (Exception ex)
            //{
            //    return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            //}
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        // GET: Client/Create
        [HttpPost]
        public JsonResult Create(RequestModel model)
        {
            try
            {
                RequestModel requestRepo = new RequestModel();
                var auth = AuthorizationGateway.GetAuthorizedInfo();
                model.StatusID = RequestStatus.AWAITING;
                model.UserID = auth.ID;
                var result = requestRepo.Create(model, auth);

                var context = GlobalHost.ConnectionManager.GetHubContext<PushNotificationHub>();

                context.Clients.Group("Providers").sendRequestToProviders(result);

                return Json(new { Result = "OK", Record = result }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Client/Create
        public ActionResult GetBidsForMyRequest(int requestID)
        {
            try
            {
                var auth = AuthorizationGateway.GetAuthorizedInfo();
                RequestModel model = new RequestModel();
                var result = model.GetBidsForMyRequest(requestID, auth);

                return Json(new { Result = "OK", Record = result }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //// GET: Client/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Client/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Client/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Client/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
