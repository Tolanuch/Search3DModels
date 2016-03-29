namespace Search3DModel
{
    partial class AddFromFolderForm
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
            this.folderListBox = new System.Windows.Forms.ListBox();
            this.modelsListBox = new System.Windows.Forms.ListBox();
            this.addModelsButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.addFolderButton = new System.Windows.Forms.Button();
            this.showModelsButton = new System.Windows.Forms.Button();
            this.outputListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // folderListBox
            // 
            this.folderListBox.FormattingEnabled = true;
            this.folderListBox.HorizontalScrollbar = true;
            this.folderListBox.Location = new System.Drawing.Point(12, 12);
            this.folderListBox.Name = "folderListBox";
            this.folderListBox.Size = new System.Drawing.Size(238, 134);
            this.folderListBox.TabIndex = 0;
            this.folderListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.folderListBox_KeyDown);
            // 
            // modelsListBox
            // 
            this.modelsListBox.FormattingEnabled = true;
            this.modelsListBox.HorizontalScrollbar = true;
            this.modelsListBox.Location = new System.Drawing.Point(316, 12);
            this.modelsListBox.Name = "modelsListBox";
            this.modelsListBox.Size = new System.Drawing.Size(402, 303);
            this.modelsListBox.TabIndex = 1;
            this.modelsListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.modelsListBox_KeyDown);
            // 
            // addModelsButton
            // 
            this.addModelsButton.Location = new System.Drawing.Point(517, 326);
            this.addModelsButton.Name = "addModelsButton";
            this.addModelsButton.Size = new System.Drawing.Size(99, 31);
            this.addModelsButton.TabIndex = 2;
            this.addModelsButton.Text = "Add models";
            this.addModelsButton.UseVisualStyleBackColor = true;
            this.addModelsButton.Click += new System.EventHandler(this.addModelsButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(643, 326);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 31);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // addFolderButton
            // 
            this.addFolderButton.Location = new System.Drawing.Point(12, 157);
            this.addFolderButton.Name = "addFolderButton";
            this.addFolderButton.Size = new System.Drawing.Size(105, 31);
            this.addFolderButton.TabIndex = 4;
            this.addFolderButton.Text = "Add folder";
            this.addFolderButton.UseVisualStyleBackColor = true;
            this.addFolderButton.Click += new System.EventHandler(this.addFolderButton_Click);
            // 
            // showModelsButton
            // 
            this.showModelsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.showModelsButton.Location = new System.Drawing.Point(256, 61);
            this.showModelsButton.Name = "showModelsButton";
            this.showModelsButton.Size = new System.Drawing.Size(54, 30);
            this.showModelsButton.TabIndex = 5;
            this.showModelsButton.Tag = "Show models in folder(s)";
            this.showModelsButton.Text = " >>";
            this.showModelsButton.UseVisualStyleBackColor = true;
            this.showModelsButton.Click += new System.EventHandler(this.showModelsButton_Click);
            // 
            // outputListBox
            // 
            this.outputListBox.BackColor = System.Drawing.SystemColors.Control;
            this.outputListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.outputListBox.FormattingEnabled = true;
            this.outputListBox.Items.AddRange(new object[] {
            "Output:"});
            this.outputListBox.Location = new System.Drawing.Point(12, 210);
            this.outputListBox.Name = "outputListBox";
            this.outputListBox.Size = new System.Drawing.Size(298, 143);
            this.outputListBox.TabIndex = 6;
            // 
            // AddFromFolderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 369);
            this.Controls.Add(this.outputListBox);
            this.Controls.Add(this.showModelsButton);
            this.Controls.Add(this.addFolderButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addModelsButton);
            this.Controls.Add(this.modelsListBox);
            this.Controls.Add(this.folderListBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddFromFolderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add from folder";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox folderListBox;
        private System.Windows.Forms.ListBox modelsListBox;
        private System.Windows.Forms.Button addModelsButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button addFolderButton;
        private System.Windows.Forms.Button showModelsButton;
        private System.Windows.Forms.ListBox outputListBox;
    }
}