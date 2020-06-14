using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.CareMake
{
  public partial  class ExtraPictureCarMake :BaseEntity
    {

         public int CarMakeId { get; set; }
        public int ImageType { get; set; }
        public string ImagePath { get; set; }
        public string Note { get; set; }
    }
}
