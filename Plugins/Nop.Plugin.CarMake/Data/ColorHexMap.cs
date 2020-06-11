using Nop.Plugin.CarMake.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Nop.Plugin.CarMake.Data
{
    public class ColorHexMap : EntityTypeConfiguration<ColorHex>
    {

        public ColorHexMap()
        {
            ToTable("ColorHex");
            HasKey(f => f.Id);
            Property(f => f.Name);
            Property(f => f.hex);
         }
    }
}