using Nop.Core.Data;
using Nop.Core.Domain.CareMake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.CarMake
{
    public class ExtraMakeCarImagesService : IExtraMakeCarImagesService
    {
        private IRepository<ExtraPictureCarMake> _ExtraCarMakeImages;
        public ExtraMakeCarImagesService(IRepository<ExtraPictureCarMake> ExtraCarMakeImages)
        {
            this._ExtraCarMakeImages = ExtraCarMakeImages;
        }
        public List<ExtraPictureCarMake> GetCarMakeExtraImages(int CarMakeId)
        {
            var model = _ExtraCarMakeImages.Table.Where(f => f.CarMakeId == CarMakeId).ToList();
            // var model = _CarMakeBulkRepo.Table.Where(f=>f.CarId == CarMakeId).ToList();
            return model;
        }
    }
}
