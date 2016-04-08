namespace Search3DModel
{
    partial class SearchInfoForm
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
            this.paramListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // paramListBox
            // 
            this.paramListBox.FormattingEnabled = true;
            this.paramListBox.HorizontalScrollbar = true;
            this.paramListBox.Location = new System.Drawing.Point(12, 12);
            this.paramListBox.Name = "paramListBox";
            this.paramListBox.Size = new System.Drawing.Size(304, 199);
            this.paramListBox.TabIndex = 0;
            // 
            // SearchInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 226);
            this.Controls.Add(this.paramListBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchInfoForm";
            this.Text = "Search Information";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox paramListBox;
    }
}