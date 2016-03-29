namespace Search3DModel
{
    partial class SearchForm
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
            this.components = new System.ComponentModel.Container();
            this.xLabel = new System.Windows.Forms.Label();
            this.xTextBox = new System.Windows.Forms.TextBox();
            this.yLabel = new System.Windows.Forms.Label();
            this.yTextBox = new System.Windows.Forms.TextBox();
            this.zlabel = new System.Windows.Forms.Label();
            this.zTextBox = new System.Windows.Forms.TextBox();
            this.deviationLabel = new System.Windows.Forms.Label();
            this.searchButton = new System.Windows.Forms.Button();
            this.deviationBox = new System.Windows.Forms.NumericUpDown();
            this.percentLabel = new System.Windows.Forms.Label();
            this.searchGridView = new System.Windows.Forms.DataGridView();
            this.openButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.hint = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.deviationBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // xLabel
            // 
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(16, 18);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(87, 13);
            this.xLabel.TabIndex = 0;
            this.xLabel.Text = "Enter X (Length):";
            // 
            // xTextBox
            // 
            this.xTextBox.Location = new System.Drawing.Point(19, 34);
            this.xTextBox.Name = "xTextBox";
            this.xTextBox.Size = new System.Drawing.Size(80, 20);
            this.xTextBox.TabIndex = 1;
            this.xTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericOnly);
            this.xTextBox.Leave += new System.EventHandler(this.checkFormat);
            // 
            // yLabel
            // 
            this.yLabel.AutoSize = true;
            this.yLabel.Location = new System.Drawing.Point(17, 62);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(82, 13);
            this.yLabel.TabIndex = 2;
            this.yLabel.Text = "Enter Y (Width):";
            // 
            // yTextBox
            // 
            this.yTextBox.Location = new System.Drawing.Point(19, 78);
            this.yTextBox.Name = "yTextBox";
            this.yTextBox.Size = new System.Drawing.Size(80, 20);
            this.yTextBox.TabIndex = 3;
            this.yTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericOnly);
            this.yTextBox.Leave += new System.EventHandler(this.checkFormat);
            // 
            // zlabel
            // 
            this.zlabel.AutoSize = true;
            this.zlabel.Location = new System.Drawing.Point(17, 108);
            this.zlabel.Name = "zlabel";
            this.zlabel.Size = new System.Drawing.Size(85, 13);
            this.zlabel.TabIndex = 4;
            this.zlabel.Text = "Enter Z (Height):";
            // 
            // zTextBox
            // 
            this.zTextBox.Location = new System.Drawing.Point(19, 124);
            this.zTextBox.Name = "zTextBox";
            this.zTextBox.Size = new System.Drawing.Size(80, 20);
            this.zTextBox.TabIndex = 5;
            this.zTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericOnly);
            this.zTextBox.Leave += new System.EventHandler(this.checkFormat);
            // 
            // deviationLabel
            // 
            this.deviationLabel.AutoSize = true;
            this.deviationLabel.Location = new System.Drawing.Point(17, 167);
            this.deviationLabel.Name = "deviationLabel";
            this.deviationLabel.Size = new System.Drawing.Size(55, 13);
            this.deviationLabel.TabIndex = 6;
            this.deviationLabel.Text = "Deviation:";
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(19, 221);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 8;
            this.searchButton.Tag = "";
            this.searchButton.Text = "Search";
            this.hint.SetToolTip(this.searchButton, "Search model in database");
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // deviationBox
            // 
            this.deviationBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.deviationBox.Location = new System.Drawing.Point(19, 183);
            this.deviationBox.Name = "deviationBox";
            this.deviationBox.Size = new System.Drawing.Size(53, 20);
            this.deviationBox.TabIndex = 9;
            this.deviationBox.Leave += new System.EventHandler(this.deviationBox_Leave);
            // 
            // percentLabel
            // 
            this.percentLabel.AutoSize = true;
            this.percentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.percentLabel.Location = new System.Drawing.Point(74, 184);
            this.percentLabel.Name = "percentLabel";
            this.percentLabel.Size = new System.Drawing.Size(20, 16);
            this.percentLabel.TabIndex = 10;
            this.percentLabel.Text = "%";
            // 
            // searchGridView
            // 
            this.searchGridView.AllowUserToAddRows = false;
            this.searchGridView.AllowUserToDeleteRows = false;
            this.searchGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.searchGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.searchGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.searchGridView.Location = new System.Drawing.Point(115, 13);
            this.searchGridView.Name = "searchGridView";
            this.searchGridView.ReadOnly = true;
            this.searchGridView.Size = new System.Drawing.Size(542, 306);
            this.searchGridView.TabIndex = 11;
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(501, 325);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 23);
            this.openButton.TabIndex = 12;
            this.openButton.Tag = "";
            this.openButton.Text = "Open";
            this.hint.SetToolTip(this.openButton, "Open selected model");
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(582, 325);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 13;
            this.closeButton.Tag = "";
            this.closeButton.Text = "Close";
            this.hint.SetToolTip(this.closeButton, "Closing search window");
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // hint
            // 
            this.hint.AutoPopDelay = 5000;
            this.hint.InitialDelay = 500;
            this.hint.ReshowDelay = 100;
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 360);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.searchGridView);
            this.Controls.Add(this.percentLabel);
            this.Controls.Add(this.deviationBox);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.deviationLabel);
            this.Controls.Add(this.zTextBox);
            this.Controls.Add(this.zlabel);
            this.Controls.Add(this.yTextBox);
            this.Controls.Add(this.yLabel);
            this.Controls.Add(this.xTextBox);
            this.Controls.Add(this.xLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchForm";
            this.Text = "Search";
            ((System.ComponentModel.ISupportInitialize)(this.deviationBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.TextBox xTextBox;
        private System.Windows.Forms.Label yLabel;
        private System.Windows.Forms.TextBox yTextBox;
        private System.Windows.Forms.Label zlabel;
        private System.Windows.Forms.TextBox zTextBox;
        private System.Windows.Forms.Label deviationLabel;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.NumericUpDown deviationBox;
        private System.Windows.Forms.Label percentLabel;
        private System.Windows.Forms.DataGridView searchGridView;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.ToolTip hint;
    }
}