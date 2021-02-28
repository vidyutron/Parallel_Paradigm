
namespace Sync_ed
{
    partial class Non_Blocking
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_calculate = new System.Windows.Forms.Button();
            this.btn_calculate_async = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_calculate
            // 
            this.btn_calculate.Location = new System.Drawing.Point(300, 192);
            this.btn_calculate.Name = "btn_calculate";
            this.btn_calculate.Size = new System.Drawing.Size(185, 48);
            this.btn_calculate.TabIndex = 1;
            this.btn_calculate.Text = "Calculate";
            this.btn_calculate.UseVisualStyleBackColor = true;
            this.btn_calculate.Click += new System.EventHandler(this.btn_calculate_Click);
            // 
            // btn_calculate_async
            // 
            this.btn_calculate_async.Location = new System.Drawing.Point(300, 264);
            this.btn_calculate_async.Name = "btn_calculate_async";
            this.btn_calculate_async.Size = new System.Drawing.Size(185, 48);
            this.btn_calculate_async.TabIndex = 1;
            this.btn_calculate_async.Text = "Calculate - Async";
            this.btn_calculate_async.UseVisualStyleBackColor = true;
            this.btn_calculate_async.Click += new System.EventHandler(this.btn_calculate_async_ClickAsync);
            // 
            // Non_Blocking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_calculate_async);
            this.Controls.Add(this.btn_calculate);
            this.Name = "Non_Blocking";
            this.Text = "Non-Blocking Window";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_calculate;
        private System.Windows.Forms.Button btn_calculate_async;
    }
}