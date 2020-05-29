using MetroFramework.Forms;

namespace lab_8
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.metroSM = new MetroFramework.Components.MetroStyleManager(this.components);
            this.MBtnGet = new MetroFramework.Controls.MetroButton();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.metroRadioButton4 = new MetroFramework.Controls.MetroRadioButton();
            this.metroRadioButton3 = new MetroFramework.Controls.MetroRadioButton();
            this.metroRadioButton2 = new MetroFramework.Controls.MetroRadioButton();
            this.metroRadioButton1 = new MetroFramework.Controls.MetroRadioButton();
            this.DGView = new System.Windows.Forms.DataGridView();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ItemCheckConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtConstrainsWay = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.metroSM)).BeginInit();
            this.metroPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGView)).BeginInit();
            this.MenuStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroSM
            // 
            this.metroSM.Owner = null;
            // 
            // MBtnGet
            // 
            this.MBtnGet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MBtnGet.Location = new System.Drawing.Point(2, 2);
            this.MBtnGet.Margin = new System.Windows.Forms.Padding(2);
            this.MBtnGet.Name = "MBtnGet";
            this.MBtnGet.Size = new System.Drawing.Size(184, 45);
            this.MBtnGet.TabIndex = 0;
            this.MBtnGet.Text = "Получить данные";
            this.MBtnGet.Click += new System.EventHandler(this.MBtnGet_Click);
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.metroRadioButton4);
            this.metroPanel1.Controls.Add(this.metroRadioButton3);
            this.metroPanel1.Controls.Add(this.metroRadioButton2);
            this.metroPanel1.Controls.Add(this.metroRadioButton1);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 8;
            this.metroPanel1.Location = new System.Drawing.Point(2, 51);
            this.metroPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(184, 192);
            this.metroPanel1.TabIndex = 8;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 8;
            // 
            // metroRadioButton4
            // 
            this.metroRadioButton4.AutoSize = true;
            this.metroRadioButton4.Location = new System.Drawing.Point(34, 110);
            this.metroRadioButton4.Margin = new System.Windows.Forms.Padding(2);
            this.metroRadioButton4.Name = "metroRadioButton4";
            this.metroRadioButton4.Size = new System.Drawing.Size(79, 15);
            this.metroRadioButton4.TabIndex = 11;
            this.metroRadioButton4.Text = "Динамика";
            this.metroRadioButton4.UseVisualStyleBackColor = true;
            // 
            // metroRadioButton3
            // 
            this.metroRadioButton3.AutoSize = true;
            this.metroRadioButton3.Location = new System.Drawing.Point(34, 80);
            this.metroRadioButton3.Margin = new System.Windows.Forms.Padding(2);
            this.metroRadioButton3.Name = "metroRadioButton3";
            this.metroRadioButton3.Size = new System.Drawing.Size(74, 15);
            this.metroRadioButton3.TabIndex = 10;
            this.metroRadioButton3.Text = "Рекурсия";
            this.metroRadioButton3.UseVisualStyleBackColor = true;
            // 
            // metroRadioButton2
            // 
            this.metroRadioButton2.AutoSize = true;
            this.metroRadioButton2.Location = new System.Drawing.Point(34, 50);
            this.metroRadioButton2.Margin = new System.Windows.Forms.Padding(2);
            this.metroRadioButton2.Name = "metroRadioButton2";
            this.metroRadioButton2.Size = new System.Drawing.Size(116, 15);
            this.metroRadioButton2.TabIndex = 9;
            this.metroRadioButton2.Text = "Бинарные маски";
            this.metroRadioButton2.UseVisualStyleBackColor = true;
            // 
            // metroRadioButton1
            // 
            this.metroRadioButton1.AutoSize = true;
            this.metroRadioButton1.Checked = true;
            this.metroRadioButton1.Location = new System.Drawing.Point(34, 20);
            this.metroRadioButton1.Margin = new System.Windows.Forms.Padding(2);
            this.metroRadioButton1.Name = "metroRadioButton1";
            this.metroRadioButton1.Size = new System.Drawing.Size(125, 15);
            this.metroRadioButton1.TabIndex = 8;
            this.metroRadioButton1.TabStop = true;
            this.metroRadioButton1.Text = "Жадный алгоритм";
            this.metroRadioButton1.UseVisualStyleBackColor = true;
            // 
            // DGView
            // 
            this.DGView.AllowUserToAddRows = false;
            this.DGView.AllowUserToDeleteRows = false;
            this.DGView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DGView.Location = new System.Drawing.Point(190, 2);
            this.DGView.Margin = new System.Windows.Forms.Padding(2);
            this.DGView.Name = "DGView";
            this.DGView.RowHeadersVisible = false;
            this.DGView.RowHeadersWidth = 51;
            this.tableLayoutPanel1.SetRowSpan(this.DGView, 5);
            this.DGView.RowTemplate.Height = 24;
            this.DGView.Size = new System.Drawing.Size(412, 330);
            this.DGView.TabIndex = 9;
            // 
            // metroButton2
            // 
            this.metroButton2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroButton2.Location = new System.Drawing.Point(2, 247);
            this.metroButton2.Margin = new System.Windows.Forms.Padding(2);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(184, 45);
            this.metroButton2.TabIndex = 10;
            this.metroButton2.Text = "Найти решение";
            this.metroButton2.Click += new System.EventHandler(this.metroButton2_Click);
            // 
            // MenuStrip
            // 
            this.MenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemCheckConnection});
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(245, 26);
            // 
            // ItemCheckConnection
            // 
            this.ItemCheckConnection.Name = "ItemCheckConnection";
            this.ItemCheckConnection.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F1)));
            this.ItemCheckConnection.Size = new System.Drawing.Size(244, 22);
            this.ItemCheckConnection.Text = "Проверить соединение";
            this.ItemCheckConnection.Click += new System.EventHandler(this.ItemCheckConnection_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.12583F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.87417F));
            this.tableLayoutPanel1.Controls.Add(this.MBtnGet, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.metroPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.DGView, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.metroButton2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtConstrainsWay, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 60);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.13652F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.86348F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(604, 334);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 294);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Ограничение на длину пути:";
            // 
            // txtConstrainsWay
            // 
            this.txtConstrainsWay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConstrainsWay.Location = new System.Drawing.Point(3, 310);
            this.txtConstrainsWay.Name = "txtConstrainsWay";
            this.txtConstrainsWay.Size = new System.Drawing.Size(182, 20);
            this.txtConstrainsWay.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 410);
            this.ContextMenuStrip = this.MenuStrip;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(15, 60, 15, 16);
            this.Resizable = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "Optimal Solution";
            ((System.ComponentModel.ISupportInitialize)(this.metroSM)).EndInit();
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGView)).EndInit();
            this.MenuStrip.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Components.MetroStyleManager metroSM;
        private MetroFramework.Controls.MetroButton MBtnGet;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroRadioButton metroRadioButton4;
        private MetroFramework.Controls.MetroRadioButton metroRadioButton3;
        private MetroFramework.Controls.MetroRadioButton metroRadioButton2;
        private MetroFramework.Controls.MetroRadioButton metroRadioButton1;
        private System.Windows.Forms.DataGridView DGView;
        private MetroFramework.Controls.MetroButton metroButton2;
        private System.Windows.Forms.ContextMenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ItemCheckConnection;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConstrainsWay;
    }
}

