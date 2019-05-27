namespace Callibot
{
    partial class MainForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.CurrentPosition = new System.Windows.Forms.Button();
            this.OpenCOM = new System.Windows.Forms.Button();
            this.SetID = new System.Windows.Forms.Button();
            this.MotorID = new System.Windows.Forms.TextBox();
            this.ServoOn = new System.Windows.Forms.Button();
            this.Move = new System.Windows.Forms.Button();
            this.Joint1Target = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Joint2Target = new System.Windows.Forms.TextBox();
            this.Joint3Target = new System.Windows.Forms.TextBox();
            this.Joint5Target = new System.Windows.Forms.TextBox();
            this.Joint6Target = new System.Windows.Forms.TextBox();
            this.Joint1Speed = new System.Windows.Forms.TextBox();
            this.Joint2Speed = new System.Windows.Forms.TextBox();
            this.Joint3Speed = new System.Windows.Forms.TextBox();
            this.Joint4Speed = new System.Windows.Forms.TextBox();
            this.Joint5Speed = new System.Windows.Forms.TextBox();
            this.Joint2 = new System.Windows.Forms.CheckBox();
            this.Joint3 = new System.Windows.Forms.CheckBox();
            this.Joint4 = new System.Windows.Forms.CheckBox();
            this.Joint5 = new System.Windows.Forms.CheckBox();
            this.Joint6 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Joint1 = new System.Windows.Forms.CheckBox();
            this.Joint6Speed = new System.Windows.Forms.TextBox();
            this.Joint4Target = new System.Windows.Forms.TextBox();
            this.SyncMove = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Teaching = new System.Windows.Forms.Button();
            this.Initial = new System.Windows.Forms.Button();
            this.Forward_Kinematics = new System.Windows.Forms.Button();
            this.TrailMove = new System.Windows.Forms.Button();
            this.RollingPaper = new System.Windows.Forms.Button();
            this.PlayExisting = new System.Windows.Forms.Button();
            this.PlayAK = new System.Windows.Forms.Button();
            this.PlaySA = new System.Windows.Forms.Button();
            this.Inverse_Kinematics = new System.Windows.Forms.Button();
            this.PlayJYLQ = new System.Windows.Forms.Button();
            this.PlayCWZL = new System.Windows.Forms.Button();
            this.Shi = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Play_DZYN = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.Liuchenghuajiaoxue = new System.Windows.Forms.Button();
            this.Sanlianxie = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(216, 623);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(59, 25);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(211, 728);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(152, 25);
            this.textBox2.TabIndex = 2;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // CurrentPosition
            // 
            this.CurrentPosition.Location = new System.Drawing.Point(876, 329);
            this.CurrentPosition.Margin = new System.Windows.Forms.Padding(4);
            this.CurrentPosition.Name = "CurrentPosition";
            this.CurrentPosition.Size = new System.Drawing.Size(100, 41);
            this.CurrentPosition.TabIndex = 3;
            this.CurrentPosition.Text = "Current Position";
            this.CurrentPosition.UseVisualStyleBackColor = true;
            this.CurrentPosition.Visible = false;
            this.CurrentPosition.Click += new System.EventHandler(this.CurrentPosition_Click);
            // 
            // OpenCOM
            // 
            this.OpenCOM.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OpenCOM.Location = new System.Drawing.Point(35, 191);
            this.OpenCOM.Margin = new System.Windows.Forms.Padding(4);
            this.OpenCOM.Name = "OpenCOM";
            this.OpenCOM.Size = new System.Drawing.Size(145, 76);
            this.OpenCOM.TabIndex = 4;
            this.OpenCOM.Text = "COM OFF";
            this.OpenCOM.UseVisualStyleBackColor = true;
            this.OpenCOM.Click += new System.EventHandler(this.OpenCOM_Click);
            // 
            // SetID
            // 
            this.SetID.Enabled = false;
            this.SetID.Location = new System.Drawing.Point(879, 275);
            this.SetID.Margin = new System.Windows.Forms.Padding(4);
            this.SetID.Name = "SetID";
            this.SetID.Size = new System.Drawing.Size(100, 29);
            this.SetID.TabIndex = 5;
            this.SetID.Text = "Set ID";
            this.SetID.UseVisualStyleBackColor = true;
            this.SetID.Visible = false;
            this.SetID.Click += new System.EventHandler(this.SetID_Click);
            // 
            // MotorID
            // 
            this.MotorID.Location = new System.Drawing.Point(893, 642);
            this.MotorID.Margin = new System.Windows.Forms.Padding(4);
            this.MotorID.Name = "MotorID";
            this.MotorID.Size = new System.Drawing.Size(53, 25);
            this.MotorID.TabIndex = 6;
            this.MotorID.Visible = false;
            // 
            // ServoOn
            // 
            this.ServoOn.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ServoOn.Location = new System.Drawing.Point(211, 191);
            this.ServoOn.Margin = new System.Windows.Forms.Padding(4);
            this.ServoOn.Name = "ServoOn";
            this.ServoOn.Size = new System.Drawing.Size(145, 76);
            this.ServoOn.TabIndex = 7;
            this.ServoOn.Text = "Servo OFF";
            this.ServoOn.UseVisualStyleBackColor = true;
            this.ServoOn.Click += new System.EventHandler(this.ServoOn_Click);
            // 
            // Move
            // 
            this.Move.Location = new System.Drawing.Point(1284, 567);
            this.Move.Margin = new System.Windows.Forms.Padding(4);
            this.Move.Name = "Move";
            this.Move.Size = new System.Drawing.Size(100, 31);
            this.Move.TabIndex = 9;
            this.Move.Text = "Move";
            this.Move.UseVisualStyleBackColor = true;
            this.Move.Click += new System.EventHandler(this.Move_Click);
            // 
            // Joint1Target
            // 
            this.Joint1Target.Location = new System.Drawing.Point(133, 28);
            this.Joint1Target.Margin = new System.Windows.Forms.Padding(4);
            this.Joint1Target.Name = "Joint1Target";
            this.Joint1Target.Size = new System.Drawing.Size(109, 25);
            this.Joint1Target.TabIndex = 10;
            this.Joint1Target.Text = "180";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.78723F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 119F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tableLayoutPanel1.Controls.Add(this.Joint1Target, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.Joint2Target, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.Joint3Target, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.Joint5Target, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.Joint6Target, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.Joint1Speed, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.Joint2Speed, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.Joint3Speed, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.Joint4Speed, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.Joint5Speed, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.Joint2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.Joint3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.Joint4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.Joint5, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.Joint6, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.Joint1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Joint6Speed, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.Joint4Target, 1, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1071, 329);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.90244F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56.09756F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(360, 222);
            this.tableLayoutPanel1.TabIndex = 11;
            this.tableLayoutPanel1.Visible = false;
            // 
            // Joint2Target
            // 
            this.Joint2Target.Location = new System.Drawing.Point(133, 59);
            this.Joint2Target.Margin = new System.Windows.Forms.Padding(4);
            this.Joint2Target.Name = "Joint2Target";
            this.Joint2Target.Size = new System.Drawing.Size(109, 25);
            this.Joint2Target.TabIndex = 11;
            this.Joint2Target.Text = "180";
            // 
            // Joint3Target
            // 
            this.Joint3Target.Location = new System.Drawing.Point(133, 92);
            this.Joint3Target.Margin = new System.Windows.Forms.Padding(4);
            this.Joint3Target.Name = "Joint3Target";
            this.Joint3Target.Size = new System.Drawing.Size(109, 25);
            this.Joint3Target.TabIndex = 12;
            this.Joint3Target.Text = "180";
            // 
            // Joint5Target
            // 
            this.Joint5Target.Location = new System.Drawing.Point(133, 158);
            this.Joint5Target.Margin = new System.Windows.Forms.Padding(4);
            this.Joint5Target.Name = "Joint5Target";
            this.Joint5Target.Size = new System.Drawing.Size(109, 25);
            this.Joint5Target.TabIndex = 14;
            this.Joint5Target.Text = "180";
            // 
            // Joint6Target
            // 
            this.Joint6Target.Location = new System.Drawing.Point(133, 191);
            this.Joint6Target.Margin = new System.Windows.Forms.Padding(4);
            this.Joint6Target.Name = "Joint6Target";
            this.Joint6Target.Size = new System.Drawing.Size(109, 25);
            this.Joint6Target.TabIndex = 15;
            this.Joint6Target.Text = "180";
            // 
            // Joint1Speed
            // 
            this.Joint1Speed.Location = new System.Drawing.Point(252, 28);
            this.Joint1Speed.Margin = new System.Windows.Forms.Padding(4);
            this.Joint1Speed.Name = "Joint1Speed";
            this.Joint1Speed.Size = new System.Drawing.Size(103, 25);
            this.Joint1Speed.TabIndex = 16;
            this.Joint1Speed.Text = "5";
            // 
            // Joint2Speed
            // 
            this.Joint2Speed.Location = new System.Drawing.Point(252, 59);
            this.Joint2Speed.Margin = new System.Windows.Forms.Padding(4);
            this.Joint2Speed.Name = "Joint2Speed";
            this.Joint2Speed.Size = new System.Drawing.Size(103, 25);
            this.Joint2Speed.TabIndex = 17;
            this.Joint2Speed.Text = "5";
            // 
            // Joint3Speed
            // 
            this.Joint3Speed.Location = new System.Drawing.Point(252, 92);
            this.Joint3Speed.Margin = new System.Windows.Forms.Padding(4);
            this.Joint3Speed.Name = "Joint3Speed";
            this.Joint3Speed.Size = new System.Drawing.Size(103, 25);
            this.Joint3Speed.TabIndex = 18;
            this.Joint3Speed.Text = "5";
            // 
            // Joint4Speed
            // 
            this.Joint4Speed.Location = new System.Drawing.Point(252, 125);
            this.Joint4Speed.Margin = new System.Windows.Forms.Padding(4);
            this.Joint4Speed.Name = "Joint4Speed";
            this.Joint4Speed.Size = new System.Drawing.Size(103, 25);
            this.Joint4Speed.TabIndex = 19;
            this.Joint4Speed.Text = "5";
            // 
            // Joint5Speed
            // 
            this.Joint5Speed.Location = new System.Drawing.Point(252, 158);
            this.Joint5Speed.Margin = new System.Windows.Forms.Padding(4);
            this.Joint5Speed.Name = "Joint5Speed";
            this.Joint5Speed.Size = new System.Drawing.Size(103, 25);
            this.Joint5Speed.TabIndex = 20;
            this.Joint5Speed.Text = "5";
            // 
            // Joint2
            // 
            this.Joint2.AutoSize = true;
            this.Joint2.Location = new System.Drawing.Point(4, 59);
            this.Joint2.Margin = new System.Windows.Forms.Padding(4);
            this.Joint2.Name = "Joint2";
            this.Joint2.Size = new System.Drawing.Size(101, 19);
            this.Joint2.TabIndex = 23;
            this.Joint2.Text = "Joint Two";
            this.Joint2.UseVisualStyleBackColor = true;
            // 
            // Joint3
            // 
            this.Joint3.AutoSize = true;
            this.Joint3.Location = new System.Drawing.Point(4, 92);
            this.Joint3.Margin = new System.Windows.Forms.Padding(4);
            this.Joint3.Name = "Joint3";
            this.Joint3.Size = new System.Drawing.Size(117, 19);
            this.Joint3.TabIndex = 24;
            this.Joint3.Text = "Joint Three";
            this.Joint3.UseVisualStyleBackColor = true;
            // 
            // Joint4
            // 
            this.Joint4.AutoSize = true;
            this.Joint4.Location = new System.Drawing.Point(4, 125);
            this.Joint4.Margin = new System.Windows.Forms.Padding(4);
            this.Joint4.Name = "Joint4";
            this.Joint4.Size = new System.Drawing.Size(109, 19);
            this.Joint4.TabIndex = 25;
            this.Joint4.Text = "Joint Four";
            this.Joint4.UseVisualStyleBackColor = true;
            // 
            // Joint5
            // 
            this.Joint5.AutoSize = true;
            this.Joint5.Location = new System.Drawing.Point(4, 158);
            this.Joint5.Margin = new System.Windows.Forms.Padding(4);
            this.Joint5.Name = "Joint5";
            this.Joint5.Size = new System.Drawing.Size(109, 19);
            this.Joint5.TabIndex = 26;
            this.Joint5.Text = "Joint Five";
            this.Joint5.UseVisualStyleBackColor = true;
            // 
            // Joint6
            // 
            this.Joint6.AutoSize = true;
            this.Joint6.Location = new System.Drawing.Point(4, 191);
            this.Joint6.Margin = new System.Windows.Forms.Padding(4);
            this.Joint6.Name = "Joint6";
            this.Joint6.Size = new System.Drawing.Size(101, 19);
            this.Joint6.TabIndex = 27;
            this.Joint6.Text = "Joint Six";
            this.Joint6.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(133, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 15);
            this.label1.TabIndex = 28;
            this.label1.Text = "Target Degree";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(252, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 15);
            this.label2.TabIndex = 29;
            this.label2.Text = "Moving Speed";
            // 
            // Joint1
            // 
            this.Joint1.AutoSize = true;
            this.Joint1.Location = new System.Drawing.Point(4, 28);
            this.Joint1.Margin = new System.Windows.Forms.Padding(4);
            this.Joint1.Name = "Joint1";
            this.Joint1.Size = new System.Drawing.Size(101, 19);
            this.Joint1.TabIndex = 22;
            this.Joint1.Text = "Joint One";
            this.Joint1.UseVisualStyleBackColor = true;
            // 
            // Joint6Speed
            // 
            this.Joint6Speed.Location = new System.Drawing.Point(252, 191);
            this.Joint6Speed.Margin = new System.Windows.Forms.Padding(4);
            this.Joint6Speed.Name = "Joint6Speed";
            this.Joint6Speed.Size = new System.Drawing.Size(103, 25);
            this.Joint6Speed.TabIndex = 21;
            this.Joint6Speed.Text = "5";
            // 
            // Joint4Target
            // 
            this.Joint4Target.Location = new System.Drawing.Point(133, 125);
            this.Joint4Target.Margin = new System.Windows.Forms.Padding(4);
            this.Joint4Target.Name = "Joint4Target";
            this.Joint4Target.Size = new System.Drawing.Size(109, 25);
            this.Joint4Target.TabIndex = 13;
            this.Joint4Target.Text = "180";
            // 
            // SyncMove
            // 
            this.SyncMove.Location = new System.Drawing.Point(1124, 567);
            this.SyncMove.Margin = new System.Windows.Forms.Padding(4);
            this.SyncMove.Name = "SyncMove";
            this.SyncMove.Size = new System.Drawing.Size(100, 29);
            this.SyncMove.TabIndex = 12;
            this.SyncMove.Text = "Sync Move";
            this.SyncMove.UseVisualStyleBackColor = true;
            this.SyncMove.Click += new System.EventHandler(this.SyncMove_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(307, 623);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(56, 25);
            this.textBox3.TabIndex = 14;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(893, 679);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(53, 25);
            this.textBox4.TabIndex = 15;
            this.textBox4.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 642);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "Motor ID";
            // 
            // Teaching
            // 
            this.Teaching.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Teaching.Location = new System.Drawing.Point(36, 471);
            this.Teaching.Margin = new System.Windows.Forms.Padding(4);
            this.Teaching.Name = "Teaching";
            this.Teaching.Size = new System.Drawing.Size(145, 82);
            this.Teaching.TabIndex = 18;
            this.Teaching.Text = "TEACH";
            this.Teaching.UseVisualStyleBackColor = true;
            this.Teaching.Click += new System.EventHandler(this.Teaching_Click);
            // 
            // Initial
            // 
            this.Initial.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Initial.Location = new System.Drawing.Point(36, 275);
            this.Initial.Margin = new System.Windows.Forms.Padding(4);
            this.Initial.Name = "Initial";
            this.Initial.Size = new System.Drawing.Size(145, 70);
            this.Initial.TabIndex = 21;
            this.Initial.Text = "Initial";
            this.Initial.UseVisualStyleBackColor = true;
            this.Initial.Click += new System.EventHandler(this.Initial_Click);
            // 
            // Forward_Kinematics
            // 
            this.Forward_Kinematics.Location = new System.Drawing.Point(876, 382);
            this.Forward_Kinematics.Name = "Forward_Kinematics";
            this.Forward_Kinematics.Size = new System.Drawing.Size(91, 40);
            this.Forward_Kinematics.TabIndex = 22;
            this.Forward_Kinematics.Text = "Forward Kinematics";
            this.Forward_Kinematics.UseVisualStyleBackColor = true;
            this.Forward_Kinematics.Visible = false;
            this.Forward_Kinematics.Click += new System.EventHandler(this.ForwardKinematics_Click);
            // 
            // TrailMove
            // 
            this.TrailMove.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TrailMove.Location = new System.Drawing.Point(36, 680);
            this.TrailMove.Name = "TrailMove";
            this.TrailMove.Size = new System.Drawing.Size(145, 68);
            this.TrailMove.TabIndex = 30;
            this.TrailMove.Text = "Trail Move";
            this.TrailMove.UseVisualStyleBackColor = true;
            this.TrailMove.Click += new System.EventHandler(this.TrailMove_Click);
            // 
            // RollingPaper
            // 
            this.RollingPaper.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RollingPaper.Location = new System.Drawing.Point(727, 671);
            this.RollingPaper.Name = "RollingPaper";
            this.RollingPaper.Size = new System.Drawing.Size(145, 37);
            this.RollingPaper.TabIndex = 31;
            this.RollingPaper.Text = "Rolling Paper";
            this.RollingPaper.UseVisualStyleBackColor = true;
            this.RollingPaper.Visible = false;
            this.RollingPaper.Click += new System.EventHandler(this.RollingPaper_Click);
            // 
            // PlayExisting
            // 
            this.PlayExisting.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PlayExisting.Location = new System.Drawing.Point(36, 586);
            this.PlayExisting.Margin = new System.Windows.Forms.Padding(4);
            this.PlayExisting.Name = "PlayExisting";
            this.PlayExisting.Size = new System.Drawing.Size(145, 72);
            this.PlayExisting.TabIndex = 40;
            this.PlayExisting.Text = "PlayExisting";
            this.PlayExisting.UseVisualStyleBackColor = true;
            this.PlayExisting.Click += new System.EventHandler(this.PlayExisting_Click);
            // 
            // PlayAK
            // 
            this.PlayAK.Location = new System.Drawing.Point(876, 541);
            this.PlayAK.Margin = new System.Windows.Forms.Padding(4);
            this.PlayAK.Name = "PlayAK";
            this.PlayAK.Size = new System.Drawing.Size(91, 29);
            this.PlayAK.TabIndex = 41;
            this.PlayAK.Text = "PlayAK";
            this.PlayAK.UseVisualStyleBackColor = true;
            this.PlayAK.Visible = false;
            this.PlayAK.Click += new System.EventHandler(this.PlayAK_Click);
            // 
            // PlaySA
            // 
            this.PlaySA.Font = new System.Drawing.Font("宋体", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PlaySA.Location = new System.Drawing.Point(36, 352);
            this.PlaySA.Margin = new System.Windows.Forms.Padding(4);
            this.PlaySA.Name = "PlaySA";
            this.PlaySA.Size = new System.Drawing.Size(144, 79);
            this.PlaySA.TabIndex = 42;
            this.PlaySA.Text = "科学与艺术";
            this.PlaySA.UseVisualStyleBackColor = true;
            this.PlaySA.Click += new System.EventHandler(this.PlaySA_Click);
            // 
            // Inverse_Kinematics
            // 
            this.Inverse_Kinematics.Location = new System.Drawing.Point(879, 434);
            this.Inverse_Kinematics.Name = "Inverse_Kinematics";
            this.Inverse_Kinematics.Size = new System.Drawing.Size(89, 40);
            this.Inverse_Kinematics.TabIndex = 43;
            this.Inverse_Kinematics.Text = "Inverse Kinematics";
            this.Inverse_Kinematics.UseVisualStyleBackColor = true;
            this.Inverse_Kinematics.Visible = false;
            this.Inverse_Kinematics.Click += new System.EventHandler(this.InverseKinematics_Click);
            // 
            // PlayJYLQ
            // 
            this.PlayJYLQ.Location = new System.Drawing.Point(245, 586);
            this.PlayJYLQ.Margin = new System.Windows.Forms.Padding(4);
            this.PlayJYLQ.Name = "PlayJYLQ";
            this.PlayJYLQ.Size = new System.Drawing.Size(91, 30);
            this.PlayJYLQ.TabIndex = 44;
            this.PlayJYLQ.Text = "敬业乐群";
            this.PlayJYLQ.UseVisualStyleBackColor = true;
            this.PlayJYLQ.Click += new System.EventHandler(this.PlayJYLQ_Click);
            // 
            // PlayCWZL
            // 
            this.PlayCWZL.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PlayCWZL.Location = new System.Drawing.Point(211, 352);
            this.PlayCWZL.Margin = new System.Windows.Forms.Padding(4);
            this.PlayCWZL.Name = "PlayCWZL";
            this.PlayCWZL.Size = new System.Drawing.Size(145, 79);
            this.PlayCWZL.TabIndex = 45;
            this.PlayCWZL.Text = "藏往知来";
            this.PlayCWZL.UseVisualStyleBackColor = true;
            this.PlayCWZL.Click += new System.EventHandler(this.PlayCWZL_Click);
            // 
            // Shi
            // 
            this.Shi.Location = new System.Drawing.Point(876, 488);
            this.Shi.Margin = new System.Windows.Forms.Padding(4);
            this.Shi.Name = "Shi";
            this.Shi.Size = new System.Drawing.Size(92, 26);
            this.Shi.TabIndex = 46;
            this.Shi.Text = "Shi";
            this.Shi.UseVisualStyleBackColor = true;
            this.Shi.Visible = false;
            this.Shi.Click += new System.EventHandler(this.Shi_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::Callibot.Properties.Resources.P41113_192759_副本;
            this.pictureBox2.Location = new System.Drawing.Point(504, 180);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(976, 568);
            this.pictureBox2.TabIndex = 48;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Callibot.Properties.Resources.书法机器人;
            this.pictureBox1.Location = new System.Drawing.Point(-1, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1571, 120);
            this.pictureBox1.TabIndex = 47;
            this.pictureBox1.TabStop = false;
            // 
            // Play_DZYN
            // 
            this.Play_DZYN.Location = new System.Drawing.Point(245, 679);
            this.Play_DZYN.Margin = new System.Windows.Forms.Padding(4);
            this.Play_DZYN.Name = "Play_DZYN";
            this.Play_DZYN.Size = new System.Drawing.Size(100, 29);
            this.Play_DZYN.TabIndex = 49;
            this.Play_DZYN.Text = "东紫延年";
            this.Play_DZYN.UseVisualStyleBackColor = true;
            this.Play_DZYN.Click += new System.EventHandler(this.Play_DZYN_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(211, 471);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 82);
            this.button1.TabIndex = 50;
            this.button1.Text = "TEST";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(211, 275);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(145, 70);
            this.button2.TabIndex = 51;
            this.button2.Text = "博文约礼";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(73, 783);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(152, 23);
            this.button3.TabIndex = 52;
            this.button3.Text = "Teach Many";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(504, 204);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(117, 63);
            this.button4.TabIndex = 53;
            this.button4.Text = "左移";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(640, 204);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(129, 63);
            this.button5.TabIndex = 54;
            this.button5.Text = "右移";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(504, 323);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(117, 75);
            this.button6.TabIndex = 55;
            this.button6.Text = "卷纸";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Liuchenghuajiaoxue
            // 
            this.Liuchenghuajiaoxue.Location = new System.Drawing.Point(650, 325);
            this.Liuchenghuajiaoxue.Name = "Liuchenghuajiaoxue";
            this.Liuchenghuajiaoxue.Size = new System.Drawing.Size(128, 71);
            this.Liuchenghuajiaoxue.TabIndex = 56;
            this.Liuchenghuajiaoxue.Text = "流程化教学";
            this.Liuchenghuajiaoxue.UseVisualStyleBackColor = true;
            this.Liuchenghuajiaoxue.Click += new System.EventHandler(this.Liuchenghuajiaoxue_Click);
            // 
            // Sanlianxie
            // 
            this.Sanlianxie.Location = new System.Drawing.Point(504, 443);
            this.Sanlianxie.Name = "Sanlianxie";
            this.Sanlianxie.Size = new System.Drawing.Size(117, 71);
            this.Sanlianxie.TabIndex = 57;
            this.Sanlianxie.Text = "三连写";
            this.Sanlianxie.UseVisualStyleBackColor = true;
            this.Sanlianxie.Click += new System.EventHandler(this.Sanlianxie_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(44)))));
            this.ClientSize = new System.Drawing.Size(1799, 835);
            this.Controls.Add(this.Sanlianxie);
            this.Controls.Add(this.Liuchenghuajiaoxue);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Play_DZYN);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.SyncMove);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Move);
            this.Controls.Add(this.Shi);
            this.Controls.Add(this.PlayCWZL);
            this.Controls.Add(this.PlayJYLQ);
            this.Controls.Add(this.Inverse_Kinematics);
            this.Controls.Add(this.PlaySA);
            this.Controls.Add(this.PlayAK);
            this.Controls.Add(this.PlayExisting);
            this.Controls.Add(this.RollingPaper);
            this.Controls.Add(this.TrailMove);
            this.Controls.Add(this.Forward_Kinematics);
            this.Controls.Add(this.Initial);
            this.Controls.Add(this.Teaching);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.ServoOn);
            this.Controls.Add(this.MotorID);
            this.Controls.Add(this.SetID);
            this.Controls.Add(this.OpenCOM);
            this.Controls.Add(this.CurrentPosition);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Callibot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button CurrentPosition;
        private System.Windows.Forms.Button OpenCOM;
        private System.Windows.Forms.Button SetID;
        private System.Windows.Forms.TextBox MotorID;
        private System.Windows.Forms.Button ServoOn;
        private System.Windows.Forms.Button Move;
        private System.Windows.Forms.TextBox Joint1Target;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox Joint2Target;
        private System.Windows.Forms.TextBox Joint3Target;
        private System.Windows.Forms.TextBox Joint4Target;
        private System.Windows.Forms.TextBox Joint5Target;
        private System.Windows.Forms.TextBox Joint6Target;
        private System.Windows.Forms.TextBox Joint1Speed;
        private System.Windows.Forms.TextBox Joint2Speed;
        private System.Windows.Forms.TextBox Joint3Speed;
        private System.Windows.Forms.TextBox Joint4Speed;
        private System.Windows.Forms.TextBox Joint5Speed;
        private System.Windows.Forms.TextBox Joint6Speed;
        private System.Windows.Forms.CheckBox Joint1;
        private System.Windows.Forms.CheckBox Joint2;
        private System.Windows.Forms.CheckBox Joint3;
        private System.Windows.Forms.CheckBox Joint4;
        private System.Windows.Forms.CheckBox Joint5;
        private System.Windows.Forms.CheckBox Joint6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SyncMove;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Teaching;
        private System.Windows.Forms.Button Initial;
        private System.Windows.Forms.Button Forward_Kinematics;
        private System.Windows.Forms.Button TrailMove;
        private System.Windows.Forms.Button RollingPaper;
        private System.Windows.Forms.Button PlayExisting;
        private System.Windows.Forms.Button PlayAK;
        private System.Windows.Forms.Button PlaySA;
        private System.Windows.Forms.Button Inverse_Kinematics;
        private System.Windows.Forms.Button PlayJYLQ;
        private System.Windows.Forms.Button PlayCWZL;
        private System.Windows.Forms.Button Shi;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button Play_DZYN;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button Liuchenghuajiaoxue;
        private System.Windows.Forms.Button Sanlianxie;
    }
}


