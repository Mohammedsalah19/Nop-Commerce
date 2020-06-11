using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.CareMake
{
   public partial class CarMakeBulkImages : BaseEntity
    {
        public int CarId { get; set; }
         public int ImageType { get; set; }
         public string ColorHex { get; set; }
        public string FolderPath { get; set; }
    }
}
