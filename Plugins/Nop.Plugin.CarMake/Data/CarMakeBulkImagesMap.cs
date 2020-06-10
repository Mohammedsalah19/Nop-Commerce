using Nop.Plugin.CarMake.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Nop.Plugin.CarMake.Data
{
    public class CarMakeBulkImagesMap : EntityTypeConfiguration<CarMakeBulkImages>
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