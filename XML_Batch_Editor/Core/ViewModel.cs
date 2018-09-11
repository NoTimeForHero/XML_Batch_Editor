using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XML_Batch_Editor.Annotations;

namespace XML_Batch_Editor.Core
{
    public class ViewModel : XObservable
    {
        public ViewModel()
        {
            // Если мы создаём ViewModel при создании формы, значит текущим контекст синхронизации является UI контекст
            context = SynchronizationContext.Current;
        }
    }

    public class XObservable : INotifyPropertyChanged
    {
        public SynchronizationContext context;
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaiseAndSetIfChanged<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            if (property != null && property.Equals(value)) return;
            //Console.WriteLine($"Property {propertyName} changed from {property} to {value}");
            property = value;

            if (context == null) PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            else context.Post(o => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)),null);
        }

        // Подписка на изменение конкретного свойства
        protected void Subscribe(string property, Action action)
        {
            PropertyChanged += (o, ev) =>
            {
                if (!property.Equals(ev.PropertyName)) return;
                action();
            };
        }
    }
}
