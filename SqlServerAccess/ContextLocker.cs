using System;
using System.Collections.Generic;

namespace EFCache.POC.SqlServer
{
    public class ContextLocker
    {
        //TODO: Move to DI (No need for lazy)
        private static Lazy<ContextLocker> _lazy = new Lazy<ContextLocker>(() => new ContextLocker());
        private Dictionary<ContextType, object> _internalDictionary = new Dictionary<ContextType, object>();

        private ContextLocker()
        {
            SingleLock = new object();
            _internalDictionary.Add(ContextType.SingleContext, SingleLock);
        }

        public static ContextLocker Instance => _lazy.Value;
        private object SingleLock { get; }

        public object GetLockingObject(ContextType contextType)
        {
            return _internalDictionary[contextType];
        }

        public enum ContextType
        {
            SingleContext
        }
    }
}