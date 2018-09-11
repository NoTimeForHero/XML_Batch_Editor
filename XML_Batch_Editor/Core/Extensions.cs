using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace XML_Batch_Editor.Core
{
    public static class Extensions
    {
        public static int Occurencies(this string original, string pattern)
        {
            int count = 0;
            int i = 0;
            while ((i = original.IndexOf(pattern, i, StringComparison.Ordinal)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }


        public static void BindCommand<T>(this T invoker, ICommand command) where T : ButtonBase
        {
            T invoker1 = invoker;
            ICommand command1 = command;

            invoker1.Enabled = command1.CanExecute(null);
            invoker1.Click += delegate { command1.Execute(null); };
            command1.CanExecuteChanged += delegate
            {
                invoker1.Enabled = command1.CanExecute(null);
            };
        }

        public static BindingSource ToBindingSource<T>(this ObservableConcurrentQueue<T> container, SynchronizationContext ctx)
        {
            DataTable data = new DataTable();
            data.Columns.Add();
            var src = new BindingSource { DataSource = data };

            container.ToList().ForEach(x => data.Rows.Add(x));
            container.CollectionChanged += (o, ev) =>
            {
                if (ev.eventType != QueueChangedEventType.Added) return;
                if (!container.TryDequeue(out T value)) return;
                ctx.Post(x => data.Rows.Add(value), null);
            };
            return src;
        }
    }
}
