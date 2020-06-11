using Nop.Core.Data;
using Nop.Core.Domain.CareMake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.CarMake
{
    public class Photo360ServiceCarMake : IPhoto360ServiceCarMake
    {
        private IRepository<CarMakeBulkImages> _CarMakeBulkRepo;
        public Photo360ServiceCarMake(IRepository<CarMakeBulkImages> _carMakeBulkRepo)
        {
            this._CarMakeBulkRepo = _carMakeBulkRepo;
        }
        public List<CarMakeBulkImages> GetCarMake(int CarMakeId)
        {

            var model = _CarMakeBulkRepo.Table.Where(f=>f.CarId == CarMakeId).ToList();
           // var model = _CarMakeBulkRepo.Table.Where(f=>f.CarId == CarMakeId).ToList();
            return model;
        }
    }
}
