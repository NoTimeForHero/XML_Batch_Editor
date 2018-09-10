namespace XML_Batch_Editor.Controls
{
    partial class ControlPath
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.bChoose = new System.Windows.Forms.Button();
            this.cbPath = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.bChoose, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbPath, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(359, 28);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // bChoose
            // 
            this.bChoose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bChoose.Location = new System.Drawing.Point(282, 2);
            this.bChoose.Margin = new System.Windows.Forms.Padding(0, 2, 3, 3);
            this.bChoose.Name = "bChoose";
            this.bChoose.Size = new System.Drawing.Size(74, 23);
            this.bChoose.TabIndex = 2;
            this.bChoose.Text = "Выбрать";
            this.bChoose.UseVisualStyleBackColor = true;
            this.bChoose.Click += new System.EventHandler(this.bChoose_Click);
            // 
            // cbPath
            // 
            this.cbPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbPath.FormattingEnabled = true;
            this.cbPath.Location = new System.Drawing.Point(3, 3);
            this.cbPath.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.cbPath.Name = "cbPath";
            this.cbPath.Size = new System.Drawing.Size(279, 21);
            this.cbPath.TabIndex = 1;
            // 
            // ControlPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ControlPath";
            this.Size = new System.Drawing.Size(359, 28);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button bChoose;
        private System.Windows.Forms.ComboBox cbPath;
    }
}
