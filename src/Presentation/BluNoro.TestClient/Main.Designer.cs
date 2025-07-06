namespace BluNoro.TestClient
{
    partial class Main
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            txt_IpAdress = new TextBox();
            label1 = new Label();
            txt_port = new TextBox();
            label2 = new Label();
            btn_start = new Button();
            dgw_log = new DataGridView();
            context = new DataGridViewTextBoxColumn();
            Time = new DataGridViewTextBoxColumn();
            btn_dissconnect = new Button();
            gr_Connection = new GroupBox();
            gr_Authentication = new GroupBox();
            btn_authenticate = new Button();
            label5 = new Label();
            txt_output = new TextBox();
            txt_Password = new TextBox();
            txt_Username = new TextBox();
            label4 = new Label();
            label3 = new Label();
            box_chats = new ListBox();
            gr_chats_messages = new GroupBox();
            btn_reload = new Button();
            btn_send = new Button();
            txt_userInputMessage = new TextBox();
            box_messages = new ListBox();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgw_log).BeginInit();
            gr_Connection.SuspendLayout();
            gr_Authentication.SuspendLayout();
            gr_chats_messages.SuspendLayout();
            SuspendLayout();
            // 
            // txt_IpAdress
            // 
            txt_IpAdress.Location = new Point(8, 67);
            txt_IpAdress.Margin = new Padding(5, 6, 5, 6);
            txt_IpAdress.Name = "txt_IpAdress";
            txt_IpAdress.Size = new Size(126, 35);
            txt_IpAdress.TabIndex = 0;
            txt_IpAdress.TabStop = false;
            txt_IpAdress.Text = "127.0.0.1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 40);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(93, 30);
            label1.TabIndex = 1;
            label1.Text = "IpAdress";
            // 
            // txt_port
            // 
            txt_port.Location = new Point(155, 67);
            txt_port.Margin = new Padding(5, 6, 5, 6);
            txt_port.Name = "txt_port";
            txt_port.Size = new Size(118, 35);
            txt_port.TabIndex = 2;
            txt_port.TabStop = false;
            txt_port.Text = "9000";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(137, 70);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(18, 30);
            label2.TabIndex = 3;
            label2.Text = ":";
            // 
            // btn_start
            // 
            btn_start.Location = new Point(8, 111);
            btn_start.Name = "btn_start";
            btn_start.Size = new Size(119, 44);
            btn_start.TabIndex = 0;
            btn_start.Text = "Connect";
            btn_start.UseVisualStyleBackColor = true;
            btn_start.Click += btn_start_Click;
            // 
            // dgw_log
            // 
            dgw_log.AllowUserToAddRows = false;
            dgw_log.AllowUserToDeleteRows = false;
            dgw_log.AllowUserToOrderColumns = true;
            dgw_log.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgw_log.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgw_log.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgw_log.Columns.AddRange(new DataGridViewColumn[] { context, Time });
            dgw_log.Location = new Point(991, 52);
            dgw_log.Name = "dgw_log";
            dgw_log.ReadOnly = true;
            dgw_log.Size = new Size(490, 818);
            dgw_log.TabIndex = 5;
            dgw_log.CellDoubleClick += dgw_log_CellDoubleClick;
            // 
            // context
            // 
            context.DataPropertyName = "context";
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            context.DefaultCellStyle = dataGridViewCellStyle1;
            context.FillWeight = 6F;
            context.HeaderText = "Message";
            context.Name = "context";
            context.ReadOnly = true;
            // 
            // Time
            // 
            Time.DataPropertyName = "Time";
            Time.FillWeight = 3F;
            Time.HeaderText = "Time of send";
            Time.Name = "Time";
            Time.ReadOnly = true;
            // 
            // btn_dissconnect
            // 
            btn_dissconnect.Enabled = false;
            btn_dissconnect.Location = new Point(137, 111);
            btn_dissconnect.Name = "btn_dissconnect";
            btn_dissconnect.Size = new Size(138, 44);
            btn_dissconnect.TabIndex = 7;
            btn_dissconnect.TabStop = false;
            btn_dissconnect.Text = "Disconnect";
            btn_dissconnect.UseVisualStyleBackColor = true;
            btn_dissconnect.Click += btn_dissconnect_Click;
            // 
            // gr_Connection
            // 
            gr_Connection.Controls.Add(label1);
            gr_Connection.Controls.Add(btn_dissconnect);
            gr_Connection.Controls.Add(txt_IpAdress);
            gr_Connection.Controls.Add(txt_port);
            gr_Connection.Controls.Add(label2);
            gr_Connection.Controls.Add(btn_start);
            gr_Connection.Location = new Point(12, 12);
            gr_Connection.Name = "gr_Connection";
            gr_Connection.Size = new Size(281, 168);
            gr_Connection.TabIndex = 8;
            gr_Connection.TabStop = false;
            gr_Connection.Text = "Connection";
            // 
            // gr_Authentication
            // 
            gr_Authentication.Controls.Add(btn_authenticate);
            gr_Authentication.Controls.Add(label5);
            gr_Authentication.Controls.Add(txt_output);
            gr_Authentication.Controls.Add(txt_Password);
            gr_Authentication.Controls.Add(txt_Username);
            gr_Authentication.Controls.Add(label4);
            gr_Authentication.Controls.Add(label3);
            gr_Authentication.Location = new Point(12, 186);
            gr_Authentication.Name = "gr_Authentication";
            gr_Authentication.Size = new Size(494, 243);
            gr_Authentication.TabIndex = 9;
            gr_Authentication.TabStop = false;
            gr_Authentication.Text = "Authentication";
            gr_Authentication.Visible = false;
            // 
            // btn_authenticate
            // 
            btn_authenticate.Location = new Point(6, 187);
            btn_authenticate.Name = "btn_authenticate";
            btn_authenticate.Size = new Size(106, 46);
            btn_authenticate.TabIndex = 3;
            btn_authenticate.Text = "Send";
            btn_authenticate.UseVisualStyleBackColor = true;
            btn_authenticate.Click += btn_authenticate_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(279, 39);
            label5.Name = "label5";
            label5.Size = new Size(84, 30);
            label5.TabIndex = 13;
            label5.Text = "Output:";
            // 
            // txt_output
            // 
            txt_output.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            txt_output.Location = new Point(279, 72);
            txt_output.Multiline = true;
            txt_output.Name = "txt_output";
            txt_output.ReadOnly = true;
            txt_output.Size = new Size(209, 161);
            txt_output.TabIndex = 12;
            txt_output.TabStop = false;
            // 
            // txt_Password
            // 
            txt_Password.Location = new Point(8, 146);
            txt_Password.Name = "txt_Password";
            txt_Password.Size = new Size(265, 35);
            txt_Password.TabIndex = 2;
            // 
            // txt_Username
            // 
            txt_Username.Location = new Point(8, 72);
            txt_Username.Name = "txt_Username";
            txt_Username.Size = new Size(265, 35);
            txt_Username.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(8, 113);
            label4.Name = "label4";
            label4.Size = new Size(99, 30);
            label4.TabIndex = 11;
            label4.Text = "Password";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 42);
            label3.Name = "label3";
            label3.Size = new Size(106, 30);
            label3.TabIndex = 10;
            label3.Text = "Username";
            // 
            // box_chats
            // 
            box_chats.FormattingEnabled = true;
            box_chats.ItemHeight = 30;
            box_chats.Location = new Point(8, 34);
            box_chats.Name = "box_chats";
            box_chats.Size = new Size(155, 244);
            box_chats.TabIndex = 10;
            box_chats.SelectedValueChanged += box_chats_SelectedValueChanged;
            // 
            // gr_chats_messages
            // 
            gr_chats_messages.Controls.Add(btn_reload);
            gr_chats_messages.Controls.Add(btn_send);
            gr_chats_messages.Controls.Add(txt_userInputMessage);
            gr_chats_messages.Controls.Add(box_messages);
            gr_chats_messages.Controls.Add(box_chats);
            gr_chats_messages.Location = new Point(12, 447);
            gr_chats_messages.Name = "gr_chats_messages";
            gr_chats_messages.Size = new Size(973, 347);
            gr_chats_messages.TabIndex = 11;
            gr_chats_messages.TabStop = false;
            gr_chats_messages.Text = "Chats";
            gr_chats_messages.Visible = false;
            // 
            // btn_reload
            // 
            btn_reload.Location = new Point(8, 299);
            btn_reload.Name = "btn_reload";
            btn_reload.Size = new Size(155, 38);
            btn_reload.TabIndex = 13;
            btn_reload.Text = "Reload";
            btn_reload.UseVisualStyleBackColor = true;
            btn_reload.Click += btn_reload_Click;
            // 
            // btn_send
            // 
            btn_send.Enabled = false;
            btn_send.Location = new Point(400, 284);
            btn_send.Name = "btn_send";
            btn_send.Size = new Size(88, 54);
            btn_send.TabIndex = 14;
            btn_send.Text = "Send";
            btn_send.UseVisualStyleBackColor = true;
            btn_send.Click += btn_send_Click;
            // 
            // txt_userInputMessage
            // 
            txt_userInputMessage.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            txt_userInputMessage.Location = new Point(169, 284);
            txt_userInputMessage.Multiline = true;
            txt_userInputMessage.Name = "txt_userInputMessage";
            txt_userInputMessage.PlaceholderText = "Message";
            txt_userInputMessage.Size = new Size(225, 53);
            txt_userInputMessage.TabIndex = 13;
            // 
            // box_messages
            // 
            box_messages.FormattingEnabled = true;
            box_messages.ItemHeight = 30;
            box_messages.Location = new Point(169, 34);
            box_messages.Name = "box_messages";
            box_messages.Size = new Size(798, 244);
            box_messages.TabIndex = 12;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(991, 19);
            label6.Name = "label6";
            label6.Size = new Size(152, 30);
            label6.TabIndex = 12;
            label6.Text = "Debug window";
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1493, 882);
            Controls.Add(label6);
            Controls.Add(gr_chats_messages);
            Controls.Add(gr_Authentication);
            Controls.Add(gr_Connection);
            Controls.Add(dgw_log);
            Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            Margin = new Padding(5, 6, 5, 6);
            Name = "Main";
            Text = "Main";
            Load += Main_Load;
            ((System.ComponentModel.ISupportInitialize)dgw_log).EndInit();
            gr_Connection.ResumeLayout(false);
            gr_Connection.PerformLayout();
            gr_Authentication.ResumeLayout(false);
            gr_Authentication.PerformLayout();
            gr_chats_messages.ResumeLayout(false);
            gr_chats_messages.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txt_IpAdress;
        private Label label1;
        private TextBox txt_port;
        private Label label2;
        private Button btn_start;
        private DataGridView dgw_log;
        private Button btn_dissconnect;
        private GroupBox gr_Connection;
        private GroupBox gr_Authentication;
        private TextBox txt_Password;
        private TextBox txt_Username;
        private Label label4;
        private Label label3;
        private Button btn_authenticate;
        private Label label5;
        private TextBox txt_output;
        private ListBox box_chats;
        private GroupBox gr_chats_messages;
        private Button btn_send;
        private TextBox txt_userInputMessage;
        private ListBox box_messages;
        private Label label6;
        private Button btn_reload;
        private DataGridViewTextBoxColumn context;
        private DataGridViewTextBoxColumn Time;
    }
}
