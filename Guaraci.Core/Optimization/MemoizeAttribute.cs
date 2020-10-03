using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Guaraci.Core.Optimization
{
    [AttributeUsage(AttributeTargets.Method, Inherited=true)]
    public class MemoizeAttribute: Attribute
    {
    }
}
