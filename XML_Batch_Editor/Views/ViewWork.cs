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
using Timer = System.Windows.Forms.Timer;

namespace XML_Batch_Editor.Views
{
    public partial class ViewWork : Form
    {
        public VM_Work ViewModel;

        private DataTable data;

        public ViewWork()
        {
            InitializeComponent();
            InitializeBindings();
        }

        private void InitializeBindings()
        {
            ViewModel = new VM_Work();

            data = new DataTable();
            data.Columns.Add();
            dgvLog.DataSource = new BindingSource {DataSource = data};

            Timer timer = new Timer();
            timer.Interval = 100;
            timer.Enabled = true;
            timer.Tick += (o, ev) =>
            {
                pgStatus.Maximum = ViewModel.pgBarState.Maximum;
                pgStatus.Minimum = ViewModel.pgBarState.Minimum;
                pgStatus.Value = ViewModel.pgBarState.Value;
                pgStatus.Style = ViewModel.pgBarState.UnknownValue ? ProgressBarStyle.Marquee : ProgressBarStyle.Blocks;

                lblStatus.Text = ViewModel.LabelStatus;

                while (ViewModel.Log.TryDequeue(out string message))
                {
                    data.Rows.Add(message);
                }
            };
            timer.Start();
        }
    }
}
