using Nop.Plugin.CarMake.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Nop.Plugin.CarMake.Data
{
    public class CarMakeImagesMap : EntityTypeConfiguration<CarMakeImages>
    {

        public CarMakeImagesMap()
        {
            ToTable("CarMakeImages");
            HasKey(f => f.Id);
            Property(f => f.CarMakeId);    
            Property(f => f.ImageType);
            Property(f => f.ImagePath);
            Property(f => f.Note);
        }
    }
}