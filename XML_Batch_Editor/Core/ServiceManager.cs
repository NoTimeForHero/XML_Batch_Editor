using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML_Batch_Editor.Core
{
    public interface IService { }

    public class ServiceManager
    {
        private static readonly Lazy<ServiceManager> lazy =
            new Lazy<ServiceManager>(() => new ServiceManager());

        public static ServiceManager Instance => lazy.Value;

        private readonly ConcurrentDictionary<Type, IService> storage = new ConcurrentDictionary<Type, IService>();

        public bool Register<T>() where T : IService
        {
            Type type = typeof(T);
            if (storage.ContainsKey(type)) return false;
            if (!storage.TryAdd(type, Activator.CreateInstance<T>())) return false;
            return true;
        }

        public T Resolve<T>() where T : IService
        {
            if (!storage.ContainsKey(typeof(T))) return default(T);
            if (!storage.TryGetValue(typeof(T), out IService ivalue)) return default(T);
            if (!(ivalue is T value)) return default(T);
            return value;
        }
    }
}
