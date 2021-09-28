namespace GIS_ex_210910 {
    partial class RTK {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
                }
            base.Dispose(disposing);
            }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( ) {
            this.components = new System.ComponentModel.Container();
            this.combx_port = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_serialConn = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.richTextBox_received = new System.Windows.Forms.RichTextBox();
            this.lb_ntrip = new System.Windows.Forms.Label();
            this.btn_ntripConn = new System.Windows.Forms.Button();
            this.btn_serialDisconn = new System.Windows.Forms.Button();
            this.btn_ntripDisconn = new System.Windows.Forms.Button();
            this.btn_ntripStart = new System.Windows.Forms.Button();
            this.btn_ntripStop = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.txt_checkPoint = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // combx_port
            // 
            this.combx_port.FormattingEnabled = true;
            this.combx_port.Location = new System.Drawing.Point(81, 9);
            this.combx_port.Name = "combx_port";
            this.combx_port.Size = new System.Drawing.Size(121, 20);
            this.combx_port.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Serial Port";
            // 
            // btn_serialConn
            // 
            this.btn_serialConn.Location = new System.Drawing.Point(226, 7);
            this.btn_serialConn.Name = "btn_serialConn";
            this.btn_serialConn.Size = new System.Drawing.Size(75, 23);
            this.btn_serialConn.TabIndex = 2;
            this.btn_serialConn.Text = "connect";
            this.btn_serialConn.UseVisualStyleBackColor = true;
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // richTextBox_received
            // 
            this.richTextBox_received.Location = new System.Drawing.Point(12, 81);
            this.richTextBox_received.Name = "richTextBox_received";
            this.richTextBox_received.ReadOnly = true;
            this.richTextBox_received.Size = new System.Drawing.Size(776, 335);
            this.richTextBox_received.TabIndex = 3;
            this.richTextBox_received.Text = "";
            // 
            // lb_ntrip
            // 
            this.lb_ntrip.AutoSize = true;
            this.lb_ntrip.Location = new System.Drawing.Point(12, 43);
            this.lb_ntrip.Name = "lb_ntrip";
            this.lb_ntrip.Size = new System.Drawing.Size(41, 12);
            this.lb_ntrip.TabIndex = 4;
            this.lb_ntrip.Text = "NTRIP";
            // 
            // btn_ntripConn
            // 
            this.btn_ntripConn.Location = new System.Drawing.Point(413, 43);
            this.btn_ntripConn.Name = "btn_ntripConn";
            this.btn_ntripConn.Size = new System.Drawing.Size(75, 23);
            this.btn_ntripConn.TabIndex = 5;
            this.btn_ntripConn.Text = "connect";
            this.btn_ntripConn.UseVisualStyleBackColor = true;
            this.btn_ntripConn.Click += new System.EventHandler(this.btn_ntripConn_Click);
            // 
            // btn_serialDisconn
            // 
            this.btn_serialDisconn.Location = new System.Drawing.Point(307, 6);
            this.btn_serialDisconn.Name = "btn_serialDisconn";
            this.btn_serialDisconn.Size = new System.Drawing.Size(75, 23);
            this.btn_serialDisconn.TabIndex = 8;
            this.btn_serialDisconn.Text = "disconnect";
            this.btn_serialDisconn.UseVisualStyleBackColor = true;
            this.btn_serialDisconn.Click += new System.EventHandler(this.btn_serialDisconn_Click);
            // 
            // btn_ntripDisconn
            // 
            this.btn_ntripDisconn.Location = new System.Drawing.Point(494, 43);
            this.btn_ntripDisconn.Name = "btn_ntripDisconn";
            this.btn_ntripDisconn.Size = new System.Drawing.Size(75, 23);
            this.btn_ntripDisconn.TabIndex = 9;
            this.btn_ntripDisconn.Text = "disconnect";
            this.btn_ntripDisconn.UseVisualStyleBackColor = true;
            this.btn_ntripDisconn.Click += new System.EventHandler(this.btn_ntripDisconn_Click);
            // 
            // btn_ntripStart
            // 
            this.btn_ntripStart.Location = new System.Drawing.Point(608, 12);
            this.btn_ntripStart.Name = "btn_ntripStart";
            this.btn_ntripStart.Size = new System.Drawing.Size(87, 54);
            this.btn_ntripStart.TabIndex = 10;
            this.btn_ntripStart.Text = "Ntrip Start";
            this.btn_ntripStart.UseVisualStyleBackColor = true;
            this.btn_ntripStart.Click += new System.EventHandler(this.btn_ntripStart_Click);
            // 
            // btn_ntripStop
            // 
            this.btn_ntripStop.Location = new System.Drawing.Point(701, 12);
            this.btn_ntripStop.Name = "btn_ntripStop";
            this.btn_ntripStop.Size = new System.Drawing.Size(87, 54);
            this.btn_ntripStop.TabIndex = 11;
            this.btn_ntripStop.Text = "Ntrip Stop";
            this.btn_ntripStop.UseVisualStyleBackColor = true;
            this.btn_ntripStop.Click += new System.EventHandler(this.btn_ntripStop_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(713, 422);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(75, 23);
            this.btn_clear.TabIndex = 12;
            this.btn_clear.Text = "clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // txt_checkPoint
            // 
            this.txt_checkPoint.Location = new System.Drawing.Point(12, 424);
            this.txt_checkPoint.Multiline = true;
            this.txt_checkPoint.Name = "txt_checkPoint";
            this.txt_checkPoint.ReadOnly = true;
            this.txt_checkPoint.Size = new System.Drawing.Size(683, 31);
            this.txt_checkPoint.TabIndex = 13;
            // 
            // RTK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 467);
            this.Controls.Add(this.txt_checkPoint);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.btn_ntripStop);
            this.Controls.Add(this.btn_ntripStart);
            this.Controls.Add(this.btn_ntripDisconn);
            this.Controls.Add(this.btn_serialDisconn);
            this.Controls.Add(this.btn_ntripConn);
            this.Controls.Add(this.lb_ntrip);
            this.Controls.Add(this.richTextBox_received);
            this.Controls.Add(this.btn_serialConn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combx_port);
            this.Name = "RTK";
            this.Text = "RTK";
            this.Load += new System.EventHandler(this.RTK_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

            }

        #endregion
        private System.Windows.Forms.ComboBox combx_port;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_serialConn;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.RichTextBox richTextBox_received;
        private System.Windows.Forms.Label lb_ntrip;
        private System.Windows.Forms.Button btn_ntripConn;
        private System.Windows.Forms.Button btn_serialDisconn;
        private System.Windows.Forms.Button btn_ntripDisconn;
        private System.Windows.Forms.Button btn_ntripStart;
        private System.Windows.Forms.Button btn_ntripStop;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.TextBox txt_checkPoint;
        }
    }