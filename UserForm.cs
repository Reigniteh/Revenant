using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Phasmophobia_Save_Editor
{
  public class UserForm : Form
  {
    private Dictionary<string, string> Saves;
    public string SelectedUsername;
    public string SelectedPath;
    private IContainer components;
    private Label label1;
    private ComboBox usernamesBox;
    private Button cancelBtn;
    private Button okBtn;

    public UserForm() => this.InitializeComponent();

    private void UserForm_Load(object sender, EventArgs e)
    {
      this.Saves = Utils.EnumerateSaves();
      foreach (KeyValuePair<string, string> save in this.Saves)
        this.usernamesBox.Items.Add((object) save.Key);
      this.usernamesBox.SelectedIndex = 0;
    }

    private void usernamesBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(((ComboBox) sender).SelectedText))
        return;
      this.SelectedUsername = ((ComboBox) sender).SelectedText;
      this.SelectedPath = this.Saves[this.SelectedUsername];
    }

    private void cancelBtn_Click(object sender, EventArgs e) => this.DialogResult = DialogResult.Cancel;

    private void okBtn_Click(object sender, EventArgs e) => this.DialogResult = DialogResult.OK;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserForm));
            this.label1 = new System.Windows.Forms.Label();
            this.usernamesBox = new System.Windows.Forms.ComboBox();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(80, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Windows user:";
            // 
            // usernamesBox
            // 
            this.usernamesBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.usernamesBox.FormattingEnabled = true;
            this.usernamesBox.Location = new System.Drawing.Point(12, 30);
            this.usernamesBox.Name = "usernamesBox";
            this.usernamesBox.Size = new System.Drawing.Size(293, 21);
            this.usernamesBox.TabIndex = 1;
            this.usernamesBox.SelectedIndexChanged += new System.EventHandler(this.usernamesBox_SelectedIndexChanged);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(12, 60);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(146, 23);
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(164, 60);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(141, 23);
            this.okBtn.TabIndex = 3;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 95);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.usernamesBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Revenant Phasmo Tool @Reigniteh";
            this.Load += new System.EventHandler(this.UserForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }
  }
}
