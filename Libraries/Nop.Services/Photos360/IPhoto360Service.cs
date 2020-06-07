using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Photos360
{
   public interface IPhoto360Service
    {
        string GetProductPath(int ProductId);
    }
}
