namespace FesterMapEditor
{
    partial class StartupDialog
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnBrowse1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLevelBackground = new System.Windows.Forms.TextBox();
            this.txtLevelHeight = new System.Windows.Forms.TextBox();
            this.txtLevelWidth = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBrowse2 = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.txtLoad = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCreate);
            this.groupBox1.Controls.Add(this.btnBrowse1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtLevelBackground);
            this.groupBox1.Controls.Add(this.txtLevelHeight);
            this.groupBox1.Controls.Add(this.txtLevelWidth);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnCreate
            // 
            this.btnCreate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCreate.Location = new System.Drawing.Point(305, 13);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(67, 31);
            this.btnCreate.TabIndex = 7;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnBrowse1
            // 
            this.btnBrowse1.Location = new System.Drawing.Point(305, 66);
            this.btnBrowse1.Name = "btnBrowse1";
            this.btnBrowse1.Size = new System.Drawing.Size(56, 23);
            this.btnBrowse1.TabIndex = 6;
            this.btnBrowse1.Text = "Browse...";
            this.btnBrowse1.UseVisualStyleBackColor = true;
            this.btnBrowse1.Click += new System.EventHandler(this.btnBrowse1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Background Image:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Level Height:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Level Width:";
            // 
            // txtLevelBackground
            // 
            this.txtLevelBackground.Location = new System.Drawing.Point(164, 67);
            this.txtLevelBackground.Name = "txtLevelBackground";
            this.txtLevelBackground.Size = new System.Drawing.Size(135, 20);
            this.txtLevelBackground.TabIndex = 2;
            // 
            // txtLevelHeight
            // 
            this.txtLevelHeight.Location = new System.Drawing.Point(164, 41);
            this.txtLevelHeight.Name = "txtLevelHeight";
            this.txtLevelHeight.Size = new System.Drawing.Size(135, 20);
            this.txtLevelHeight.TabIndex = 1;
            this.txtLevelHeight.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // txtLevelWidth
            // 
            this.txtLevelWidth.Location = new System.Drawing.Point(164, 15);
            this.txtLevelWidth.Name = "txtLevelWidth";
            this.txtLevelWidth.Size = new System.Drawing.Size(135, 20);
            this.txtLevelWidth.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnBrowse2);
            this.groupBox2.Controls.Add(this.btnLoad);
            this.groupBox2.Controls.Add(this.txtLoad);
            this.groupBox2.Location = new System.Drawing.Point(12, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(378, 72);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Load";
            // 
            // btnBrowse2
            // 
            this.btnBrowse2.Location = new System.Drawing.Point(217, 16);
            this.btnBrowse2.Name = "btnBrowse2";
            this.btnBrowse2.Size = new System.Drawing.Size(56, 23);
            this.btnBrowse2.TabIndex = 7;
            this.btnBrowse2.Text = "Browse...";
            this.btnBrowse2.UseVisualStyleBackColor = true;
            this.btnBrowse2.Click += new System.EventHandler(this.btnBrowse2_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnLoad.Location = new System.Drawing.Point(235, 43);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(137, 23);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txtLoad
            // 
            this.txtLoad.Location = new System.Drawing.Point(17, 19);
            this.txtLoad.Name = "txtLoad";
            this.txtLoad.Size = new System.Drawing.Size(194, 20);
            this.txtLoad.TabIndex = 0;
            // 
            // StartupDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 203);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "StartupDialog";
            this.Text = "StartupDialog";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtLevelBackground;
        private System.Windows.Forms.TextBox txtLevelHeight;
        private System.Windows.Forms.TextBox txtLevelWidth;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnBrowse1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowse2;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TextBox txtLoad;
        private System.Windows.Forms.Button btnCreate;
    }
}