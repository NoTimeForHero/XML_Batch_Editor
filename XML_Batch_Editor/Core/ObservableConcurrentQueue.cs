using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XML_Batch_Editor.Annotations;

namespace XML_Batch_Editor.Core
{
    // Отсутствие в BCL интерфейса IQueue вынуждает писать подобные обёртки
    public class ObservableConcurrentQueue<T> : IProducerConsumerCollection<T>, IReadOnlyCollection<T>
    {
        public event QueueuChangedEventHandler CollectionChanged;
        public delegate void QueueuChangedEventHandler(object sender, QueueChangedEventArgs<T> args);

        private readonly ConcurrentQueue<T> CQ;

        public ObservableConcurrentQueue()
        {
            CQ = new ConcurrentQueue<T>();
        }

        #region ConcurrentQueue<T> Pseudo-Override
        public void Enqueue(T item)
        {
            CQ.Enqueue(item);
            CollectionChanged?.Invoke(this, new QueueChangedEventArgs<T>(QueueChangedEventType.Added, item));
        }

        public bool TryDequeue(out T result)
        {
            bool status = CQ.TryDequeue(out result);
            CollectionChanged?.Invoke(this, new QueueChangedEventArgs<T>(QueueChangedEventType.Removed, result, status));
            return status;
        }

        public bool TryPeek(out T result)
        {
            return CQ.TryPeek(out result);
        }
        #endregion

        #region IReadOnlyCollection<T> Implementation
        public IEnumerator<T> GetEnumerator()
        {
            return CQ.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return CQ.GetEnumerator();
        }

        public int Count => CQ.Count;
        public object SyncRoot => throw new NotSupportedException("ConcurrentCollection_SyncRoot_NotSupported");
        public bool IsSynchronized => false;

        public void CopyTo(Array array, int index)
        {
            ((ICollection)CQ.ToList()).CopyTo(array, index);
        }

        int IReadOnlyCollection<T>.Count => CQ.Count;
        #endregion

        #region IProducerConsumerCollection<T> Implementation
        public void CopyTo(T[] array, int index)
        {
            ((ICollection)CQ.ToList()).CopyTo(array, index);
        }

        public bool TryAdd(T item)
        {
            Enqueue(item);
            return true;
        }

        public bool TryTake(out T item)
        {
            return TryDequeue(out item);
        }

        public T[] ToArray()
        {
            return CQ.ToArray();
        }
        #endregion
    }

    public enum QueueChangedEventType
    {
        Added, Removed
    }

    public class QueueChangedEventArgs<T>
    {
        public QueueChangedEventType eventType;
        public bool? result;
        public T item;

        public QueueChangedEventArgs(QueueChangedEventType eventType, T item, bool? result = null)
        {
            this.eventType = eventType;
            this.item = item;
            this.result = result;
        }
    }
}
