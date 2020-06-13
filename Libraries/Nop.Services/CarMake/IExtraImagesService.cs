using Nop.Core.Domain.CareMake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.CarMake
{
  public  interface IExtraImagesService
    {
        List<CarMakeExtraImages> GetCarMakeExtraImages(int CarMakeId);

    }
}
