using Nop.Core.Domain.CareMake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.CarMake
{
   public interface IExtraMakeCarImagesService
    {
        List<ExtraPictureCarMake> GetCarMakeExtraImages(int CarMakeId);

    }
}
