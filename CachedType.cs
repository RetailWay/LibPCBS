using System;

namespace RetailWay.Integration.LibPCBS
{
    public struct CachedType<T>
    {
        private const uint Lifetime = 120; // Измеряется в секундах
        
        private T _cache;
        private DateTime _created;
        private readonly Func<T> _builder;
        private readonly Action<T> _destructor;

        public T Value
        {
            get
            {
                if ((DateTime.UtcNow - _created).TotalSeconds < Lifetime)
                    return _cache;
                _destructor?.Invoke(_cache);
                _created = DateTime.UtcNow;
                _cache = _builder.Invoke();
                return _cache;
            }
            /*set
            {
                _destructor?.Invoke(_cache);
                _cache = value;
                _created = DateTime.UtcNow;
            }*/
        }

        public CachedType(Func<T> builder, Action<T> destructor = null)
        {
            _builder = builder;
            _destructor = destructor;
            _cache = builder.Invoke();
            _created = DateTime.UtcNow;
        }
    }
}