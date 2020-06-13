using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.CareMake
{
  public  class CarMakeExtraImages : BaseEntity
    {
        public int CarMakeId { get; set; }
        public bool ImageType { get; set; }
        public string ImagePath { get; set; }
    }

}