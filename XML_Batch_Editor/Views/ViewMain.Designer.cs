using XML_Batch_Editor.Controls;

namespace XML_Batch_Editor.Views
{
    partial class ViewMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chkValidateXSD = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textXPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textSearch = new System.Windows.Forms.TextBox();
            this.textReplace = new System.Windows.Forms.TextBox();
            this.chkRegExp = new System.Windows.Forms.CheckBox();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblErrors = new System.Windows.Forms.Label();
            this.cpInputDir = new XML_Batch_Editor.Controls.ControlPath();
            this.cpXSD = new XML_Batch_Editor.Controls.ControlPath();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.Controls.Add(this.lblCount, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.chkValidateXSD, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.cpInputDir, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cpXSD, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.textXPath, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label5, 3, 8);
            this.tableLayoutPanel1.Controls.Add(this.textSearch, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.textReplace, 3, 9);
            this.tableLayoutPanel1.Controls.Add(this.chkRegExp, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.pnlButtons, 3, 13);
            this.tableLayoutPanel1.Controls.Add(this.lblErrors, 1, 12);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 15;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(659, 344);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblCount
            // 
            this.lblCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCount.Location = new System.Drawing.Point(23, 85);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(292, 25);
            this.lblCount.TabIndex = 1000;
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(23, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(292, 25);
            this.label4.TabIndex = 10;
            this.label4.Text = "Искать значение:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkValidateXSD
            // 
            this.chkValidateXSD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkValidateXSD.AutoSize = true;
            this.chkValidateXSD.Location = new System.Drawing.Point(348, 88);
            this.chkValidateXSD.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.chkValidateXSD.Name = "chkValidateXSD";
            this.chkValidateXSD.Size = new System.Drawing.Size(285, 19);
            this.chkValidateXSD.TabIndex = 12;
            this.chkValidateXSD.Text = "Проверять входные файлы по XSD";
            this.chkValidateXSD.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(23, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(292, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Входная директория:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(341, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(292, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "XSD схема:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.label3, 3);
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(23, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(610, 30);
            this.label3.TabIndex = 10;
            this.label3.Text = "XML Path Language (XPath):";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textXPath
            // 
            this.textXPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.textXPath, 3);
            this.textXPath.Location = new System.Drawing.Point(23, 153);
            this.textXPath.Name = "textXPath";
            this.textXPath.Size = new System.Drawing.Size(610, 20);
            this.textXPath.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(341, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(292, 25);
            this.label5.TabIndex = 12;
            this.label5.Text = "Заменить на значение:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textSearch
            // 
            this.textSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textSearch.Location = new System.Drawing.Point(23, 214);
            this.textSearch.Name = "textSearch";
            this.textSearch.Size = new System.Drawing.Size(292, 20);
            this.textSearch.TabIndex = 20;
            // 
            // textReplace
            // 
            this.textReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textReplace.Location = new System.Drawing.Point(341, 214);
            this.textReplace.Name = "textReplace";
            this.textReplace.Size = new System.Drawing.Size(292, 20);
            this.textReplace.TabIndex = 25;
            // 
            // chkRegExp
            // 
            this.chkRegExp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRegExp.AutoSize = true;
            this.chkRegExp.Location = new System.Drawing.Point(23, 240);
            this.chkRegExp.Name = "chkRegExp";
            this.chkRegExp.Size = new System.Drawing.Size(292, 17);
            this.chkRegExp.TabIndex = 28;
            this.chkRegExp.Text = "Использовать регулярные выражения";
            this.chkRegExp.UseVisualStyleBackColor = true;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlButtons.Controls.Add(this.btnReplace);
            this.pnlButtons.Controls.Add(this.btnSearch);
            this.pnlButtons.Location = new System.Drawing.Point(386, 290);
            this.pnlButtons.Margin = new System.Windows.Forms.Padding(0);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(250, 34);
            this.pnlButtons.TabIndex = 999;
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(130, 3);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(120, 28);
            this.btnReplace.TabIndex = 35;
            this.btnReplace.Text = "Замена";
            this.btnReplace.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(0, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(120, 28);
            this.btnSearch.TabIndex = 30;
            this.btnSearch.Text = "Поиск";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // lblErrors
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.lblErrors, 2);
            this.lblErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblErrors.ForeColor = System.Drawing.Color.Maroon;
            this.lblErrors.Location = new System.Drawing.Point(23, 270);
            this.lblErrors.Name = "lblErrors";
            this.tableLayoutPanel1.SetRowSpan(this.lblErrors, 2);
            this.lblErrors.Size = new System.Drawing.Size(312, 54);
            this.lblErrors.TabIndex = 1001;
            // 
            // cpInputDir
            // 
            this.cpInputDir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cpInputDir.Location = new System.Drawing.Point(26, 51);
            this.cpInputDir.ManualPathInsert = false;
            this.cpInputDir.Margin = new System.Windows.Forms.Padding(6);
            this.cpInputDir.Name = "cpInputDir";
            this.cpInputDir.Path = "";
            this.cpInputDir.Size = new System.Drawing.Size(286, 28);
            this.cpInputDir.TabIndex = 5;
            // 
            // cpXSD
            // 
            this.cpXSD.ControlType = XML_Batch_Editor.Controls.PathType.OpenFile;
            this.cpXSD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cpXSD.Enabled = false;
            this.cpXSD.Filter = "XML Schema definition (*.xsd)|*.xsd";
            this.cpXSD.InitialDirectory = "";
            this.cpXSD.Location = new System.Drawing.Point(344, 51);
            this.cpXSD.ManualPathInsert = false;
            this.cpXSD.Margin = new System.Windows.Forms.Padding(6);
            this.cpXSD.Name = "cpXSD";
            this.cpXSD.Path = "";
            this.cpXSD.Size = new System.Drawing.Size(286, 28);
            this.cpXSD.TabIndex = 10;
            // 
            // ViewMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(178)))));
            this.ClientSize = new System.Drawing.Size(659, 344);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.Name = "ViewMain";
            this.Text = "Пакетное редактирование XML";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private ControlPath cpInputDir;
        private System.Windows.Forms.CheckBox chkValidateXSD;
        private System.Windows.Forms.Label label2;
        private ControlPath cpXSD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textXPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textSearch;
        private System.Windows.Forms.TextBox textReplace;
        private System.Windows.Forms.CheckBox chkRegExp;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblErrors;
    }
}

