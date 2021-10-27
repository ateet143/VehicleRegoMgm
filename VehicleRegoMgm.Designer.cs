
namespace VehicleRegoMgm
{
    partial class VehicleRegoMgm
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
            this.label1 = new System.Windows.Forms.Label();
            this.TextBoxInput = new System.Windows.Forms.TextBox();
            this.ButtonBinarySearch = new System.Windows.Forms.Button();
            this.ButtonLinearSearch = new System.Windows.Forms.Button();
            this.ButtonEnter = new System.Windows.Forms.Button();
            this.ButtonEdit = new System.Windows.Forms.Button();
            this.ButtonExit = new System.Windows.Forms.Button();
            this.ButtonTag = new System.Windows.Forms.Button();
            this.ButtonOpen = new System.Windows.Forms.Button();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.ButtonReset = new System.Windows.Forms.Button();
            this.ListBoxVehicle = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bell MT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(225, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Vehicle Plate Details";
            // 
            // TextBoxInput
            // 
            this.TextBoxInput.Location = new System.Drawing.Point(235, 45);
            this.TextBoxInput.Multiline = true;
            this.TextBoxInput.Name = "TextBoxInput";
            this.TextBoxInput.Size = new System.Drawing.Size(130, 34);
            this.TextBoxInput.TabIndex = 2;
            this.TextBoxInput.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxInput_Validating);
            // 
            // ButtonBinarySearch
            // 
            this.ButtonBinarySearch.BackColor = System.Drawing.Color.White;
            this.ButtonBinarySearch.Location = new System.Drawing.Point(179, 90);
            this.ButtonBinarySearch.Name = "ButtonBinarySearch";
            this.ButtonBinarySearch.Size = new System.Drawing.Size(113, 40);
            this.ButtonBinarySearch.TabIndex = 3;
            this.ButtonBinarySearch.Text = "Binary Search";
            this.ButtonBinarySearch.UseVisualStyleBackColor = false;
            this.ButtonBinarySearch.Click += new System.EventHandler(this.ButtonBinarySearch_Click);
            // 
            // ButtonLinearSearch
            // 
            this.ButtonLinearSearch.BackColor = System.Drawing.Color.White;
            this.ButtonLinearSearch.Location = new System.Drawing.Point(315, 91);
            this.ButtonLinearSearch.Name = "ButtonLinearSearch";
            this.ButtonLinearSearch.Size = new System.Drawing.Size(113, 40);
            this.ButtonLinearSearch.TabIndex = 4;
            this.ButtonLinearSearch.Text = "Linear Search";
            this.ButtonLinearSearch.UseVisualStyleBackColor = false;
            this.ButtonLinearSearch.Click += new System.EventHandler(this.ButtonLinearSearch_Click);
            // 
            // ButtonEnter
            // 
            this.ButtonEnter.BackColor = System.Drawing.Color.White;
            this.ButtonEnter.Location = new System.Drawing.Point(252, 142);
            this.ButtonEnter.Name = "ButtonEnter";
            this.ButtonEnter.Size = new System.Drawing.Size(113, 40);
            this.ButtonEnter.TabIndex = 5;
            this.ButtonEnter.Text = "Enter";
            this.ButtonEnter.UseVisualStyleBackColor = false;
            this.ButtonEnter.Click += new System.EventHandler(this.ButtonEnter_Click);
            // 
            // ButtonEdit
            // 
            this.ButtonEdit.BackColor = System.Drawing.Color.White;
            this.ButtonEdit.Location = new System.Drawing.Point(252, 193);
            this.ButtonEdit.Name = "ButtonEdit";
            this.ButtonEdit.Size = new System.Drawing.Size(113, 40);
            this.ButtonEdit.TabIndex = 6;
            this.ButtonEdit.Text = "Edit";
            this.ButtonEdit.UseVisualStyleBackColor = false;
            this.ButtonEdit.Click += new System.EventHandler(this.ButtonEdit_Click);
            // 
            // ButtonExit
            // 
            this.ButtonExit.BackColor = System.Drawing.Color.White;
            this.ButtonExit.Location = new System.Drawing.Point(252, 249);
            this.ButtonExit.Name = "ButtonExit";
            this.ButtonExit.Size = new System.Drawing.Size(113, 40);
            this.ButtonExit.TabIndex = 7;
            this.ButtonExit.Text = "Exit";
            this.ButtonExit.UseVisualStyleBackColor = false;
            this.ButtonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            // 
            // ButtonTag
            // 
            this.ButtonTag.BackColor = System.Drawing.Color.White;
            this.ButtonTag.Location = new System.Drawing.Point(252, 311);
            this.ButtonTag.Name = "ButtonTag";
            this.ButtonTag.Size = new System.Drawing.Size(113, 40);
            this.ButtonTag.TabIndex = 8;
            this.ButtonTag.Text = "Tag";
            this.ButtonTag.UseVisualStyleBackColor = false;
            this.ButtonTag.Click += new System.EventHandler(this.ButtonTag_Click);
            // 
            // ButtonOpen
            // 
            this.ButtonOpen.BackColor = System.Drawing.Color.White;
            this.ButtonOpen.Location = new System.Drawing.Point(179, 383);
            this.ButtonOpen.Name = "ButtonOpen";
            this.ButtonOpen.Size = new System.Drawing.Size(113, 40);
            this.ButtonOpen.TabIndex = 9;
            this.ButtonOpen.Text = "Open";
            this.ButtonOpen.UseVisualStyleBackColor = false;
            // 
            // ButtonSave
            // 
            this.ButtonSave.BackColor = System.Drawing.Color.White;
            this.ButtonSave.Location = new System.Drawing.Point(330, 383);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(113, 40);
            this.ButtonSave.TabIndex = 10;
            this.ButtonSave.Text = "Save";
            this.ButtonSave.UseVisualStyleBackColor = false;
            // 
            // ButtonReset
            // 
            this.ButtonReset.BackColor = System.Drawing.Color.White;
            this.ButtonReset.Location = new System.Drawing.Point(22, 385);
            this.ButtonReset.Name = "ButtonReset";
            this.ButtonReset.Size = new System.Drawing.Size(140, 40);
            this.ButtonReset.TabIndex = 11;
            this.ButtonReset.Text = "Reset";
            this.ButtonReset.UseVisualStyleBackColor = false;
            this.ButtonReset.Click += new System.EventHandler(this.ButtonReset_Click);
            // 
            // ListBoxVehicle
            // 
            this.ListBoxVehicle.FormattingEnabled = true;
            this.ListBoxVehicle.Location = new System.Drawing.Point(22, 23);
            this.ListBoxVehicle.Name = "ListBoxVehicle";
            this.ListBoxVehicle.Size = new System.Drawing.Size(120, 342);
            this.ListBoxVehicle.TabIndex = 12;
            // 
            // VehicleRegoMgm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(499, 450);
            this.Controls.Add(this.ListBoxVehicle);
            this.Controls.Add(this.ButtonReset);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.ButtonOpen);
            this.Controls.Add(this.ButtonTag);
            this.Controls.Add(this.ButtonExit);
            this.Controls.Add(this.ButtonEdit);
            this.Controls.Add(this.ButtonEnter);
            this.Controls.Add(this.ButtonLinearSearch);
            this.Controls.Add(this.ButtonBinarySearch);
            this.Controls.Add(this.TextBoxInput);
            this.Controls.Add(this.label1);
            this.Name = "VehicleRegoMgm";
            this.Text = "VehicleRegoMgm";
            this.Load += new System.EventHandler(this.VehicleRegoMgm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBoxInput;
        private System.Windows.Forms.Button ButtonBinarySearch;
        private System.Windows.Forms.Button ButtonLinearSearch;
        private System.Windows.Forms.Button ButtonEnter;
        private System.Windows.Forms.Button ButtonEdit;
        private System.Windows.Forms.Button ButtonExit;
        private System.Windows.Forms.Button ButtonTag;
        private System.Windows.Forms.Button ButtonOpen;
        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.Button ButtonReset;
        private System.Windows.Forms.ListBox ListBoxVehicle;
    }
}

