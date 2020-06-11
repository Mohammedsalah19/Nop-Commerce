using Nop.Core.Domain.CareMake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.CarMake
{
   public class CarMakeBulkImagesMap  : NopEntityTypeConfiguration<CarMakeBulkImages>
    {
        public CarMakeBulkImagesMap()
        {
            ToTable("CarMakeBulkImages");
            HasKey(f => f.Id);
            Property(f => f.ImageType);
            Property(f => f.ColorHex);
            Property(f => f.FolderPath);
        }
    }
}
