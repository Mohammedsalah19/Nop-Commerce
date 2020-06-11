using Nop.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nop.Plugin.CarMake.Domain
{
    public class ColorHex : BaseEntity
    {
        public int id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Hex")]

        public string Hex { get; set; }
    }
}