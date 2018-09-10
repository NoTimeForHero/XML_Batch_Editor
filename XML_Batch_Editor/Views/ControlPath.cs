using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.WindowsAPICodePack.Dialogs;
using XML_Batch_Editor.Annotations;

namespace XML_Batch_Editor.Controls
{
    public enum PathType
    {
        SelectFolder = 0,
        OpenFile = 1,
        SaveFile = 2
    }

    [Designer(typeof(DenyHeightSizingDesigner))]
    public partial class ControlPath : UserControl, INotifyPropertyChanged
    {

#region Control Properties
        [Browsable(true), DefaultValue(true)]
        [Category("CUSTOM"), Description("Разрешено ли пользователю вставить путь автоматически")]
        public bool ManualPathInsert
        {
            get => cbPath.DropDownStyle == ComboBoxStyle.DropDown;
            set => cbPath.DropDownStyle = value ? ComboBoxStyle.DropDown : ComboBoxStyle.DropDownList;
        }

        [Browsable(true), DefaultValue((int) 10)]
        [Category("CUSTOM"), Description("Сколько элементов может лежать в DropDownList")]
        public int MaxValuesInList { get; set; }

        [Browsable(true), DefaultValue(typeof(PathType), "SelectFolder")]
        [Category("CUSTOM"), Description("Тип диалога открываемого по кнопке")]
        public PathType ControlType { get; set; }

        [Browsable(true), DefaultValue(null)]
        [Category("CUSTOM"), Description("Начальная директория для диалога")]
        public string InitialDirectory { get; set; }

        [Browsable(true), DefaultValue(null)]
        [Category("CUSTOM"), Description("Фильтр файлов")]
        public string Filter { get; set; }

        [Browsable(false)]
        public ComboBox.ObjectCollection PathValues
        {
            get => cbPath.Items;
            set
            {
                cbPath.Items.Clear();
                foreach (var item in value) cbPath.Items.Add(item);
            }
        }

        public string Path
        {
            get => cbPath.Text;
            set
            {
                if (string.IsNullOrEmpty(value)) return;

                int index = cbPath.Items.IndexOf(value);
                if (index < 0)
                {
                    if (cbPath.Items.Count >= MaxValuesInList)
                        cbPath.Items.RemoveAt(cbPath.Items.Count - 1);
                    cbPath.Items.Insert(0, value);
                    index = 0;
                }

                cbPath.SelectedIndex = index;
                OnPropertyChanged();
            }
        }

        #endregion

        // Запрещаем изменять высоту элемента через код или ручную установку свойства в дизайнере
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            int currentHeight = tableLayoutPanel1.Height;
            base.SetBoundsCore(x, y, width, currentHeight, specified);
        }

        public ControlPath()
        {
            MaxValuesInList = 10;
            InitializeComponent();
            cbPath.SelectedIndexChanged += (e, v) => OnPropertyChanged(nameof(Path));
        }


        private void bChoose_Click(object sender, EventArgs e)
        {
            string path;
            switch (ControlType)
            {
                case PathType.SelectFolder:
                    path = DialogFolder();
                    break;
                case PathType.OpenFile:
                case PathType.SaveFile:
                    path = DialogFile(ControlType);
                    break;
                default:
                    throw new NotImplementedException($"Unknown enum {nameof(PathType)} value {ControlType}!");
            }

            Path = path;
        }

        private string DialogFile(PathType type)
        {
            FileDialog dialog = null;
            if (type == PathType.OpenFile) dialog = new OpenFileDialog();
            if (type == PathType.SaveFile) dialog = new SaveFileDialog();
            if (dialog == null) throw new ArgumentException($"Unknown enum value \"{type}\" for this method!", nameof(PathType));

            dialog.Filter = Filter;
            dialog.InitialDirectory = InitialDirectory;

            DialogResult result = dialog.ShowDialog();
            return result == DialogResult.OK ? dialog.FileName : null;
        }


        private string DialogFolder()
        {
            if (!CommonFileDialog.IsPlatformSupported)
            {
                var dialog = new FolderBrowserDialog();
                if (InitialDirectory != null) dialog.SelectedPath = InitialDirectory;
                DialogResult result = dialog.ShowDialog();
                return result == DialogResult.OK ? dialog.SelectedPath : null;
            }
            else
            {
                var dialog = new CommonOpenFileDialog
                {
                    IsFolderPicker = true
                };
                if (InitialDirectory != null) dialog.InitialDirectory = InitialDirectory;
                CommonFileDialogResult result = dialog.ShowDialog();
                return result == CommonFileDialogResult.Ok ? dialog.FileName : null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    internal class DenyHeightSizingDesigner : ControlDesigner
    {
        DenyHeightSizingDesigner()
        {
            AutoResizeHandles = true;
        }

        // Отключаем в дизайнере кнопки изменения высоты элемента
        public override SelectionRules SelectionRules => SelectionRules.LeftSizeable | SelectionRules.RightSizeable | SelectionRules.Moveable;
    }
}
