namespace BluChat.TestClient
{
    partial class NumberOfWindows
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
            label1 = new Label();
            nmb_number = new NumericUpDown();
            btn_start = new Button();
            ((System.ComponentModel.ISupportInitialize)nmb_number).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 18);
            label1.Name = "label1";
            label1.Size = new Size(135, 30);
            label1.TabIndex = 0;
            label1.Text = "Kolik Klientu?";
            // 
            // nmb_number
            // 
            nmb_number.Location = new Point(12, 60);
            nmb_number.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nmb_number.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nmb_number.Name = "nmb_number";
            nmb_number.Size = new Size(228, 35);
            nmb_number.TabIndex = 2;
            nmb_number.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btn_start
            // 
            btn_start.Location = new Point(12, 111);
            btn_start.Name = "btn_start";
            btn_start.Size = new Size(228, 55);
            btn_start.TabIndex = 1;
            btn_start.Text = "Start";
            btn_start.UseVisualStyleBackColor = true;
            btn_start.Click += btn_start_Click;
            // 
            // NumberOfWindows
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(296, 204);
            Controls.Add(btn_start);
            Controls.Add(nmb_number);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            Margin = new Padding(5, 6, 5, 6);
            Name = "NumberOfWindows";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "NumberOfWindows";
            ((System.ComponentModel.ISupportInitialize)nmb_number).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private NumericUpDown nmb_number;
        private Button btn_start;
    }
}