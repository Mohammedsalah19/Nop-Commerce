using Nop.Plugin.BulkImages.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.BulkImages.Data
{
   public class Image360Map : EntityTypeConfiguration<Images360>
    {
        
        public Image360Map()
        {
            ToTable("Image360");
            HasKey(f => f.Image360_ID);
            Property(f => f.FilePath);
            Property(f => f.Note);
           // this.HasRequired(i => i.Product).WithRequiredPrincipal(m => m.Id).HasForeignKey(i => i.Product);

        }
    }
}
