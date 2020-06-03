using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Nop.Core.Data;
using Nop.Plugin.BulkImages.Domain;
using System.Web;


namespace Nop.Plugin.BulkImages.Controllers
{
    public class ImageBulkManagementController : BasePluginController
    {
        private IRepository<Images360> _imageRepo;

        public ImageBulkManagementController(IRepository<Images360> imageRepo)
        {
            this._imageRepo = imageRepo;
        }
        public ActionResult Configure(int? id)
        {
            return View("/Plugins/Nop.Plugin.UploadBulkImages/Views/Configure.cshtml");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("/Plugins/Nop.Plugin.UploadBulkImages/Views/Configure.cshtml",new Images360());
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase FilePath)
        {
            return View("/Plugins/Nop.Plugin.UploadBulkImages/Views/Configure.cshtml", new Images360());

        }

    }
}

