using Nop.Core;
using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.BulkImages.Domain
{
  public  class Images360 :BaseEntity
    {
        public int Image360_ID { get; set; }
        public string FilePath { get; set; }
        public string Note { get; set; }
        public int ProductId { get; set; }
    }
}
