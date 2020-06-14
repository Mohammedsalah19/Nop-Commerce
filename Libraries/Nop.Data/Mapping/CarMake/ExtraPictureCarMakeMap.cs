using Nop.Core.Domain.CareMake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.CarMake
{
  public  class ExtraPictureCarMakeMap : NopEntityTypeConfiguration<ExtraPictureCarMake>
    {
        public ExtraPictureCarMakeMap()
        {
            ToTable("ExtraPictureCarMake");
            HasKey(f => f.Id);
            Property(f => f.CarMakeId);
            Property(f => f.ImageType);
            Property(f => f.ImagePath);
            Property(f => f.Note);
        }
    }
}