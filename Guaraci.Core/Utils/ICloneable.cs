using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guaraci.Core.Utils
{
    public interface ICloneable<T> : ICloneable
    {
        new T Clone();
    }
}
