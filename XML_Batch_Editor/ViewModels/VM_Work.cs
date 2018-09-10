using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using XML_Batch_Editor.Core;

namespace XML_Batch_Editor.ViewModels
{
    public class VM_Work : ViewModel
    {
        private string _LabelStatus;
        public string LabelStatus
        {
            get => _LabelStatus;
            set => RaiseAndSetIfChanged(ref _LabelStatus, value);
        }

        public ProgressBarState pgBarState { get; }

        // ConcurrentQueue для лога выбрана по причине того, что она lock-free
        public ObservableConcurrentQueue<string> Log;

        public VM_Work(Form form) : base(form)
        {
            Log = new ObservableConcurrentQueue<string>();
            pgBarState = new ProgressBarState {context = context};
        }
    }

    public class ProgressBarState : XObservable
    {
        private int _minimum;
        private int _maximum = 100;
        private int _value;

        private bool _unknownValue;

        public int Minimum
        {
            get => _minimum;
            set => RaiseAndSetIfChanged(ref _minimum, value);
        }

        public int Maximum
        {
            get => _maximum;
            set => RaiseAndSetIfChanged(ref _maximum, value);
        }

        public int Value
        {
            get => _value;
            set => RaiseAndSetIfChanged(ref _value, value);
        }

        public bool UnknownValue
        {
            get => _unknownValue;
            set => RaiseAndSetIfChanged(ref _unknownValue, value);
        }
    }
}
