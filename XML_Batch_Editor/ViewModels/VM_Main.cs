using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using XML_Batch_Editor.Annotations;
using XML_Batch_Editor.Core;
using XML_Batch_Editor.Model;
using XML_Batch_Editor.Views;
using MessageBox = System.Windows.MessageBox;

namespace XML_Batch_Editor.ViewModels
{
    public class VM_Main : ViewModel
    {
        #region Property Variables

        private string _PathToInputDirectory;
        private string _LabelCount;

        private string _PathToXSD;
        private bool _UseXSD;

        private string _XPath;

        private bool _UseRegularExpressions;
        private string _Search;
        private string _Replace;
        #endregion

        private readonly ServiceMain serviceMain = ServiceManager.Instance.Resolve<ServiceMain>();

        public VM_Main(Form form) : base(form)
        {
            OnSearch = new SimpleCommand(x => DoWork(false));
            OnReplace = new SimpleCommand(x => DoWork(true));

           Subscribe(nameof(PathToInputDirectory), () => LabelCount = "Файлов XML в директории: " + serviceMain.FilesCount(PathToInputDirectory));
        }

        private void DoWork(bool Replace)
        {
            // TODO: WinForms недопустимо вызывать из ViewModel, необходимо понять куда перенести эту ответственность
            ViewWork work = new ViewWork();
            var VM = work.ViewModel;

            int currentValue = 0;

            // Возможно для этой задачи стоит заменить INPC вьюмодели с Dispatcher-ом на Timer для формы
            // Иначе при быстрых задачах в UI поток будет сыпаться огромное количество запросов на обновление UI
            // Но от этого пострадает читаемость кода, поэтому оставлю пока всё как есть
            for (int x = 0; x < 5; x++)
            {
                int wait = x;
                Task.Factory.StartNew(async () =>
                {
                    await Task.Delay(100*(wait/2));

                    for (int i = 0; i <= 19; i++)
                    {
                        VM.pgBarState.Value = Interlocked.Increment(ref currentValue);
                        VM.LabelStatus = $"Обработано документов: {VM.pgBarState.Value}/{VM.pgBarState.Maximum}";

                        VM.Log.TryAdd($"Hello from thread {Thread.CurrentThread.ManagedThreadId}");
                        await Task.Delay(100);
                    }
                });
            }

            work.ShowDialog(View);
        }

        #region Reactive Bindings

        public SimpleCommand OnSearch { get; set; }
        public SimpleCommand OnReplace { get; set; }

        public string LabelCount
        {
            get => _LabelCount;
            set => RaiseAndSetIfChanged(ref _LabelCount, value);
        }

        public string PathToInputDirectory
        {
            get => _PathToInputDirectory;
            set => RaiseAndSetIfChanged(ref _PathToInputDirectory, value);
        }

        public string PathToXSD
        {
            get => _PathToXSD;
            set => RaiseAndSetIfChanged(ref _PathToXSD, value);
        }

        public bool UseXSD
        {
            get => _UseXSD;
            set => RaiseAndSetIfChanged(ref _UseXSD, value);
        }

        public string XPath
        {
            get => _XPath;
            set => RaiseAndSetIfChanged(ref _XPath, value);
        }

        public string Search
        {
            get => _Search;
            set => RaiseAndSetIfChanged(ref _Search, value);
        }

        public string Replace
        {
            get => _Replace;
            set => RaiseAndSetIfChanged(ref _Replace, value);
        }

        public bool UseRegularExpressions
        {
            get => _UseRegularExpressions;
            set => RaiseAndSetIfChanged(ref _UseRegularExpressions, value);
        }
        #endregion

    }
}
