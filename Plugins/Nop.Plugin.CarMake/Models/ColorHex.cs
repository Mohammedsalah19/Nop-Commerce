using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Plugin.CarMake.Models
{
    public class ColorHex: BaseEntity
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string hex { get; set; }
    }
}