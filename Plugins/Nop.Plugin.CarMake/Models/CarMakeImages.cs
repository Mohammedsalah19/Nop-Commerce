using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Plugin.CarMake.Models
{
    public class CarMakeImages : BaseEntity
    {
        public int Id { get; set; }
        public int CarMakeId { get; set; }
        public int ImageType { get; set; }
        public string ImagePath { get; set; }
        public string Note { get; set; }

    }
}