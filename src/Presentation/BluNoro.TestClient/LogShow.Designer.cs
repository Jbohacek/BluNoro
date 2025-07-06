namespace BluChat.TestClient
{
    partial class LogShow
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
            btn_ok = new Button();
            txt_text = new TextBox();
            SuspendLayout();
            // 
            // btn_ok
            // 
            btn_ok.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_ok.Location = new Point(325, 753);
            btn_ok.Margin = new Padding(5, 6, 5, 6);
            btn_ok.Name = "btn_ok";
            btn_ok.Size = new Size(129, 46);
            btn_ok.TabIndex = 0;
            btn_ok.Text = "OK";
            btn_ok.UseVisualStyleBackColor = true;
            btn_ok.Click += btn_ok_Click;
            // 
            // txt_text
            // 
            txt_text.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txt_text.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            txt_text.Location = new Point(12, 12);
            txt_text.Multiline = true;
            txt_text.Name = "txt_text";
            txt_text.ReadOnly = true;
            txt_text.Size = new Size(789, 732);
            txt_text.TabIndex = 1;
            // 
            // LogShow
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(813, 814);
            Controls.Add(txt_text);
            Controls.Add(btn_ok);
            Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            Margin = new Padding(5, 6, 5, 6);
            Name = "LogShow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LogShow";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_ok;
        private TextBox txt_text;
    }
}