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
using Nop.Services.Catalog;

namespace Nop.Plugin.BulkImages.Controllers
{

    public class ImageBulkManagementController : BasePluginController
    {
        private IRepository<Images360> _imageRepo;
        private readonly IProductService _productService;


        public ImageBulkManagementController(IRepository<Images360> imageRepo, IProductService productService)
        {
            this._imageRepo = imageRepo;
            this._productService = productService;
        }
        //public ActionResult Configure(int? id)
        //{
        //    return View("/Plugins/Nop.Plugin.UploadBulkImages/Views/Configure.cshtml");
        //}
        [HttpGet]

        public ActionResult Create()
        {


            return View("~/Plugins/Nop.Plugin.BulkImages/Views/ImageBulkManagement/Create.cshtml", new Images360());
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase FilePath)
        {
            return View("~/Plugins/BulkImages/Views/ImageBulkManagement/Create.cshtml", new Images360());

        }
     

        public JsonResult GetAllProducts()
        {

            var allProducts = _productService.SearchProducts(
                                                                categoryIds: null,
                                                                pageSize: 100,
                                                                showHidden: true
                                                            );
            var response = allProducts.Select(r => new { r.Id, r.Name }).ToArray();

            return Json(response);
        }

    }
}

