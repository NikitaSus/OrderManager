namespace OrderManager
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
            this.cityDistrict = new System.Windows.Forms.TextBox();
            this.firstDeliveryDateTime = new System.Windows.Forms.TextBox();
            this.filterOrders = new System.Windows.Forms.Button();
            this.listBoxOrder = new System.Windows.Forms.ListBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cityDistrict
            // 
            this.cityDistrict.Location = new System.Drawing.Point(51, 120);
            this.cityDistrict.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cityDistrict.Name = "cityDistrict";
            this.cityDistrict.Size = new System.Drawing.Size(145, 20);
            this.cityDistrict.TabIndex = 0;
            // 
            // firstDeliveryDateTime
            // 
            this.firstDeliveryDateTime.Location = new System.Drawing.Point(51, 159);
            this.firstDeliveryDateTime.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.firstDeliveryDateTime.Name = "firstDeliveryDateTime";
            this.firstDeliveryDateTime.Size = new System.Drawing.Size(145, 20);
            this.firstDeliveryDateTime.TabIndex = 1;
            // 
            // filterOrders
            // 
            this.filterOrders.Location = new System.Drawing.Point(51, 194);
            this.filterOrders.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.filterOrders.Name = "filterOrders";
            this.filterOrders.Size = new System.Drawing.Size(144, 40);
            this.filterOrders.TabIndex = 2;
            this.filterOrders.Text = "Отфильтровать и сохранить";
            this.filterOrders.UseVisualStyleBackColor = true;
            this.filterOrders.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // listBoxOrder
            // 
            this.listBoxOrder.FormattingEnabled = true;
            this.listBoxOrder.Location = new System.Drawing.Point(226, 70);
            this.listBoxOrder.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listBoxOrder.Name = "listBoxOrder";
            this.listBoxOrder.Size = new System.Drawing.Size(337, 238);
            this.listBoxOrder.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 105);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Район";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 144);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Время";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxOrder);
            this.Controls.Add(this.filterOrders);
            this.Controls.Add(this.firstDeliveryDateTime);
            this.Controls.Add(this.cityDistrict);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox cityDistrict;
        private System.Windows.Forms.TextBox firstDeliveryDateTime;
        private System.Windows.Forms.Button filterOrders;
        private System.Windows.Forms.ListBox listBoxOrder;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

