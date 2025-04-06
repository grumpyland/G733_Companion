namespace G733_Companion
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.applySettings = new System.Windows.Forms.Button();
            this.headset_connected_check = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_cycling = new System.Windows.Forms.RadioButton();
            this.radioButton_breathing = new System.Windows.Forms.RadioButton();
            this.radioButton_fixed = new System.Windows.Forms.RadioButton();
            this.radioButton_off = new System.Windows.Forms.RadioButton();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar_brightness = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton_down = new System.Windows.Forms.RadioButton();
            this.radioButton_top = new System.Windows.Forms.RadioButton();
            this.radioButton_both = new System.Windows.Forms.RadioButton();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.pickcolor = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.battery_level_update_timer = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.HID_read_timer = new System.Windows.Forms.Timer(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.timer_of_the_shame = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.customProgressBar1 = new G733_Companion.CustomProgressBar();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_brightness)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // applySettings
            // 
            this.applySettings.Location = new System.Drawing.Point(28, 397);
            this.applySettings.Name = "applySettings";
            this.applySettings.Size = new System.Drawing.Size(203, 24);
            this.applySettings.TabIndex = 0;
            this.applySettings.Text = "Apply light settings";
            this.applySettings.UseVisualStyleBackColor = true;
            this.applySettings.Click += new System.EventHandler(this.buttonApplySettings_Click);
            // 
            // headset_connected_check
            // 
            this.headset_connected_check.Enabled = true;
            this.headset_connected_check.Interval = 1000;
            this.headset_connected_check.Tick += new System.EventHandler(this.headset_connected_check_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_cycling);
            this.groupBox1.Controls.Add(this.radioButton_breathing);
            this.groupBox1.Controls.Add(this.radioButton_fixed);
            this.groupBox1.Controls.Add(this.radioButton_off);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(26, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(106, 118);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Light Mode";
            // 
            // radioButton_cycling
            // 
            this.radioButton_cycling.AutoSize = true;
            this.radioButton_cycling.Location = new System.Drawing.Point(18, 88);
            this.radioButton_cycling.Name = "radioButton_cycling";
            this.radioButton_cycling.Size = new System.Drawing.Size(58, 17);
            this.radioButton_cycling.TabIndex = 3;
            this.radioButton_cycling.TabStop = true;
            this.radioButton_cycling.Text = "Cycling";
            this.radioButton_cycling.UseVisualStyleBackColor = true;
            this.radioButton_cycling.CheckedChanged += new System.EventHandler(this.radioButton_cycling_CheckedChanged);
            // 
            // radioButton_breathing
            // 
            this.radioButton_breathing.AutoSize = true;
            this.radioButton_breathing.Location = new System.Drawing.Point(18, 65);
            this.radioButton_breathing.Name = "radioButton_breathing";
            this.radioButton_breathing.Size = new System.Drawing.Size(71, 17);
            this.radioButton_breathing.TabIndex = 2;
            this.radioButton_breathing.TabStop = true;
            this.radioButton_breathing.Text = "Breathing";
            this.radioButton_breathing.UseVisualStyleBackColor = true;
            this.radioButton_breathing.CheckedChanged += new System.EventHandler(this.radioButton_breathing_CheckedChanged);
            // 
            // radioButton_fixed
            // 
            this.radioButton_fixed.AutoSize = true;
            this.radioButton_fixed.Location = new System.Drawing.Point(18, 42);
            this.radioButton_fixed.Name = "radioButton_fixed";
            this.radioButton_fixed.Size = new System.Drawing.Size(50, 17);
            this.radioButton_fixed.TabIndex = 1;
            this.radioButton_fixed.TabStop = true;
            this.radioButton_fixed.Text = "Fixed";
            this.radioButton_fixed.UseVisualStyleBackColor = true;
            this.radioButton_fixed.CheckedChanged += new System.EventHandler(this.radioButton_fixed_CheckedChanged);
            // 
            // radioButton_off
            // 
            this.radioButton_off.AutoSize = true;
            this.radioButton_off.Location = new System.Drawing.Point(18, 20);
            this.radioButton_off.Name = "radioButton_off";
            this.radioButton_off.Size = new System.Drawing.Size(38, 17);
            this.radioButton_off.TabIndex = 0;
            this.radioButton_off.TabStop = true;
            this.radioButton_off.Text = "Off";
            this.radioButton_off.UseVisualStyleBackColor = true;
            this.radioButton_off.CheckedChanged += new System.EventHandler(this.radioButton_off_CheckedChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 100;
            this.trackBar1.Location = new System.Drawing.Point(28, 222);
            this.trackBar1.Maximum = 20000;
            this.trackBar1.Minimum = 1000;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(203, 45);
            this.trackBar1.SmallChange = 100;
            this.trackBar1.TabIndex = 2;
            this.trackBar1.TickFrequency = 100;
            this.trackBar1.Value = 5000;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(195, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // trackBar_brightness
            // 
            this.trackBar_brightness.Location = new System.Drawing.Point(26, 307);
            this.trackBar_brightness.Maximum = 100;
            this.trackBar_brightness.Name = "trackBar_brightness";
            this.trackBar_brightness.Size = new System.Drawing.Size(205, 45);
            this.trackBar_brightness.SmallChange = 5;
            this.trackBar_brightness.TabIndex = 4;
            this.trackBar_brightness.Value = 100;
            this.trackBar_brightness.Scroll += new System.EventHandler(this.trackBar_brightness_Scroll);
            this.trackBar_brightness.ValueChanged += new System.EventHandler(this.trackBar_brightness_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(195, 291);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "label2";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton_down);
            this.groupBox2.Controls.Add(this.radioButton_top);
            this.groupBox2.Controls.Add(this.radioButton_both);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(138, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(107, 118);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Light side to apply";
            // 
            // radioButton_down
            // 
            this.radioButton_down.AutoSize = true;
            this.radioButton_down.Location = new System.Drawing.Point(15, 66);
            this.radioButton_down.Name = "radioButton_down";
            this.radioButton_down.Size = new System.Drawing.Size(59, 17);
            this.radioButton_down.TabIndex = 2;
            this.radioButton_down.TabStop = true;
            this.radioButton_down.Text = "Bottom";
            this.radioButton_down.UseVisualStyleBackColor = true;
            this.radioButton_down.CheckedChanged += new System.EventHandler(this.radioButton_down_CheckedChanged);
            // 
            // radioButton_top
            // 
            this.radioButton_top.AutoSize = true;
            this.radioButton_top.Location = new System.Drawing.Point(15, 42);
            this.radioButton_top.Name = "radioButton_top";
            this.radioButton_top.Size = new System.Drawing.Size(41, 17);
            this.radioButton_top.TabIndex = 1;
            this.radioButton_top.TabStop = true;
            this.radioButton_top.Text = "Top";
            this.radioButton_top.UseVisualStyleBackColor = true;
            this.radioButton_top.CheckedChanged += new System.EventHandler(this.radioButton_top_CheckedChanged);
            // 
            // radioButton_both
            // 
            this.radioButton_both.AutoSize = true;
            this.radioButton_both.Location = new System.Drawing.Point(15, 20);
            this.radioButton_both.Name = "radioButton_both";
            this.radioButton_both.Size = new System.Drawing.Size(74, 17);
            this.radioButton_both.TabIndex = 0;
            this.radioButton_both.TabStop = true;
            this.radioButton_both.Text = "Both Sides";
            this.radioButton_both.UseVisualStyleBackColor = true;
            this.radioButton_both.CheckedChanged += new System.EventHandler(this.radioButton_both_CheckedChanged);
            // 
            // pickcolor
            // 
            this.pickcolor.Location = new System.Drawing.Point(28, 368);
            this.pickcolor.Name = "pickcolor";
            this.pickcolor.Size = new System.Drawing.Size(156, 23);
            this.pickcolor.TabIndex = 7;
            this.pickcolor.Text = "Pick a color";
            this.pickcolor.UseVisualStyleBackColor = true;
            this.pickcolor.Click += new System.EventHandler(this.buttonPickcolor_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(190, 368);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(41, 23);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label3.Location = new System.Drawing.Point(682, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Sidetone";
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(685, 74);
            this.trackBar2.Maximum = 100;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(203, 45);
            this.trackBar2.TabIndex = 1;
            this.trackBar2.Value = 100;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            this.trackBar2.ValueChanged += new System.EventHandler(this.trackBar2_ValueChanged);
            this.trackBar2.MouseCaptureChanged += new System.EventHandler(this.trackBar2_MouseCaptureChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label4.Location = new System.Drawing.Point(28, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 18);
            this.label4.TabIndex = 11;
            this.label4.Text = "Light Effects";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(28, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "Duration";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(852, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "label6";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(28, 291);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 15);
            this.label7.TabIndex = 14;
            this.label7.Text = "Brightness";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label8.Location = new System.Drawing.Point(682, 436);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 15);
            this.label8.TabIndex = 15;
            this.label8.Text = "Battery";
            // 
            // battery_level_update_timer
            // 
            this.battery_level_update_timer.Interval = 60000;
            this.battery_level_update_timer.Tick += new System.EventHandler(this.battery_level_update_timer_Tick);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(685, 518);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(203, 23);
            this.progressBar1.TabIndex = 16;
            this.progressBar1.Value = 70;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(682, 491);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "100%";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label10.Location = new System.Drawing.Point(811, 491);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Approx X hours";
            // 
            // HID_read_timer
            // 
            this.HID_read_timer.Interval = 1000;
            this.HID_read_timer.Tick += new System.EventHandler(this.HID_read_timer_Tick);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(846, 436);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "label11";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(-1, 560);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(915, 23);
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            // 
            // timer_of_the_shame
            // 
            this.timer_of_the_shame.Tick += new System.EventHandler(this.timer_of_the_shame_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.ForeColor = System.Drawing.Color.Black;
            this.checkBox1.Location = new System.Drawing.Point(791, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(109, 17);
            this.checkBox1.TabIndex = 23;
            this.checkBox1.Text = "launch on startup";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // customProgressBar1
            // 
            this.customProgressBar1.Location = new System.Drawing.Point(685, 465);
            this.customProgressBar1.Name = "customProgressBar1";
            this.customProgressBar1.Size = new System.Drawing.Size(203, 23);
            this.customProgressBar1.TabIndex = 22;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(912, 593);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.customProgressBar1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pickcolor);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackBar_brightness);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.applySettings);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "G733 Companion";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_brightness)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button applySettings;
        private System.Windows.Forms.Timer headset_connected_check;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_breathing;
        private System.Windows.Forms.RadioButton radioButton_fixed;
        private System.Windows.Forms.RadioButton radioButton_off;
        private System.Windows.Forms.RadioButton radioButton_cycling;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar_brightness;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton_down;
        private System.Windows.Forms.RadioButton radioButton_top;
        private System.Windows.Forms.RadioButton radioButton_both;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button pickcolor;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Timer battery_level_update_timer;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Timer HID_read_timer;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Timer timer_of_the_shame;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private CustomProgressBar customProgressBar1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

