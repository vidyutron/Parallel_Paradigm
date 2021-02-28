
namespace Sync_ed
{
    partial class Entry
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_blocking = new System.Windows.Forms.Button();
            this.btn_nonblocking = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_blocking
            // 
            this.btn_blocking.Location = new System.Drawing.Point(293, 128);
            this.btn_blocking.Name = "btn_blocking";
            this.btn_blocking.Size = new System.Drawing.Size(196, 52);
            this.btn_blocking.TabIndex = 0;
            this.btn_blocking.Text = "Blocking Window";
            this.btn_blocking.UseVisualStyleBackColor = true;
            this.btn_blocking.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_nonblocking
            // 
            this.btn_nonblocking.Location = new System.Drawing.Point(293, 201);
            this.btn_nonblocking.Name = "btn_nonblocking";
            this.btn_nonblocking.Size = new System.Drawing.Size(196, 52);
            this.btn_nonblocking.TabIndex = 1;
            this.btn_nonblocking.Text = "Non-Blocking Window";
            this.btn_nonblocking.UseVisualStyleBackColor = true;
            this.btn_nonblocking.Click += new System.EventHandler(this.btn_nonblocking_Click);
            // 
            // Entry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_nonblocking);
            this.Controls.Add(this.btn_blocking);
            this.Name = "Entry";
            this.Text = "Entry Window";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_blocking;
        private System.Windows.Forms.Button btn_nonblocking;
    }
}

