using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ant.Cargo.Client.Controllers
{
    public class UIController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetView(String id)
        {
            return PartialView(id);
        }
    }
}
