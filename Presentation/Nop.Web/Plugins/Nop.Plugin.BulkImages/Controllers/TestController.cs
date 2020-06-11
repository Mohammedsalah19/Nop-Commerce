using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.BulkImages.Controllers
{
    [AdminAuthorize]
  public  class TestController : BasePluginController
    {
      
        [HttpGet]
    
        public ActionResult Index()
        {
             return View("~/Plugins/Nop.Plugin.BulkImages/Views/Index.cshtml");

        }
    }
}
