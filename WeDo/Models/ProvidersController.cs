using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeDo.DAL;

namespace WeDo.Models
{
    public class ProvidersController : Controller
    {
        // GET: Providers
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetPendingNotifications()
        {




            try {

            }
            catch(Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

    }
}