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
using System.IO.Compression;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Security.AccessControl;
using System.Security.Principal;
using Nop.Plugin.BulkImages.Models;

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
        public ActionResult UploadFile(HttpPostedFileBase FilePath, int? Products)
        {
            var webRoot = Server.MapPath("~/Content/");
            string TempPath = Path.Combine(webRoot, $"Images/Picture360/{Products}");

            string Fullpath = "";
            // upload zip file
            Directory.CreateDirectory(TempPath);
            //// this for access denied
            DirectoryInfo directory = new DirectoryInfo(TempPath);
            DirectorySecurity security = directory.GetAccessControl();
            security.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                                                                                   FileSystemRights.FullControl, 
                                                                                   InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit,
                                                                                   PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            directory.SetAccessControl(security);


            string ZipFileName = Path.Combine(TempPath, FilePath.FileName);

            using (Stream inputStream = FilePath.InputStream)
            {
                using (var filestream = new FileStream(TempPath, FileMode.Open))
                {
                    inputStream.CopyTo(filestream);
                }
            }

            if (Path.GetExtension(ZipFileName).Equals(".zip"))
            {


                using (var s = new ZipInputStream(System.IO.File.OpenRead(ZipFileName)))
                {
                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        string directoryName = Path.GetDirectoryName(theEntry.Name);
                        string fileName = Path.GetFileName(theEntry.Name);

                        // create directory
                        if (fileName != String.Empty)
                        {
                            if (fileName.IndexOfAny(@"!@#$%^*/~\".ToCharArray()) > 0)
                            {
                                continue;
                            }


                            using (FileStream streamWriter = System.IO.File.Create(TempPath + theEntry.Name))
                            {
                                var data = new byte[2048];

                                while (true)
                                {
                                    int size = s.Read(data, 0, data.Length);

                                    if (size > 0)
                                        streamWriter.Write(data, 0, size);

                                    else
                                        break;

                                }
                            }
                        }
                    }

                }
                Fullpath = TempPath;
                var _image360 = new Images360()
                {
                    FilePath = Fullpath,
                    ProductId = Products.GetValueOrDefault(),

                };
                _imageRepo.Insert(_image360);
            }

            return View("~/Plugins/Nop.Plugin.BulkImages/Views/ImageBulkManagement/List.cshtml", new Images360());

        }

        public JsonResult GetAllProducts()
        {
            var allProducts = _productService.SearchProducts(
                                                                categoryIds: null,
                                                                pageSize: 100,
                                                                showHidden: true
                                                            );
            var response = allProducts.Select(r => new { r.Id, r.Name }).ToArray();

            return Json(response,JsonRequestBehavior.AllowGet);
        }



        public ActionResult List()
        {
 
            return View("~/Plugins/Nop.Plugin.BulkImages/Views/ImageBulkManagement/List.cshtml");
        }



        public JsonResult ProductList()
        {
            var List = _imageRepo.Table.Where(f => f.FilePath !=null).ToList();

            var allProducts = _productService.SearchProducts(
                                                                           categoryIds: null,
                                                                           pageSize: 100,
                                                                           showHidden: true
                                                                       );

            var model = List.Select(f => new Image360VM()
            {
                Id = f.Image360_ID,
                ProductName = allProducts.Where(s => s.Id == f.ProductId).Select(s => s.Name).FirstOrDefault(),
                FilePath = f.FilePath
            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Delete(int Id )
        {
            var model = _imageRepo.GetById(Id);

            string fullPath = model.FilePath;
            // If directory does not exist, don't even try   
            if (Directory.Exists(fullPath))
            {
                Directory.Delete(fullPath,true);
            }
            _imageRepo.Delete(model);
            return View("~/Plugins/Nop.Plugin.BulkImages/Views/ImageBulkManagement/List.cshtml");

        }

    }

}

