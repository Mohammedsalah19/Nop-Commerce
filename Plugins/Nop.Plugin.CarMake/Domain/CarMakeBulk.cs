using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Plugin.CarMake.Domain
{
    public class CarMakeBulk: BaseEntity
    {
        public int CarId { get; set; }
        public int ImageType { get; set; }
        public string ColorHex { get; set; }
        public string FolderPath { get; set; }
    }
}