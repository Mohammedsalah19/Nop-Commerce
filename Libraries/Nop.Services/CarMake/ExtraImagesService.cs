using Nop.Core.Data;
using Nop.Core.Domain.CareMake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.CarMake
{
    public class ExtraImagesService : IExtraImagesService
    {
        private IRepository<CarMakeExtraImages> _CarMakeExtraImagesRepo;
        public ExtraImagesService(IRepository<CarMakeExtraImages> CarMakeExtraImagesRepo)
        {
            this._CarMakeExtraImagesRepo = CarMakeExtraImagesRepo;
        }
        public List<CarMakeExtraImages> GetCarMakeExtraImages(int CarMakeId)
        {

            var model = _CarMakeExtraImagesRepo.Table.Where(f => f.CarMakeId == CarMakeId).ToList();
            // var model = _CarMakeBulkRepo.Table.Where(f=>f.CarId == CarMakeId).ToList();
            return model;

            throw new NotImplementedException();
        }
    }
}
