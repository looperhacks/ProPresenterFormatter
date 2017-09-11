namespace ProPresenterFormatter
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.searchFileButton = new System.Windows.Forms.Button();
            this.filePathText = new System.Windows.Forms.TextBox();
            this.primitiveDisplay = new System.Windows.Forms.RichTextBox();
            this.slideBackButton = new System.Windows.Forms.Button();
            this.slideDescriptionBox = new System.Windows.Forms.TextBox();
            this.slideForwardButton = new System.Windows.Forms.Button();
            this.advancedDisplay = new System.Windows.Forms.Integration.ElementHost();
            this.saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // searchFileButton
            // 
            this.searchFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchFileButton.Location = new System.Drawing.Point(243, 11);
            this.searchFileButton.Name = "searchFileButton";
            this.searchFileButton.Size = new System.Drawing.Size(99, 23);
            this.searchFileButton.TabIndex = 0;
            this.searchFileButton.Text = "Durchsuchen";
            this.searchFileButton.UseVisualStyleBackColor = true;
            this.searchFileButton.Click += new System.EventHandler(this.OpenFile);
            // 
            // filePathText
            // 
            this.filePathText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filePathText.Location = new System.Drawing.Point(12, 11);
            this.filePathText.Name = "filePathText";
            this.filePathText.Size = new System.Drawing.Size(225, 20);
            this.filePathText.TabIndex = 1;
            // 
            // primitiveDisplay
            // 
            this.primitiveDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.primitiveDisplay.BackColor = System.Drawing.Color.Black;
            this.primitiveDisplay.Location = new System.Drawing.Point(12, 40);
            this.primitiveDisplay.Name = "primitiveDisplay";
            this.primitiveDisplay.Size = new System.Drawing.Size(200, 237);
            this.primitiveDisplay.TabIndex = 2;
            this.primitiveDisplay.Text = "";
            // 
            // slideBackButton
            // 
            this.slideBackButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.slideBackButton.Location = new System.Drawing.Point(12, 283);
            this.slideBackButton.Name = "slideBackButton";
            this.slideBackButton.Size = new System.Drawing.Size(30, 23);
            this.slideBackButton.TabIndex = 3;
            this.slideBackButton.Text = "<";
            this.slideBackButton.UseVisualStyleBackColor = true;
            this.slideBackButton.Click += new System.EventHandler(this.SlideBack);
            // 
            // slideDescriptionBox
            // 
            this.slideDescriptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.slideDescriptionBox.Location = new System.Drawing.Point(48, 286);
            this.slideDescriptionBox.Name = "slideDescriptionBox";
            this.slideDescriptionBox.ReadOnly = true;
            this.slideDescriptionBox.Size = new System.Drawing.Size(339, 20);
            this.slideDescriptionBox.TabIndex = 4;
            // 
            // slideForwardButton
            // 
            this.slideForwardButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.slideForwardButton.Location = new System.Drawing.Point(393, 283);
            this.slideForwardButton.Name = "slideForwardButton";
            this.slideForwardButton.Size = new System.Drawing.Size(30, 23);
            this.slideForwardButton.TabIndex = 5;
            this.slideForwardButton.Text = ">";
            this.slideForwardButton.UseVisualStyleBackColor = true;
            this.slideForwardButton.Click += new System.EventHandler(this.SlideForward);
            // 
            // advancedDisplay
            // 
            this.advancedDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.advancedDisplay.Location = new System.Drawing.Point(218, 40);
            this.advancedDisplay.Name = "advancedDisplay";
            this.advancedDisplay.Size = new System.Drawing.Size(200, 237);
            this.advancedDisplay.TabIndex = 6;
            this.advancedDisplay.Text = "elementHost1";
            this.advancedDisplay.Child = null;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(348, 11);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 7;
            this.saveButton.Text = "Speichern";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 318);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.advancedDisplay);
            this.Controls.Add(this.slideForwardButton);
            this.Controls.Add(this.slideDescriptionBox);
            this.Controls.Add(this.slideBackButton);
            this.Controls.Add(this.primitiveDisplay);
            this.Controls.Add(this.filePathText);
            this.Controls.Add(this.searchFileButton);
            this.Name = "MainForm";
            this.Text = "ProPresenterFormatter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button searchFileButton;
        private System.Windows.Forms.TextBox filePathText;
        private System.Windows.Forms.RichTextBox primitiveDisplay;
        private System.Windows.Forms.Button slideBackButton;
        private System.Windows.Forms.TextBox slideDescriptionBox;
        private System.Windows.Forms.Button slideForwardButton;
        private System.Windows.Forms.Integration.ElementHost advancedDisplay;
        private System.Windows.Forms.Button saveButton;
    }
}

