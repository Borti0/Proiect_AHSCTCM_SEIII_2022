namespace ButtonConfigAPP
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
            this.ADD = new System.Windows.Forms.Button();
            this.DEL = new System.Windows.Forms.Button();
            this.Button_nr = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Mode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Page_select = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Actions = new System.Windows.Forms.ComboBox();
            this.Actions_label = new System.Windows.Forms.Label();
            this.Path_to_app_ = new System.Windows.Forms.TextBox();
            this.LabelPath = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Updates = new System.Windows.Forms.Label();
            this.Updates_label = new System.Windows.Forms.Label();
            this.Layout_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ADD
            // 
            this.ADD.Location = new System.Drawing.Point(12, 11);
            this.ADD.Name = "ADD";
            this.ADD.Size = new System.Drawing.Size(81, 34);
            this.ADD.TabIndex = 0;
            this.ADD.Text = "Add Button Action";
            this.ADD.UseVisualStyleBackColor = true;
            this.ADD.Click += new System.EventHandler(this.ADD_Click);
            // 
            // DEL
            // 
            this.DEL.Location = new System.Drawing.Point(14, 50);
            this.DEL.Name = "DEL";
            this.DEL.Size = new System.Drawing.Size(79, 34);
            this.DEL.TabIndex = 1;
            this.DEL.Text = "Del Button";
            this.DEL.UseVisualStyleBackColor = true;
            this.DEL.Click += new System.EventHandler(this.DEL_Click);
            // 
            // Button_nr
            // 
            this.Button_nr.FormattingEnabled = true;
            this.Button_nr.Items.AddRange(new object[] {
            "Button1",
            "Button2",
            "Button3",
            "Button4",
            "Button5",
            "Button6"});
            this.Button_nr.Location = new System.Drawing.Point(131, 32);
            this.Button_nr.Name = "Button_nr";
            this.Button_nr.Size = new System.Drawing.Size(81, 21);
            this.Button_nr.TabIndex = 2;
            this.Button_nr.SelectedIndexChanged += new System.EventHandler(this.Button_nr_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select Button:";
            // 
            // Mode
            // 
            this.Mode.FormattingEnabled = true;
            this.Mode.Items.AddRange(new object[] {
            "Launch App",
            "Action",
            "Sound Effect"});
            this.Mode.Location = new System.Drawing.Point(239, 32);
            this.Mode.Name = "Mode";
            this.Mode.Size = new System.Drawing.Size(81, 21);
            this.Mode.TabIndex = 4;
            this.Mode.SelectedIndexChanged += new System.EventHandler(this.Mode_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(236, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Select Mode:";
            // 
            // Page_select
            // 
            this.Page_select.FormattingEnabled = true;
            this.Page_select.Items.AddRange(new object[] {
            "Page1",
            "Page2",
            "Page3"});
            this.Page_select.Location = new System.Drawing.Point(337, 32);
            this.Page_select.Name = "Page_select";
            this.Page_select.Size = new System.Drawing.Size(86, 21);
            this.Page_select.TabIndex = 6;
            this.Page_select.SelectedIndexChanged += new System.EventHandler(this.Page_select_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(334, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Select Page:";
            // 
            // Actions
            // 
            this.Actions.FormattingEnabled = true;
            this.Actions.Items.AddRange(new object[] {
            "Volume up",
            "Volume down",
            "Volume mute",
            "Print Screen",
            "Open new text file",
            "Lock Screen"});
            this.Actions.Location = new System.Drawing.Point(131, 82);
            this.Actions.Name = "Actions";
            this.Actions.Size = new System.Drawing.Size(121, 21);
            this.Actions.TabIndex = 8;
            this.Actions.Visible = false;
            this.Actions.SelectedIndexChanged += new System.EventHandler(this.Actions_SelectedIndexChanged);
            // 
            // Actions_label
            // 
            this.Actions_label.AutoSize = true;
            this.Actions_label.Location = new System.Drawing.Point(128, 66);
            this.Actions_label.Name = "Actions_label";
            this.Actions_label.Size = new System.Drawing.Size(45, 13);
            this.Actions_label.TabIndex = 9;
            this.Actions_label.Text = "Actions:";
            this.Actions_label.Visible = false;
            // 
            // Path_to_app_
            // 
            this.Path_to_app_.Location = new System.Drawing.Point(131, 140);
            this.Path_to_app_.Name = "Path_to_app_";
            this.Path_to_app_.Size = new System.Drawing.Size(288, 20);
            this.Path_to_app_.TabIndex = 10;
            this.Path_to_app_.Visible = false;
            this.Path_to_app_.TextChanged += new System.EventHandler(this.Path_to_app__TextChanged);
            // 
            // LabelPath
            // 
            this.LabelPath.AutoSize = true;
            this.LabelPath.Location = new System.Drawing.Point(135, 124);
            this.LabelPath.Name = "LabelPath";
            this.LabelPath.Size = new System.Drawing.Size(139, 13);
            this.LabelPath.TabIndex = 11;
            this.LabelPath.Text = "Path of App or Sound efect:";
            this.LabelPath.Visible = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(12, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 33);
            this.button1.TabIndex = 12;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Updates
            // 
            this.Updates.AutoSize = true;
            this.Updates.Location = new System.Drawing.Point(271, 100);
            this.Updates.Name = "Updates";
            this.Updates.Size = new System.Drawing.Size(35, 13);
            this.Updates.TabIndex = 13;
            this.Updates.Text = "label4";
            this.Updates.Visible = false;
            // 
            // Updates_label
            // 
            this.Updates_label.AutoSize = true;
            this.Updates_label.Location = new System.Drawing.Point(271, 82);
            this.Updates_label.Name = "Updates_label";
            this.Updates_label.Size = new System.Drawing.Size(50, 13);
            this.Updates_label.TabIndex = 14;
            this.Updates_label.Text = "Updates:";
            // 
            // Layout_button
            // 
            this.Layout_button.Location = new System.Drawing.Point(12, 129);
            this.Layout_button.Name = "Layout_button";
            this.Layout_button.Size = new System.Drawing.Size(81, 33);
            this.Layout_button.TabIndex = 15;
            this.Layout_button.Text = "Layout";
            this.Layout_button.UseVisualStyleBackColor = true;
            this.Layout_button.Click += new System.EventHandler(this.Layout_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 187);
            this.Controls.Add(this.Layout_button);
            this.Controls.Add(this.Updates_label);
            this.Controls.Add(this.Updates);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LabelPath);
            this.Controls.Add(this.Path_to_app_);
            this.Controls.Add(this.Actions_label);
            this.Controls.Add(this.Actions);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Page_select);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Mode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Button_nr);
            this.Controls.Add(this.DEL);
            this.Controls.Add(this.ADD);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Macro_KeyPad";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ADD;
        private System.Windows.Forms.Button DEL;
        private System.Windows.Forms.ComboBox Button_nr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Mode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox Page_select;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Actions;
        private System.Windows.Forms.Label Actions_label;
        private System.Windows.Forms.TextBox Path_to_app_;
        private System.Windows.Forms.Label LabelPath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label Updates;
        private System.Windows.Forms.Label Updates_label;
        private System.Windows.Forms.Button Layout_button;
    }
}

