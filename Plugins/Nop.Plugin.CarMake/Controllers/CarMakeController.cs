using Nop.Plugin.CarMake.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Plugin.CarMake.Controllers
{
    public class CarMakeController : Controller
    {
        // GET: CarMake
        public ActionResult CreateBulk()
        {
            return View("~/Plugins/Nop.Plugin.CarMake/Views/CarMake/CreateBulk.cshtml",new CarMakeBulk());
        }


        public ActionResult BulkList()
        {
            return View("~/Plugins/Nop.Plugin.CarMake/Views/CarMake/BulkList.cshtml");
        }


        public ActionResult CreateCarMakeImages()
        {
            return View("~/Plugins/Nop.Plugin.CarMake/Views/CarMake/CreateCarMakeImages.cshtml", new CarMakeImages());
        }


        public ActionResult ImagesList()
        {
            return View("~/Plugins/Nop.Plugin.CarMake/Views/CarMake/ImagesList.cshtml");
        }
    }
}