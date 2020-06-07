using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Photo360
{
   public partial class Image360 : BaseEntity
    {
        public int Image360_ID { get; set; }
        public string FilePath { get; set; }
        public string Note { get; set; }
        public int ProductId { get; set; }


    }
}
