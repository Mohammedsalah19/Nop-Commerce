using Nop.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Photo360;
namespace Nop.Services.Photos360
{
    public class Photo360Service : IPhoto360Service
    {
        private IRepository<Image360> _imageRepo;
        public Photo360Service(IRepository<Image360> imageRepo)
        {
            this._imageRepo = imageRepo;
        }
        public string GetProductPath(int ProductId)
        {
            var model = _imageRepo.Table.Where(f => f.ProductId == ProductId).Select(f => f.FilePath).FirstOrDefault();
            return model;
        }
    }
}
