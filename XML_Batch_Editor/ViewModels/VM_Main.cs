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
using FluentValidation;
using FluentValidation.Results;
using XML_Batch_Editor.Annotations;
using XML_Batch_Editor.Core;
using XML_Batch_Editor.Services;
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

        private bool _NeedReplace;
        private bool _UseRegularExpressions;
        private string _Search;
        private string _Replace;
        #endregion

        private readonly ServiceMain serviceMain;

        public VM_Main()
        {
            serviceMain = ServiceManager.Instance.Resolve<ServiceMain>();

            RegisterValidator<VM_Main,ValidatorMain>();

            OnSearch = new SimpleCommand(x => DoWork(false), x => !HaveErrors).Subscribe(this);
            OnReplace = new SimpleCommand(x => DoWork(true), x => !HaveErrors).Subscribe(this);

            Subscribe(nameof(PathToInputDirectory), () => LabelCount = "Файлов XML в директории: " + serviceMain.FilesCount(PathToInputDirectory));
        }

        private void DoWork(bool replace)
        {
            NeedReplace = replace;

            // TODO: WinForms недопустимо вызывать из ViewModel или сервиса, необходимо понять куда перенести эту ответственность
            ViewWork work = new ViewWork();
            serviceMain.Convert(this, work.ViewModel);
            work.ShowDialog();
        }

        #region Reactive Bindings

        public SimpleCommand OnSearch { get; set; }
        public SimpleCommand OnReplace { get; set; }

        public bool NeedReplace
        {
            get => _NeedReplace;
            set => RaiseAndSetIfChanged(ref _NeedReplace, value);
        }

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
