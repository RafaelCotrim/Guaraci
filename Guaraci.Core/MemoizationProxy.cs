using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Guaraci.Core.Optimization
{
    public class MemoizationProxy: DispatchProxy
    {
        private Dictionary<object[], object> _memory = new Dictionary<object[], object>(new ArgumentsComparer());
      
        private object _decorated;

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            // TODO If args.length == 0, prevent memoization

            if (targetMethod.GetCustomAttribute<MemoizeAttribute>() is null)
                return targetMethod.Invoke(_decorated, args);

            object val;

            _memory.TryGetValue(args, out val);

            if(val != null)
                return val;

            val = targetMethod.Invoke(_decorated, args);
            _memory.Add(args, val);
            return val;
        }

        public static T Create<T>(T decorated) 
        {
            object proxy = Create<T, MemoizationProxy>();
            ((MemoizationProxy)proxy).SetParameters(decorated);

            return (T)proxy;
        }

        private void SetParameters(object decorated)
        {
            if (decorated == null)
            {
                throw new ArgumentNullException(nameof(decorated));
            }
            _decorated = decorated;
        }
    }
}
