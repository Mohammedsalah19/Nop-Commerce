using Nop.Core.Domain.Photo360;
 
namespace Nop.Data.Mapping.Image360Map
{
  public  class Image360Map : NopEntityTypeConfiguration<Image360>
    {
        public Image360Map()
        {
            ToTable("Image360");
            HasKey(f => f.Image360_ID);
            Property(f => f.FilePath);
            Property(f => f.Note);
        }
    }
}
