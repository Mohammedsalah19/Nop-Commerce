using ICSharpCode.SharpZipLib.Core;
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
        private IRepository<ExtraPictureCarMake> _ExtraPictureCarMake;
        private readonly ICategoryService _CategoryService;
 
        public CarMakeController(IRepository<CarMakeImages> CarMakeImagesRepo , IRepository<CarMakeBulkImages> CarMakeBulkRepo, IRepository<ColorHex> ColorHexRepo, ICategoryService CategoryService, IRepository<ExtraPictureCarMake> ExtraPictureCarMake)
        {
            this._CarMakeBulkRepo = CarMakeBulkRepo;
            this._ColorHexRepo = ColorHexRepo;
            this._CarMakeBulkRepo = CarMakeBulkRepo;
            this._CategoryService = CategoryService;
            this._ExtraPictureCarMake = ExtraPictureCarMake;
        }

        #region Bulk image

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
                 PicturePath = "Content/Images/CarMake/Image360/interior/";

            }
            else if (ImageType == 2)
            {
                 PicturePath = "Content/Images/CarMake/Image360/exterior/";


            }
            var corlorWithOutHash = ColorHex.Substring(1);
            PicturePath = PicturePath + $"{CarId}/{corlorWithOutHash}";
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
                ZipFile file = null;
                try
                {
                    FileStream fs =System.IO.File.OpenRead(ZipFileName);
                    file = new ZipFile(fs);

                    //if (!String.IsNullOrEmpty(password))
                    //{
                    //    // AES encrypted entries are handled automatically
                    //    file.Password = password;
                    //}

                    foreach (ZipEntry zipEntry in file)
                    {
                        if (!zipEntry.IsFile)
                        {
                            // Ignore directories
                            continue;
                        }

                        String entryFileName = zipEntry.Name;
                        // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                        // Optionally match entrynames against a selection list here to skip as desired.
                        // The unpacked length is available in the zipEntry.Size property.

                        // 4K is optimum
                        byte[] buffer = new byte[4096];
                        Stream zipStream = file.GetInputStream(zipEntry);

                        // Manipulate the output filename here as desired.
                        String fullZipToPath = Path.Combine(TempPath, entryFileName);
                        string directoryName = Path.GetDirectoryName(fullZipToPath);

                        if (directoryName.Length > 0)
                        {
                            Directory.CreateDirectory(directoryName);
                        }

                        // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                        // of the file, but does not waste memory.
                        // The "using" will close the stream even if an exception occurs.
                        using (FileStream streamWriter =System.IO.File.Create(fullZipToPath))
                        {
                            StreamUtils.Copy(zipStream, streamWriter, buffer);
                        }
                    }
                }
                finally
                {
                    if (file != null)
                    {
                        file.IsStreamOwner = true; // Makes close also shut the underlying stream
                        file.Close(); // Ensure we release resources
                   
                    }

                    if (System.IO.File.Exists(ZipFileName))
                    {
                        System.IO.File.Delete(ZipFileName);
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


        public ActionResult DeleteMakeBukl(int Id)
        {
            var webRoot = Server.MapPath("~/");

            var model = _CarMakeBulkRepo.GetById(Id);
            string fullpath = Path.Combine(webRoot, model.FolderPath);
            // If directory does not exist, don't even try   
            if (Directory.Exists(fullpath))
            {
                Directory.Delete(fullpath, true);
            }
            _CarMakeBulkRepo.Delete(model);
            return RedirectToAction("BulkList");

        }

        #endregion


        public ActionResult CreateCarMakeImages()
        {


            return View("~/Plugins/Nop.Plugin.CarMake/Views/CarMake/CreateCarMakeImages.cshtml", new ExtraPictureCarMake());
        }
        [HttpPost]
        public ActionResult CreateCarMakeImages(int? CarMakeId, int? ImageType, HttpPostedFileBase ImagePath)
        {
            var webRoot = Server.MapPath("~/");
            string PicturePath = "";
            if (ImageType == 1)
            {
                PicturePath = "Content/Images/CarMake/ExtraImages/interior/";

            }
            else if (ImageType == 2)
            {
                PicturePath = "Content/Images/CarMake/ExtraImages/exterior/";


            }
            PicturePath = PicturePath + $"{CarMakeId}";
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
            var path = Path.Combine(TempPath, Path.GetFileName(ImagePath.FileName));
            ImagePath.SaveAs(path);
            var model = new ExtraPictureCarMake()
            {
                CarMakeId = CarMakeId.GetValueOrDefault(),
                ImageType = ImageType.GetValueOrDefault(),
                ImagePath = PicturePath
            };
            _ExtraPictureCarMake.Insert(model);


            return View("~/Plugins/Nop.Plugin.CarMake/Views/CarMake/CreateCarMakeImages.cshtml", new ExtraPictureCarMake());
        }

        public ActionResult ImagesList()
        {
            return View("~/Plugins/Nop.Plugin.CarMake/Views/CarMake/ImagesList.cshtml");
        }


        public JsonResult ExtraCarMakeList()
        {
            var List = _ExtraPictureCarMake.Table.ToList();
            var AllCatList = _CategoryService.GetAllCategories();

            var model = List.Select(f => new Domain.CarMakeBulk()
            {
                Id = f.Id,
                CarMakeName = AllCatList.Where(s => s.Id == f.CarMakeId).Select(s => s.Name).FirstOrDefault(),
                Type = f.ImageType == 1 ? "Interior" : "Exterior",
                FolderPath =f.ImagePath
            }).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);

        }




        public ActionResult DeleteExtraMakeCarImage(int Id)
        {
            var webRoot = Server.MapPath("~/");

            var model = _ExtraPictureCarMake.GetById(Id);
            string fullpath = Path.Combine(webRoot, model.ImagePath);
            // If directory does not exist, don't even try   
            if (Directory.Exists(fullpath))
            {
               System.IO.File.Delete(fullpath);

             }
            _ExtraPictureCarMake.Delete(model);
            return RedirectToAction("ImagesList");

        }

        #region ColorHex

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



        public ActionResult DeleteColorHex(int id)
        {
            if (id != 0)
            {
                var model = _ColorHexRepo.GetById(id);
                _ColorHexRepo.Delete(model);
            }
            return RedirectToAction("ColorHexList");

        }
        #endregion

        public JsonResult CarMakeList()
        {
            var List = _CarMakeBulkRepo.Table.ToList();
            var AllCatList = _CategoryService.GetAllCategories();

            var model = List.Select(f => new Domain.CarMakeBulk()
            {
                Id = f.Id,
                CarMakeName = AllCatList.Where(s => s.Id == f.CarId).Select(s => s.Name).FirstOrDefault(),
                ColorHex = f.ColorHex,
                Type = f.ImageType == 1 ? "Interior" : "Exterior"
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

    }
}