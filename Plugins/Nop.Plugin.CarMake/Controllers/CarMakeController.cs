using ICSharpCode.SharpZipLib.Zip;
using Nop.Core.Data;
using Nop.Plugin.CarMake.Models;
using Nop.Services.Catalog;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace Nop.Plugin.CarMake.Controllers
{
    public class CarMakeController :  BasePluginController
    {
        private IRepository<ColorHex> _ColorHexRepo;
        private IRepository<CarMakeBulkImages> _CarMakeBulkRepo;
        private IRepository<CarMakeImages> _CarMakeImagesRepo;
        private readonly ICategoryService _CategoryService;

        public CarMakeController(IRepository<CarMakeImages> CarMakeImagesRepo , IRepository<CarMakeBulkImages> CarMakeBulkRepo, IRepository<ColorHex> ColorHexRepo, ICategoryService CategoryService)
        {
            this._CarMakeBulkRepo = CarMakeBulkRepo;
            this._ColorHexRepo = ColorHexRepo;
            this._CarMakeBulkRepo = CarMakeBulkRepo;
            this._CategoryService = CategoryService;
        }
        // GET: CarMake
        public ActionResult CreateBulk()
        {
            return View("~/Plugins/Nop.Plugin.CarMake/Views/CarMake/CreateBulk.cshtml",new Domain.CarMakeBulk());
        }

        [HttpPost]
        public ActionResult CreateBulk(int? CarId ,int? ImageType ,string ColorHex, HttpPostedFileBase FolderPath)
        {

            var webRoot = Server.MapPath("~/");
            string PicturePath ="";
            if (ImageType == 1 )
            {
                 PicturePath = "Content/Images/CarMake/interior/";

            }
            else if (ImageType == 2)
            {
                 PicturePath = "Content/Images/CarMake/exterior/";


            }
            PicturePath = PicturePath + $"{CarId}/{ColorHex}";
            string TempPath = Path.Combine(webRoot, PicturePath);

            Directory.CreateDirectory(TempPath);


            //// this for access denied
            DirectoryInfo directory = new DirectoryInfo(TempPath);
            DirectorySecurity security = directory.GetAccessControl();
            security.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                                                                                   FileSystemRights.FullControl,
                                                                                   InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit,
                                                                                   PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            directory.SetAccessControl(security);
            var path = Path.Combine(TempPath, Path.GetFileName(FolderPath.FileName));
            FolderPath.SaveAs(path);

            string ZipFileName = Path.Combine(TempPath, FolderPath.FileName);
            if (Path.GetExtension(ZipFileName).Equals(".zip"))
            {
                using (var s = new ZipInputStream(System.IO.File.OpenRead(ZipFileName)))
                {
                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        //  string directoryName = Path.GetDirectoryName(theEntry.Name);
                        string fileName = Path.GetFileName(theEntry.Name);

                        // create directory
                        if (fileName != String.Empty)
                        {
                            if (fileName.IndexOfAny(@"!@#$%^*/~\".ToCharArray()) > 0)
                            {
                                continue;
                            }
                            using (FileStream streamWriter = System.IO.File.Create(path + theEntry.Name))
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
                var model = new Models.CarMakeBulkImages()
                {
                    CarId = CarId.GetValueOrDefault(),
                    ImageType = ImageType.GetValueOrDefault(),
                    ColorHex = ColorHex,
                    FolderPath = PicturePath
                };
                _CarMakeBulkRepo.Insert(model);
            }
            return RedirectToAction("BulkList");
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
        [HttpGet]
        public ActionResult ColorHex()
        {

            return View("~/Plugins/Nop.Plugin.CarMake/Views/CarMake/ColorHex.cshtml");
        }
        [HttpPost]
        public ActionResult ColorHex(string Name, string Hex)
        {

            if (!String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Hex))
            {
                var model = new Models.ColorHex()
                {
                    Name = Name,
                    hex = Hex
                };

                _ColorHexRepo.Insert(model);
                return RedirectToAction("ColorHexList");
            }
            else
            {
                return View("~/Plugins/Nop.Plugin.CarMake/Views/CarMake/ColorHex.cshtml");

            }

        }
        public ActionResult ColorHexList()
        {

            return View("~/Plugins/Nop.Plugin.CarMake/Views/CarMake/ColorHexList.cshtml");

        }


        public JsonResult ColorList()
        {
            var List = _ColorHexRepo.Table.ToList();

            var model = List.Select(f => new Domain.ColorHex()
            {
                id = f.Id,
                Name = f.Name,
                Hex = f.hex
            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SelectMakeCars()
        {
            var List = _CategoryService.GetAllCategories();

            var model = List.Select(f => new Domain.MakeCatVM()
            {
                id = f.Id,
                Name = f.Name,
            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Delete(int id)
        {
            if (id != 0)
            {
                var model = _ColorHexRepo.GetById(id);
                _ColorHexRepo.Delete(model);
            }
            return RedirectToAction("ColorHexList");

        }
    }
}