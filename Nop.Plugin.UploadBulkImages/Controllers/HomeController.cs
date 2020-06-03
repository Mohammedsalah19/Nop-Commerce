using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Nop.Plugin.UploadBulkImages.Controllers
{
    public class HomeController : BasePluginController
    {
        public ActionResult Configure()
        {
            return View("/Plugins/Nop.Plugin.UploadBulkImages/Views/UploadBulkImages/Configure.cshtml");
        }

    }
}
