using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XML_Batch_Editor.Core;
using XML_Batch_Editor.ViewModels;
using Binding = System.Windows.Forms.Binding;

namespace XML_Batch_Editor.Views
{
    public partial class ViewWork : Form
    {
        public VM_Work ViewModel;

        public ViewWork()
        {
            InitializeComponent();
            InitializeBindings();
        }

        private void InitializeBindings()
        {
            ViewModel = new VM_Work();

            dgvLog.DataSource = ViewModel.Log.ToBindingSource(SynchronizationContext.Current);
            dgvLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DataBindings.Add("Text", ViewModel, nameof(ViewModel.Title));
            lblStatus.DataBindings.Add("Text", ViewModel, nameof(ViewModel.LabelStatus));

            var names = new[]
            {
                nameof(ViewModel.pgBarState) + "." + nameof(ViewModel.pgBarState.Minimum),
                nameof(ViewModel.pgBarState) + "." + nameof(ViewModel.pgBarState.Maximum),
                nameof(ViewModel.pgBarState) + "." + nameof(ViewModel.pgBarState.Value),
                nameof(ViewModel.pgBarState) + "." + nameof(ViewModel.pgBarState.UnknownValue),
            };

            pgStatus.DataBindings.Add("Minimum", ViewModel, names[0]);
            pgStatus.DataBindings.Add("Maximum", ViewModel, names[1]);
            pgStatus.DataBindings.Add("Value", ViewModel, names[2]);

            var bindingStyle = new Binding("Style", ViewModel, names[3]);
            bindingStyle.Format += ConvertProgressStyle;
            bindingStyle.Parse += ConvertProgressStyle;
            pgStatus.DataBindings.Add(bindingStyle);
        }

        private void ConvertProgressStyle(object sender, ConvertEventArgs e)
        {
            if (!(e.Value is bool isInderterminated)) return;
            e.Value = isInderterminated ? ProgressBarStyle.Marquee : ProgressBarStyle.Blocks;
        }
    }
}
