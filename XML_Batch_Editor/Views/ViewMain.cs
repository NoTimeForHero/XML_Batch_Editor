using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Data;
using System.Windows.Forms;
using XML_Batch_Editor.Core;
using XML_Batch_Editor.ViewModels;
using Binding = System.Windows.Forms.Binding;

namespace XML_Batch_Editor.Views
{
    public partial class ViewMain : Form
    {
        private VM_Main ViewModel;

        public ViewMain()
        {
            InitializeComponent();
            InitializeBindings();
        }

        private void InitializeBindings()
        {
            ViewModel = new VM_Main();

            chkValidateXSD.DataBindings.Add("Checked", ViewModel, nameof(ViewModel.UseXSD), true, DataSourceUpdateMode.OnPropertyChanged);

            cpXSD.DataBindings.Add(new Binding("Enabled", ViewModel, nameof(ViewModel.UseXSD)){
                DataSourceUpdateMode = DataSourceUpdateMode.Never, ControlUpdateMode = ControlUpdateMode.OnPropertyChanged
            });

            cpInputDir.DataBindings.Add("Path", ViewModel, nameof(ViewModel.PathToInputDirectory), true, DataSourceUpdateMode.OnPropertyChanged);
            cpXSD.DataBindings.Add("Path", ViewModel, nameof(ViewModel.PathToXSD), true, DataSourceUpdateMode.OnPropertyChanged);

            lblCount.DataBindings.Add("Text", ViewModel, nameof(ViewModel.LabelCount));

            textXPath.DataBindings.Add("Text", ViewModel, nameof(ViewModel.XPath), true, DataSourceUpdateMode.OnPropertyChanged);

            textSearch.DataBindings.Add("Text", ViewModel, nameof(ViewModel.Search), true, DataSourceUpdateMode.OnPropertyChanged);
            textReplace.DataBindings.Add("Text", ViewModel, nameof(ViewModel.Replace), true, DataSourceUpdateMode.OnPropertyChanged);

            chkRegExp.DataBindings.Add("Checked", ViewModel, nameof(ViewModel.UseRegularExpressions), true, DataSourceUpdateMode.OnPropertyChanged);

            btnReplace.BindCommand(ViewModel.OnReplace);
            btnSearch.BindCommand(ViewModel.OnSearch);
        }
    }
}
