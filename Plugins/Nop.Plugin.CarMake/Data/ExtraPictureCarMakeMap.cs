using Nop.Plugin.CarMake.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Nop.Plugin.CarMake.Data
{
    public class ExtraPictureCarMakeMap : EntityTypeConfiguration<ExtraPictureCarMake>
    {

        public ExtraPictureCarMakeMap()
        {
            ToTable("ExtraPictureCarMake");
            HasKey(f => f.Id);
            Property(f => f.CarMakeId);
            Property(f => f.ImagePath);
            Property(f => f.ImageType);
            Property(f => f.Note);
        }
    }
}