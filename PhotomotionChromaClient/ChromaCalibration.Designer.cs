namespace PhotomotionChromaClient
{
    partial class ChromaCalibration
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.numericUpDownHue = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownHueTolerance = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownSaturation = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownValueTolerance = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownValue = new System.Windows.Forms.NumericUpDown();
            this.buttonSaveGeneral = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxCameraId = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.panelColor = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownSaturationTolerance = new System.Windows.Forms.NumericUpDown();
            this.trackBarHue = new System.Windows.Forms.TrackBar();
            this.trackBarHueTolerance = new System.Windows.Forms.TrackBar();
            this.trackBarSaturation = new System.Windows.Forms.TrackBar();
            this.trackBarValueTolerance = new System.Windows.Forms.TrackBar();
            this.trackBarValue = new System.Windows.Forms.TrackBar();
            this.trackBarSaturationTolerance = new System.Windows.Forms.TrackBar();
            this.labelImagePath = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHueTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSaturation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValueTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSaturationTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHueTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSaturation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarValueTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSaturationTolerance)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox.Location = new System.Drawing.Point(93, 24);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(450, 300);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseClick);
            // 
            // numericUpDownHue
            // 
            this.numericUpDownHue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDownHue.Location = new System.Drawing.Point(119, 334);
            this.numericUpDownHue.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDownHue.Name = "numericUpDownHue";
            this.numericUpDownHue.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownHue.TabIndex = 1;
            this.numericUpDownHue.ValueChanged += new System.EventHandler(this.numericUpDownValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 336);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Hue";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 365);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Hue Tolerance";
            // 
            // numericUpDownHueTolerance
            // 
            this.numericUpDownHueTolerance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDownHueTolerance.Location = new System.Drawing.Point(119, 363);
            this.numericUpDownHueTolerance.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDownHueTolerance.Name = "numericUpDownHueTolerance";
            this.numericUpDownHueTolerance.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownHueTolerance.TabIndex = 3;
            this.numericUpDownHueTolerance.ValueChanged += new System.EventHandler(this.numericUpDownValueChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(58, 396);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Saturation";
            // 
            // numericUpDownSaturation
            // 
            this.numericUpDownSaturation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDownSaturation.Location = new System.Drawing.Point(119, 392);
            this.numericUpDownSaturation.Name = "numericUpDownSaturation";
            this.numericUpDownSaturation.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownSaturation.TabIndex = 9;
            this.numericUpDownSaturation.ValueChanged += new System.EventHandler(this.numericUpDownValueChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 484);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Value Tolerance";
            // 
            // numericUpDownValueTolerance
            // 
            this.numericUpDownValueTolerance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDownValueTolerance.Location = new System.Drawing.Point(119, 481);
            this.numericUpDownValueTolerance.Name = "numericUpDownValueTolerance";
            this.numericUpDownValueTolerance.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownValueTolerance.TabIndex = 11;
            this.numericUpDownValueTolerance.ValueChanged += new System.EventHandler(this.numericUpDownValueChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(79, 454);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Value";
            // 
            // numericUpDownValue
            // 
            this.numericUpDownValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDownValue.Location = new System.Drawing.Point(119, 450);
            this.numericUpDownValue.Name = "numericUpDownValue";
            this.numericUpDownValue.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownValue.TabIndex = 13;
            this.numericUpDownValue.ValueChanged += new System.EventHandler(this.numericUpDownValueChanged);
            // 
            // buttonSaveGeneral
            // 
            this.buttonSaveGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSaveGeneral.Location = new System.Drawing.Point(480, 541);
            this.buttonSaveGeneral.Name = "buttonSaveGeneral";
            this.buttonSaveGeneral.Size = new System.Drawing.Size(97, 23);
            this.buttonSaveGeneral.TabIndex = 18;
            this.buttonSaveGeneral.Text = "Save General";
            this.buttonSaveGeneral.UseVisualStyleBackColor = true;
            this.buttonSaveGeneral.Click += new System.EventHandler(this.buttonSaveGeneral_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOpen.Location = new System.Drawing.Point(292, 541);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(89, 23);
            this.buttonOpen.TabIndex = 19;
            this.buttonOpen.Text = "Open Picture";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 546);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Camera Id:";
            // 
            // textBoxCameraId
            // 
            this.textBoxCameraId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxCameraId.Location = new System.Drawing.Point(85, 544);
            this.textBoxCameraId.Name = "textBoxCameraId";
            this.textBoxCameraId.Size = new System.Drawing.Size(68, 20);
            this.textBoxCameraId.TabIndex = 21;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSave.Location = new System.Drawing.Point(393, 541);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 22;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonLoad.Location = new System.Drawing.Point(166, 541);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(113, 23);
            this.buttonLoad.TabIndex = 23;
            this.buttonLoad.Text = "Load Camera Config";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // panelColor
            // 
            this.panelColor.Location = new System.Drawing.Point(11, 259);
            this.panelColor.Name = "panelColor";
            this.panelColor.Size = new System.Drawing.Size(67, 65);
            this.panelColor.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 425);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Saturation Tolerance";
            // 
            // numericUpDownSaturationTolerance
            // 
            this.numericUpDownSaturationTolerance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDownSaturationTolerance.Location = new System.Drawing.Point(119, 421);
            this.numericUpDownSaturationTolerance.Name = "numericUpDownSaturationTolerance";
            this.numericUpDownSaturationTolerance.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownSaturationTolerance.TabIndex = 26;
            // 
            // trackBarHue
            // 
            this.trackBarHue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackBarHue.Location = new System.Drawing.Point(175, 333);
            this.trackBarHue.Maximum = 360;
            this.trackBarHue.Name = "trackBarHue";
            this.trackBarHue.Size = new System.Drawing.Size(411, 45);
            this.trackBarHue.TabIndex = 28;
            this.trackBarHue.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // trackBarHueTolerance
            // 
            this.trackBarHueTolerance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackBarHueTolerance.Location = new System.Drawing.Point(175, 362);
            this.trackBarHueTolerance.Maximum = 360;
            this.trackBarHueTolerance.Name = "trackBarHueTolerance";
            this.trackBarHueTolerance.Size = new System.Drawing.Size(411, 45);
            this.trackBarHueTolerance.TabIndex = 29;
            this.trackBarHueTolerance.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // trackBarSaturation
            // 
            this.trackBarSaturation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackBarSaturation.Location = new System.Drawing.Point(175, 392);
            this.trackBarSaturation.Maximum = 100;
            this.trackBarSaturation.Name = "trackBarSaturation";
            this.trackBarSaturation.Size = new System.Drawing.Size(411, 45);
            this.trackBarSaturation.TabIndex = 30;
            this.trackBarSaturation.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // trackBarValueTolerance
            // 
            this.trackBarValueTolerance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackBarValueTolerance.Location = new System.Drawing.Point(175, 480);
            this.trackBarValueTolerance.Maximum = 100;
            this.trackBarValueTolerance.Name = "trackBarValueTolerance";
            this.trackBarValueTolerance.Size = new System.Drawing.Size(411, 45);
            this.trackBarValueTolerance.TabIndex = 33;
            this.trackBarValueTolerance.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // trackBarValue
            // 
            this.trackBarValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackBarValue.Location = new System.Drawing.Point(175, 450);
            this.trackBarValue.Maximum = 100;
            this.trackBarValue.Name = "trackBarValue";
            this.trackBarValue.Size = new System.Drawing.Size(411, 45);
            this.trackBarValue.TabIndex = 32;
            this.trackBarValue.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // trackBarSaturationTolerance
            // 
            this.trackBarSaturationTolerance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackBarSaturationTolerance.Location = new System.Drawing.Point(175, 421);
            this.trackBarSaturationTolerance.Maximum = 100;
            this.trackBarSaturationTolerance.Name = "trackBarSaturationTolerance";
            this.trackBarSaturationTolerance.Size = new System.Drawing.Size(411, 45);
            this.trackBarSaturationTolerance.TabIndex = 31;
            this.trackBarSaturationTolerance.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // labelImagePath
            // 
            this.labelImagePath.AutoSize = true;
            this.labelImagePath.Location = new System.Drawing.Point(28, 9);
            this.labelImagePath.Name = "labelImagePath";
            this.labelImagePath.Size = new System.Drawing.Size(0, 13);
            this.labelImagePath.TabIndex = 34;
            // 
            // ChromaCalibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 596);
            this.Controls.Add(this.labelImagePath);
            this.Controls.Add(this.trackBarValueTolerance);
            this.Controls.Add(this.trackBarValue);
            this.Controls.Add(this.trackBarSaturationTolerance);
            this.Controls.Add(this.trackBarSaturation);
            this.Controls.Add(this.trackBarHueTolerance);
            this.Controls.Add(this.trackBarHue);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numericUpDownSaturationTolerance);
            this.Controls.Add(this.panelColor);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxCameraId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.buttonSaveGeneral);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownValueTolerance);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDownSaturation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownHueTolerance);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownHue);
            this.Controls.Add(this.pictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ChromaCalibration";
            this.Text = "ChromaCalibration";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChromaCalibration_FormClosed);
            this.Load += new System.EventHandler(this.ChromaCalibration_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHueTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSaturation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValueTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSaturationTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHueTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSaturation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarValueTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSaturationTolerance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.NumericUpDown numericUpDownHue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownHueTolerance;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownSaturation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownValueTolerance;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownValue;
        private System.Windows.Forms.Button buttonSaveGeneral;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxCameraId;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Panel panelColor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownSaturationTolerance;
        private System.Windows.Forms.TrackBar trackBarHue;
        private System.Windows.Forms.TrackBar trackBarHueTolerance;
        private System.Windows.Forms.TrackBar trackBarSaturation;
        private System.Windows.Forms.TrackBar trackBarValueTolerance;
        private System.Windows.Forms.TrackBar trackBarValue;
        private System.Windows.Forms.TrackBar trackBarSaturationTolerance;
        private System.Windows.Forms.Label labelImagePath;
    }
}