using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace Phasmophobia_Save_Editor
{
  public class MainForm : Form
  {
    private Dictionary<string, SaveGenericProperty> sf;
    private static string SavePath;
    private static bool ExperienceSet = false;
    private static Dictionary<string, string> NameLookupTable = new Dictionary<string, string>()
    {
      {
        "moneyNumUpDown",
        "PlayersMoney"
      },
      {
        "expNumUpDown",
        "Level"
      },
      {
        "emfNumUpDown",
        "EMFReaderInventory"
      },
      {
        "flashlightNumUpDown",
        "FlashlightInventory"
      },
      {
        "cameraNumUpDown",
        "CameraInventory"
      },
      {
        "lighterNumUpDown",
        "LighterInventory"
      },
      {
        "candleNumUpDown",
        "CandleInventory"
      },
      {
        "uvFlashlightNumUpDown",
        "UVFlashlightInventory"
      },
      {
        "crucifixNumUpDown",
        "CrucifixInventory"
      },
      {
        "dslrCameraNumUpDown",
        "DSLRCameraInventory"
      },
      {
        "evpRecorderNumUpDown",
        "EVPRecorderInventory"
      },
      {
        "saltNumUpDown",
        "SaltInventory"
      },
      {
        "sageNumUpDown",
        "SageInventory"
      },
      {
        "tripodNumUpDown",
        "TripodInventory"
      },
      {
        "strongFlashlightNumUpDown",
        "StrongFlashlightInventory"
      },
      {
        "motionSensorNumUpDown",
        "MotionSensorInventory"
      },
      {
        "soundSensorNumUpDown",
        "SoundSensorInventory"
      },
      {
        "sanityPillsNumUpDown",
        "SanityPillsInventory"
      },
      {
        "thermometerNumUpDown",
        "ThermometerInventory"
      },
      {
        "ghostWritingBookNumUpDown",
        "GhostWritingBookInventory"
      },
      {
        "dotsProjectorNumUpDown",
        "DOTSProjectorInventory"
      },
      {
        "parabolicMicNumUpDown",
        "ParabolicMicrophoneInventory"
      },
      {
        "glowstickNumUpDown",
        "GlowstickInventory"
      },
      {
        "goProNumUpDown",
        "HeadMountedCameraInventory"
      }
    };
        private IContainer components;
    private NumericUpDown expNumUpDown;
    private NumericUpDown moneyNumUpDown;
    private ComboBox ghostTypeDropDown;
    private Label label1;
    private Label label2;
    private Label label3;
    private Button saveBtn;
    private GroupBox invGroupBox;
    private NumericUpDown emfNumUpDown;
    private Label label4;
    private NumericUpDown flashlightNumUpDown;
    private Label label5;
    private NumericUpDown cameraNumUpDown;
    private Label label6;
    private NumericUpDown lighterNumUpDown;
    private Label label7;
    private NumericUpDown candleNumUpDown;
    private Label label8;
    private NumericUpDown uvFlashlightNumUpDown;
    private Label label9;
    private NumericUpDown crucifixNumUpDown;
    private Label label10;
    private NumericUpDown dslrCameraNumUpDown;
    private Label label11;
    private NumericUpDown evpRecorderNumUpDown;
    private Label label12;
    private NumericUpDown saltNumUpDown;
    private Label label13;
    private NumericUpDown sageNumUpDown;
    private Label label14;
    private NumericUpDown tripodNumUpDown;
    private Label label15;
    private NumericUpDown strongFlashlightNumUpDown;
    private Label label16;
    private NumericUpDown motionSensorNumUpDown;
    private Label label17;
    private NumericUpDown soundSensorNumUpDown;
    private Label label18;
    private NumericUpDown sanityPillsNumUpDown;
    private Label label19;
    private NumericUpDown thermometerNumUpDown;
    private Label label20;
    private NumericUpDown ghostWritingBookNumUpDown;
    private Label label21;
    private NumericUpDown dotsProjectorNumUpDown;
    private Label label22;
    private NumericUpDown parabolicMicNumUpDown;
    private Label label23;
    private NumericUpDown goProNumUpDown;
    private Label label25;
    private NumericUpDown glowstickNumUpDown;
    private Label label24;
    private Button batchBtn;
    private Label label26;
    private NumericUpDown levelNumUpDown;
    private NumericUpDown batchNumUpDown;
    private Label evidenceLabel1;
    private Label evidenceLabel2;
    private Label evidenceLabel3;

    public MainForm() => this.InitializeComponent();

    private void MainForm_Load(object sender, EventArgs e)
    {
      Dictionary<string, string> dictionary = Utils.EnumerateSaves();
      if (dictionary.Count == 0)
      {
        int num = (int) MessageBox.Show("Couldn't find a Phasmophobia save file!\r\nPlay the game before you use this editor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        Application.Exit();
      }
      else
      {
        if (dictionary.Count == 1)
          MainForm.SavePath = dictionary.Values.First<string>();
        else if (dictionary.Count > 1)
        {
          UserForm userForm = new UserForm();
          switch (userForm.ShowDialog())
          {
            case DialogResult.OK:
              MainForm.SavePath = userForm.SelectedPath;
              break;
            case DialogResult.Cancel:
              Application.Exit();
              return;
          }
        }
        this.sf = JsonConvert.DeserializeObject<Dictionary<string, SaveGenericProperty>>(Encoding.UTF8.GetString(Crypto.DecryptSaveData(File.ReadAllBytes(MainForm.SavePath))));
        this.ghostTypeDropDown.SelectedIndex = this.ghostTypeDropDown.FindStringExact((string) this.sf["GhostType"].Value);
        this.EnumEvidence((string) this.sf["GhostType"].Value);
        this.EnumFields();
      }
    }

    private void EnumEvidence(string ghost)
    {
      string[] typesByGhostName = GameInfo.GetEvidenceTypesByGhostName(ghost);
      for (int index = 0; index < 3; ++index)
        this.Controls["evidenceLabel" + (index + 1).ToString()].Text = typesByGhostName[index];
    }

    private void EnumFields()
    {
      foreach (Control control in (ArrangedElementCollection) this.Controls)
      {
        if (control.GetType() == typeof (NumericUpDown))
        {
          NumericUpDown numericUpDown = (NumericUpDown) control;
          if (!(numericUpDown.Name == "levelNumUpDown") && !(numericUpDown.Name == "batchNumUpDown"))
            numericUpDown.Value = (Decimal) Convert.ToInt32(this.sf[MainForm.NameLookupTable[numericUpDown.Name]].Value);
        }
      }
      foreach (Control control in (ArrangedElementCollection) this.invGroupBox.Controls)
      {
        if (control.GetType() == typeof (NumericUpDown))
        {
          NumericUpDown numericUpDown = (NumericUpDown) control;
          numericUpDown.Value = (Decimal) Convert.ToInt32(this.sf[MainForm.NameLookupTable[numericUpDown.Name]].Value);
        }
      }
    }

    private void NumUpDown_ValueChanged(object sender, EventArgs e)
    {
      NumericUpDown numericUpDown = (NumericUpDown) sender;
      string key;
      if (numericUpDown.Name == "expNumUpDown")
      {
        key = "Level";
        this.levelNumUpDown.Value = Math.Floor(numericUpDown.Value / 100M);
        MainForm.ExperienceSet = true;
      }
      else
      {
        if (numericUpDown.Name == "levelNumUpDown")
        {
          if (!MainForm.ExperienceSet)
            return;
          this.expNumUpDown.Value = numericUpDown.Value * 100M;
          return;
        }
        key = MainForm.NameLookupTable[numericUpDown.Name];
      }
      this.sf[key].Value = (object) (int) numericUpDown.Value;
    }

    private void ghostTypeDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
      ComboBox comboBox = (ComboBox) sender;
      if (string.IsNullOrEmpty(comboBox.Text))
        return;
      this.sf["GhostType"].Value = (object) comboBox.Text;
      this.EnumEvidence(comboBox.Text);
    }

    private void saveBtn_Click(object sender, EventArgs e)
    {
      byte[] bytes = Crypto.EncryptSaveData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject((object) this.sf, Formatting.Indented)));
      File.WriteAllBytes(MainForm.SavePath, bytes);
      int num = (int) MessageBox.Show("Successfully saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void maxBtn_Click(object sender, EventArgs e)
    {
      foreach (Control control in (ArrangedElementCollection) this.invGroupBox.Controls)
      {
        if (control.GetType() == typeof (NumericUpDown))
        {
          NumericUpDown sender1 = (NumericUpDown) control;
          sender1.Value = this.batchNumUpDown.Value;
          this.NumUpDown_ValueChanged((object) sender1, (EventArgs) null);
        }
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.expNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.moneyNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.ghostTypeDropDown = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.saveBtn = new System.Windows.Forms.Button();
            this.invGroupBox = new System.Windows.Forms.GroupBox();
            this.goProNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label25 = new System.Windows.Forms.Label();
            this.glowstickNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label24 = new System.Windows.Forms.Label();
            this.parabolicMicNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label23 = new System.Windows.Forms.Label();
            this.dotsProjectorNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label22 = new System.Windows.Forms.Label();
            this.ghostWritingBookNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            this.thermometerNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.sanityPillsNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.soundSensorNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.motionSensorNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.strongFlashlightNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.tripodNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.sageNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.saltNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.evpRecorderNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.dslrCameraNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.crucifixNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.uvFlashlightNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.candleNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.lighterNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.cameraNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.flashlightNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.emfNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.batchBtn = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.levelNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.batchNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.evidenceLabel1 = new System.Windows.Forms.Label();
            this.evidenceLabel2 = new System.Windows.Forms.Label();
            this.evidenceLabel3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.expNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moneyNumUpDown)).BeginInit();
            this.invGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.goProNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glowstickNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.parabolicMicNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dotsProjectorNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ghostWritingBookNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thermometerNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sanityPillsNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.soundSensorNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.motionSensorNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.strongFlashlightNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tripodNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sageNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saltNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.evpRecorderNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dslrCameraNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.crucifixNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uvFlashlightNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.candleNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lighterNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cameraNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flashlightNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emfNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.levelNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.batchNumUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // expNumUpDown
            // 
            this.expNumUpDown.Location = new System.Drawing.Point(84, 40);
            this.expNumUpDown.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.expNumUpDown.Name = "expNumUpDown";
            this.expNumUpDown.Size = new System.Drawing.Size(272, 20);
            this.expNumUpDown.TabIndex = 2;
            this.expNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // moneyNumUpDown
            // 
            this.moneyNumUpDown.Location = new System.Drawing.Point(84, 14);
            this.moneyNumUpDown.Maximum = new decimal(new int[] {
            250000,
            0,
            0,
            0});
            this.moneyNumUpDown.Name = "moneyNumUpDown";
            this.moneyNumUpDown.Size = new System.Drawing.Size(272, 20);
            this.moneyNumUpDown.TabIndex = 1;
            this.moneyNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // ghostTypeDropDown
            // 
            this.ghostTypeDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ghostTypeDropDown.FormattingEnabled = true;
            this.ghostTypeDropDown.Items.AddRange(new object[] {
            "Banshee",
            "Demon",
            "Goryo",
            "Hantu",
            "Jinn",
            "Mare",
            "Moroi",
            "Myling",
            "Obake",
            "Oni",
            "Onryo",
            "Phantom",
            "Poltergeist",
            "Raiju",
            "Revenant",
            "Shade",
            "Spirit",
            "The Twins",
            "Wraith",
            "Yokai",
            "Yurei"});
            this.ghostTypeDropDown.Location = new System.Drawing.Point(84, 16);
            this.ghostTypeDropDown.Name = "ghostTypeDropDown";
            this.ghostTypeDropDown.Size = new System.Drawing.Size(119, 21);
            this.ghostTypeDropDown.TabIndex = 0;
            this.ghostTypeDropDown.Visible = false;
            this.ghostTypeDropDown.SelectedIndexChanged += new System.EventHandler(this.ghostTypeDropDown_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ghost:";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Money:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Level:";
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(17, 68);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(339, 23);
            this.saveBtn.TabIndex = 28;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // invGroupBox
            // 
            this.invGroupBox.Controls.Add(this.goProNumUpDown);
            this.invGroupBox.Controls.Add(this.label25);
            this.invGroupBox.Controls.Add(this.glowstickNumUpDown);
            this.invGroupBox.Controls.Add(this.label24);
            this.invGroupBox.Controls.Add(this.parabolicMicNumUpDown);
            this.invGroupBox.Controls.Add(this.label23);
            this.invGroupBox.Controls.Add(this.dotsProjectorNumUpDown);
            this.invGroupBox.Controls.Add(this.label22);
            this.invGroupBox.Controls.Add(this.ghostWritingBookNumUpDown);
            this.invGroupBox.Controls.Add(this.label21);
            this.invGroupBox.Controls.Add(this.thermometerNumUpDown);
            this.invGroupBox.Controls.Add(this.label20);
            this.invGroupBox.Controls.Add(this.sanityPillsNumUpDown);
            this.invGroupBox.Controls.Add(this.label19);
            this.invGroupBox.Controls.Add(this.soundSensorNumUpDown);
            this.invGroupBox.Controls.Add(this.label18);
            this.invGroupBox.Controls.Add(this.motionSensorNumUpDown);
            this.invGroupBox.Controls.Add(this.label17);
            this.invGroupBox.Controls.Add(this.strongFlashlightNumUpDown);
            this.invGroupBox.Controls.Add(this.label16);
            this.invGroupBox.Controls.Add(this.tripodNumUpDown);
            this.invGroupBox.Controls.Add(this.label15);
            this.invGroupBox.Controls.Add(this.sageNumUpDown);
            this.invGroupBox.Controls.Add(this.label14);
            this.invGroupBox.Controls.Add(this.saltNumUpDown);
            this.invGroupBox.Controls.Add(this.label13);
            this.invGroupBox.Controls.Add(this.evpRecorderNumUpDown);
            this.invGroupBox.Controls.Add(this.label12);
            this.invGroupBox.Controls.Add(this.dslrCameraNumUpDown);
            this.invGroupBox.Controls.Add(this.label11);
            this.invGroupBox.Controls.Add(this.crucifixNumUpDown);
            this.invGroupBox.Controls.Add(this.label10);
            this.invGroupBox.Controls.Add(this.uvFlashlightNumUpDown);
            this.invGroupBox.Controls.Add(this.label9);
            this.invGroupBox.Controls.Add(this.candleNumUpDown);
            this.invGroupBox.Controls.Add(this.label8);
            this.invGroupBox.Controls.Add(this.lighterNumUpDown);
            this.invGroupBox.Controls.Add(this.label7);
            this.invGroupBox.Controls.Add(this.cameraNumUpDown);
            this.invGroupBox.Controls.Add(this.label6);
            this.invGroupBox.Controls.Add(this.flashlightNumUpDown);
            this.invGroupBox.Controls.Add(this.label5);
            this.invGroupBox.Controls.Add(this.emfNumUpDown);
            this.invGroupBox.Controls.Add(this.label4);
            this.invGroupBox.Location = new System.Drawing.Point(17, 95);
            this.invGroupBox.Name = "invGroupBox";
            this.invGroupBox.Size = new System.Drawing.Size(339, 302);
            this.invGroupBox.TabIndex = 7;
            this.invGroupBox.TabStop = false;
            this.invGroupBox.Text = "Inventory";
            this.invGroupBox.Visible = false;
            // 
            // goProNumUpDown
            // 
            this.goProNumUpDown.Location = new System.Drawing.Point(264, 267);
            this.goProNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.goProNumUpDown.Name = "goProNumUpDown";
            this.goProNumUpDown.Size = new System.Drawing.Size(59, 20);
            this.goProNumUpDown.TabIndex = 25;
            this.goProNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(214, 269);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(44, 13);
            this.label25.TabIndex = 42;
            this.label25.Text = "GoPro\'s";
            // 
            // glowstickNumUpDown
            // 
            this.glowstickNumUpDown.Location = new System.Drawing.Point(264, 243);
            this.glowstickNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.glowstickNumUpDown.Name = "glowstickNumUpDown";
            this.glowstickNumUpDown.Size = new System.Drawing.Size(59, 20);
            this.glowstickNumUpDown.TabIndex = 24;
            this.glowstickNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(199, 245);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(58, 13);
            this.label24.TabIndex = 40;
            this.label24.Text = "Glowsticks";
            // 
            // parabolicMicNumUpDown
            // 
            this.parabolicMicNumUpDown.Location = new System.Drawing.Point(264, 218);
            this.parabolicMicNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.parabolicMicNumUpDown.Name = "parabolicMicNumUpDown";
            this.parabolicMicNumUpDown.Size = new System.Drawing.Size(59, 20);
            this.parabolicMicNumUpDown.TabIndex = 23;
            this.parabolicMicNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(182, 220);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(76, 13);
            this.label23.TabIndex = 38;
            this.label23.Text = "Parabolic Mics";
            // 
            // dotsProjectorNumUpDown
            // 
            this.dotsProjectorNumUpDown.Location = new System.Drawing.Point(264, 193);
            this.dotsProjectorNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.dotsProjectorNumUpDown.Name = "dotsProjectorNumUpDown";
            this.dotsProjectorNumUpDown.Size = new System.Drawing.Size(59, 20);
            this.dotsProjectorNumUpDown.TabIndex = 22;
            this.dotsProjectorNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(212, 196);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(46, 13);
            this.label22.TabIndex = 36;
            this.label22.Text = "D.O.T.S";
            // 
            // ghostWritingBookNumUpDown
            // 
            this.ghostWritingBookNumUpDown.Location = new System.Drawing.Point(264, 168);
            this.ghostWritingBookNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ghostWritingBookNumUpDown.Name = "ghostWritingBookNumUpDown";
            this.ghostWritingBookNumUpDown.Size = new System.Drawing.Size(59, 20);
            this.ghostWritingBookNumUpDown.TabIndex = 21;
            this.ghostWritingBookNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(185, 170);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(73, 13);
            this.label21.TabIndex = 34;
            this.label21.Text = "Writing Books";
            // 
            // thermometerNumUpDown
            // 
            this.thermometerNumUpDown.Location = new System.Drawing.Point(264, 143);
            this.thermometerNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.thermometerNumUpDown.Name = "thermometerNumUpDown";
            this.thermometerNumUpDown.Size = new System.Drawing.Size(59, 20);
            this.thermometerNumUpDown.TabIndex = 20;
            this.thermometerNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(184, 145);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(74, 13);
            this.label20.TabIndex = 32;
            this.label20.Text = "Thermometers";
            // 
            // sanityPillsNumUpDown
            // 
            this.sanityPillsNumUpDown.Location = new System.Drawing.Point(264, 118);
            this.sanityPillsNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.sanityPillsNumUpDown.Name = "sanityPillsNumUpDown";
            this.sanityPillsNumUpDown.Size = new System.Drawing.Size(59, 20);
            this.sanityPillsNumUpDown.TabIndex = 19;
            this.sanityPillsNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(201, 120);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(57, 13);
            this.label19.TabIndex = 30;
            this.label19.Text = "Sanity Pills";
            // 
            // soundSensorNumUpDown
            // 
            this.soundSensorNumUpDown.Location = new System.Drawing.Point(264, 93);
            this.soundSensorNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.soundSensorNumUpDown.Name = "soundSensorNumUpDown";
            this.soundSensorNumUpDown.Size = new System.Drawing.Size(59, 20);
            this.soundSensorNumUpDown.TabIndex = 18;
            this.soundSensorNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(179, 95);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(79, 13);
            this.label18.TabIndex = 28;
            this.label18.Text = "Sound Sensors";
            // 
            // motionSensorNumUpDown
            // 
            this.motionSensorNumUpDown.Location = new System.Drawing.Point(264, 68);
            this.motionSensorNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.motionSensorNumUpDown.Name = "motionSensorNumUpDown";
            this.motionSensorNumUpDown.Size = new System.Drawing.Size(59, 20);
            this.motionSensorNumUpDown.TabIndex = 17;
            this.motionSensorNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(178, 70);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(80, 13);
            this.label17.TabIndex = 26;
            this.label17.Text = "Motion Sensors";
            // 
            // strongFlashlightNumUpDown
            // 
            this.strongFlashlightNumUpDown.Location = new System.Drawing.Point(264, 44);
            this.strongFlashlightNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.strongFlashlightNumUpDown.Name = "strongFlashlightNumUpDown";
            this.strongFlashlightNumUpDown.Size = new System.Drawing.Size(59, 20);
            this.strongFlashlightNumUpDown.TabIndex = 16;
            this.strongFlashlightNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(168, 46);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(90, 13);
            this.label16.TabIndex = 24;
            this.label16.Text = "Strong Flashlights";
            // 
            // tripodNumUpDown
            // 
            this.tripodNumUpDown.Location = new System.Drawing.Point(264, 19);
            this.tripodNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.tripodNumUpDown.Name = "tripodNumUpDown";
            this.tripodNumUpDown.Size = new System.Drawing.Size(59, 20);
            this.tripodNumUpDown.TabIndex = 15;
            this.tripodNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(216, 21);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(42, 13);
            this.label15.TabIndex = 22;
            this.label15.Text = "Tripods";
            // 
            // sageNumUpDown
            // 
            this.sageNumUpDown.Location = new System.Drawing.Point(103, 267);
            this.sageNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.sageNumUpDown.Name = "sageNumUpDown";
            this.sageNumUpDown.Size = new System.Drawing.Size(59, 20);
            this.sageNumUpDown.TabIndex = 14;
            this.sageNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(65, 269);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(32, 13);
            this.label14.TabIndex = 20;
            this.label14.Text = "Sage";
            // 
            // saltNumUpDown
            // 
            this.saltNumUpDown.Location = new System.Drawing.Point(103, 242);
            this.saltNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.saltNumUpDown.Name = "saltNumUpDown";
            this.saltNumUpDown.Size = new System.Drawing.Size(59, 20);
            this.saltNumUpDown.TabIndex = 13;
            this.saltNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(72, 244);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(25, 13);
            this.label13.TabIndex = 18;
            this.label13.Text = "Salt";
            // 
            // evpRecorderNumUpDown
            // 
            this.evpRecorderNumUpDown.Location = new System.Drawing.Point(103, 218);
            this.evpRecorderNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.evpRecorderNumUpDown.Name = "evpRecorderNumUpDown";
            this.evpRecorderNumUpDown.Size = new System.Drawing.Size(59, 20);
            this.evpRecorderNumUpDown.TabIndex = 12;
            this.evpRecorderNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(17, 220);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "EVP Recorders";
            // 
            // dslrCameraNumUpDown
            // 
            this.dslrCameraNumUpDown.Location = new System.Drawing.Point(103, 194);
            this.dslrCameraNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.dslrCameraNumUpDown.Name = "dslrCameraNumUpDown";
            this.dslrCameraNumUpDown.Size = new System.Drawing.Size(59, 20);
            this.dslrCameraNumUpDown.TabIndex = 11;
            this.dslrCameraNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 196);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "DSLR Cameras";
            // 
            // crucifixNumUpDown
            // 
            this.crucifixNumUpDown.Location = new System.Drawing.Point(103, 169);
            this.crucifixNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.crucifixNumUpDown.Name = "crucifixNumUpDown";
            this.crucifixNumUpDown.Size = new System.Drawing.Size(59, 20);
            this.crucifixNumUpDown.TabIndex = 10;
            this.crucifixNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(45, 171);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Crucifixes";
            // 
            // uvFlashlightNumUpDown
            // 
            this.uvFlashlightNumUpDown.Location = new System.Drawing.Point(103, 144);
            this.uvFlashlightNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.uvFlashlightNumUpDown.Name = "uvFlashlightNumUpDown";
            this.uvFlashlightNumUpDown.Size = new System.Drawing.Size(59, 20);
            this.uvFlashlightNumUpDown.TabIndex = 9;
            this.uvFlashlightNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 146);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "UV Flashlights";
            // 
            // candleNumUpDown
            // 
            this.candleNumUpDown.Location = new System.Drawing.Point(103, 119);
            this.candleNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.candleNumUpDown.Name = "candleNumUpDown";
            this.candleNumUpDown.Size = new System.Drawing.Size(59, 20);
            this.candleNumUpDown.TabIndex = 8;
            this.candleNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(52, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Candles";
            // 
            // lighterNumUpDown
            // 
            this.lighterNumUpDown.Location = new System.Drawing.Point(103, 94);
            this.lighterNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.lighterNumUpDown.Name = "lighterNumUpDown";
            this.lighterNumUpDown.Size = new System.Drawing.Size(59, 20);
            this.lighterNumUpDown.TabIndex = 7;
            this.lighterNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(53, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Lighters";
            // 
            // cameraNumUpDown
            // 
            this.cameraNumUpDown.Location = new System.Drawing.Point(103, 69);
            this.cameraNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.cameraNumUpDown.Name = "cameraNumUpDown";
            this.cameraNumUpDown.Size = new System.Drawing.Size(59, 20);
            this.cameraNumUpDown.TabIndex = 6;
            this.cameraNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(49, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Cameras";
            // 
            // flashlightNumUpDown
            // 
            this.flashlightNumUpDown.Location = new System.Drawing.Point(103, 44);
            this.flashlightNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.flashlightNumUpDown.Name = "flashlightNumUpDown";
            this.flashlightNumUpDown.Size = new System.Drawing.Size(59, 20);
            this.flashlightNumUpDown.TabIndex = 5;
            this.flashlightNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Flashlights";
            // 
            // emfNumUpDown
            // 
            this.emfNumUpDown.Location = new System.Drawing.Point(103, 19);
            this.emfNumUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.emfNumUpDown.Name = "emfNumUpDown";
            this.emfNumUpDown.Size = new System.Drawing.Size(59, 20);
            this.emfNumUpDown.TabIndex = 4;
            this.emfNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "EMF Readers";
            // 
            // batchBtn
            // 
            this.batchBtn.Location = new System.Drawing.Point(17, 403);
            this.batchBtn.Name = "batchBtn";
            this.batchBtn.Size = new System.Drawing.Size(75, 23);
            this.batchBtn.TabIndex = 26;
            this.batchBtn.Text = "Batch";
            this.batchBtn.UseVisualStyleBackColor = true;
            this.batchBtn.Visible = false;
            this.batchBtn.Click += new System.EventHandler(this.maxBtn_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(226, 71);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(36, 13);
            this.label26.TabIndex = 27;
            this.label26.Text = "Level:";
            this.label26.Visible = false;
            // 
            // levelNumUpDown
            // 
            this.levelNumUpDown.Location = new System.Drawing.Point(268, 69);
            this.levelNumUpDown.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.levelNumUpDown.Name = "levelNumUpDown";
            this.levelNumUpDown.Size = new System.Drawing.Size(88, 20);
            this.levelNumUpDown.TabIndex = 3;
            this.levelNumUpDown.Visible = false;
            this.levelNumUpDown.ValueChanged += new System.EventHandler(this.NumUpDown_ValueChanged);
            // 
            // batchNumUpDown
            // 
            this.batchNumUpDown.Location = new System.Drawing.Point(98, 405);
            this.batchNumUpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.batchNumUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.batchNumUpDown.Name = "batchNumUpDown";
            this.batchNumUpDown.Size = new System.Drawing.Size(54, 20);
            this.batchNumUpDown.TabIndex = 27;
            this.batchNumUpDown.Value = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.batchNumUpDown.Visible = false;
            // 
            // evidenceLabel1
            // 
            this.evidenceLabel1.AutoSize = true;
            this.evidenceLabel1.Location = new System.Drawing.Point(226, 19);
            this.evidenceLabel1.Name = "evidenceLabel1";
            this.evidenceLabel1.Size = new System.Drawing.Size(41, 13);
            this.evidenceLabel1.TabIndex = 29;
            this.evidenceLabel1.Text = "label27";
            this.evidenceLabel1.Visible = false;
            // 
            // evidenceLabel2
            // 
            this.evidenceLabel2.AutoSize = true;
            this.evidenceLabel2.Location = new System.Drawing.Point(226, 32);
            this.evidenceLabel2.Name = "evidenceLabel2";
            this.evidenceLabel2.Size = new System.Drawing.Size(41, 13);
            this.evidenceLabel2.TabIndex = 30;
            this.evidenceLabel2.Text = "label28";
            this.evidenceLabel2.Visible = false;
            // 
            // evidenceLabel3
            // 
            this.evidenceLabel3.AutoSize = true;
            this.evidenceLabel3.Location = new System.Drawing.Point(226, 45);
            this.evidenceLabel3.Name = "evidenceLabel3";
            this.evidenceLabel3.Size = new System.Drawing.Size(41, 13);
            this.evidenceLabel3.TabIndex = 31;
            this.evidenceLabel3.Text = "label29";
            this.evidenceLabel3.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 103);
            this.Controls.Add(this.evidenceLabel3);
            this.Controls.Add(this.batchNumUpDown);
            this.Controls.Add(this.levelNumUpDown);
            this.Controls.Add(this.evidenceLabel2);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.batchBtn);
            this.Controls.Add(this.evidenceLabel1);
            this.Controls.Add(this.invGroupBox);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ghostTypeDropDown);
            this.Controls.Add(this.moneyNumUpDown);
            this.Controls.Add(this.expNumUpDown);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Revenant Phasmo Tool @Reigniteh";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.expNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moneyNumUpDown)).EndInit();
            this.invGroupBox.ResumeLayout(false);
            this.invGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.goProNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glowstickNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.parabolicMicNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dotsProjectorNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ghostWritingBookNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thermometerNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sanityPillsNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.soundSensorNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.motionSensorNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.strongFlashlightNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tripodNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sageNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saltNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.evpRecorderNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dslrCameraNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.crucifixNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uvFlashlightNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.candleNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lighterNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cameraNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flashlightNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emfNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.levelNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.batchNumUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }
  }
}
